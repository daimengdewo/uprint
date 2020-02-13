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


namespace uprint
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
        private int nextnbr;



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
            Font printFont = new Font("宋体", 9);//打印字体
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
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                nextnbr = int.Parse(next_nbr.Text);
                time = int.Parse(print_time.Text);
                position = textBox1.Text.ToString();
                for (int a = 0; a < time; a = a + 1)
                {
                    int b = nextnbr + 1 + a;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("         1  \n");
                    sb.Append("************************************\n");
                    sb.Append("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
                    sb.Append("当前排号：" + b + " 号\n");
                    sb.Append("本小票仅当次有效，过号无效。\n");
                    sb.Append("当前门店：" + "1 " + position + " 店\n");
                    sb.Append("离开排队队伍本小票无效。\n");
                    sb.Append("************************************\n");
                    string s1 = sb.ToString();
                    Print(s1);
                }
            }
            else
            {
                try
                {
                    time = int.Parse(print_time.Text);
                    position = textBox1.Text.ToString();
                    for (int a = 0; a < time; a = a + 1)
                    {
                        int b = a + 1;
                        StringBuilder sb = new StringBuilder();
                        sb.Append("         1  \n");
                        sb.Append("************************************\n");
                        sb.Append("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
                        sb.Append("当前排号：" + b + " 号\n");
                        sb.Append("本小票仅当次有效，过号无效。\n");
                        sb.Append("当前门店：" + "1 " + position + " 店\n");
                        sb.Append("离开排队队伍本小票无效。\n");
                        sb.Append("************************************\n");
                        string s1 = sb.ToString();
                        Print(s1);
                    }
                }
                catch (Exception ec) { MessageBox.Show(ec.Message); }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a;
            if (checkBox1.Checked == true)
            {
                nextnbr = int.Parse(next_nbr.Text);
                time = int.Parse(print_time.Text);
                position = textBox1.Text.ToString();
            }
            else
            {
                try
                {
                    time = int.Parse(print_time.Text);
                    position = textBox1.Text.ToString();
                    nextnbr = 0;
                }
                catch (Exception ec) { MessageBox.Show(ec.Message); }
            }
            a = nextnbr + 1;
            StringBuilder sb = new StringBuilder();
            sb.Append("         1  \n");
            sb.Append("************************************\n");
            sb.Append("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
            sb.Append("当前排号：" + a + " 号\n");
            sb.Append("当前门店：" + "1 " + position + " 店\n");
            sb.Append("本小票仅当次有效，过号无效。\n");
            sb.Append("离开排队队伍本小票无效。\n");
            sb.Append("************************************\n");
            string s1 = sb.ToString();
            MessageBox.Show(s1);
        }

        private void nbr(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void thenbr(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
    }
}
