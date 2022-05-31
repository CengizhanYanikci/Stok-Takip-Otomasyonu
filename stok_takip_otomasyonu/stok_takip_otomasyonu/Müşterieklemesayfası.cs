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
    public partial class Müşterieklemesayfası : Form
    {
        public Müşterieklemesayfası()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-OAG0E9N;Initial Catalog=Stok_Takip;Integrated Security=True");
        #region müşteriekleme
        private void guna2GradientButton1_Click(object sender, EventArgs e)//Müşteri Ekleme Butonu
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into müşteri(tc,adsoyad,telefon,adres,email) values(@tc,@adsoyad,@telefon,@adres,@email)", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@adres", txtAdres.Text);
                komut.Parameters.AddWithValue("@email", txtEmail.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Müşteri Kaydı Eklendi");
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Böyle Bir Müşteri Var", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
