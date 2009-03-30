using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace SQLCracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void RunBt_Click(object sender, EventArgs e)
        {
            HashAlgorithm hash;
            hash = new SHA1Managed();
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(PassWordTxt.Text);
            
            byte[] plainTextPlusSaltedBytes = new byte[plainTextBytes.Length + 4];
            for(int index = 0; index < plainTextBytes.Length; index++)
            {
                plainTextPlusSaltedBytes.SetValue(plainTextBytes[index], index);
            }
            for (int index = 0; index < 4; index++)
            {
             //   plainTextPlusSaltedBytes[plainTextBytes.Length + index] = salt[index];
            }
            short SaltValue1 = Convert.ToSByte(SaltTxt.Text.Substring(0,2), 16);
            short SaltValue2 = Convert.ToSByte(SaltTxt.Text.Substring(2, 2), 16);
            short SaltValue3 = Convert.ToSByte(SaltTxt.Text.Substring(4, 2), 16);
            short SaltValue4 = Convert.ToSByte(SaltTxt.Text.Substring(6, 2), 16);
            plainTextPlusSaltedBytes[plainTextBytes.Length] = (byte)SaltValue1;
            plainTextPlusSaltedBytes[plainTextBytes.Length + 1] = (byte)SaltValue2;
            plainTextPlusSaltedBytes[plainTextBytes.Length + 2] = (byte)SaltValue3;
            plainTextPlusSaltedBytes[plainTextBytes.Length + 3] = (byte)SaltValue4;
            

            //plainTextPlusSaltedBytes[plainTextBytes.Length]
            byte[] result = hash.ComputeHash(plainTextPlusSaltedBytes);
            StringBuilder sbResult = new StringBuilder();
            for(int index = 0; index < result.Length; index++)
            {
               sbResult.Append( Convert.ToString( (short)result[index],16).PadLeft(2,'0'));
            }

            ResultTxt.Text = sbResult.ToString().ToUpper();

            byte[] UpperHash = new byte[20];
            for (int index = 0; index < 20; index++)
            {               
                UpperHash[index] = Convert.ToByte(UpperHashTxt.Text.Substring(index * 2, 2), 16);
            }

            bool Match = true;
            for (int index = 0; index < 20; index++)
            {
                if(result[index] != UpperHash[index])
                {
                    Match = false;
                }
            }
            if (Match)
            {
                PassTxt.Text = "passed";
            }
        }
    }
}