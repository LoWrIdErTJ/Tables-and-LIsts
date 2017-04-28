using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableListCommands;

namespace UnitTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TableRowInsert dlg = new TableRowInsert();
            TableColumnInsert dlg = new TableColumnInsert();
            dlg.containsHeader = false;
            dlg.delimiter = ',';
            dlg.csvFileContent = @"h1,h2,h3
            l1,l2,l3
            l4,l5,l6";
            dlg.Show();
            

        }
    }
}
