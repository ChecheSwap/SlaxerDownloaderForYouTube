using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExtractor;
using System.Threading;
namespace Slaxer
{
    
    public partial class Mybase : Form
    {

        public Mybase()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.FormClosing += this.formclosing;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Mybase_Load(object sender, EventArgs e)
        {}

        public void formclosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Desea Salir del Programa?","Enviroment Runtime.",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }            
        }
    }
}
