using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace stok_takip_otomasyonu
{
    public partial class ürünekle : Form
    {
        public ürünekle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-OAG0E9N;Initial Catalog=Stok_Takip;Integrated Security=True");
        bool durum;
        #region barkodkontrol
        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urun", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtBarkodNo.Text == read["barkodno"].ToString() || txtBarkodNo.Text=="")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        #endregion
        #region KategoriveMarkagetir
        private void ürünekle_Load(object sender, EventArgs e)
        {
            kategorigetir();
            markagetir();
        }
        private void kategorigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboKategori.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }
        private void markagetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }
        #endregion
        #region ürünekleme
        private void guna2GradientButton1_Click(object sender, EventArgs e)//Yeni Ürün Ekleme Butonu
        {
            try
            {
                barkodkontrol();
                if (durum == true)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into urun (barkodno,kategori,marka,urunadi,miktari,alisfiyati,satisfiyati,tarih) values(@barkodno,@kategori,@marka,@urunadi,@miktari,@alisfiyati,@satisfiyati,@tarih)", baglanti);
                    komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                    komut.Parameters.AddWithValue("@kategori", comboKategori.Text);
                    komut.Parameters.AddWithValue("@marka", comboMarka.Text);
                    komut.Parameters.AddWithValue("@urunadi", txtÜrünAdı.Text);
                    komut.Parameters.AddWithValue("@miktari", int.Parse(txtMiktarı.Text));
                    komut.Parameters.AddWithValue("@alisfiyati", double.Parse(txtAlışFiyatı.Text));
                    komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatışFiyatı.Text));
                    komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());

                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Ürün Eklendi");
                }
                else
                {
                    MessageBox.Show("Böyle Bir BarkodNo Var", "Uyarı");
                }
                comboMarka.Items.Clear();
                foreach (Control item in groupBox1.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                    if (item is ComboBox)
                    {
                        item.Text = "";
                    }
                }
            }
            catch
            {
                MessageBox.Show("İfadeleri Doldurmadan Ekleyemezsin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region varolanürünekleme
        private void guna2GradientButton2_Click(object sender, EventArgs e)//Var Olan Ürüne Ekleme Yapma Butonu
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update urun set miktari=miktari+'" + int.Parse(Miktarıtxt.Text) + "' where barkodno ='" + BarkodNotxt.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                MessageBox.Show("Var Olan Ürüne Ekleme Yapıldı");
            }
            catch
            {
                MessageBox.Show("Var Olan Barkodu Yazmadan Ekleyemezsin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region barkodiletextboxadeğişgenleriyazdırma
        private void BarkodNotxt_TextChanged(object sender, EventArgs e)
        {
            if (BarkodNotxt.Text=="")
            {
                lblmiktar.Text = ""; 
                foreach(Control item in groupBox2.Controls)
                {
                    if(item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urun where barkodno like '"+BarkodNotxt.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                Kategoritxt.Text = read["kategori"].ToString();
                Markatxt.Text = read["marka"].ToString();
                ÜrünAdıtxt.Text = read["urunadi"].ToString();
                lblmiktar.Text = read["miktari"].ToString();
                AlışFiyatıtxt.Text = read["alisfiyati"].ToString();
                SatışFiyatıtxt.Text = read["satisfiyati"].ToString();
            }
            baglanti.Close();
        }
        #endregion
        #region boşlar
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void Kategori_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void comboKategori_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
