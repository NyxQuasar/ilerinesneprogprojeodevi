using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace nasip
{   
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath.ToString() + "\\admin.mdb");
        OleDbCommand komut = new OleDbCommand();

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) //Şifre gizleme/gösterme
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            komut.Connection = baglanti;
            komut.CommandText = "Select * From yonetim where admintc='" + textBox1.Text + "' and sifre='" + textBox2.Text + "'";
            baglanti.Open();
            OleDbDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.Read())
            {
                anasayfa a = new anasayfa();
                this.Hide();
                a.Show();
            }
            else
            {
                MessageBox.Show("Hatalı veya Eksik Giriş Yaptınız!");
            }
            baglanti.Close();
        }

        private void giris_Load(object sender, EventArgs e)
        {

        }
    }   
}
