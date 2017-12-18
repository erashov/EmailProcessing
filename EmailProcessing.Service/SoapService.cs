using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using EmailProcessing.DAL.Entities;
using EmailProcessing.Model.EmailProcessingViewModels;

namespace EmailProcessing.Service
{
    public class SoapService : ISoapService
    {
        public HttpWebRequest SendRequest(string message)
        {
            throw new NotImplementedException();
        }

        public string SendRequest(Setting setting, List<ParamMessage> list)
        {
            return SOAPManual(setting, list);
        }

        private String SOAPManual(Setting setting, List<ParamMessage> list)
        {
            // const string url = "URL";
            const string action = "METHOD_NAME";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(list);
            HttpWebRequest webRequest = CreateWebRequest(setting.ServiceUrl, action);

            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            string result;
            using (WebResponse response = webRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    result = rd.ReadToEnd();
                }
            }
            result = "sucsess";
            return result;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(List<ParamMessage> list)
        {
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string param = string.Empty;
            foreach (var item in list)
            {
                param += $"<{item.Name}>{item.Value}</{item.Name}>\r\n";
            }
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <soap:Body>
                    <Method xmlns=""http://www.sample.com/path/"">
                    " + param + @"
                    </Method>
                    </soap:Body>
                    </soap:Envelope>");
            return soapEnvelopeXml;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}
