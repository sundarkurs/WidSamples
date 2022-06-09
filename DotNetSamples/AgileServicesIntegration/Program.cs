using AgileServicesIntegration.Models.Placeholder;
using AgileServicesIntegration.Models.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static AgileServicesIntegration.Models.XML.PlaceholderXmlModel;

namespace AgileServicesIntegration
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            LoadOpenObjects();

            //var placeholders = GetAllPlaceholders();

            //UploadRequestBuilder requestBuilder = new UploadRequestBuilder();
            //var uploadRequest = await requestBuilder.GetXMLFormData();

            string fileName = $"XPlmSample{1}.xml";
            //var uploadResponse = UploadToAgile(uploadRequest, fileName);

            Console.WriteLine("Completed");

        }

        public static List<DocumentPlaceholderModel> GetAllPlaceholders()
        {
            string getUrl = "http://haudeerlplm92s:8181/WSAUploadMarketingFiles/GetOpenDocuments";

            try
            {
                var xmlSerializer = new XmlSerializer(typeof(GetOpenDocumentsResponse));
                var openDocuments = new GetOpenDocumentsResponse();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getUrl);
                request.Method = "GET";
                request.ContentType = "application/json";
                WebResponse response = request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    openDocuments = (GetOpenDocumentsResponse)xmlSerializer.Deserialize(reader);
                }

                return MapPlaceholder(openDocuments);

            }
            catch (XmlException exXML)
            {
                throw new XmlException(exXML.Message);
            }
            catch (WebException exWeb)
            {
                throw new WebException(exWeb.Message);
            }
        }

        public static List<DocumentPlaceholderModel> MapPlaceholder(GetOpenDocumentsResponse openDocuments)
        {
            var result = new List<DocumentPlaceholderModel>();

            foreach (var openDocument in openDocuments.Documents.Document)
            {
                var documentPlaceholderModel = new DocumentPlaceholderModel()
                {
                    DocumentIdInternal = openDocument.DocumentIdInternal,
                    DocumentId = openDocument.DocumentId,
                    Version = openDocument.Version,
                    Issue = openDocument.Issue,
                    DocumentName = openDocument.Name,
                    Language = openDocument.Language,
                    Class = openDocument.Class,
                    ProjectHType = openDocument.ProjectHType,
                    ClassDescription = openDocument.ClassDescription,
                    OEMPrivateLabel = openDocument.OEM_PrivatLabel,
                    LZFNumber = openDocument.LZFNumber,
                    WSA = openDocument.WSA
                };

                result.Add(documentPlaceholderModel);
            }

            return result;
        }

        public static XMLUploadResponse UploadToAgile(byte[] bytes, string fileName)
        {
            string postURL = "http://haudeerlplm92s:8181/WSAUploadMarketingFiles/UploadMarketingFiles";

            try
            {
                Dictionary<string, object> postParameters = new Dictionary<string, object>();

                if (bytes != null)
                {
                    postParameters.Add("fileToUpload", new FormUpload.FileParameter(bytes, fileName, "text/xml"));
                }

                string userAgent = "PLM Service";

                HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(postURL, userAgent, postParameters);

                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());

                string responseXML = responseReader.ReadToEnd();

                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    var xmlSerializer = new XmlSerializer(typeof(XPlm));
                    XPlm rootXPlmResponse = (XPlm)xmlSerializer.Deserialize(new StringReader(responseXML));
                    var xmlUploadResponse = new XMLUploadResponse();

                    var fieldListResponse = rootXPlmResponse.FieldCollection.FieldList;

                    foreach (var itemField in fieldListResponse)
                    {
                        switch (itemField.Name)
                        {
                            case "Status":
                                xmlUploadResponse.StatusValue = itemField.Value;
                                break;
                            case "Message":
                                xmlUploadResponse.MessageValue = itemField.Value;
                                break;
                        }
                    }

                    webResponse.Close();
                    return xmlUploadResponse;
                }
                else
                {
                    webResponse.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LoadOpenObjects()
        {

            string filePath = "C:\\Sun\\widex\\poc\\DocCenter\\DocCenterSample\\AgileServicesIntegration\\Data\\OpenObjects\\GetOpenDocuments.xml";
            var openObectsData = File.ReadAllBytes(filePath);

            string content = Encoding.Default.GetString(openObectsData);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);

            var xmlSerializer = new XmlSerializer(typeof(GetOpenDocumentsResponse));
            var openDocuments = (GetOpenDocumentsResponse)xmlSerializer.Deserialize(new StreamReader(filePath));

        }
    }
}
