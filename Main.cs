using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure;
using Azure.Storage.Files.DataLake;
using Azure.Storage.Files.DataLake.Models;
using Azure.Storage;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace BYOSA_Utility
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        DataTable manifest = new DataTable();

        private void btnConnect_Click(object sender, EventArgs e)
        {



            if (txtAccountName.Text.Length == 0)
            {
                MessageBox.Show("The Account Name is missing", "CDM Manifest Utility");
                txtAccountName.Focus();
                return;
            }

            if (txtAccountKey.Text.Length == 0)
            {
                MessageBox.Show("The Account Key is missing", "CDM Manifest Utility");
                txtAccountKey.Focus();
                return;
            }

            if (txtContainer.Text.Length == 0)
            {
                MessageBox.Show("The container name is missing", "CDM Manifest Utility");
                txtContainer.Focus();
                return;
            }

            try
            {
                StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(txtAccountName.Text, txtAccountKey.Text);
                DataLakeServiceClient client = GetDataLakeServiceClient(txtAccountName.Text, txtAccountKey.Text, sharedKeyCredential);
                DataLakeFileSystemClient fileClient = client.GetFileSystemClient(txtContainer.Text);

                if (client.Uri != null)
                {
                    MessageBox.Show("Connection successful: " + client.Uri, "CDM Manifest Utility");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error connecting to the container: " + ex.Message, "CDM Manifest Utility");
            }
        }

        private static DataLakeServiceClient GetDataLakeServiceClient(string accountName, string accountKey, StorageSharedKeyCredential sharedKeyCredential)
        {

            string dfsUri = "https://" + accountName + ".dfs.core.windows.net";

            DataLakeServiceClient dataLakeServiceClient = new DataLakeServiceClient(new Uri(dfsUri), sharedKeyCredential);

            return dataLakeServiceClient;
        }

        private async Task ListContainersInDirectoryAsync(DataLakeFileSystemClient client, StorageSharedKeyCredential sharedKeyCredential, string path, string regExPattern, string entityName)
        {
            string directoryName = string.Empty;
            string fileName = string.Empty;
            string[] fileNames = new string[0];
            string[] directories = new string[0];
            int counter = 0;
            Regex regEx;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                await foreach (PathItem pathItem in client.GetPathsAsync(path, true, false, System.Threading.CancellationToken.None))
                {
                    if ((bool)pathItem.IsDirectory)
                    {
                        directoryName = pathItem.Name;

                        directories = pathItem.Name.Split('/');

                        for (int k = 0; k < directories.Length; k++)
                        {

                            IAsyncEnumerator<PathItem> enumerator = client.GetPathsAsync(directories[k].ToString()).GetAsyncEnumerator();

                            await enumerator.MoveNextAsync();

                            PathItem item = enumerator.Current;

                            while (item != null)
                            {
                                if (!await enumerator.MoveNextAsync())
                                {
                                    break;
                                }

                                item = enumerator.Current;
                            }
                        }
                    }
                    else
                    {
                        if (regExPattern.StartsWith("/"))
                            regEx = new Regex(regExPattern.Remove(0, 1));
                        else
                            regEx = new Regex(regExPattern);

                        UpdateResults(directoryName, pathItem.Name.Replace(path, ""), path, regEx.Match(pathItem.Name.Replace(path, "")).Success.ToString(), entityName);
                        counter++;
                    }
                }

                UpdateStatus(entityName + " validation complete - " + counter.ToString() + " file(s) found");
            }
            catch (RequestFailedException rfex)
            {
                if (rfex.ErrorCode == "PathNotFound")
                {
                    UpdateStatus("Error: The path was not found for " + path.ToString());
                }
                else if (rfex.ErrorCode == "FilesystemNotFound")
                {
                    UpdateStatus("Error: The file system was not found for " + path.ToString());
                }
                else
                {
                    UpdateStatus("Error: An exception was generated reading from the data lake:  " + rfex.Message);
                }
            }
            catch (Exception ex)
            {
                UpdateStatus("An error occurred :" + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

            ToolTip tip = new ToolTip();
            tip.SetToolTip(txtManifestRoot, "The root location where the default manifest file is stored in the data lake");

            this.Text = "CDM Manifest Utility (" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";
        }

        private void btnOpenManifest_Click(object sender, EventArgs e)
        {
            LoadManifest();
        }

        private void LoadManifest()
        {

            StringBuilder sb = new StringBuilder();
            string fileName;
            string fullFileName;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = false;
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Text files (*.txt)|*.txt|JSON files (*.json)|*.json";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    Cursor.Current = Cursors.WaitCursor;

                    //get the file selected
                    fileName = openFileDialog.SafeFileName.ToString();
                    fullFileName = openFileDialog.FileName.ToString();
                    txtManifestPath.Text = openFileDialog.FileName.ToString();
                    using (StreamReader r = new StreamReader(fullFileName))
                    {

                        //format the JSON
                        try
                        {
                            var options = new JsonSerializerOptions()
                            {
                                WriteIndented = true,
                                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                            };

                            var jsonElement = JsonSerializer.Deserialize<JsonElement>(r.ReadToEnd().ToString());

                            txtManifestContent.Text = JsonSerializer.Serialize(jsonElement, options);
                        }
                        catch (JsonException jx)
                        {
                            MessageBox.Show("The manifest file is not in a proper JSON format: " + jx.Message, "CDM Manifest Utility");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error has occurred: " + ex.Message, "CDM Manifest Utility");
                        }
                        finally
                        {
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
            }
        }

        private bool ValidateManifest()
        {
            bool valid = false;
            //read in the JSON from the text box
            try
            {
                if (txtManifestContent.TextLength == 0)
                {
                    throw new Exception("The default manifest contents are missing. Load the content and try again");
                }

                //build out the data table to store the values from the manifest
                //clear out the structure
                manifest.Rows.Clear();
                manifest.Columns.Clear();

                manifest.Columns.Add("EntityName");
                manifest.Columns.Add("EntityPath");
                manifest.Columns.Add("RootLocation");
                manifest.Columns.Add("Regex");


                string json = txtManifestContent.Text.ToString();
                Cursor.Current = Cursors.WaitCursor;

                var options = new JsonDocumentOptions()
                {
                };

                JsonElement root;
                JsonElement entityName;
                JsonElement entityPath;
                JsonElement dataPartitions;
                JsonElement rootLocation;
                JsonElement regEx;

                JsonDocument document = JsonDocument.Parse(json, options);

                if (document.RootElement.TryGetProperty("entities", out root) == false)
                {
                    throw new Exception("The entitiies node cannot be found");
                }

                var entities = root.EnumerateArray();

                while (entities.MoveNext())
                {
                    #region manifestvalidation 
                    if (entities.Current.TryGetProperty("entityName", out entityName) == false)
                    {
                        throw new Exception("The entity name cannot be found");
                    }

                    if (entities.Current.TryGetProperty("entityPath", out entityPath) == false)
                    {
                        throw new Exception("The entity path cannot be found");
                    }

                    if (entities.Current.TryGetProperty("dataPartitionPatterns", out dataPartitions) == false)
                    {
                        throw new Exception("The data partitions pattern node cannot be found");
                    }

                    #endregion
                    var dataPartitionPatterns = entities.Current.GetProperty("dataPartitionPatterns");
                    var partitions = dataPartitionPatterns.EnumerateArray();

                    while (partitions.MoveNext())
                    {

                        if (partitions.Current.TryGetProperty("rootLocation", out rootLocation) == false)
                        {
                            throw new Exception("The root location value cannot be found");
                        }

                        if (partitions.Current.TryGetProperty("regularExpression", out regEx) == false)
                        {
                            throw new Exception("The regular expression value cannot be found");
                        }

                        manifest.Rows.Add(entityName.ToString(), entityPath.ToString(), rootLocation.ToString(), regEx.ToString());
                    }

                }

                UpdateStatus("Manifest file valid: " + manifest.Rows.Count.ToString() + " entities found");

                valid = true;

                return valid;
            }
            catch (JsonException jsex)
            {
                MessageBox.Show("The Manifest file does not represent a JSON structure: " + jsex.Message, "CDM Manifest Utility");
                UpdateStatus("Error: Manifest file is not valid");
                return valid;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred: " + ex.Message, "CDM Manifest Utility");
                UpdateStatus("Error: An error has occurred: " + ex.Message);
                return valid;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private async void btnValidate_Click(object sender, EventArgs e)
        {

            string entityPath = string.Empty;
            string entity = string.Empty;
            string regEx = string.Empty;
            string root = string.Empty;

            //remove the path if there is one
            root = txtManifestRoot.Text.Replace("/","");
           
            //clear status and results grid
            txtResults.Text = "";
            gridResults.Rows.Clear();

            if (ValidateManifest())
            {
                DataLakeServiceClient client = GetConnection();
                DataLakeFileSystemClient fileClient = client.GetFileSystemClient(txtContainer.Text);
                StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(txtAccountName.Text, txtAccountKey.Text);
               
                for (int i = 0; i < manifest.Rows.Count; i++)
                {
                    if (manifest.Rows[i][2].ToString().Length > 0)
                    {
                        entity = manifest.Rows[i][0].ToString();
                        entityPath = "/" + root + "/" + manifest.Rows[i][2].ToString();
                        regEx = manifest.Rows[i][3].ToString();

                        UpdateStatus("Validating " + entity);
                        await ListContainersInDirectoryAsync(fileClient, sharedKeyCredential, entityPath, regEx, entity);
                    }
                }
                UpdateStatus("Validation complete");
            }
        }

        private DataLakeServiceClient GetConnection()
        {

            if (txtAccountName.Text.Length == 0)
            {
                throw new Exception("The Account Name is missing");
            }

            if (txtAccountKey.Text.Length == 0)
            {

                throw new Exception("The Account Key is missing");
            }

            if (txtContainer.Text.Length == 0)
            {
                throw new Exception("The container name is missing");
            }

            try
            {
                StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(txtAccountName.Text, txtAccountKey.Text);
                DataLakeServiceClient client = GetDataLakeServiceClient(txtAccountName.Text, txtAccountKey.Text, sharedKeyCredential);

                if (client.Uri != null)
                {
                    UpdateStatus("Connection successful: " + client.Uri);
                }
                return client;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error connecting to the container: " + ex.Message, "CDM Manifest Utility");
                return null;

            }
        }

        private void UpdateStatus(string message)
        {
            if (txtResults.TextLength > 0)
                txtResults.AppendText(Environment.NewLine);

            txtResults.AppendText(message);
        }

        private void UpdateResults(string directory, string fileName, string path, string passedRegex, string entity)
        {
            int rowId = gridResults.Rows.Add();

            DataGridViewRow row = gridResults.Rows[rowId];
            row.Cells["FileName"].Value = fileName;
            row.Cells["Regex"].Value = passedRegex;
            row.Cells["Entity"].Value = entity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fileName = "CDMManifestResults.csv";
            
            //get the file path
            DialogResult result = this.folderBrowserDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                string filePath = folderBrowserDialog.SelectedPath + @"\" + fileName;
                //put the results in a data table

                // Creating DataTable.
                DataTable dt = new DataTable();

                //Adding the Columns.
                foreach (DataGridViewColumn column in gridResults.Columns)
                {
                    dt.Columns.Add(column.HeaderText);
                }

                //Adding the Rows.
                foreach (DataGridViewRow row in gridResults.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                }

                saveFile(dt, filePath);
            }
        }

        private void saveFile(DataTable dtDataTable, string strFilePath)
        {
            try
            { 
                StreamWriter sw = new StreamWriter(strFilePath, false);
                //headers    
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    sw.Write(dtDataTable.Columns[i]);
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                foreach (DataRow dr in dtDataTable.Rows)
                {
                    for (int i = 0; i < dtDataTable.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString();
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString());
                            }
                        }
                        if (i < dtDataTable.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
                MessageBox.Show("The file was successfully saved");
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was an exception saving the file: " + ex.Message);
            }
        }
    }
}
    