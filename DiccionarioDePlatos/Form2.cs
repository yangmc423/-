using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace DiccionarioDePlatos
{
    public partial class Form2 : Form
    {
        Form1 f1;
        public string strDishName;
        public string strTranslation;
        public string strPinYin;
        public string strDishFrom;
        public string strTasty;
        public string strMaterial;
        public string strIntro;
        public string strMethod;
        public string strComment;
        public string strRecommend;
        public byte[] byteImage = null;

        private string connect = Properties.Settings.Default.mydishConnectionString;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 f1)
        {
            this.f1 = f1;
            InitializeComponent();
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.f1.Show();
            base.OnClosing(e);
        }

        public void callclose()
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fopenf = new OpenFileDialog();
            if (fopenf.ShowDialog() == DialogResult.OK)
            {
                Bitmap bit = new Bitmap(fopenf.FileName);
                pictureBox1.Image = bit;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                ms.Close();
                string inser1 = "insert into [dish] values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox9.Text + "','" + textBox10.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "'," + "@testpic)";
                SqlCommand cmd = new SqlCommand(inser1, conn);
                SqlParameter para = new SqlParameter("@testpic", SqlDbType.Image, byteImage.Length);
                para.Value = byteImage;
               cmd.Parameters.Add(para);
                //Test Open
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Cannot Open!");
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    return;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    pictureBox1.Image = null;
                }
            }
        }
    }
}