using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nasip
{
    public partial class yenile : Form
    {
        public yenile() // tabloyu yenilemek için bir yol.
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            anasayfa a = new anasayfa();
            this.Hide();
            a.Show();
        }
    }
}