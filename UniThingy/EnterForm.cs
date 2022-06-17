using MaterialSkin.Controls;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace UniThingy
{
    public partial class EnterForm : MaterialForm
    {
        SqlConnection MysqlConnection;
        string ConnectionSQL = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\UniThingy\UniThingy\UniThingyDatabase.mdf;Integrated Security=True";
        SqlDataReader MysqlDataReader = null;
        public EnterForm()
        {
            InitializeComponent();

            var materialSkingManager = MaterialSkinManager.Instance;
            materialSkingManager.AddFormToManage(this);
            materialSkingManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkingManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.Blue500, Accent.LightBlue200, TextShade.WHITE);
        }

        public void InsertImage(byte[] image)
        {
            using (SqlConnection cn = new SqlConnection(ConnectionSQL))
            {
                cn.Open();
                for (int i = 1; i <= 20; i++)
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE [Questions] SET [Image]=@Image WHERE [Num]=@Num", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Image", image);
                        cmd.Parameters.AddWithValue("@Num", i);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public byte[] ConvertImageToBytes(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public Image ConvertBytesToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        private void EnterForm_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new Form1();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
