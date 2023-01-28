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
using System.Text.RegularExpressions;

namespace TextConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "SRT Files|*.srt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    string textWithoutTimestamps = RemoveTimestamps(file);
                    SaveFile(textWithoutTimestamps, file);
                }
            }
        }

        private string RemoveTimestamps(string file)
        {
            string text = File.ReadAllText(file);
            string textWithoutTimestamps = Regex.Replace(text, @"(\d{2}:\d{2}:\d{2}[,.]\d{3}\s-->\s\d{2}:\d{2}:\d{2}[,.]\d{3}\s)", "");
            return textWithoutTimestamps;
        }

        private void SaveFile(string text, string file)
        {
            string newFile = Path.ChangeExtension(file, ".txt");
            File.WriteAllText(newFile, text);
        }
    }
}
