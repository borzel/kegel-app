using KegelApp.Ipc.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KegelApp.WinClient
{
    public class NancyClient
    {
        string url;

        public NancyClient(string url)
        {
            this.url = url;
        }

        public List<UserData> GetUser()
        {
            return Get<List<UserData>>("user/list");
        }

        public void PutUser(UserData user)
        {
            Put<UserData>("user/save", user);
        }

        T Get<T>(string urlPath)
        {
            //Create a Web-Request to an URL
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/" + urlPath);

            //Defined poperties for the Web-Request
            httpWebRequest.Method = "GET";
            httpWebRequest.MediaType = "HTTP/1.1";
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.UserAgent = "KegelApp.WinClient";

            //Send Web-Request and receive a Web-Response
            HttpWebResponse httpWebesponse = (HttpWebResponse)httpWebRequest.GetResponse();

            Stream dataStream = httpWebesponse.GetResponseStream();
            StreamReader streamreader = new StreamReader(dataStream, Encoding.UTF8);
            JsonSerializer ser = new JsonSerializer();
            return (T)ser.Deserialize(streamreader, typeof(T));
        }

        void Put<T>(string urlPath, T data)
        {
            //Create a Web-Request to an URL
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/" + urlPath);

            //Defined poperties for the Web-Request
            httpWebRequest.Method = "PUT";
            httpWebRequest.MediaType = "HTTP/1.1";
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.UserAgent = "KegelApp.WinClient";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(data);

                streamWriter.Write(json);
            }

            try
            {

                //Send Web-Request and receive a Web-Response
                HttpWebResponse httpWebesponse = (HttpWebResponse)httpWebRequest.GetResponse();

                if (httpWebesponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
