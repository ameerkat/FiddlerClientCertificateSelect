using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiddlerClientCertificateSelect
{
    public partial class ClientSelectorSettings : Form
    {
        private DataGridViewColumnCollection columns; 

        public ClientSelectorSettings(DataGridViewColumnCollection columns)
        {
            InitializeComponent();

            this.columns = columns;
            foreach (DataGridViewColumn column in columns)
            {
                if (!column.Visible)
                {
                    this.listBox1.Items.Add(column.Name);
                }
                else
                {
                    this.listBox2.Items.Add(column.Name);
                }
            }
        }

        private DataGridViewColumn GetColumn(string columnName)
        {
            foreach (DataGridViewColumn column in columns)
            {
                if (string.Equals(column.Name, columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return column;
                }
            }

            return null;
        }

        private void RemoveColumnButton_Click(object sender, EventArgs e)
        {
            string columnToRemoveName = (string)listBox2.SelectedItem;
            var column = GetColumn(columnToRemoveName);
            if (column != null)
            {
                // Remove from selected columns
                column.Visible = false;
                listBox2.Items.Remove(columnToRemoveName);

                // Add to unselected
                listBox1.Items.Add(columnToRemoveName);
            }
        }

        private void AddColumnButton_Click(object sender, EventArgs e)
        {
            string columnToAddName = (string)listBox1.SelectedItem;
            var column = GetColumn(columnToAddName);
            if (column != null)
            {
                // Add to selected columns
                column.Visible = true;
                listBox2.Items.Add(columnToAddName);

                // Remove from unselected
                listBox1.Items.Remove(columnToAddName);
            }
        }
    }
}
