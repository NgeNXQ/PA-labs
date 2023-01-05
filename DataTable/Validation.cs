using System;
using System.Windows.Forms;

namespace DataTable
{
    internal static class Validation
    {
        public static bool ValidateKeyInput(RichTextBox richTextBox, out int result)
        {
            result = -1;

            if (richTextBox.Text.Length == 0)
            {
                MessageBox.Show($"Enter the Key.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (richTextBox.Text.Length > IndexRecord.MAX_KEY_LENGTH)
            {
                MessageBox.Show($"The Key has to be less or equal to {IndexRecord.MAX_KEY_LENGTH} characters.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Int32.TryParse(richTextBox.Text, out result))
            {
                MessageBox.Show($"The field must contain only digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                richTextBox.Text = String.Empty;
            }

            if (result == 0)
            {
                MessageBox.Show($"The key has to be greater than 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public static bool ValidateValueInput(RichTextBox richTextBox, int length) => richTextBox.Text.Length <= length;
    }
}