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
    public partial class Form1 : Form
    {
        private string connect = Properties.Settings.Default.mydishConnectionString;
        Form2 f2;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("����");
            comboBox1.Items.Add("Castellano");
            comboBox1.Items.Add("PinYin");
            comboBox1.SelectedIndex = 0;
            pictureBox1.Image = Image.FromFile(System.IO.Path.GetFullPath("diccionario_foodie.jpg"));
            textBox2.Text = "\r\n\r\n\r\nWelcome to the dish diccionary, here you��ll learn about all the things of Chinese and Spanish food. More dishes informations are just in one click!" +
                "\r\n\r\n��ӭ����ʳ��ʵ䡣�й����в����������͵�һ�У�һ������" + "\r\n\r\nBienvenido al diccionario de los platos, aqu�� puedes encontrar las cosas de las comidas chinas y espanoles. Toda la informaci��n de las comidas a un solo clic.";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            f2 = new Form2(this);
            f2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string select = "";
            switch (comboBox1.SelectedIndex) { 
                case 0:
                    select = "select * from dish where dishName = '" + textBox1.Text + "';";
                    break;
                case 1:
                    select = "select * from dish where mtranslation = '" + textBox1.Text + "';";
                    break;
                case 2:
                    select = "select * from dish where pinYin = '" + textBox1.Text + "';";
                    break;
                default:
                    MessageBox.Show("Select Error!");
                    return;
            }
            using (SqlConnection conn = new SqlConnection(connect))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = select;
                    cmd.Connection = conn;
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read() == true)//find
                    {
                        switch (comboBox1.SelectedIndex) { 
                            case 0:
                                textBox2.Text = "����: " + r[0].ToString() + "\r\n����: " + r[1].ToString() + "\r\nƴ��: " + r[2].ToString() + "\r\n��ϵ: " +
                                    r[3].ToString() + "\r\n��ζ: " + r[4].ToString() + "\r\n���ò���: " + r[5].ToString() + "\r\n���: " + r[6].ToString() + "\r\n����: " +
                                    r[7].ToString() + "\r\n����: " + r[8].ToString() + "\r\n�Ƽ�����: " + r[9].ToString();
                                pictureBox1.Image = new Bitmap(new MemoryStream((byte[])r[10]));
                                break;
                            case 1:
                            case 2:
                                textBox2.Text = "����: " + r[0].ToString() + "\r\n����: " + r[1].ToString() + "\r\nƴ��: " + r[2].ToString() + "\r\n��ϵ: " +
                                    r[3].ToString() + "\r\n��ζ: " + r[4].ToString() + "\r\n���ò���: " + r[5].ToString() + "\r\n���: " + r[6].ToString() + "\r\n����: " +
                                    r[7].ToString() + "\r\n����: " + r[8].ToString() + "\r\n�Ƽ�����: " + r[9].ToString();
                                pictureBox1.Image = new Bitmap(new MemoryStream((byte[])r[10]));
                                break;
                            default:
                                break;
                        }
                    }
                    else 
                    {
                        throw new Exception("Find Error!");
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    textBox1.Text = "";
                }
            
            }
        }
    }
}
