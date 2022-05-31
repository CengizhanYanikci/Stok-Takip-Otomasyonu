using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stok_takip_otomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region formiçineformaçma
        private Form activeForm = null;
        private void openChildForm(Form childform)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock=DockStyle.Fill;
            panelChildForm.Controls.Add(childform);
            panelChildForm.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }
        #endregion


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            openChildForm(new satissayfasi());
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            openChildForm(new Müşterieklemesayfası());

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            openChildForm(new müşteri_listeleme());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            openChildForm(new ürünekle());
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            openChildForm(new KategoriSayfası());
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            openChildForm(new markaekleme());
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            openChildForm(new ürünlisteleme());
        }

        private void guna2Button8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }
        #region boşlar
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            openChildForm(new satışlısteleme());
        }
    }
}
