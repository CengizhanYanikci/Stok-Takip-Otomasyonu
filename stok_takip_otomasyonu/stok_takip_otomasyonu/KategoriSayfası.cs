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
    public partial class KategoriSayfası : Form
    {
        public KategoriSayfası()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-OAG0E9N;Initial Catalog=Stok_Takip;Integrated Security=True");
        bool durum;
        #region kategorigetir
        private void kategorikontrol()//Kategori Kontrol Etme Metodu
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (textBox1.Text == read["kategori"].ToString() || textBox1.Text == "")
                {
                    durum = false;  
                }
            }
            baglanti.Close();
        }
        #endregion
        #region kategoriekleme
        private void guna2GradientButton1_Click(object sender, EventArgs e)//Kategori Ekleme Butonu
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Bu Alanı Boş Bırakamazsın","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                kategorikontrol();
                if (durum==true)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into kategoribilgileri(kategori) values('" + textBox1.Text + "')", baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kategori Eklendi");
                }
                else
                {
                    MessageBox.Show("Böyle Kategori Zaten Var");
                }
                textBox1.Text = "";
            }
            #endregion
        }
    }
}
