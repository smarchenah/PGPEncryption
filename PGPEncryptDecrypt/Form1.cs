using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGPArticle.Services;
using PGPArticle.Contracts;

namespace PGPEncryptDecrypt
{
    public partial class Form1 : Form
    {
        IEncryptionService encryptionService = new EncryptionService(@"C:\Program Files (x86)\GNU\GnuPG\pub\gpg2.exe");
        string _FileNameToEncript = "";
        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// ENCRYPT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {            
            openFileDialog1.InitialDirectory = @"C:\Users\marchena.INCAS\Desktop\Sergio\Dokumenten\VoiceCleaner";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _FileNameToEncript = openFileDialog1.FileName;
                //var encryptedFile = encryptionService.EncryptFile("73047DAF", _FileNameToEncript, _FileNameToEncript + ".pgp");
                var encryptedFile = encryptionService.EncryptFile("7C041649", _FileNameToEncript, _FileNameToEncript + ".pgp");
                lblInfo.Text = "The File " + openFileDialog1.SafeFileName + " has been successfully encrypted.";
                txtInfo.Text = _FileNameToEncript + ".gpg";
            }            
        }


        /// <summary>
        /// DECRYPT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {            
            openFileDialog1.InitialDirectory = @"C:\Users\marchena.INCAS\Desktop\Sergio\Dokumenten\VoiceCleaner";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _FileNameToEncript = openFileDialog1.FileName;
                var encryptedFile = encryptionService.DecryptFile(_FileNameToEncript, _FileNameToEncript.Substring(0, _FileNameToEncript.Length - 4));
                lblInfo.Text = "The File " + openFileDialog1.SafeFileName + " has been successfully decrypted.";
                txtInfo.Text = _FileNameToEncript.Substring(0, _FileNameToEncript.Length - 4);
            }            
        }
    }
}
