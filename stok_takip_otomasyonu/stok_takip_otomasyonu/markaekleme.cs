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
    public partial class markaekleme : Form
    {
        public markaekleme()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-OAG0E9N;Initial Catalog=Stok_Takip;Integrated Security=True");
        bool durum;
        #region markakontrol
        private void markakontrol()//Marka Kontrol Metodu
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (textBox1.Text == read["marka"].ToString() || textBox1.Text == "")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        #endregion
        #region markaekleme
        private void guna2GradientButton1_Click_1(object sender, EventArgs e)//Marka Ekleme Butonu
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Bu Alanı Boş Bırakamazsın", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                markakontrol();
                if (durum== true)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into markabilgileri(marka) values('" + textBox1.Text + "')", baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Marka Eklendi");
                }
                else
                {
                    MessageBox.Show("Böyle Bir Marka Var");
                }
                textBox1.Text = "";
            }
        }
        #endregion
        #region boşlar
        private void markaekleme_Load(object sender, EventArgs e)
        {
        }
        #endregion
    }
}