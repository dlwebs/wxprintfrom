using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace form
{
    public partial class Form1 : Form
    {
        PrinterHelper printerHelper = new PrinterHelper("http://wxprinter.webs.dlwebs.com/");
        private bool printing = false;
        private string Code = "";
        private string ShowURL = "";
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string showurl,string code)
        {
            ShowURL = showurl;
            Code = code;
            InitializeComponent();  
        }
        private string _code = "";
        private string ReadConfig()
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory+"\\config.txt", FileMode.Open);

            StreamReader sr = new StreamReader(fs);

            var readLine = sr.ReadLine();
            sr.Close();
            fs.Close();
            if (readLine != null)
            {
                _code = readLine.Trim().Split(',')[1];
                return readLine.Trim().Split(',')[0];
            }
            else
            {
                return "";
            }
           
        }
        
       

        private void Form1_Load(object sender, EventArgs e)
        {
           
           webBrowser1.Url = new Uri(ShowURL);// new Uri(ReadConfig());
           // webKitBrowser1.Url = new Uri(ShowURL);
            //label1.Text = GetCode();
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(pic_current.Image, 0, 0, pic_current.Image.Width, pic_current.Image.Height);
        }

      
        private void timer2_Tick(object sender, EventArgs e)
        {
            
            lbl_code.Text = printerHelper.GetPrintCode(Code);
            if (printing == false)
            {
                pic_current.Visible = false;
             
                PrintModel.NeedPrintImgModel needPrint = printerHelper.GetNeedPrintImg(Code);
                 if (needPrint != null)
                 {
                     printing = true;
                     WebRequest webreq = WebRequest.Create(needPrint.resource_content);
                     WebResponse webres = webreq.GetResponse();
                     Stream stream = webres.GetResponseStream();
                     Image image;
                     image = Image.FromStream(stream);
                     stream.Close();
                     pic_current.Visible = true;
                     pic_current.Image = image;
                                             
                     PrintService printService = new PrintService();
                     printService.printImage(image);                   
                    /* printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage);
             
                     printDocument1.Print();  
 */

                     printerHelper.SetSuccessPrinted(needPrint.resource_id);
                     
                     
                    // Thread.Sleep(4000);
                   
                     printing = false;
                 }
              

            }
                      
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}