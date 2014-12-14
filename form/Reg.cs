using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
 
namespace form
{
    public partial class Reg : Form
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var code = txt_code.Text.Trim();
            PrinterHelper printerHelper = new PrinterHelper("http://wxprinter.webs.dlwebs.com/");
            PrintModel.ActiveCodeMessage result = printerHelper.ActiveDeviced(txt_code.Text.Trim());
            MessageBox.Show(result.message);
            if (result.msgcode == "1")
            {
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\conf.txt", FileMode.Append);


               
                StreamWriter sw = new StreamWriter(fs,Encoding.Default);
                sw.Write(code);
                sw.Close(); 
                showMain(code);
            }

        }

        private void showMain(string code)
        {
            this.Hide();
            Form1 form = new Form1("http://wxprinter.webs.dlwebs.com/index.php/printer/" + code, code);
            form.Show();
        }

        private void Reg_Load(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\conf.txt", FileMode.Open);

            StreamReader sr = new StreamReader(fs);

            var readLine = sr.ReadLine();
            sr.Close();
            fs.Close();
            if (readLine != null)
            {
                showMain(readLine);
            }
            
        }
    }
}
