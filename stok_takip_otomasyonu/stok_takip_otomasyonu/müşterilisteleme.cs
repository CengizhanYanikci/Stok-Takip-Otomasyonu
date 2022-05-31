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
    public partial class müşteri_listeleme : Form
    {
        public müşteri_listeleme()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-OAG0E9N;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();
        #region kayıtgösterme
        private void müşteri_listeleme_Load(object sender, EventArgs e)
        {
            {
                Kayıt_Göster();
            }
        }
       

        private void Kayıt_Göster()//Kayıtları DataGridViewe Aktarma
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from müşteri", baglanti);
            adtr.Fill(daset, "müşteri");
            dataGridView1.DataSource = daset.Tables["müşteri"];
            baglanti.Close();
        }
        #endregion
        #region butongüncelle
        private void btngüncelle_Click(object sender, EventArgs e)//Müşteri Güncelleme Butonu
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update müşteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,email=@email where tc=@tc", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["Müşteri"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Müşteri Kaydı Güncellendi");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
        #endregion
        #region butonsil
        private void btnSil_Click(object sender, EventArgs e)//Müşteri Silme Butonu
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from müşteri where tc='" + dataGridView1.CurrentRow.Cells["tc"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["Müşteri"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Kaydı Silindi");
        }
        #endregion
        #region tcara
        private void txtTcAra_TextChanged(object sender, EventArgs e)//Tc Arama 
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from müşteri where tc like '%" + txtTcAra.Text + "%'", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        #endregion
        #region datagrid
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
