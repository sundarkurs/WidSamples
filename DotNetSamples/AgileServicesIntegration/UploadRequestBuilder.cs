using AgileServicesIntegration.Models;
using AgileServicesIntegration.Models.Placeholder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static AgileServicesIntegration.Models.XML.PlaceholderXmlModel;

namespace AgileServicesIntegration
{
    public class UploadRequestBuilder
    {
        public async Task<byte[]> GetXMLFormData()
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(XPlm));
                var rootXPlm = await BuildAgileUploadXML();

                SaveAsFile(rootXPlm);

                // Convert XPlm object to byte Array
                var memStream = new MemoryStream();
                byte[] xmlDataBytes = null;
                using (var xmlTextWriter = new XmlTextWriter(memStream, Encoding.Unicode))
                {
                    xmlSerializer.Serialize(xmlTextWriter, rootXPlm);
                    memStream = xmlTextWriter.BaseStream as MemoryStream;
                }
                if (memStream != null)
                {
                    xmlDataBytes = memStream.ToArray();
                }
                else
                {
                    xmlDataBytes = null;
                }

                return xmlDataBytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<XPlm> BuildAgileUploadXML()
        {
            string directoryPath = "C:\\Sun\\widex\\poc\\DocCenter\\DocCenterSample\\AgileServicesIntegration\\Data\\Upload\\";

            var uploadRequest = new XPlm();

            var fieldCollection = BuildHeaderFields();
            uploadRequest.FieldCollection = fieldCollection;

            var documents = Directory.GetFiles(directoryPath);

            var tableCollection = new TableCollection() { TableList = new List<Table>() };

            foreach (var document in documents)
            {
                if (document.Contains("metadata.json"))
                {
                    var tableMetaData = await BuildDocumentTableDataAsync(document);
                    tableCollection.TableList.Add(tableMetaData);
                }
                else
                {
                    var tableLangFiles = await BuildFileTableDataAsync(document);
                    tableCollection.TableList.Add(tableLangFiles);
                }

                uploadRequest.TableCollection = tableCollection;
            }

            return uploadRequest;
        }

        private FieldCollection BuildHeaderFields()
        {
            return new FieldCollection()
            {
                FieldList = new List<Field>()
                {
                    new Field() { Name = XMLFieldConstants.Date, Value = DateTime.Now.ToString("yyyy-MM-dd") },
                    new Field() { Name = XMLFieldConstants.Time, Value = DateTime.Now.ToString("HH:mm:ss") },
                    new Field() { Name = XMLFieldConstants.SourceName, Value = XMLFieldConstants.SourceValue },
                    new Field() { Name = XMLFieldConstants.ActionName, Value = XMLFieldConstants.ActionValue }
                }
            };
        }

        private async Task<Table> BuildDocumentTableDataAsync(string filePath)
        {
            //Build XML with placeholders data extracting from metadata file.

            var documentTableData = new Table() { Name = XMLFieldConstants.TableDocumentName };

            var metadataFile = File.ReadAllBytes(filePath);

            if (metadataFile.Length > 0)
            {
                string metadataJson = Encoding.Default.GetString(metadataFile);

                var documentPlaceholderModel = JsonConvert.DeserializeObject<DocumentPlaceholderModel>(metadataJson);

                documentTableData.FieldCollectionTable = new FieldCollectionTable();
                documentTableData.FieldCollectionTable.FieldCollectionTableFieldCollection = new FieldCollectionTableFieldCollection();

                var documentFields = new List<FieldCollectionTableFieldCollectionField>() {
                    new FieldCollectionTableFieldCollectionField {
                        Name = XMLFieldConstants.DocumentId,
                        Value = documentPlaceholderModel.DocumentId
                    },
                    new FieldCollectionTableFieldCollectionField {
                        Name = XMLFieldConstants.DocumentLanguage,
                        Value = documentPlaceholderModel.Language
                    },
                    new FieldCollectionTableFieldCollectionField {
                        Name = XMLFieldConstants.DocumentType,
                        Value = documentPlaceholderModel.Class
                    }
                };

                documentTableData.FieldCollectionTable.FieldCollectionTableFieldCollection.FieldCollectionTableFieldCollectionFieldList = documentFields;
            }
            return documentTableData;
        }

        private async Task<Table> BuildFileTableDataAsync(string filePath)
        {
            // Build XML with Lang file fields and Base64 converted file data. 

            var fileTableData = new Table() { Name = XMLFieldConstants.TableFileName };

            var documentData = File.ReadAllBytes(filePath);

            fileTableData.FieldCollectionTable = new FieldCollectionTable();
            fileTableData.FieldCollectionTable.FieldCollectionTableFieldCollection = new FieldCollectionTableFieldCollection();

            var fileFields = new List<FieldCollectionTableFieldCollectionField>()
            {
                new FieldCollectionTableFieldCollectionField
                {
                    Name = XMLFieldConstants.FileName,
                    Value = "masterfile.pdf"
                },
                new FieldCollectionTableFieldCollectionField
                {
                    Name = XMLFieldConstants.FilePathName,
                    Value = XMLFieldConstants.FilePathValue,
                },
                new FieldCollectionTableFieldCollectionField
                {
                    Name = XMLFieldConstants.FileType,
                    Value = "pdf"
                },
                new FieldCollectionTableFieldCollectionField
                {
                    Name = XMLFieldConstants.FileData,
                    Value = documentData.Length > 0 ? Convert.ToBase64String(documentData) : string.Empty
                }
            };

            fileTableData.FieldCollectionTable.FieldCollectionTableFieldCollection.FieldCollectionTableFieldCollectionFieldList = fileFields;

            return fileTableData;
        }

        public static void SaveAsFile(XPlm rootXPlm)
        {
            try
            {
                string directory = "C:\\Sun\\Random\\XmlUploadFiles\\";
                string fileName = "UploadRequest.xml";

                bool exists = System.IO.Directory.Exists(directory);

                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

                TextWriter txtWriter = new StreamWriter($"{directory}{fileName}");
                XmlSerializer serializer = new XmlSerializer(typeof(XPlm));
                serializer.Serialize(txtWriter, rootXPlm);
                txtWriter.Close();
            }
            catch (IOException exIO)
            {
                throw exIO;
            }
        }
    }
}
