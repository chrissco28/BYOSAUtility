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
using Microsoft.VisualBasic.FileIO;
using Parquet.Data;
using Parquet;

namespace BYOSA_Utility
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        DataTable manifest = new DataTable();
        StringBuilder log = new StringBuilder();
        DataTable resultsDetails = new DataTable();
        string messageHeader = "CDM Manifest Utility";
        DataTable entityStructure = new DataTable();

        private void btnTest_Click(object sender, EventArgs e)
        {   
            DataLakeServiceClient client = GetConnection();
        }

        private static DataLakeServiceClient GetDataLakeServiceClient(string accountName, string accountKey, StorageSharedKeyCredential sharedKeyCredential)
        {

            string dfsUri = "https://" + accountName + ".dfs.core.windows.net";

            DataLakeServiceClient dataLakeServiceClient = new DataLakeServiceClient(new Uri(dfsUri), sharedKeyCredential);

            return dataLakeServiceClient;
        }

        /// <summary>
        /// Get all the items within the specified container
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sharedKeyCredential"></param>
        /// <param name="path"></param>
        /// <param name="regExPattern"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        private async Task ListContainersInDirectoryAsync(DataLakeFileSystemClient client, StorageSharedKeyCredential sharedKeyCredential, string path, string regExPattern, string entityName)
        {
            string directoryName = string.Empty;
            string fileName = string.Empty;
            string[] fileNames = new string[0];
            string[] directories = new string[0];
           
            int counter = 1;
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

                        int rowId = gridResults.Rows.Add();

                        //add the results to a results grid shown to the user
                        DataGridViewRow row = gridResults.Rows[rowId];
                        row.Cells["FileName"].Value = pathItem.Name.Replace(path, "");
                        row.Cells["Regex"].Value = regEx.Match(pathItem.Name.Replace(path, "")).Success.ToString();
                        row.Cells["Entity"].Value = entityName;
                        
                        //column is not visible to the user
                        //use this column for file/manifest validation and only select one file for performance
                        row.Cells["FileCount"].Value = counter.ToString();

                    }
                    counter++;
                }
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

            resultsDetails.Columns.Add("EntityName");
            resultsDetails.Columns.Add("AttributeOrder");
            resultsDetails.Columns.Add("ManifestAttributeName");
            resultsDetails.Columns.Add("ManifestDataType");
            resultsDetails.Columns.Add("FileAttributeName");
            resultsDetails.Columns.Add("FileDataType");
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
                            MessageBox.Show("The manifest file is not in a proper JSON format: " + jx.Message, messageHeader);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error has occurred: " + ex.Message, messageHeader);
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
            string[] entityManifestPaths;
            string entityManifestPath = string.Empty;
            

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
                manifest.Columns.Add("EntityManifestPath");

                //clear the results table if there are records; this handles a user clicking on validate multiple times
                resultsDetails.Rows.Clear();

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

                        //parse the entity path to get the entity manifest location. the last path is the entity name so get everything before that
                        entityManifestPaths = entityPath.ToString().Split("/");

                        for (int i = 0; i < entityManifestPaths.Length; i++)
                        {
                            if (i == 0)
                                if (!chkRoot.Checked)
                                {
                                    entityManifestPath = txtManifestRoot.Text.ToString();
                                }
                                else
                                {
                                    entityManifestPath = "/";
                                }
                                
                            if (i + 1 != entityManifestPaths.Length)
                                entityManifestPath = entityManifestPath + "/" + entityManifestPaths[i].ToString();
                        }

                        manifest.Rows.Add(entityName.ToString(), entityPath.ToString(), rootLocation.ToString(), regEx.ToString(), entityManifestPath.ToString());
                    }

                }

                UpdateStatus("Manifest file valid: " + manifest.Rows.Count.ToString() + " entities found");

                valid = true;

                return valid;
            }
            catch (JsonException jsex)
            {
                MessageBox.Show("The Manifest file does not represent a JSON structure: " + jsex.Message, messageHeader);
                UpdateStatus("Error: Manifest file is not valid");
                return valid;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred: " + ex.Message, messageHeader);
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
            bool entityChecked = false;
            string entityManifestPath = string.Empty;

            if (!chkRoot.Checked)
            { 
                //remove the path if there is one
                if (txtManifestRoot.Text.StartsWith("/"))
                { 
                    root = txtManifestRoot.Text.Replace("/","");
                }
                else
                {
                    root = txtManifestRoot.Text;
                }
            }
            else
            {
                root = string.Empty;
            }

            //clear status and results grid
            txtResults.Text = "";
            gridResults.Rows.Clear();
            
            //clear the log 
            log.Clear();

            UpdateStatus("Starting Regex Validation");
            UpdateStatus("--------------------");

            if (ValidateManifest())
            {
                try
                {
                    DataLakeServiceClient client = GetConnection();

                    if (client != null)
                    { 
                        DataLakeFileSystemClient fileClient = client.GetFileSystemClient(txtContainer.Text);
                        StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(txtAccountName.Text, txtAccountKey.Text);

                        if (!fileClient.Exists())
                        {
                            throw new Exception("There was a problem connecting to the container");
                        }

                        for (int i = 0; i < manifest.Rows.Count; i++)
                        {
                            if (manifest.Rows[i][2].ToString().Length > 0)
                            {
                                entity = manifest.Rows[i][0].ToString();
                                entityPath = "/" + root + "/" + manifest.Rows[i][2].ToString();
                                regEx = manifest.Rows[i][3].ToString();

                                UpdateStatus("Validating Regex Patterns For " + entity);

                                //get all the items within the container; results are stored in data table
                                await ListContainersInDirectoryAsync(fileClient, sharedKeyCredential, entityPath, regEx, entity);
                            }
                        }
                        UpdateStatus("Completed Regex Validation");
                        UpdateStatus("--------------------");
                        UpdateStatus("Starting Entity Manifest Validation");
                        //go through each entity in the manifest table
                        foreach (DataRow entityRow in manifest.Rows)
                        {
                            entity = entityRow["EntityName"].ToString();
                            entityManifestPath = entityRow["EntityManifestPath"].ToString();
                            entityChecked = false;
                            //validate the entity and manifest files
                            foreach (DataGridViewRow row in gridResults.Rows)
                            {
                                if ((row.Cells["Regex"].Value.ToString() == "True") && (row.Cells["Entity"].Value.ToString() == entity) && (entityChecked == false))
                                {
                                    UpdateStatus("--------------------");
                                    UpdateStatus("Starting " + entity + " Manifest Validation");
                                    bool hasErrors = ValidateEntityManifest(row.Cells["FileName"].Value.ToString(), entity, fileClient, entityManifestPath);

                                    if (hasErrors)
                                        UpdateStatus("Completed " + entity + " Manifest Validation With Warnings");
                                    else
                                        UpdateStatus("Completed " + entity + " Manifest Validation Without Warnings");

                                    entityChecked = true;
                                }
                            }
                        }
                        UpdateStatus("--------------------");
                        UpdateStatus("Completed Entity Manifest Validation");
                        UpdateStatus("--------------------");
                        SaveLogFiles();
                        UpdateStatus("Validation Complete. Log Files Generated");
                    }
                }
                catch (Exception ex)
                {
                    UpdateStatus("There was an exception generated:" + ex.Message);
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
                throw new Exception("The Container Name is missing");
            }

            try
            {
                StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(txtAccountName.Text, txtAccountKey.Text);
                DataLakeServiceClient client = GetDataLakeServiceClient(txtAccountName.Text, txtAccountKey.Text, sharedKeyCredential);
                DataLakeFileSystemClient fileClient = client.GetFileSystemClient(txtContainer.Text);

                if (client.Uri != null && fileClient.Exists())
                {
                    UpdateStatus("Connection successful: " + client.Uri);
                }
                else
                {
                    throw new Exception("There was an error connecting to the container. Verify connection details");
                }
                return client;
            }
            catch (Exception ex)
            {
                UpdateStatus(ex.Message);
                return null;

            }
        }

        private void UpdateStatus(string message)
        {
            if (txtResults.TextLength > 0)
                txtResults.AppendText(Environment.NewLine);

            txtResults.AppendText(message);
            Log(message);
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

                SaveCSVFile(dt, filePath, true);
            }
        }

        private void SaveCSVFile(DataTable dtDataTable, string strFilePath, bool showMessage)
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
                
                if(showMessage)
                    MessageBox.Show("The file was successfully saved", messageHeader);
            }
            catch(Exception ex)
            {
                if (showMessage)
                    MessageBox.Show("There was an exception saving the file: " + ex.Message, messageHeader);
            }
        }


        private void txtManifestRoot_KeyDown(object sender, KeyEventArgs e)
        {
            ClearManifestSample();
        }


        private void ClearManifestSample()
        {
            if (txtManifestRoot.Text == @"/sampleFolder/sampleFolder/")
            {
                txtManifestRoot.Clear();
            }
        }

        private bool ValidateEntityManifest(string filePath, string entityName, DataLakeFileSystemClient client, string entityManifestPath)
        {
            string line = string.Empty;
            int manifestColumnCount = 1;
            string manifestContent = string.Empty;
            bool attributeNameMatch = true;
            bool attributeDataTypeMatch = true;
            bool hasErrors = false;
            StringBuilder sb = new StringBuilder();
            int maxResultCount = 0;
            string manifestAttributeName;
            string manifestDataType;
            string entityAttributeName;
            string entityDataType;
           
            JsonElement root;
            JsonElement attributeName;
            JsonElement dataType;

            DataRow[] dataRow;
            #region tabledeclartion 
            entityStructure.Rows.Clear();
            entityStructure.Columns.Clear();
            entityStructure.Columns.Add("AttributeName");
            entityStructure.Columns.Add("AttributeOrder");
            entityStructure.Columns.Add("DataType");


            DataTable manifestStructure = new DataTable();
            manifestStructure.Rows.Clear();
            manifestStructure.Columns.Clear();
            manifestStructure.Columns.Add("AttributeName");
            manifestStructure.Columns.Add("AttributeOrder");
            manifestStructure.Columns.Add("DataType");

            #endregion
            string filterExpression = "EntityName = '" + entityName + "'";
            dataRow = manifest.Select(filterExpression);

            try
            { 
                DataLakeFileClient entityManifest = client.GetFileClient(entityManifestPath);
                DataLakeFileClient fileClient = client.GetFileClient(filePath);
            
                //verify the connection
                if (!entityManifest.Exists())
                {
                    throw new Exception("Unable to connect to the entity manifest container. Verify connection details and manifest");
                }

                //verify the connection
                if (!fileClient.Exists())
                {
                    throw new Exception("Unable to connect to the file container. Verify connection details");
                }

                Response<FileDownloadInfo> downloadResponseFile = fileClient.Read();
                Response<FileDownloadInfo> downloadResponseEntity = entityManifest.Read();

                if (filePath.EndsWith("csv"))
                {
                    ReadCSV(downloadResponseFile);
                }
                else if (filePath.EndsWith("parquet"))
                {
                    
                    ReadParquet(downloadResponseFile);
                }

                Log("Entity Manifest Path: " + entityManifest.Name);
                Log("Entity File Path: " + filePath);

                //download the manifest file
                manifestContent = ReadManifest(downloadResponseEntity);

                var JSONoptions = new JsonDocumentOptions()
                {
                };

                JsonDocument document = JsonDocument.Parse(manifestContent, JSONoptions);

                if (document.RootElement.TryGetProperty("definitions", out root) == false)
                {
                    throw new Exception("The entities node cannot be found");
                }

                var definition = root.EnumerateArray();

                while (definition.MoveNext())
                {
                    var hasAttributes = definition.Current.GetProperty("hasAttributes");
                    var attributes = hasAttributes.EnumerateArray();

                    while (attributes.MoveNext())
                    {
                    
                        if (attributes.Current.TryGetProperty("name", out attributeName) == false)
                        {
                            throw new Exception("The attribute name cannot be found");
                        }
                  
                        if (attributes.Current.TryGetProperty("dataFormat", out dataType) == false)
                        {
                            throw new Exception("The data format cannot be found");
                        }
                        manifestStructure.Rows.Add(attributeName.ToString(), manifestColumnCount.ToString(), dataType.ToString());
                        //increment the counter
                        manifestColumnCount++;
                    }
                }

                //compare the two tables now
                //look for the number of rows
                if (manifestStructure.Rows.Count == entityStructure.Rows.Count)
                {
                    UpdateStatus("Attribute Counts: Match (" + manifestStructure.Rows.Count.ToString() + ")");
                    //compare colum names, data types
                    for (int x = 0; x < manifestStructure.Rows.Count; x++)
                    {
                        //if the column headers are in the file do the check
                        if (chkHasHeaders.Checked)
                        { 
                            if(manifestStructure.Rows[x]["AttributeName"].ToString().ToLower() != entityStructure.Rows[x]["AttributeName"].ToString().ToLower())
                            {
                                if (x == 0)
                                {
                                    sb.AppendLine();
                                    sb.AppendLine("--- Attribute Details ---");
                                }
                                //increment the counter in the logs to reflect the output from the data tables
                                sb.AppendLine("Mismatch in attribute name in position " + x.ToString() + 1);
                                attributeNameMatch = false;
                                hasErrors = true;
                            }
                        }

                        if (manifestStructure.Rows[x]["DataType"].ToString().ToLower() != entityStructure.Rows[x]["DataType"].ToString().ToLower())
                        {
                            //only add this formating if it hasn't been added and on the first loop
                            if ((x == 0) && (attributeNameMatch == true))
                            {
                                sb.AppendLine();
                                sb.AppendLine("--- Start Attribute Details ---");
                            }

                            if (attributeNameMatch)
                            {
                                sb.AppendLine("Mismatch in data types for " + manifestStructure.Rows[x]["AttributeName"].ToString());
                            
                            }
                            else
                            {
                                //increment the counter in the logs to reflect the output from the data tables
                                sb.AppendLine("Mismatch in data types in position " + x.ToString() + 1);
                            }
                            hasErrors = true;
                            attributeDataTypeMatch = false;
                        }
                    }


                    if (chkHasHeaders.Checked)
                    {
                        if (attributeNameMatch)
                            UpdateStatus("Attribute Names: Match");
                        else
                            UpdateStatus("Attribute Names: Mismatch");
                    }
                    else
                        UpdateStatus("Attribute Names: Skipped");


                    if (attributeDataTypeMatch)
                        UpdateStatus("Attribute Data Types: Match");
                    else
                        UpdateStatus("Attribute Data Types: Mismatch");
                }
                else
                {
                    UpdateStatus("Attribute Counts: Mismatch - Manifest: " + manifestStructure.Rows.Count.ToString() + " - File: " + entityStructure.Rows.Count.ToString());
                    hasErrors = true;
                }
                sb.AppendLine("--- End Attribute Details ---");
                Log(sb.ToString());
             
                if (manifestStructure.Rows.Count > entityStructure.Rows.Count)
                    maxResultCount = manifestStructure.Rows.Count;
                else
                    maxResultCount = entityStructure.Rows.Count;

                //structure the results so the entities can be compared side-by-side in Excel
                for (int x = 0; x < maxResultCount; x++)
                {
                    if (x > 30)
                        System.Diagnostics.Debug.WriteLine("---");

                    //if the row is out of range
                    try
                    { 
                        manifestAttributeName = manifestStructure.Rows[x]["AttributeName"].ToString();
                        manifestDataType = manifestStructure.Rows[x]["DataType"].ToString();
                    }
                    catch
                    {
                        manifestAttributeName = " - ";
                        manifestDataType = " - ";
                    }

                    //if the row is out of range
                    try
                    {
                        entityAttributeName = entityStructure.Rows[x]["AttributeName"].ToString();
                        entityDataType = entityStructure.Rows[x]["DataType"].ToString();
                    }
                    catch
                    {
                        entityAttributeName = " - ";
                        entityDataType = " - ";
                    }

                    resultsDetails.Rows.Add(entityName, x, manifestAttributeName, manifestDataType, entityAttributeName, entityDataType);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, messageHeader);
                hasErrors = true;
            }
            return hasErrors;
        }

        private void Log(string message)
        {
            log.AppendLine(message);
        }
        
        private void SaveLogFiles()
        {
            //save the details in a text file
            string filePathName = Directory.GetCurrentDirectory() + @"\cdmutilitylog.txt";
            //this code section write stringbuilder content to physical text file.
            using (StreamWriter swriter = new StreamWriter(filePathName, false))
            {
                swriter.Write(log.ToString());
            }

            //save the comparison into csv file
            SaveCSVFile(resultsDetails, "cdmutilityresults.csv", false);
        }

        private void chkHasHeaders_CheckedChanged(object sender, EventArgs e)
        {
            if(chkHasHeaders.Checked==false)
            {
                MessageBox.Show("Warning: Without headers the utility cannot compare entity field names.", messageHeader);
            }
        }

        private void chkRoot_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRoot.Checked)
            { 
                txtManifestRoot.Enabled = false;
                txtManifestRoot.Text = "/";
            }
            else
                txtManifestRoot.Enabled = true;
        }


        private void ReadParquet(Response<FileDownloadInfo> downloadResponseFile)
        {
             // open parquet file reader
            using (var parquetReader = new ParquetReader(downloadResponseFile.Value.Content))
            {
                // get file schema (available straight after opening parquet reader)
                // however, get only data fields as only they contain data values
                DataField[] dataFields = parquetReader.Schema.GetDataFields();

                if (entityStructure.Columns.Count > 0)
                {
                    entityStructure.Rows.Clear();
                    entityStructure.Columns.Clear();
                }

                for (int k = 0; k < dataFields.Length; k++)
                {
                    entityStructure.Rows.Add(dataFields[k].Name, k+1, dataFields[k].DataType);
                }
            }
            
        }

        private void ReadCSV(Response<FileDownloadInfo> downloadResponseFile)
        {
            string[] columnValue;
            bool failedParse = false;
            string attributeDataType = "string";
            DateTime DTresult;
            decimal Decresult;
            int Intresult;
            int columnCount = 1;

            //read in the first row for column names and to get the initial structure
            using (TextFieldParser fieldParser = new TextFieldParser(downloadResponseFile.Value.Content))
            {
                
                fieldParser.TextFieldType = FieldType.Delimited;
                fieldParser.Delimiters = new string[] { "," };

                if (cmbQuote.Text == "None")
                    fieldParser.HasFieldsEnclosedInQuotes = false;
                else
                    fieldParser.HasFieldsEnclosedInQuotes = true;

                //get each of the fields
                columnValue = fieldParser.ReadFields();

                //if there headers in the CSV files then the first row contains the name;
                if (chkHasHeaders.Checked)
                {
                    fieldParser.ReadLine();

                    //Processing row
                    string[] columns = fieldParser.ReadFields();

                    foreach (string column in columns)
                    {
                        entityStructure.Rows.Add(column, columnCount, "string");
                        columnCount++;
                    }
                }
                //reset the counter
                columnCount = 1;
                //read in a record to try the data types
                fieldParser.ReadLine();

                //Processing row
                string[] values = fieldParser.ReadFields();

                foreach (string value in values)
                {
                    //check for the dates
                    if (Int32.TryParse(value, out Intresult) && failedParse == false)
                    {
                        attributeDataType = "int32";
                    }
                    else if (Decimal.TryParse(value, out Decresult) && failedParse == false)
                    {
                        attributeDataType = "decimal";
                    }
                    else if (DateTime.TryParse(value, out DTresult) && failedParse == false)
                    {
                        attributeDataType = "dateTime";
                    }
                    else
                    {
                        failedParse = true;
                        attributeDataType = "string";
                    }

                    if (!chkHasHeaders.Checked)
                    {
                        entityStructure.Rows.Add("N/A", columnCount, attributeDataType);
                        columnCount++;
                    }
                    //update the attribute data type for records that already exist
                    else
                    {
                        if (entityStructure.Rows[columnCount]["DataType"] != null)
                        {
                            entityStructure.Rows[columnCount]["DataType"] = attributeDataType;
                        }
                    }

                    //reset the failed parse check
                    failedParse = false;
                }
            }
        }

        private string ReadManifest(Response<FileDownloadInfo> downloadResponseFile)
        {
            string manifestContent = string.Empty;

            //download the manifest file
            using (StreamReader streamReaderManifest = new StreamReader(downloadResponseFile.Value.Content))
            {

                //format the JSON
                try
                {
                    var options = new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    };

                    var jsonElement = JsonSerializer.Deserialize<JsonElement>(streamReaderManifest.ReadToEnd().ToString());

                    manifestContent = JsonSerializer.Serialize(jsonElement, options);

                }
                catch (JsonException jx)
                {
                    MessageBox.Show("The manifest file is not in a proper JSON format: " + jx.Message, messageHeader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error has occurred: " + ex.Message, messageHeader);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            return manifestContent;
        }
    }
}
    