# BYOSAUtility
The BYOSA Utility (AKA CDM Manifest Utility) is designed to validate and test default manifest files for Customer Insights solutions that are using the attach to Common Data Model (CDM) data ingestion approach. The tool validates:
1) The manifest file is a properly structure JSON file
2) The critical CDM elements are present within the manifest file
3) The location of the files within the Azure Data Lake is valid
4) If the regex pattern matches the files found within the Azure Data Lake
5) For each entity that is found in the default manifest file, the utility will compare the entity manifest file and one data file (CSV is currently supported, parquet is coming). In this test the utility is comparing attribute counts, attribute names, and attribute data types.
6) Two log files are generated in addition to the results grid. The files are saved in the same location the utility is executed. One file (.txt) provides detailed notes of the analysis and the second file (csv) provides the comparison of the attributes between the file and entity manifest.

Instructions:
1) To connection the Azure Data Lake, enter in the account name, key, and container name
2) Update the folder root location where the default manifest file is stored within the data lake.
3) Select the default manifest file and/or paste the manifest contents
Click on Validate

Progress is updated in the status box
Detailed results are loaded into the Results grid

Click on Save to save the content from the Results grid
