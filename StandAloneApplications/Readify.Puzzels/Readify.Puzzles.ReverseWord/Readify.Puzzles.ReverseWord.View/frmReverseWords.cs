using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Readify.Puzzles.ReverseWord.View
{
    public partial class frmReverseWords : Form
    {
        public frmReverseWords()
        {
            InitializeComponent();
        }

        private void btnReverseWord_Click(object sender, EventArgs e)
        {
            txtReverseWordsResult.Text = StringHelper.ReverseWords(txtReverseWords.Text);
        }
    }
}
