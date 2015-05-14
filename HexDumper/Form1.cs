using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace HexDumper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "*.* | *.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                StringBuilder str = DumpHex(op.FileName);
                saveData(ref str);
            }
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            StringBuilder str = DumpHex(string.Join("", filePath));
            saveData(ref str);
        }

        // saveFile
        private void saveData(ref StringBuilder str)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "*.* | *.*";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(sf.FileName, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(str.ToString());
                    }
                }
            }
        }
        // ouput format
        private StringBuilder DumpHex(string filePath)
        {
            StringBuilder HexStr = new StringBuilder("");
            using (Stream stream = new FileStream(filePath, FileMode.Open))
            {
                int bin;
                // textBox1.Text is Separator, b.ToString("X") is Hex


                while ((bin = stream.ReadByte()) != -1)
                    HexStr.Append(textBox1.Text + bin.ToString("X"));
            }
            return HexStr;
        }


    }
}
