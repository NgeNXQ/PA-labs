using System;
using System.Windows.Forms;

namespace DataTable
{
    public partial class Form3 : Form
    {
        private Action operation;

        public Form3(Action choice)
        {
            operation = choice;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validation.ValidateKeyInput(KeyInput, out int key))
            {
                if (Validation.ValidateValueInput(ValueInput, MainRecord.MAX_VALUE_LENGTH))
                {
                    switch (operation)
                    {
                        case Action.Editing:
                            if (MainForm.ExternalInterface.DataTable.Edit(key, ValueInput.Text))
                                MainForm.ExternalInterface.UpdateTables();
                            else
                                MessageBox.Show($"Selected Key does not exist.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                        case Action.Adding:
                            if (MainForm.ExternalInterface.DataTable.Add(key, ValueInput.Text))
                                MainForm.ExternalInterface.UpdateTables();
                            else
                                MessageBox.Show($"Selected Key is already exists.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }
            }
        }      
    }
}