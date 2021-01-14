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
using ZXing;

namespace nasip//te varsa
{
    public partial class anasayfa : Form
    {
        public anasayfa()
        {
            InitializeComponent();
        }


        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=hastane.mdb");
        OleDbCommand komut = new OleDbCommand();


        private void Form2_Load(object sender, EventArgs e)
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
            dataGridView1.Update();   //deneme
            dataGridView1.Refresh(); //deneme
            /* List view denemesi hatalı başarısız
            listView1.Columns.Add("ID");
            listView1.Columns.Add("Tc");
            listView1.Columns.Add("Adı");
            listView1.Columns.Add("Soyadı");
            listView1.Columns.Add("Telefon");
            listView1.Columns.Add("Yatış Tarihi");
            listView1.Columns.Add("Cıkış Tarihi");
            listView1.Columns.Add("Dosya No");
            listView1.Columns.Add("Notlar");

            komut.Connection = baglanti;
            komut.CommandText = ("SELECT * FROM arsiv");
            OleDbDataReader okuyucu = komut.ExecuteReader();

            while (okuyucu.Read())
            {
                int count = listView1.Items.Count;
                listView1.Items.Add(okuyucu["ID"].ToString());
                listView1.Items[count].SubItems.Add(okuyucu["Tc"].ToString());
                listView1.Items[count].SubItems.Add(okuyucu["Adı"].ToString());
                listView1.Items[count].SubItems.Add(okuyucu["Soyadı"].ToString());
                listView1.Items[count].SubItems.Add(okuyucu["Telefon"].ToString());
                listView1.Items[count].SubItems.Add(okuyucu["Yatış Tarihi"].ToString());
                listView1.Items[count].SubItems.Add(okuyucu["Çıkış Tarihi"].ToString());
                listView1.Items[count].SubItems.Add(okuyucu["Dosya No"].ToString());
                listView1.Items[count].SubItems.Add(okuyucu["Notlar"].ToString());
            }*/
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                baglan();
                OleDbCommand kaydet = new OleDbCommand("insert into arsiv (tcno,adi,soyadi,telno,yatistar,cikistar,dosyano,notlar) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "' , '" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')", baglanti);
                kaydet.ExecuteNonQuery();
                dataGridView1.Update();   //deneme
                dataGridView1.Refresh(); //deneme

                //komut.ExecuteNonQuery();
                //komut.Dispose();
                baglanti.Close();


            }
            catch (Exception hata)
            {
                MessageBox.Show("Uyarı!\nHata Mesajı: " + hata.Message);
            }
            //deneme
            f5();
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

        void f5()
        {
            //ev yapımı geleneksel datagrid güncelleyici
            yenile a = new yenile();
            this.Hide();
            a.Show();
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
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

        /*deneysel
        public string tcno;
        public void veri_oku()
        {
            try
            {
                int stk;
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                OleDbCommand komut = new OleDbCommand("Select * from arisv  where tcno='" + textBox1.Text + "'", baglanti);
                OleDbDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    textBox1.Text = oku.GetString(1);
                    textBox2.Text = oku.GetString(2);
                    textBox3.Text = oku.GetString(3);
                    textBox4.Text = oku.GetString(4);
                    textBox5.Text = oku.GetString(5);
                    textBox6.Text = oku.GetString(6);
                    textBox7.Text = oku.GetString(7);
                    textBox8.Text = oku.GetString(8);
                }
                baglanti.Close();
            }
            catch
            {
                MessageBox.Show("Tekrar Deneyiniz.");
            }
        }       */


        private void button2_Click(object sender, EventArgs e)
        {
            //if (textBox1.Text == )
            if (baglanti.State == ConnectionState.Closed) baglanti.Open();
            OleDbCommand sil = new OleDbCommand("delete from arsiv where tcno ='" + textBox1.Text + "'", baglanti);
            sil.ExecuteNonQuery();
            baglanti.Close();

            // OleDbCommand veri = new OleDbCommand("SELECT tcno FROM arsiv WHERE tcno = '" + textBox1.Text + "'", baglanti);
            //tcno =veri.ExecuteScalar().ToString ();
            /* OleDbDataReader oku = null;
             oku = veri.ExecuteReader();
             while (oku.Read())
             {
                 tcno = oku["tcno"].ToString();
             }
             oku.Close();
             baglanti.Close();

             if (tcno != textBox1.Text)
             {
                 MessageBox.Show("Böyle bir Hasta Kaydı Yoktur.");
             }

             else
             {
                 veri_oku();
             } */

            f5();
        }

        private void button8_Click(object sender, EventArgs e)
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


        private void button6_Click(object sender, EventArgs e)
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
            //saygılar hocam
            MessageBox.Show("İlker Sülün\n Okul No: 221903546 \n Bilgisayar Programcılığı İÖ\n\n Copyright 2021", "Hakkımda", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


    }
}
