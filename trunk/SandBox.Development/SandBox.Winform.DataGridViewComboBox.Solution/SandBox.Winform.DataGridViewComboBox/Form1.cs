using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SandBox.Winform.DataGridViewComboBox
{
    public partial class Form1 : Form
    {
        List<Party> lp = new List<Party>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearGrid();
            Party p = new Party();
            p.Name = "User1";
            p.Job = "JobType1";
            lp.Add(p);

            p = new Party();
            p.Name = "User2";
            p.Job = "JobType2";
            lp.Add(p);

            dataGridView1.DataSource = lp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearGrid();
            dataGridView1.Columns.Add("ID", "Product ID");
            dataGridView1.Columns.Add("Name", "Product Name");
            dataGridView1.Columns.Add("Description", "Description");
            dataGridView1.Columns.Add("Price", "Price");

            BindingSource bindingsource = new BindingSource();

            bindingsource.Add("Type A");
            bindingsource.Add("Type B");
            bindingsource.Add("Type C");

            DataGridViewComboBoxColumn comboBoxCol = new DataGridViewComboBoxColumn();
            comboBoxCol.HeaderText = "Types";
            comboBoxCol.DataSource = bindingsource;
            dataGridView1.Columns.Add(comboBoxCol);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearGrid();
            dataGridView1.AutoGenerateColumns = false;

            ColorName[] colors = new ColorName[]
            {
                new ColorName(1,"Red"),              
                new ColorName(2,"Blue"),  
                new ColorName(3,"Green")    
            };

            DataGridViewTextBoxColumn colText = new DataGridViewTextBoxColumn();
            colText.DataPropertyName = "Name";
            colText.HeaderText = "Name";
            colText.Name = "Name";
            dataGridView1.Columns.Add(colText);

            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            col.DataPropertyName = "Color";
            col.HeaderText = "Color";
            col.Name = "Color";
            col.DisplayMember = "Name";
            col.ValueMember = "Self";
            col.DataSource = colors;

            dataGridView1.Columns.Add(col);

            BindingList<Car> blCar = new BindingList<Car>();
            
            Car c = new Car();
            c.Name = "Mazda";
            c.Color = colors[0];
            blCar.Add(c);
            
            dataGridView1.DataSource = blCar;
        }
        private void clearGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
        }
    }
}