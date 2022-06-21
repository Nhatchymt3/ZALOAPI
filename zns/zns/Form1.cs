
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZaloDotNetSDK;

namespace zns
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class Wrapper
        {
            [JsonProperty("root")]
            public DataSet DataSet { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ZaloClient client = new ZaloClient(textBox1.Text);
            JObject result = client.getListFollower(0,50);
            object obj = JsonConvert.DeserializeObject(result.ToString());
            dataGridView1.DataSource = obj;
        }
    }
}
