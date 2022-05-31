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
    public partial class satissayfasi : Form
    {
        public satissayfasi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-OAG0E9N;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();
        private void sepetilistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from sepet", baglanti);
            adtr.Fill(daset,"sepet");
            dataGridView1.DataSource = daset.Tables["sepet"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            baglanti.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)//Satış Yapma Butonu
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into satis(tc,adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyati,tarih) values(@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyati,@tarih)", baglanti);
                    komut.Parameters.AddWithValue("@tc", txtTC.Text);
                    komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                    komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                    komut.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                    komut.Parameters.AddWithValue("@urunadi", dataGridView1.Rows[i].Cells["urunadi"].Value.ToString());
                    komut.Parameters.AddWithValue("@miktari", int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()));
                    komut.Parameters.AddWithValue("@satisfiyati", double.Parse(dataGridView1.Rows[i].Cells["satisfiyati"].Value.ToString()));
                    komut.Parameters.AddWithValue("@toplamfiyati", double.Parse(dataGridView1.Rows[i].Cells["toplamfiyati"].Value.ToString()));
                    komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                    komut.ExecuteNonQuery();
                    SqlCommand komut2 = new SqlCommand("update urun set miktari=miktari-'" + int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()) + "' where barkodno ='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "'", baglanti);
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                }
                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("delete from sepet", baglanti);
                komut3.ExecuteNonQuery();
                baglanti.Close();
                daset.Tables["sepet"].Clear();
                sepetilistele();
                hesapla();
            }
            catch
            {
                MessageBox.Show("Tabloda Ürün Mevcut Değil", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTC_TextChanged(object sender, EventArgs e)
        {
            if (txtTC.Text == "")
            {
                txtAdSoyad.Text = "";
                txtTelefon.Text = "";
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from müşteri where  tc like '" + txtTC.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtTelefon.Text = read["telefon"].ToString();
            }
            baglanti.Close();
        }

        private void BarkodNo_TextChanged(object sender, EventArgs e)
        {
            temizle();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urun where  barkodno like '" + txtBarkodNo.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtÜrünAdı.Text = read["urunadi"].ToString();
                txtSatışFiyatı.Text = read["satisfiyati"].ToString();
            }
            baglanti.Close();
        }

        private void temizle()
        {
            if (txtBarkodNo.Text == "")
            {
                if (txtÜrünAdı.Text == "")
                {
                    foreach (Control item in groupBox2.Controls)
                    {
                        if (item is TextBox)
                        {
                            if (item != txtMikatarı)
                            {
                                item.Text = "";
                            }
                        }

                    }
                }
            }
        }
        bool durum;
        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from sepet",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtBarkodNo.Text==read["barkodno"].ToString())
                {
                    durum = false;

                }
            }
            baglanti.Close();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                barkodkontrol();
                if (durum == true)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into sepet(tc,adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyati,tarih) values(@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyati,@tarih)", baglanti);
                    komut.Parameters.AddWithValue("@tc", txtTC.Text);
                    komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                    komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                    komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                    komut.Parameters.AddWithValue("@urunadi", txtÜrünAdı.Text);
                    komut.Parameters.AddWithValue("@miktari", int.Parse(txtMikatarı.Text));
                    komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatışFiyatı.Text));
                    komut.Parameters.AddWithValue("@toplamfiyati", double.Parse(txtToplamFiyat.Text));
                    komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                }
                else
                {
                    baglanti.Open();
                    SqlCommand komut2 = new SqlCommand("update sepet set miktari=miktari+'" + int.Parse(txtMikatarı.Text) + "' where barkodno='" + txtBarkodNo.Text + "'", baglanti);
                    komut2.ExecuteNonQuery();
                    SqlCommand komut3 = new SqlCommand("update sepet set toplamfiyati=miktari*satisfiyati where barkodno='" + txtBarkodNo.Text + "'", baglanti);
                    komut3.ExecuteNonQuery();
                    baglanti.Close();
                }
                txtMikatarı.Text = "1";
                daset.Tables["sepet"].Clear();
                sepetilistele();
                hesapla();
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtMikatarı)
                        {
                            item.Text = "";
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("Barkod No Seçmelisin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMikatarı_TextChanged(object sender, EventArgs e)
        {
            try
            {
               txtToplamFiyat.Text=(double.Parse(txtMikatarı.Text) * double.Parse(txtSatışFiyatı.Text)).ToString();
            }
            catch(Exception)
            {
                ;
            }
        }

        private void txtSatışFiyatı_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtToplamFiyat.Text = (double.Parse(txtMikatarı.Text) * double.Parse(txtSatışFiyatı.Text)).ToString();
            }
            catch (Exception)
            {
                ;
            }
        }

        private void satissayfasi_Load(object sender, EventArgs e)
        {
            sepetilistele();
        }

        private void satissayfasi_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Multiply)
            {
                txtMikatarı.Text = txtBarkodNo.Text.Substring(txtBarkodNo.Text.Length - 1);
                txtBarkodNo.Text = "";
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            try
            {
                SqlCommand komut = new SqlCommand("delete from sepet where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
                komut.ExecuteNonQuery();
            }   
            catch
            {
                MessageBox.Show("Tablo Boş");
            }  
            baglanti.Close();
            MessageBox.Show("Ürün sepetten çıkarıldı");
            daset.Tables["sepet"].Clear();
            sepetilistele();
            hesapla();
        }

        private void btnSatışİptal_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from sepet", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Ürün sepetten çıkarıldı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                daset.Tables["sepet"].Clear();
                sepetilistele();
                hesapla();
            }
            catch
            {
                MessageBox.Show("Satışı İptal Etmek Ürün Seçmelisin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void hesapla()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select sum(toplamfiyati) from sepet",baglanti);
                lblgeneltoplam.Text = komut.ExecuteScalar() + "TL";
                baglanti.Close();   
            }
            catch
            {
                ;
            }
        }
    }
}
