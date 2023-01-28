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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "SRT files|*.srt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = string.Join("; ", openFileDialog.FileNames);
            }
        }
              

        private void btnSave_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] files = txtSource.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string output = "";

                foreach (string file in files)
                {
                    string[] lines = File.ReadAllLines(file);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        // remove timestamp and "-->"
                        lines[i] = Regex.Replace(lines[i], @"\d{2}:\d{2}:\d{2},\d{3} --> \d{2}:\d{2}:\d{2},\d{3}", "");
                        // remove numbering
                        lines[i] = Regex.Replace(lines[i], @"^\d+$", "");
                        // remove whitespace
                        lines[i] = lines[i].Trim();
                    }
                    output += string.Join("\r\n", lines.Where(x => !string.IsNullOrWhiteSpace(x)));
                    output += "\r\n";
                }
                File.WriteAllText(saveFileDialog.FileName, output);
                MessageBox.Show("File saved successfully!");
            }

        }
    }
}
