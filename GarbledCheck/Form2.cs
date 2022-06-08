using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GarbledCheck.Domain;

namespace GarbledCheck
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            //objectListView1.SetObjects(new List<ConversionRecord>());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //作为解决中文乱码，只要设定几种中文常见格式就好了！
            String[] charsetArr = {"UTF-8", "GB18030", "GB2312", "GBK", "Windows-1252", "ISO8859-1"};

            var sourceString = richTextBox1.Text;
            var encodings = Encoding.GetEncodings();

            var list = new List<ConversionRecord>();
            //objectListView1.ClearObjects();
            objectListView1.SetObjects(list);

            foreach (EncodingInfo encodingInfo01 in encodings)
            {
                var mybyte = System.Text.Encoding.GetEncoding(encodingInfo01.CodePage).GetBytes(sourceString);

                foreach (string s in charsetArr)
                {
                    var encoding02 = Encoding.GetEncoding(s);
                    //相同的
                    if (encoding02.CodePage == encodingInfo01.CodePage)
                    {
                        continue;
                    }

                    //sb.AppendLine("-----------------------------------");
                    //sb.AppendLine($"猜测当前编码为：{encodingInfo01.DisplayName}|{encodingInfo01.CodePage}  尝试转换为：{encodingInfo02.DisplayName}|{encodingInfo02.CodePage}");
                    //sb.AppendLine("-----------------------------------");

                    //var mybyte = System.Text.Encoding.GetEncoding(encodingInfo01.CodePage).GetBytes(resourceString);
                    //var resultString = System.Text.Encoding.GetEncoding(encodingInfo02.CodePage)
                    //    .GetString(mybyte, 0, mybyte.Length);

                    //sb.AppendLine(resultString);
                    //sb.AppendLine();

                    var record = new ConversionRecord()
                    {
                        //SourceEncodingInfo = $"{encodingInfo01.DisplayName}|{encodingInfo01.CodePage} ",
                        //TargetEncodingInfo = $"{encoding02.EncodingName}|{encoding02.HeaderName}|{encoding02.CodePage}"
                        SourceEncodingInfo = $"{encodingInfo01.Name} ({encodingInfo01.CodePage}) ",
                        TargetEncodingInfo = $"{encoding02.HeaderName} ({encoding02.CodePage})",

                        ConversionString = System.Text.Encoding.GetEncoding(encoding02.CodePage)
                            .GetString(mybyte, 0, mybyte.Length)
                    };

                    list.Add(record);
                }
            }

            objectListView1.SetObjects(list);
        }
    }
}