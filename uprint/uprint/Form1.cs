using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;


namespace _123
{
    public partial class Form1 : Form
    {

        //定义一个字符串流，用来接收所要打印的数据

        private StringReader sr;
        private StringBuilder sb;
        //private int start;
        private int time;
        //str要打印的数据
        private string position;



        public bool Print(string str)
        {
            bool result = true;
            try
            {
                sr = new StringReader(str);               
                PrintDocument pd = new PrintDocument();
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                pd.DefaultPageSettings.Margins.Top = 2;
                pd.DefaultPageSettings.Margins.Left = 0;
                pd.DefaultPageSettings.PaperSize = new PaperSize("tom", 320, 5150);
                pd.PrinterSettings.PrinterName = pd.DefaultPageSettings.PrinterSettings.PrinterName;//默认打印机
                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                try
                {
                    pd.Print();
                }
                catch (Exception exs)
                {
                    
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return result;
        }
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Font printFont = new Font("Arial", 9);//打印字体
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            String line = "";
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
            while (count < linesPerPage && ((line = sr.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }
            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        public Form1()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { 
                time = int.Parse(print_time.Text);
                position = textBox1.Text.ToString();
            }
            catch (Exception ec) { MessageBox.Show(ec.Message); }
            
            for (int a = 0; a < time; a = a + 1){
                int b = a + 1;
                StringBuilder sb = new StringBuilder();
            sb.Append("         源康堂口罩排号小票  \n");
            sb.Append("*************************************\n");
            sb.Append("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
            sb.Append("当前排号：" + b + " 号\n");
            sb.Append("本小票仅当次有效，过号无效。\n");
            sb.Append("当前门店：" + position + " 店\n");
            sb.Append("*************************************\n");
            string s1 = sb.ToString();
            Print(s1);
            }            
        }
    }
}
