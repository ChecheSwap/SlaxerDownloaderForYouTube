using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml.Linq;
using System.Net;
using System.IO;
using YoutubeExtractor;
using System.Threading;

namespace Slaxer
{
    public partial class uno : Mybase
    {    
        ~ uno()
        {
            System.Diagnostics.Trace.WriteLine("Call is over! Code By Jesús José Navarrete Baca");
            this.Dispose(true);
        }        
        entityx central;
        SaveFileDialog myDialog;    
        public uno()
        {
            InitializeComponent();
            this.pbar.Minimum = 0;
            this.pbar.Maximum = 100;
            this.Height = 178;
            this.charging.Visible = false;
            this.centralpanel.Visible = false;
            this.central = new entityx(true);
            this.textBox1.KeyDown += (sender, args) => {this.textBox1.Text = (args.KeyData == Keys.Delete) ? null : this.textBox1.Text; };
            this.myDialog = new SaveFileDialog() { InitialDirectory = "C:\\", RestoreDirectory = true};
            this.FormClosing += this.exiting;                                     
        }

        private void uno_Load(object sender, EventArgs e)
        {           
            this.pbar.Minimum = 0;                                    
        }

        private void button1_Click(object sender, EventArgs e)
        {}

        protected void autoFormInvokeSecure(int x)
        {
            switch(x)
            {
                case 1: 
                    {                        
                            Invoke(new MethodInvoker(delegate ()
                            {                               
                                this.charging.Visible = false;
                                this.centralpanel.Visible = false;                                
                                this.Height = 183;                                
                            }));                                                                      
                        break;
                    }
                case 2:
                    {
                        Invoke(new MethodInvoker(delegate ()
                        {
                            this.charging.Visible = true;
                            this.Height = 230;
                        }
                        ));                                                                                                                        
                        break;
                    }
                case 3:
                    {
                        Invoke(new MethodInvoker(delegate ()
                        {                            
                            this.charging.Visible = false;
                            this.centralpanel.Visible = false;
                            this.Height = 648;
                            this.charging.Visible = false;
                            this.centralpanel.Visible = true;
                            this.Height = 709;
                        }));
                      
                        break;
                    }
                case 4:
                    {
                        Invoke(new MethodInvoker(delegate ()
                        {
                            this.gBox1.Enabled = false;
                        }));
                        break;
                    }
                case 5:
                    {
                        Invoke(new MethodInvoker(delegate ()
                        {
                            this.repyout.Movie = this.adecuateString(this.textBox1.Text.Trim());
                        }));
                        break;
                    }                    
                default:
                    break;
            }
        }
        
