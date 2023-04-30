using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("server=.;Database=Kütüphane;Integrated Security=true;");
            string sorgu = "SELECT count(*) FROM Admin WHERE KulaniciAdi ='" + textBox1.Text + "'and Parola ='" + textBox2.Text + "'";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            baglanti.Open();
            int kayitsayisi = Convert.ToInt32(komut.ExecuteScalar());
            baglanti.Close();
            if (kayitsayisi==0)
            {
                MessageBox.Show("Kulanıcı adı veya parola yanlış");
            }
            else
            {
                Form2 frm = new Form2();
                frm.Hide();
                frm.Show();
            }
        }
    }
}