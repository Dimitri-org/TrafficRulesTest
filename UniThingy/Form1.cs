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
using System.Timers;

namespace UniThingy
{
    public partial class Form1 : MaterialForm
    {
        //SqlConnection MysqlConnection;
        //string ConnectionSQL = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\UniThingy\UniThingy\UniThingyDatabase.mdf;Integrated Security=True";
        //SqlDataReader MysqlDataReader = null;



        int n = 1;
        public Question[] Test = new Question[200];

        int[] Order = new int[200];
        Random rnd = new Random();

        int seconds = 0;
        int minutes = 20;
        System.Timers.Timer MyTimer;


        public Form1()
        {
            InitializeComponent();

            var materialSkingManager = MaterialSkinManager.Instance;
            materialSkingManager.AddFormToManage(this);
            materialSkingManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkingManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.Blue500, Accent.LightBlue200, TextShade.WHITE);
        }

        //public void InsertImage(byte[] image, int i)
        //{
        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["UniThingy.Properties.Settings.UniThingyDatabaseConnectionString"].ConnectionString))
        //    {
        //        cn.Open();
        //        using (SqlCommand cmd = new SqlCommand("INSERT INTO [Questions] (Num, Answers, Coranswer, Image)VALUES(@Num, @Answers, @Coranswer, @Image)", cn))
        //        {
        //            cmd.Parameters.AddWithValue("@Num", i);
        //            cmd.Parameters.AddWithValue("@Answers", 0);
        //            cmd.Parameters.AddWithValue("@Coranswer", 0);
        //            cmd.Parameters.AddWithValue("@Image", image);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public byte[] ConvertImageToBytes(Image img)
        //{
        //    using(MemoryStream ms=new MemoryStream())
        //    {
        //        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //        return ms.ToArray();
        //    }
        //}

        //public Image ConvertBytesToImage(byte[] data)
        //{
        //    using(MemoryStream ms=new MemoryStream(data))
        //    {
        //        return Image.FromStream(ms);
        //    }
        //}


        private void Form1_Load(object sender, EventArgs e)
        {

            //for (int i = 1; i <= 20; i++)
            //{
            //    pictureBox1.BackgroundImage = new Bitmap($@"C:\Users\User\source\repos\UniThingy\UniThingy\Resources\Screenshot_{i}.png");
            //    InsertImage(ConvertImageToBytes(pictureBox1.BackgroundImage), i);
            //}
            //pictureBox1.BackgroundImage = null;
            //MysqlConnection = new SqlConnection(ConnectionSQL);
            //await MysqlConnection.OpenAsync();
            //SqlCommand Mysqlcommand = new SqlCommand("SELECT * FROM [Questions]", MysqlConnection);
            //MysqlDataReader = await Mysqlcommand.ExecuteReaderAsync();
            //pictureBox1.BackgroundImage = ConvertBytesToImage((byte[])MysqlDataReader["Image"]);
            //try
            //{
            //    MysqlDataReader = await Mysqlcommand.ExecuteReaderAsync();

            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    MysqlDataReader.Close();
            //}

            for (int i = 1; i <= 20; i++)
            {
                Test[i] = new Question
                {
                    num = i,
                    picture = new Bitmap($@"C:\Users\User\source\repos\UniThingy\UniThingy\Resources\Screenshot_{i}.png"),
                    usranswer = 0,
                    done = false
                };
            }
            Test[0] = new Question();Test[21] = new Question();
            Test[0].done = true;Test[21].done=true;

            Test[1].coranswer = 2; Test[1].answers = 3;
            Test[2].coranswer = 5; Test[2].answers = 5;
            Test[3].coranswer = 2; Test[3].answers = 2;
            Test[4].coranswer = 1; Test[4].answers = 2;
            Test[5].coranswer = 3; Test[5].answers = 4;
            Test[6].coranswer = 1; Test[6].answers = 3;
            Test[7].coranswer = 1; Test[7].answers = 3;
            Test[8].coranswer = 4; Test[8].answers = 4;
            Test[9].coranswer = 3; Test[9].answers = 3;
            Test[10].coranswer = 3; Test[10].answers = 4;
            Test[11].coranswer = 2; Test[11].answers = 4;
            Test[12].coranswer = 2; Test[12].answers = 5;
            Test[13].coranswer = 3; Test[13].answers = 3;
            Test[14].coranswer = 4; Test[14].answers = 5;
            Test[15].coranswer = 1; Test[15].answers = 2;
            Test[16].coranswer = 3; Test[16].answers = 3;
            Test[17].coranswer = 2; Test[17].answers = 2;
            Test[18].coranswer = 2; Test[18].answers = 2;
            Test[19].coranswer = 3; Test[19].answers = 3;
            Test[20].coranswer = 2; Test[20].answers = 3;


            
            for (int j = 1; j <= 20; j++)
            {
                Order[j] = 0;
            }
            for (int i = 1; i <= 20; i++)
            {
                Order[i] = rnd.Next(1, 21);
                for (int j = 1; j <= 20; j++)
                {
                    if (i == j) continue;
                    if (Order[i] == Order[j]) { i--; break; }
                }
            }

            UpdateUI();
            Answerschecker();

            MyTimer = new System.Timers.Timer();
            MyTimer.Interval = 1000;
            MyTimer.Elapsed += OnTimerEvent;
            MyTimer.Start();
        }

        private void OnTimerEvent(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                seconds -= 1;
                if (seconds < 0) { seconds = 59; minutes--; }
                if (minutes < 0) { minutes = 0; seconds = 0; Finisher(); }
                textBox1.Text = String.Format("{0}:{1}", minutes.ToString().PadLeft(2,'0'), seconds.ToString().PadLeft(2,'0'));
            }
            ));
        }

        public void UpdateUI()
        {
            label1.Text = "Питання №" + n;
            pictureBox1.BackgroundImage = new Bitmap(Test[Order[n]].picture);
            switch (Test[Order[n]].answers)
            {
                case 2:
                    radioButton1.Visible = true;
                    radioButton2.Visible = true;
                    radioButton3.Visible = false;
                    radioButton4.Visible = false;
                    radioButton5.Visible = false; break;
                case 3:
                    radioButton1.Visible = true;
                    radioButton2.Visible = true;
                    radioButton3.Visible = true;
                    radioButton4.Visible = false;
                    radioButton5.Visible = false; break;
                case 4:
                    radioButton1.Visible = true;
                    radioButton2.Visible = true;
                    radioButton3.Visible = true;
                    radioButton4.Visible = true;
                    radioButton5.Visible = false; break;
                case 5:
                    radioButton1.Visible = true;
                    radioButton2.Visible = true;
                    radioButton3.Visible = true;
                    radioButton4.Visible = true;
                    radioButton5.Visible = true; break;
            }
        }

        public void Answerschecker()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            switch (Test[Order[n]].usranswer)
            {
                case 1: radioButton1.Checked = true; break;
                case 2: radioButton2.Checked = true; break;
                case 3: radioButton3.Checked = true; break;
                case 4: radioButton4.Checked = true; break;
                case 5: radioButton5.Checked = true; break;
            }
        }

        public void Backwards()     //backwards
        {
            for (int i = 1; i <= 20; i++)
            {
                if (n - i < 1) return;
                else if (Test[Order[n - i]].done == true) continue;
                else { n -= i; break; }
            }
            UpdateUI();
            Answerschecker();
        }

        public void Forward()       //forward
        {
            
            for (int i = 1; i <= 20; i++)
            {
                if (n + i > 20) return;
                else if (Test[Order[n + i]].done == true) continue;
                else { n += i; break; }
            }
            UpdateUI();
            Answerschecker();
        }

        public void Finisher()       //finish
        {
            MyTimer.Stop();
            int sum = 0;
            for (int i = 1; i <= 20; i++)
            {
                if (Test[i].usranswer == Test[i].coranswer) sum++;
            }
            if(sum>=18) MessageBox.Show($"Ви склали іспит\nВи набрали: {sum}/20 балів", "Тест закінчено");
            else MessageBox.Show($"Ви НЕ склали іспит\nВи набрали: {sum}/20 балів", "Тест закінчено");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)          //backwards
        {
            Backwards();
        }

        private void button2_Click(object sender, EventArgs e)          //forward
        {
            Forward();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true) Test[Order[n]].usranswer = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true) Test[Order[n]].usranswer = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true) Test[Order[n]].usranswer = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true) Test[Order[n]].usranswer = 4;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true) Test[Order[n]].usranswer = 5;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Test[Order[n]].usranswer == 0) return;
            Test[Order[n]].done = true;
            switch (n)
            {
                case 1:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button4.BackColor = Color.Green;
                    else button4.BackColor = Color.Red; break;
                case 2:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button5.BackColor = Color.Green;
                    else button5.BackColor = Color.Red; break;
                case 3:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button6.BackColor = Color.Green;
                    else button6.BackColor = Color.Red; break;
                case 4:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button7.BackColor = Color.Green;
                    else button7.BackColor = Color.Red; break;
                case 5:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button8.BackColor = Color.Green;
                    else button8.BackColor = Color.Red; break;
                case 6:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button9.BackColor = Color.Green;
                    else button9.BackColor = Color.Red; break;
                case 7:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button10.BackColor = Color.Green;
                    else button10.BackColor = Color.Red; break;
                case 8:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button11.BackColor = Color.Green;
                    else button11.BackColor = Color.Red; break;
                case 9:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button12.BackColor = Color.Green;
                    else button12.BackColor = Color.Red; break;
                case 10:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button13.BackColor = Color.Green;
                    else button13.BackColor = Color.Red; break;
                case 11:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button14.BackColor = Color.Green;
                    else button14.BackColor = Color.Red; break;
                case 12:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button15.BackColor = Color.Green;
                    else button15.BackColor = Color.Red; break;
                case 13:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button16.BackColor = Color.Green;
                    else button16.BackColor = Color.Red; break;
                case 14:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button17.BackColor = Color.Green;
                    else button17.BackColor = Color.Red; break;
                case 15:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button18.BackColor = Color.Green;
                    else button18.BackColor = Color.Red; break;
                case 16:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button19.BackColor = Color.Green;
                    else button19.BackColor = Color.Red; break;
                case 17:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button20.BackColor = Color.Green;
                    else button20.BackColor = Color.Red; break;
                case 18:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button21.BackColor = Color.Green;
                    else button21.BackColor = Color.Red; break;
                case 19:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button22.BackColor = Color.Green;
                    else button22.BackColor = Color.Red; break;
                case 20:
                    if(Test[Order[n]].usranswer==Test[Order[n]].coranswer) button23.BackColor = Color.Green;
                    else button23.BackColor = Color.Red; break;
            }
            Backwards();
            Forward();

            for (int i = 1; i <= 20; i++)
            {
                if (Test[i].done == false) return;
            }
            Finisher();
        }
    }

    public class Question
    {
        public int num;
        public int answers;
        public int coranswer;
        public int usranswer;
        public Bitmap picture;
        public bool done;
    }
}