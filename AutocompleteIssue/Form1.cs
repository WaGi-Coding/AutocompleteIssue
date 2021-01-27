using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/*
 * I want the user being able to edit the command after he clicked it in the suggestion-dropdown,
 * instead of instantly sending it. So he always has to confirm with a press on enter to send,
 * after suggestion is put in the input textbox. I've tried it with a boolean,
 * but then he has to press enter twice for non-suggested commands.
*/
namespace AutocompleteIssue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += textBox1.Text + Environment.NewLine;
            textBox1.Text = string.Empty;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                e.SuppressKeyPress = true;
                textBox1.SelectAll();
            }

            // This is also the case if selecting a suggestion from autocomplete dropdown. I won't send instantly, but i want to keep pressing Enter to send in the end
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                // This is not what i want
                //if (textBox1.AutoCompleteCustomSource.Contains(textBox1.Text) && textBox1.Text.EndsWith(" "))
                //{
                //    textBox1.SelectionStart = textBox1.Text.Length;
                //    return;
                //}

                button1_Click(sender, null);
            }
        }



        protected override bool ProcessTabKey(bool forward)
        {
            Control ctl = this.ActiveControl;
            if (ctl != null && ctl == textBox1 && !string.IsNullOrEmpty(textBox1.Text))
            {
                TextBox tb = (TextBox)ctl;
                tb.Focus();
                tb.SelectionStart = tb.Text.Length;
                return true;
            }
            return base.ProcessTabKey(forward); // process TAB key as normal
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
        }
    }


}
