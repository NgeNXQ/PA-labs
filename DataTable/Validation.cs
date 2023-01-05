using System;
using System.Windows.Forms;

namespace DataTable
{
    internal static class Validation
    {
        public static bool ValidateKeyInput(RichTextBox richTextBox, int length, out int result)
        {
            result = -1;

            if (richTextBox.Text.Length == 0)
            {
                MessageBox.Show($"Enter the key.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (richTextBox.Text.Length > length)
            {
                MessageBox.Show($"The Key has to be less or equal to {length} characters.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Int32.TryParse(richTextBox.Text, out result))
            {
                MessageBox.Show($"The field must contain only digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                richTextBox.Text = String.Empty;
                return false;
            }

            if (result < 1)
            {
                MessageBox.Show($"The key has to be greater than 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public static bool ValidateValueInput(RichTextBox richTextBox, int length)
        {
            if(richTextBox.Text.Length == 0)
            {
                MessageBox.Show($"Enter the value that you want to store.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (richTextBox.Text.Length > length)
            {
                MessageBox.Show($"Maximum amount of characters is {length}.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            return true;
        }
    }
}