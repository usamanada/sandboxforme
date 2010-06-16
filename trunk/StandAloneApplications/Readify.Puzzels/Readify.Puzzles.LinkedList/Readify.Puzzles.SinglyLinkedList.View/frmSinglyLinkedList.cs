using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Readify.Puzzles.SinglyLinkedList.View
{
    public partial class frmSinglyLinkedList : Form
    {
        private SinglyLinkedList<int> sList = new SinglyLinkedList<int>();

        public frmSinglyLinkedList()
        {
            InitializeComponent();
        }

        private void btnAddToList_Click(object sender, EventArgs e)
        {
            int result;
            if (Int32.TryParse(txtAddToList.Text, out result))
            {
                sList.AddNode(result);
            }
            else
            {
                MessageBox.Show("Invalid value entered.");
            }
        }

        private void btnFindFromTails_Click(object sender, EventArgs e)
        {
            try
            {
                int result;
                if (Int32.TryParse(txtFindFromTail.Text, out result))
                {
                    Node<int> node = sList.FindNodeFromtTail(result);
                    MessageBox.Show(String.Format("Value returned: {0}", node.NodeValue));
                }
                else
                {
                    MessageBox.Show("Invalid value entered.");
                }
                
            }
            catch (System.ArgumentException exArg)
            {
                MessageBox.Show(exArg.Message);
            }
        }
    }
}
