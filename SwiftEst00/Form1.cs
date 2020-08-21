using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SwiftEst00
{
    public partial class Form1 : Form
    {
        List<CostCode> codes = new List<CostCode>();
        string suportEmail = @"[Suport Email TBD]";
        Dictionary<string, string> standardsDic = new Dictionary<string, string>();
        Dictionary<string, int> standardColDic = new Dictionary<string, int>();
        Stopwatch t1 = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
            standardsDic = CostCodeControl.buildStandardsDic(@"S:\Documents\SwiftEst\standardsDic.csv");
            standardColDic = CostCodeControl.buildColCountDic(@"S:\Documents\SwiftEst\standersColsCnt.csv");
        }

// Event Handlers

        private void browseCodesFileBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            codesImportFileTxt.Text = openFileDialog1.FileName;
        }

        private void importCodesBtn_Click(object sender, EventArgs e)
        {
            codesListBox.Items.Clear();
            string filePath = codesImportFileTxt.Text;
            t1.Start();
            //don't open if file pathis empty
            if (!string.IsNullOrEmpty(filePath))
            {

                if (filePath.EndsWith(".csv"))
                {
                    readFromCSV(filePath);
                }
                else if (filePath.EndsWith("xlsx") || filePath.EndsWith("xls"))
                {
                    readFromExcel(filePath);
                }

                try
                {
                    foreach (CostCode code in codes)
                    {
                        codesListBox.Items.Add(CostCodeControl.getCostCodeString(code));
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Something unexpected went wrong. Please email " + suportEmail + " for help.");
                    codesListBox.Items.Clear();
                }
            }
            else
            {
                MessageBox.Show("Must enter a file path to a csv file.");
            }
            t1.Stop();
            speedTxtBox.Text = t1.Elapsed.ToString();
            t1.Reset();
            

            
            
            
        }
    
// Methods
        private void readFromCSV(string filePath)
        {
            try
            {
                codes = CostCodeControl.getCostCodesFromCSV(filePath, standardsDic);
            }
            catch (FormatException error)
            {
                MessageBox.Show(error.Message);
                //codes = new List<CostCode>();
            }
        }

        private void readFromExcel(string filePath)
        {
            
            try
            {
                codes = CostCodeControl.getCostCodesFromExcel(filePath, standardsDic, standardColDic);
            }
            catch (FormatException error)
            {
                MessageBox.Show(error.Message);
                //codes = new List<CostCode>();
            }
        }

        private void codesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
