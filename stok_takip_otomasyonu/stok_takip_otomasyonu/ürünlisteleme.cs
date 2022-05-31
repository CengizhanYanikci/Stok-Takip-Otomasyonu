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
    public partial class ürünlisteleme : Form
    {
        public ürünlisteleme()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-OAG0E9N;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();
        #region arama
        private void txtBarkodNoAra_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from urun where barkodno like '%" + txtBarkodNoAra.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        #endregion
        #region kategorivemarkagetir
        private void kategorigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                combokategori.Items.Add(read["kategori"].ToString());
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
                combomarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }
        #endregion
        #region ürünlisteleme
        private void ürünlisteleme_Load(object sender, EventArgs e)
        {
            ürünlistele();
            kategorigetir();
            markagetir();

        }

        private void ürünlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from urun", baglanti);
            adtr.Fill(daset, "urun");
            dataGridView1.DataSource = daset.Tables["urun"];
            baglanti.Close();
        }
        #endregion
        #region ButonİleGüncelle
        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update urun set urunadi=@urunadi,miktari=@miktari,alisfiyati=@alisfiyati,satisfiyati=@satisfiyati where barkodno=@barkodno", baglanti);
                komut.Parameters.AddWithValue("@barkodno", BarkodNotxt.Text);
                komut.Parameters.AddWithValue("@urunadi", ÜrünAdıtxt.Text);
                komut.Parameters.AddWithValue("@miktari", int.Parse(Miktarıtxt.Text));
                komut.Parameters.AddWithValue("@alisfiyati", double.Parse(AlışFiyatıtxt.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(SatışFiyatıtxt.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();
                daset.Tables["urun"].Clear();
                ürünlistele();
                MessageBox.Show("Güncelleme yapıldı");
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
                MessageBox.Show("Ürün Seçmeden Güncelleme Yapamazsın", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region ButonİleMarkaGüncelle
        private void btnMarkaGüncelle_Click(object sender, EventArgs e)
        {
            if (BarkodNotxt.Text != "")
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update urun set kategori=@kategori,marka=@marka where barkodno=@barkodno", baglanti);
                komut.Parameters.AddWithValue("@barkodno", BarkodNotxt.Text);
                komut.Parameters.AddWithValue("@kategori", combokategori.Text);
                komut.Parameters.AddWithValue("@marka", combomarka.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme yapıldı");
                daset.Tables["urun"].Clear();
                ürünlistele();
            }
            else
            {
                MessageBox.Show("BarkodNo Yazılı Değil", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }
        #endregion
        #region datagridwiew
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BarkodNotxt.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            Kategoritxt.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            Markatxt.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            ÜrünAdıtxt.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            Miktarıtxt.Text = dataGridView1.CurrentRow.Cells["miktari"].Value.ToString();
            AlışFiyatıtxt.Text = dataGridView1.CurrentRow.Cells["alisfiyati"].Value.ToString();
            SatışFiyatıtxt.Text = dataGridView1.CurrentRow.Cells["satisfiyati"].Value.ToString();
        }
        #endregion

        #region yanlıştıklamalar
        #endregion


        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from urun where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["urun"].Clear();
            ürünlistele();
            MessageBox.Show("Kaydı Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
