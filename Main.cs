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
                MessageBox.Show("The Account Name is missing", "BYOSA Utility");
                txtAccountName.Focus();
                return;
            }

            if (txtAccountKey.Text.Length == 0)
            {
                MessageBox.Show("The Account Key is missing", "BYOSA Utility");
                txtAccountKey.Focus();
                return;
            }

            if (txtContainer.Text.Length == 0)
            {
                MessageBox.Show("The container name is missing", "BYOSA Utility");
                txtContainer.Focus();
                return;
            }

            try
            { 
                StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(txtAccountName.Text, txtAccountKey.Text);
                DataLakeServiceClient client = GetDataLakeServiceClient(txtAccountName.Text, txtAccountKey.Text, sharedKeyCredential);
                DataLakeFileSystemClient fileClient = client.GetFileSystemClient(txtContainer.Text);
                
                if(client.Uri != null)
                {
                    MessageBox.Show("Connection successful: " + client.Uri, "BYOSA Utility");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was an error connecting to the container: " + ex.Message, "BYOSA Utility");
            }
        }
        
        private static DataLakeServiceClient GetDataLakeServiceClient(string accountName, string accountKey, StorageSharedKeyCredential sharedKeyCredential)
        {
           
            string dfsUri = "https://" + accountName + ".dfs.core.windows.net";

            DataLakeServiceClient dataLakeServiceClient = new DataLakeServiceClient(new Uri(dfsUri), sharedKeyCredential);

            return dataLakeServiceClient;
        }

        private async Task ListContainersInDirectoryAsync(DataLakeFileSystemClient client, StorageSharedKeyCredential sharedKeyCredential, string path)
        {
            string directoryName = string.Empty;
            string fileName = string.Empty;
            string[] fileNames = new string[0];
            string[] directories = new string[0];
            try
            { 
                await foreach (PathItem pathItem in client.GetPathsAsync(path, true, false, System.Threading.CancellationToken.None))
                {
                    if ((bool)pathItem.IsDirectory)
                    { 
                        directories = pathItem.Name.Split('/');

                        for (int k = 0; k < directories.Length; k++)
                        {
                     
                            IAsyncEnumerator<PathItem> enumerator = client.GetPathsAsync(directories[k].ToString()).GetAsyncEnumerator();

                            await enumerator.MoveNextAsync();

                            PathItem item = enumerator.Current;

                            while (item != null)
                            {
                                UpdateResults("Directory: " + item.IsDirectory);

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
                        UpdateResults("File found: " + pathItem.Name.Replace(path, ""));
                    }
                }
            }
            catch (RequestFailedException rfex)
            {
                if (rfex.ErrorCode == "PathNotFound")
                {
                    UpdateResults("The path was not found for " + path.ToString());
                }
            }
            catch (Exception ex)
            {
                UpdateResults("An error occurred :" + ex.Message);
            }
        }
       
        private void Main_Load(object sender, EventArgs e)
        {
            txtAccountKey.Text = @"yH7t7HbWx1d2gjCsm8rlTfoGR18uTvJ+D6RDgAxDVGbMIGF83YJcn5ll/jMARm83/EfJpJAge4a1251bYdBIbA==";
            txtAccountName.Text = "clscat";
            txtContainer.Text = "staging";
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
                            MessageBox.Show("The manifest file is not in a proper JSON format: " + jx.Message, "BYOSA Utility");
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("An error has occurred: " + ex.Message, "BYOSA Utility");
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

                UpdateResults("Manifest file test successful: " + manifest.Rows.Count.ToString() + " entities found");
               
                for (int i=0; i< manifest.Rows.Count; i++)
                {
                    UpdateResults(manifest.Rows[i][0].ToString());
                }

                valid = true;

                return valid;
            }
            catch (JsonException jsex)
            {
                MessageBox.Show("The Manifest file does not represent a JSON structure: " + jsex.Message, "BYOSA Utility");
                return valid;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred: " + ex.Message, "BYOSA Utility");
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

            if (ValidateManifest())
            { 
                DataLakeServiceClient client = GetConnection();
                DataLakeFileSystemClient fileClient = client.GetFileSystemClient(txtContainer.Text);
                StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(txtAccountName.Text, txtAccountKey.Text);

                for (int i=0; i< manifest.Rows.Count; i++)
                {
                    if (manifest.Rows[i][2].ToString().Length > 0)
                    { 
                        entity = manifest.Rows[i][0].ToString();
                        entityPath = txtManifestRoot.Text + manifest.Rows[i][2].ToString();

                        UpdateResults("Testing " + entity + " at " + entityPath);
                        await ListContainersInDirectoryAsync(fileClient, sharedKeyCredential, entityPath);
                    }
                }
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
                    UpdateResults("Connection successful: " + client.Uri);
                }
                return client;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error connecting to the container: " + ex.Message, "BYOSA Utility");
                return null;
                
            }
        }

        private void UpdateResults(string message)
        {
            if(txtResults.TextLength > 0)
                txtResults.AppendText(Environment.NewLine);
    
            txtResults.AppendText(message);
        }
    }
}
    