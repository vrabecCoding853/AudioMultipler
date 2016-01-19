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
using NAudio.Wave;

namespace AudioMultiper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var fileStream = File.Create("D:\\test\\test.mp3");
            string inputFile = "D:\\test\\a.mp3";
            Combine(inputFile,fileStream);
            fileStream.Close();
        }
        public static void Combine(string inputFile, Stream output)
        {
            for (int i = 0; i < 148; i++)
            {
                Mp3FileReader reader = new Mp3FileReader(inputFile);
                if ((output.Position == 0) && (reader.Id3v2Tag != null))
                {
                    output.Write(reader.Id3v2Tag.RawData,
                                 0,
                                 reader.Id3v2Tag.RawData.Length);
                }
                Mp3Frame frame;
                while ((frame = reader.ReadNextFrame()) != null)
                {
                    output.Write(frame.RawData, 0, frame.RawData.Length);
                }
            }
        }
    }
}

