using System;
using System.Windows.Forms;

namespace DataTable
{
    public partial class Form2 : Form
    {
        private Action operation;

        public Form2(Action choice)
        {
            operation = choice;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validation.ValidateKeyInput(KeyInput, out int key))
            {
                switch (operation)
                {
                    case Action.Getting:
                        if (MainForm.ExternalInterface.DataTable.Get(key, out string output))
                            MessageBox.Show($"{output}");
                        else
                            MessageBox.Show($"Selected Key does not exist.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                    case Action.Removing:
                        if (MainForm.ExternalInterface.DataTable.Remove(key))
                            MainForm.ExternalInterface.UpdateTables();
                        else
                            MessageBox.Show($"Selected Key does not exist.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }      
        }
    }
}