using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TreeViewCheckBoxRootLevel
{
    public partial class Form1 : Form
    {
        private TreeView myTreeView;

        // Create a Font object for the node tags.
        Font tagFont = new Font("Helvetica", 8, FontStyle.Bold);
        public Form1()
        {
            InitializeComponent();
            // Create and initialize the TreeView control.
            myTreeView = new TreeView();
            //myTreeView.Dock = DockStyle.Fill;
            myTreeView.ClientSize = new Size(292, 273);
            myTreeView.CheckBoxes = true;

            // Add nodes to the TreeView control.
            TreeNode node;
            for (int x = 1; x < 4; ++x)
            {
                // Add a root node to the TreeView control.
                node = myTreeView.Nodes.Add(String.Format("Task {0}", x));
                for (int y = 1; y < 4; ++y)
                {
                    // Add a child node to the root node.
                    node.Nodes.Add(String.Format("Subtask {0}", y));
                }
            }
            myTreeView.ExpandAll();

            // Add tags containing alert messages to a few nodes 
            // and set the node background color to highlight them.
            myTreeView.Nodes[1].Nodes[0].Tag = "urgent!";
            myTreeView.Nodes[1].Nodes[0].BackColor = Color.Yellow;
            myTreeView.SelectedNode = myTreeView.Nodes[1].Nodes[0];
            myTreeView.Nodes[2].Nodes[1].Tag = "urgent!";
            myTreeView.Nodes[2].Nodes[1].BackColor = Color.Yellow;

            // Configure the TreeView control for owner-draw and add
            // a handler for the DrawNode event.
            
            myTreeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
            myTreeView.DrawNode += new DrawTreeNodeEventHandler(myTreeView_DrawNode);
            myTreeView.BeforeCheck += new TreeViewCancelEventHandler(myTreeView_BeforeCheck);

            // Add a handler for the MouseDown event so that a node can be 
            // selected by clicking the tag text as well as the node text.
            myTreeView.MouseDown += new MouseEventHandler(myTreeView_MouseDown);

            // Initialize the form and add the TreeView control to it.

            this.Controls.Add(myTreeView);
        }

        void myTreeView_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            Console.WriteLine("Checked");
        }


        private void myTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            
                if (e.Node.Level == 0)
                {
                    e.DrawDefault = true;
                }
                else
                {
                    if ((e.State & TreeNodeStates.Selected) != 0)
                    {
                        Rectangle bounds = e.Node.Bounds;
                        bounds.X = bounds.X - 14;
                        e.Graphics.FillRectangle(SystemBrushes.ActiveCaption, bounds);                        

                        Font nodeFont = e.Node.NodeFont;
                        if (nodeFont == null)
                        {
                            nodeFont = ((TreeView)sender).Font;
                        }

                        e.Graphics.DrawString(e.Node.Text, nodeFont, SystemBrushes.ActiveCaptionText,
                            Rectangle.Inflate(NodeBounds(e.Node), 0, 0));
                        
                        
                        
                        Rectangle boundsBack = e.Node.Bounds;
                        boundsBack.X = e.Node.Bounds.X + e.Node.Bounds.Width - 17;
                        
                        e.Graphics.FillRectangle(SystemBrushes.Window, boundsBack);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(SystemBrushes.Window, NodeBounds(e.Node));

                        Font nodeFont = e.Node.NodeFont;
                        if (nodeFont == null)
                        {
                            nodeFont = ((TreeView)sender).Font;
                        }

                        e.Graphics.DrawString(e.Node.Text, nodeFont, SystemBrushes.WindowText,
                            Rectangle.Inflate(NodeBounds(e.Node), 0, 0));
                    }
            
                }
            
        }

    // Selects a node that is clicked on its label or tag text.
    private void myTreeView_MouseDown(object sender, MouseEventArgs e)
    {
        TreeNode clickedNode = myTreeView.GetNodeAt(e.X, e.Y);
        if (NodeBounds(clickedNode).Contains(e.X, e.Y))
        {
            myTreeView.SelectedNode = clickedNode;
        }
    }

    // Returns the bounds of the specified node, including the region 
    // occupied by the node label and any node tag displayed.
    private Rectangle NodeBounds(TreeNode node)
    {
        // Set the return value to the normal node bounds.
        Rectangle bounds = node.Bounds;
        bounds.X = bounds.X - 14;

        return bounds;

    }

    }
}