using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TableListCommands
{
    public partial class ListCSVGrid : Window
    {
        public String csvFileContent = string.Empty;
        public String newContent = string.Empty;
        
        public DataTable table = new DataTable();
        
        public ListCSVGrid()
        {
            InitializeComponent();
            
        }
        private void GetLatestData()
        {
            List<String> rows = new List<string>();

            for (int i = 0; i < gridCSVContent.Items.Count; i++)
            {
                string singleRow = String.Join(",", ((DataRowView)gridCSVContent.Items[i]).Row.ItemArray);
                rows.Add(singleRow);
            }
            
            rows = rows.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            newContent = String.Join(Environment.NewLine, rows);
        }

        private void frmListCSVGrid_Loaded(object sender, RoutedEventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("Item", typeof(string));
            String[] items = csvFileContent.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < items.Length; i++)
            {
                DataRow row = table.NewRow();
                row.ItemArray = new object[] { items[i].Trim() };
                table.Rows.Add(row);
            }
            gridCSVContent.ItemsSource = table.DefaultView;
            gridCSVContent.AutoGenerateColumns = true;
            gridCSVContent.SelectionMode = DataGridSelectionMode.Extended;
            gridCSVContent.SelectionUnit = DataGridSelectionUnit.CellOrRowHeader;
            gridCSVContent.CanUserAddRows = false;
            gridCSVContent.CanUserResizeRows = false;
            gridCSVContent.CanUserReorderColumns = false;
            if (gridCSVContent.Items.Count > 0)
            {
                gridCSVContent.Focus();
                gridCSVContent.SelectedItem = gridCSVContent.Items[0];
            }
        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = (gridCSVContent.SelectedItems.Count - 1); i >= 0; i--)
                {
                    try
                    {
                        DataRowView row = (DataRowView)gridCSVContent.SelectedItems[i];
                        row.Row.Delete();
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Row Delete Error: " + ex.Message);
                throw;
            }
        }

        private void frmTableCSVGrid_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GetLatestData();
        }

        private void gridCSVContent_LoadingRow(object sender, DataGridRowEventArgs e)
        {
           //e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void gridCSVContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridCSVContent.SelectedIndex > -1)
            {
                lblAt.Content = String.Format("At {0}", gridCSVContent.SelectedIndex);
                lblAtReplaceLbl.Content = String.Format("At {0}", gridCSVContent.SelectedIndex);
            }
            else 
            {
                lblAt.Content = "At ";
                lblAtReplaceLbl.Content = "At ";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Insert
            if (txtItem.Text.Trim().Length > 0)
            {
                if (gridCSVContent.SelectedIndex > -1)
                {
                    DataRow row = table.NewRow();
                    row.ItemArray = new object[] { txtItem.Text.Trim() };
                    table.Rows.InsertAt(row, gridCSVContent.SelectedIndex);
                    gridCSVContent.Focus();
                    if (gridCSVContent.Items.Count > 0)
                    {
                        gridCSVContent.SelectedItem = gridCSVContent.Items[0];
                    }
                    txtItem.Text = string.Empty;
                }
            }
        }

        private void btnReplaceItem_Click(object sender, RoutedEventArgs e)
        {
            //replace
            if (txtReplaceItem.Text.Trim().Length > 0)
            {
                if (gridCSVContent.SelectedIndex > -1)
                {
                    if (gridCSVContent.SelectedItem != null) 
                    {
                        ((DataRowView)gridCSVContent.SelectedItem).Row.ItemArray = new object[] { txtReplaceItem.Text.Trim() };
                    }                   
                    if (gridCSVContent.Items.Count > 0)
                    {
                        gridCSVContent.SelectedItem = gridCSVContent.Items[0];
                        gridCSVContent.Focus();
                    }
                    txtReplaceItem.Text = string.Empty;
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
