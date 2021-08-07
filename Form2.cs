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
using ZXing; //Barkod okuyucu için

namespace nasip
{
    public partial class anasayfa : Form
    {
        public anasayfa()
        {
            InitializeComponent();
        }
        //Bağlantı
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=hastane.mdb");
        OleDbCommand komut = new OleDbCommand();


        private void Form2_Load(object sender, EventArgs e) //Form başlangıcı
        {
            try
            {
                baglanti.Open();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Veritabanı bağlantı hatası!\n Hata Ayrıntısı:\n" + hata.Message);
                Application.Exit();
            }
            //  DataGrid ile gösterir
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM arsiv", baglanti);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void f5() //datagrid güncellemek için alternatif bir yol
        {
            yenile a = new yenile();
            this.Hide();
            a.Show();
        }

        private void button1_Click(object sender, EventArgs e) //Kayıt yapma
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Tc No kısmını boş bırakmayın.");
            }
            else
            {
                try
                {
                    if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                    baglan();
                    OleDbCommand kaydet = new OleDbCommand("insert into arsiv (tcno,adi,soyadi,telno,yatistar,cikistar,dosyano,notlar) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "' , '" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')", baglanti);
                    kaydet.ExecuteNonQuery();
                    baglanti.Close();
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Uyarı!\nHata Mesajı: " + hata.Message);
                }
                f5();
            }
        }

        void baglan()
        {
            //yedek
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            else
            {
                baglanti.Close();
                baglanti.Open();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e) //İlgili yerleri boşaltmak için
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textbarkod.Text = "";
            pictureBox.Image = null;
        }

        private void button2_Click(object sender, EventArgs e) //Kayıt Silme
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Tc No kısmını boş bırakmayın.");
            }
            else
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                OleDbCommand sil = new OleDbCommand("delete from arsiv where tcno ='" + textBox1.Text + "'", baglanti);
                sil.ExecuteNonQuery();
                baglanti.Close();
                f5();
            }
        }

        private void button8_Click(object sender, EventArgs e) //Kayıt Güncelleme
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen Tc No kısmını boş bırakmayın.");
            }
            else
            {
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                try
                {
                    OleDbCommand guncelle = new OleDbCommand("UPDATE arsiv SET adi='" + textBox2.Text + "',soyadi='" + textBox3.Text + "',telno='" + textBox4.Text + "', yatistar='" + textBox5.Text + "', cikistar='" + textBox6.Text + "', dosyano='" + textBox7.Text + "',notlar='" + textBox8.Text + "' where arsiv.tcno='" + textBox1.Text + "'", baglanti);
                    guncelle.ExecuteNonQuery();
                    baglanti.Close();
                    f5();
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Kontrol:\n" + hata);
                    baglanti.Close();
                }
            }
        }


        private void button6_Click(object sender, EventArgs e) // Barkod Reader
        {
            using(OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPG|*.jpg"})
            {
                if (ofd.ShowDialog() == DialogResult.OK) 
                {
                    pictureBox.Image = Image.FromFile(ofd.FileName);
                    BarcodeReader reader = new BarcodeReader();
                    var result = reader.Decode((Bitmap)pictureBox.Image);
                    if (result != null)
                        textbarkod.Text = result.ToString();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kullanıma açıktır.\n Faydası olması dileğiyle.\n Copyright 2020", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Programda çeşitli eksiklikler bulunmaktadır, tam değildir. \n Barkot okuyucu barkodun üzerindeki rakamları yazar. Deneysel bir özelliktir. Asıl mantığı hasta dosyasını okuyup verileri otomatik girmektir. \n İlk C# Projemdir. Amacı dilin temellerini kavrayabilmek ve kullanabilmektir.");
        }
    }
}