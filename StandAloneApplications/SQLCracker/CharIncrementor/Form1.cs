using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CharIncrementor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            size = 4;
            Word = new char[size];
            postionArray = new int[size];
            charactersLength = characters.Length;
        }
        int size;
        string characters = "0123456789";
        int charactersLength;
        int[] postionArray;
        char[] Word;
        int MissingPasswords = 1;
        bool BruteForceCheckComplete = false;

        private void button1_Click(object sender, EventArgs e)
        {
            while(MissingPasswords == 0 || !BruteForceCheckComplete)
            {
                foreach (char c in characters)
                {
                    Console.WriteLine(Word);                    
                }
                incrementNext();
            }
        }
        private void incrementNext()
        {
            for (int i = 1; i < size; i++)
            {
                if (postionArray[i] == charactersLength)
                {
                    if(i == size -1)
                    {
                        BruteForceCheckComplete = true;
                    }
                    postionArray[i] = 0;
                    Word[i] = characters[0];
                    continue;
                }
                Word[i] = characters[postionArray[i]];
                postionArray[i]++;
                break;

            }
        }
    }
}