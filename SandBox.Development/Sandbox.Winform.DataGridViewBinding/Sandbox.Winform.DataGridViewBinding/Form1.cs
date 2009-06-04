using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sandbox.Winform.DataGridViewBinding
{
    public partial class Form1 : Form
    {
        List<Party> lp = new List<Party>();
        private BindingList<State> _bindListStates = new BindingList<State>();
        private List<State> _states = new List<State>();
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn statename;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDataTableBind_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            
            DataTable dt = new System.Data.DataTable("Main");
			dt.Columns.Add( "ID", typeof(int));
			dt.Columns.Add( "State", typeof(string));

            object[] rowvals = new object[2];

            rowvals[0] = 1;
            rowvals[1] = "Vic";
            dt.Rows.Add(rowvals);

            rowvals[0] = 2;
            rowvals[1] = "NSW";
            dt.Rows.Add(rowvals);

            rowvals[0] = 3;
            rowvals[1] = "QLD";
            dt.Rows.Add(rowvals);
            dt.AcceptChanges();

            dataGridView1.DataSource = dt;
        }
        private void btnCustomListBind_Click(object sender, EventArgs e)
        {
            _states.Add(new State() { id = 1, statename = "NSW" });
            _states.Add(new State() { id = 2, statename = "VIC" });
            _states.Add(new State() { id = 3, statename = "QLD" });


            dataGridView1.DataSource = _states;
        }


        private void btnRemoveUsingNull_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            State s = _states[2];
            _states.Remove(s);

            dataGridView1.DataSource = _states;
        }


        private void btnCustomBindingList_Click(object sender, EventArgs e)
        {
            _bindListStates.Add(new State() { id = 1, statename = "NSW" });
            _bindListStates.Add(new State() { id = 2, statename = "VIC" });
            _bindListStates.Add(new State() { id = 3, statename = "QLD\nBrisbane" });

            dataGridView1.DataSource = _bindListStates;
        }

        private void btnRemoveNormal_Click(object sender, EventArgs e)
        {
            State s = _bindListStates[2];
            _bindListStates.Remove(s);
        }

        private void btnMultiLine_Click(object sender, EventArgs e)
        {
            //Added Columns
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();

            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statename = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // 
            // Id
            // 
            this.Id.DataPropertyName = "id";
            this.Id.HeaderText = "id";
            this.Id.Name = "Id";
            // 
            // statename
            // 
            this.statename.DataPropertyName = "statename";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.statename.DefaultCellStyle = dataGridViewCellStyle1;
            this.statename.HeaderText = "statename";
            this.statename.Name = "statename";

            dataGridView1.AutoGenerateColumns = false;

            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.statename});

            dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);


            //Create Data
            _states.Add(new State() { id = 1, statename = "NSW" });
            _states.Add(new State() { id = 2, statename = "VIC" });
            _states.Add(new State() { id = 3, statename = "QLD" + Environment.NewLine + "Brisbane" });

            

            dataGridView1.DataSource = _states;
            
        }

        void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dataGridView1.AutoResizeRow(e.RowIndex);
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

            dataGridView2.DataSource = lp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearGrid();
            dataGridView2.Columns.Add("ID", "Product ID");
            dataGridView2.Columns.Add("Name", "Product Name");
            dataGridView2.Columns.Add("Description", "Description");
            dataGridView2.Columns.Add("Price", "Price");

            BindingSource bindingsource = new BindingSource();

            bindingsource.Add("Type A");
            bindingsource.Add("Type B");
            bindingsource.Add("Type C");

            DataGridViewComboBoxColumn comboBoxCol = new DataGridViewComboBoxColumn();
            comboBoxCol.HeaderText = "Types";
            comboBoxCol.DataSource = bindingsource;
            dataGridView2.Columns.Add(comboBoxCol);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearGrid();
            dataGridView2.AutoGenerateColumns = false;

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
            dataGridView2.Columns.Add(colText);

            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            col.DataPropertyName = "Color";
            col.HeaderText = "Color";
            col.Name = "Color";
            col.DisplayMember = "Name";
            col.ValueMember = "Self";
            col.DataSource = colors;

            dataGridView2.Columns.Add(col);

            BindingList<Car> blCar = new BindingList<Car>();

            Car c = new Car();
            c.Name = "Mazda";
            c.Color = colors[0];
            blCar.Add(c);

            dataGridView2.DataSource = blCar;
        }
        private void clearGrid()
        {
            dataGridView2.DataSource = null;
            dataGridView2.Columns.Clear();
        }
    }
}