        private string adecuateString(string line)
        {
            line = line.Remove(24,6);
            line = line.Replace("=", "/");
            return line;
        }
        private void general()
        {
            if (!(this.textBox1.Text.Trim() == null || this.textBox1.Text.Trim() == ""))
            {
                Boolean adv = true;
                try
                {                    
                    this.autoFormInvokeSecure(2);
                    central.urlsVideoList = DownloadUrlResolver.GetDownloadUrls(this.textBox1.Text.Trim());
                    Invoke(new MethodInvoker(delegate () { getVideo(ref central.urlsVideoList); })); 
                    this.autoFormInvokeSecure(3);
                    this.autoFormInvokeSecure(4);
                    this.autoFormInvokeSecure(5);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    adv = false;                    
                    Definitions.warning(ex.ToString());                    
                }
                finally
                {
                    if (!adv)
                    {
                        this.autoFormInvokeSecure(1);                        
                    }
                        
                    central.x = null;
                }
            }
            else
            {
                Definitions.warning("Ingrese URL!");                
                central.x = null;                
            }
            Invoke(new MethodInvoker(delegate () { this.textBox1.Enabled = true; this.optionCurrentVentana.Enabled = true; }));
            
        }
        private void getVideo(ref IEnumerable<VideoInfo> myvid)
        {
            List<int> records = new List<int>();            
            foreach (VideoInfo f in myvid)
            {
                int value = (int)f.Resolution;
                if ((!(records.Contains(value)) && (value != 0)))
                {
                    records.Add(value);                    
                }                                   
            }
            records.Sort();
            central.records = records;            
            foreach(int x in central.records)
            {
                this.cbresolution.Items.Add(x.ToString() + " Pixels");
            }           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!(central.x != null))
            {
                this.textBox1.Enabled = false;
                this.optionCurrentVentana.Enabled = false;
                central.x = new Thread(this.general) { IsBackground = true };
                central.x.Start();
            }
            else
            {
                Definitions.ok("Espere, procesando peticion!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {}
        private string selectPath()
        {
            if (myDialog.ShowDialog() == DialogResult.OK)
                return myDialog.FileName;
            return null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           this.textBox2.Text = this.selectPath();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.DownloadFinally();
        }
        private void DownloadFinally()
        {
            if(this.cbresolution.SelectedIndex > -1)
            {
                if ((!(this.textBox2.Text.Trim() == null || this.textBox2.Text.Trim() == "")))
                {
                    try
                    {
                        this.button1.Enabled = false;
                        this.optionCurrentVentana.Enabled = false;
                        central.finalVideo = central.urlsVideoList.First(branch => branch.Resolution == this.central.records.ElementAt(this.cbresolution.SelectedIndex));
                        this.central.videoDownloader = new VideoDownloader(this.central.finalVideo, this.textBox2.Text.Trim() + this.central.finalVideo.VideoExtension);
                        this.central.videoDownloader.DownloadProgressChanged += (baseobject, derivateargs) =>
                        {
                            Invoke(new MethodInvoker(delegate ()
                            {
                                this.pbar.Value = (int)derivateargs.ProgressPercentage;
                                this.label3.Text = this.pbar.Value.ToString() + " %";
                                this.pbar.Update();
                            }
                            ));
                        };
                        this.central.videoDownloader.DownloadFinished += (x, y) => {
                            Invoke(new MethodInvoker(delegate ()
                            {
                                Definitions.ok("El video a sido descargado con exito!");
                                if (CheckState.Checked == this.mycheckbox.CheckState)
                                    Process.Start(this.central.videoDownloader.SavePath);
                                this.pbar.Value = 0;
                                this.label3.Text = "0 %";
                                this.restartInforms(); this.optionCurrentVentana.Enabled = true; this.button1.Enabled = true;
                            }));
                        };                        
                            this.StartDownload();
                    }catch(Exception ex)
                    {
                        Definitions.error(ex.ToString());
                    } 
                    finally
                    {
                        this.central.x = null;
                    }                                    
                }else
                {
                    Definitions.error("Seleccione ruta de descarga!");
                }
            }else
            {
                Definitions.error("Seleccione calidad de video!");
            }
        }
        private void StartDownload()
        {
            this.central.x = new Thread(this.central.videoDownloader.Execute) { IsBackground = true };
            this.central.x.Start();            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Definitions.ok("Ingrese en la ventana emergente el nombre como se va a guardar el archivo a descargar.");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://mx.linkedin.com/in/jesusjosenavarretebaca");
        }
     
        private void button4_Click(object sender, EventArgs e)
        {}

        protected void exiting(object sender, FormClosingEventArgs args)
        {
            if (this.central.x != null && this.central.x.IsAlive)
                this.central.x.Abort();
        }

        protected void newFeedback()
        {
            uno f = new uno();
            f.Show();           
        }

        protected void restartInforms()
        {
            if (!(this.central.x == null))
                Definitions.KillTheThread(ref this.central.x);
            this.central.clear();
            this.autoFormInvokeSecure(1);            
            if (this.textBox1.Enabled == false)
                this.textBox1.Enabled = true;
            if (this.gBox1.Enabled == false)
                this.gBox1.Enabled = true;
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.cbresolution.Items.Clear();
            this.textBox1.Focus();
        }
        private void actualizarFuncionesToolStripMenuItem_Click(object sender, EventArgs e)
        {}

        private void optionCurrentVentana_Click(object sender, EventArgs e)
        {
            this.restartInforms();
        }

        private void optionNewVentana_Click(object sender, EventArgs e)
        {
            this.newFeedback();
        }
    }
}
