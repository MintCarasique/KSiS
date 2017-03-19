using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reverseClient
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        public void checkTextFields()
        {
            if (inputTextBox.Text == "")
            {
                reverseButton.Enabled = false;
            }
            else
            {
                reverseButton.Enabled = true;
            }
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Clear();
            outputTextBox.Clear();
            checkTextFields();
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            checkTextFields();
        }
    }
}
