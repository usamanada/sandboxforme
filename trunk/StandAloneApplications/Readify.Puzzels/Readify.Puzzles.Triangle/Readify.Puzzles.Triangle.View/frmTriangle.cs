using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Readify.Puzzles.Triangle.View
{
    public partial class frmTriangle : Form
    {
        public frmTriangle()
        {
            InitializeComponent();
        }

        private void btnCheckTriangleType_Click(object sender, EventArgs e)
        {
            Triangles tri = new Triangles();
            
            int sideA, sideB, sideC;

            if (Int32.TryParse(txtSideA.Text, out sideA) &&
               Int32.TryParse(txtSideB.Text, out sideB) &&
               Int32.TryParse(txtSideC.Text, out sideC))
            {
                TriangleType triType = tri.GetTriangleType(sideA, sideB, sideC);
                if (triType == TriangleType.Error)
                {
                    MessageBox.Show("Invalide triangle sides entered.");
                }
                else
                {
                    MessageBox.Show(String.Format("Triangle type is: {0}", triType.ToString()));
                }
            }
            else
            {
                MessageBox.Show("Invalide triangle sides entered.");
            }
        }
    }
}
