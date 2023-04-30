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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("server=.;Database=Kütüphane;Integrated Security=true;");
        private void button1_Click(object sender, EventArgs e)
        {
            string ekle = "INSERT into Kitaplar(KitapAdi,KitapTürü,YazarAdi,ÇıkışTarihi)VALUES(@a,@b,@c,@d)";
            SqlCommand komut = new SqlCommand(ekle,baglanti);
            baglanti.Open();
            komut.Parameters.Add("@a", textBox1.Text);
            komut.Parameters.Add("@b", textBox2.Text);
            komut.Parameters.Add("@c", textBox3.Text);
            komut.Parameters.Add("@d", textBox4.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
            MessageBox.Show("Kayıt eklendi");
        }
        void listele()
        {
            string yazdır = "SELECT * FROM Kitaplar ORDER BY id DESC";
            SqlDataAdapter da = new SqlDataAdapter(yazdır, baglanti);
            baglanti.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text!="")
            {
                DialogResult cevap = MessageBox.Show(textBox4.Text + "Çıkış tarihli kitabı güncelemk istediğine eminmisin", "Dikkat", MessageBoxButtons.YesNo);
                if (cevap==DialogResult.Yes)
                {
                    string uptade = "UPTADE Kitaplar SET KitapAdi ='" + textBox1.Text + "'KitapTürü ='" + textBox2.Text + "'YazarAdi ='" + textBox3.Text + "'ÇıkışTarihi ='" + textBox4.Text + "'";
                    SqlCommand komut2 = new SqlCommand(uptade, baglanti);
                    baglanti.Open();
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                    listele();
                    MessageBox.Show("Kitaplar güncelendi");
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                DialogResult cevap = MessageBox.Show(textBox4 + "Çıkış tarihli kitabı güncelemk istediğine eminmisin", "Dikkat", MessageBoxButtons.YesNo);
                if (cevap == DialogResult.Yes)
                {
                    string sil = "DELETE FROM Kitaplar WHERE ÇıkışTarihi='" + textBox4.Text + "'";
                    SqlCommand komut2 = new SqlCommand(sil, baglanti);
                    baglanti.Open();
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                    listele();
                    MessageBox.Show("Kitaplar silindi");
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM Kitaplar WHERE KitapAdi='" + textBox1.Text + "'";
            SqlCommand komut3 = new SqlCommand(sorgu, baglanti);
            baglanti.Open();
            int kayitsayisi = Convert.ToInt32(komut3.ExecuteScalar());
            baglanti.Close();
            if (kayitsayisi == 0)
            {
                MessageBox.Show("Kitap Bulunamadı");
            }
            else
            {
                string sorgu1 = "SELECT * FROM Kitaplar WHERE KitapAdi='" + textBox5.Text + "'";
                SqlCommand komut4 = new SqlCommand(sorgu1, baglanti);
                baglanti.Open();
                SqlDataReader oku = komut4.ExecuteReader();
                while (oku.Read())
                {
                    textBox1.Text = oku[1].ToString();
                    textBox2.Text = oku[2].ToString();
                    textBox3.Text = oku[3].ToString();
                    textBox4.Text = oku[4].ToString();
                }
            }
            baglanti.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           listele();
        }
    }
}