using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using ZaloDotNetSDK;
using ZaloDotNetSDK.entities.oa;

namespace XProject.WindowForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        public class Data1
        {
            public int total { get; set; }
            public List<Follower> followers { get; set; }
        }

        public class Follower
        {
            public string user_id { get; set; }
        }

        public class Root
        {
            public Data1 data { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ZaloClient client = new ZaloClient(textBox1.Text);
            JObject result = client.getListFollower(0, 50);
            if (result.HasValues)
            {

                //var dt = JsonConvert.DeserializeObject<List<Data>>(result.ToString());
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result.ToString());
                dataGridView1.DataSource = myDeserializedClass.data.followers;
                dataGridView1.Columns["user_id"].Width = 200;
                //dataGridView1.Columns["user_id"].DataPropertyName = "user_id";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ZaloClient client = new ZaloClient(textBox1.Text);
            ////JObject result = client.sendTextMessageToUserId(textBox2.Text, "this is \n a message");
            ////JObject result = client.sendImageMessageToUserIdByUrl(textBox2.Text, "this is a message", "https://drive.google.com/drive/u/2/my-drive");

            //JObject result = client.sendImageMessageToUserIdByAttachmentId(textBox2.Text, "this is a messag", "LALmECqgBZ5UfdGzoo4s8bgzOYlECJewJxnsFTqoBZL6e68ynoGkQH3kC2U9Vt5uMA9-DeynVNXBecCjXsXYFbwtQ6tJVsX_HFGrTj9zQNn5zpLvrdXgSq7tD67RVsffJF5bPTifRcePeM8qt7bgEb6jT60YVQE-RZsCEIyd");
            ////JObject result1 = client.uploadImageForOfficialAccountAPI(new ZaloFile(label1.Text));
            ///JObject result = client.sendRequestUserProfileToUserId(textBox2.Text, "we need more infomation", "this message is for testing", "https://stc-developers.zdn.vn/zalo.png");

            JObject result = client.getProfileOfFollower(textBox2.Text);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog.Title = "Select file to be upload.";
            //which type file format you want to upload in database. just add them.
            openFileDialog.Filter = "Select Valid Document(*.pdf; *.doc; *.xlsx; *.html;*.jpg)|*.pdf; *.docx; *.xlsx; *.html;*.jpg";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog.FilterIndex = 1;
            try
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog.FileName);
                        label1.Text = path;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload document.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        public async Task<string> CallAPIGet(string url)
        {
            try
            {
                var token = textBox5.Text;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("access_token", $"{token}");
                HttpResponseMessage response = await client.GetAsync(url);
                string responsetxt = await response.Content.ReadAsStringAsync();
                return responsetxt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> CallAPISendZNS(string url, StringContent formUrlEncodedContent)
        {
            try
            {
                //var content = new FormUrlEncodedContent(formUrlEncodedContent);

                var token = textBox5.Text;
                var client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("access_token", $"{token}");
                HttpResponseMessage response = await client.PostAsync(url, formUrlEncodedContent);
                string responsetxt = await response.Content.ReadAsStringAsync();
                return responsetxt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public class Datum
        {
            public int templateId { get; set; }
            public string templateName { get; set; }
            public object createdTime { get; set; }
            public string status { get; set; }
            public string templateQuality { get; set; }
        }

        public class Metadata
        {
            public int total { get; set; }
        }

        public class Root1
        {
            public int error { get; set; }
            public string message { get; set; }
            public List<Datum> data { get; set; }
            public Metadata metadata { get; set; }
        }
        private async void button4_Click(object sender, EventArgs e)
        {

            string url = "https://business.openapi.zalo.me/template/all?offset=0&limit=100&status=1";
            var resp = await CallAPIGet(url);
            Root1 myDeserializedClass = JsonConvert.DeserializeObject<Root1>(resp);
            dataGridView2.DataSource = myDeserializedClass.data;

        }

        public class Root2
        {
            public int error { get; set; }
            public string message { get; set; }
            public string data { get; set; }


        }
        public static string _resp { get; set; }
        private async void button5_Click(object sender, EventArgs e)
        {
            var template_id = textBox6.Text;
            string url = "https://business.openapi.zalo.me/template/sample-data?template_id=" + template_id;
            _resp = await CallAPIGet(url);
            

            var root = JToken.Parse(_resp);
            
            var properties = root
                // Select nested Data object
                .SelectTokens("data")
                // Iterate through its children, return property names.
                .SelectMany(t => t.Children().OfType<JProperty>().Select(p => p.Name))
                .ToArray();

            var valueproperties = root
                // Select nested Data object
                .SelectTokens("data")
                // Iterate through its children, return property names.
                .SelectMany(t => t.Children().OfType<JProperty>().Select(p => p.Value))
                .ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                dataGridView3.Rows.Add(new object[] {
                properties[i],
                valueproperties[i],
            });
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string url = "https://business.openapi.zalo.me/message/template";
            
            var data = new TemplateData
            {
                otp = "1"
            };
            Root3 rootData = new Root3
            {
                //mode = "development",
                phone = "84379921764",
                template_id = textBox6.Text,
                template_data = data

            };
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string key = cell.Value.ToString();
                    string value = cell.Value.ToString();
                }
            }

            var content = JsonConvert.SerializeObject(rootData);
            //var content = new FormUrlEncodedContent(dict);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var resp = CallAPISendZNS(url, httpContent);

        }
        public class Root3
        {
            public string mode { get; set; }
            public string phone { get; set; }
            public string template_id { get; set; }
            public TemplateData template_data { get; set; }
            //public string tracking_id { get; set; }
        }

        public class TemplateData
        {
            public string otp { get; set; }
        }

        private async void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView2.CurrentRow.Index;
            textBox6.Text = dataGridView2.Rows[index].Cells[0].Value.ToString();
            if(textBox6.Text!=null)
            {
                var template_id = textBox6.Text;
                string url = "https://business.openapi.zalo.me/template/sample-data?template_id=" + template_id;
                _resp = await CallAPIGet(url);


                var root = JToken.Parse(_resp);

                var properties = root
                    // Select nested Data object
                    .SelectTokens("data")
                    // Iterate through its children, return property names.
                    .SelectMany(t => t.Children().OfType<JProperty>().Select(p => p.Name))
                    .ToArray();

                var valueproperties = root
                    // Select nested Data object
                    .SelectTokens("data")
                    // Iterate through its children, return property names.
                    .SelectMany(t => t.Children().OfType<JProperty>().Select(p => p.Value))
                    .ToArray();

                for (int i = 0; i < properties.Length; i++)
                {
                    dataGridView3.Rows.Add(new object[] {
                properties[i],
                valueproperties[i],
            });
                }
            }    
        }
    }
}
