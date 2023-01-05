using System;
using System.IO;
using System.Windows.Forms;

namespace DataTable
{
    public enum Action : byte
    {
        Getting = 0,
        Adding,
        Removing,
        Editing,
    }

    public partial class MainForm : Form
    {
        private readonly string main = "Files\\main.txt";
        private readonly string index = "Files\\index.txt";
        private readonly string overflow = "Files\\overflow.txt";

        public DataTable DataTable;

        private Form3 adding = new Form3(Action.Adding);
        private Form2 getting = new Form2(Action.Getting);
        private Form3 editing = new Form3(Action.Editing);
        private Form2 removing = new Form2(Action.Removing);

        public static MainForm ExternalInterface { get; private set; }

        public MainForm()
        {
            ExternalInterface = this;
            InitializeComponent();
        }

        private void Load_Click(object sender, EventArgs e)
        {
            DataTable = new DataTable(main, index, overflow);
            UpdateTables();
        }

        private void Add_Click(object sender, EventArgs e) => adding.ShowDialog();

        private void Get_Click(object sender, EventArgs e) => getting.ShowDialog();

        private void Edit_Click(object sender, EventArgs e) => editing.ShowDialog();

        private void Remove_Click(object sender, EventArgs e) => removing.ShowDialog();

        private void Clear_Click(object sender, EventArgs e)
        {
            if (DataTable != null)
            {
                DataTable.Clear();
                UpdateTables();
            }       
            else
                MessageBox.Show($"{nameof(DataTable)} {nameof(DataTable)} is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);    
        }

        public void UpdateTables()
        { 
            mainRecords.DataSource = DataTable.GetMainRecords(main);
            mainRecords.Columns[0].Visible = false;
            indexRecords.DataSource = DataTable.GetIndexRecords(index);
            overflowRecords.DataSource = DataTable.GetIndexRecords(overflow);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("Files"))
            {
                Directory.CreateDirectory("Files");
                File.Create(main).Dispose();
                File.Create(index).Dispose();
                File.Create(overflow).Dispose();
            }
        }
    }
}