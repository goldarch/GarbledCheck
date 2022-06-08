using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GarbledCheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string cResource = richTextBox1.Text; //乱码字符
            var sb = new StringBuilder();
            sb.AppendLine("DisplayName | Name | CodePage");
            sb.AppendLine("====================================");
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                //Byte[] mybyte = System.Text.Encoding.GetEncoding(ei.CodePage).GetBytes(cTxt);
                //sb.Append(ei.Name + "(" + ei.CodePage + "):" + System.Text.Encoding.GetEncoding("gb2312").GetString(mybyte, 0, mybyte.Length) + "\r\n");
                sb.AppendLine($"{ei.DisplayName} | {ei.Name} | {ei.CodePage}");
            }
            //Console.WriteLine(sb.ToString());

            richTextBox2.Text = sb.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var resourceString = richTextBox1.Text;

            var encodings = Encoding.GetEncodings();
            var sb = new StringBuilder();

            foreach (EncodingInfo encodingInfo01 in encodings)
            {
                foreach (EncodingInfo encodingInfo02 in encodings)
                {
                    //相同的
                    if (encodingInfo01.Name == encodingInfo02.Name)
                    {
                        continue;
                    }

                    sb.AppendLine("-----------------------------------");
                    sb.AppendLine($"猜测当前编码为：{encodingInfo01.DisplayName}|{encodingInfo01.CodePage}  尝试转换为：{encodingInfo02.DisplayName}|{encodingInfo02.CodePage}");
                    sb.AppendLine("-----------------------------------");

                    var mybyte = System.Text.Encoding.GetEncoding(encodingInfo01.CodePage).GetBytes(resourceString);
                    var resultString = System.Text.Encoding.GetEncoding(encodingInfo02.CodePage)
                        .GetString(mybyte, 0, mybyte.Length);

                    sb.AppendLine(resultString);
                    sb.AppendLine();
                }
            }

            richTextBox2.AppendText(sb.ToString());
        }
    }
}