using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SwiftEst00
{
    class CostCodeControl
    {
        static string procoreString = "Procore";
        static string csi2018String = "CSI2018";
        static string nahbString = "NAHB";

        public static Dictionary<string, string> buildStandardsDic(string filePath)
        {
            //Read CSV file, build dictinoary from it.
            COMCSVReader reader = new COMCSVReader();            
            List<List<string>> csvData = reader.getData(filePath);

            Dictionary<string, string> standardsDic = new Dictionary<string, string>();
            foreach (List<string> line in csvData)
            {
                standardsDic.Add(line[1], line[0]);
            }
            return standardsDic;
        }

        public static Dictionary<string, int> buildColCountDic(string filePath)
        {
            //Read CSV file, build dictinoary from it.
            COMCSVReader reader = new COMCSVReader();
            List<List<string>> csvData = reader.getData(filePath);

            Dictionary<string, int> standardsDic = new Dictionary<string, int>();
            foreach (List<string> line in csvData)
            {
                standardsDic.Add(line[0], Convert.ToInt32(line[1]));
            }
            return standardsDic;
        }

        public static CostCode getCostCodeFromText(string divisionText, string subDivisionText, string codeText, string codeDescription)
        {
            CostCode codeData = new CostCode();
            
            if (string.IsNullOrEmpty(divisionText))
            {
                divisionText = "";
            }
            if (string.IsNullOrEmpty(subDivisionText))
            {
                subDivisionText = "";
            }
            if (string.IsNullOrEmpty(codeText))
            {
                codeText = "";
            }

            codeData.code = divisionText + subDivisionText + codeText;
          
            codeData.codeDescription = codeDescription;

            return codeData;
        }

        public static List<CostCode> getCostCodesByHeader (List<List<string>> csvData, string header, Dictionary<string, string> standardsDic)
        {
            List<CostCode> costCodes = new List<CostCode>();
            string standard = standardsDic[header];
            if(standard == procoreString)
            {
                costCodes = getCostCodesFromProcore(csvData);
            }
            else if(standard == csi2018String)
            {
                costCodes = getCostCodesFromCSI2018(csvData);
            }
            else if (standard == nahbString)
            {
                costCodes = getCostCodesFromNAHB(csvData);
            }
            else
            {
                //custrom format method call here.
            }
            return costCodes;
        }

        public static List<CostCode> getCostCodesFromCSV(string filePath, Dictionary<string, string> standardsDic)
        {
            COMCSVReader reader = new COMCSVReader();
            List<CostCode> costCodes = new List<CostCode>();
            List<List<string>> csvData = reader.getData(filePath);

            StringBuilder headerBuilder = new StringBuilder();
            for (int i = 0; i < csvData[0].Count; i++)
            {
                headerBuilder.Append(csvData[0][i]);
                if (i < csvData[0].Count -1)
                {
                    headerBuilder.Append(",");
                }
            }
            

            costCodes = getCostCodesByHeader(csvData, headerBuilder.ToString(), standardsDic);
            return costCodes;
        }

        public static string getCostCodeString(CostCode code)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(code.code);            
            builder.Append(", ");
            builder.Append(code.codeDescription);

            return builder.ToString();
        }

        public static List<CostCode> getCostCodesFromExcel(string filePath, Dictionary<string, string> standardsDic, Dictionary<string,int> standardsColDic)
        {
            //Reade the header row in the excel file, then call the correct read from excel function.
            //reqiers memory managment to avoid memory leak with excel
            //don't double dot when using COM (light green) objects such as xlApp.WorkBooks.Open(filePath). must be as seen below.
            //Trade Off: traded readablility for speed. Method could have used an if statment inside of loop to call correct List<string> reader.  
            //  Insted we search header to call method that reads entire exel file.
            //  this elimnates the need for an if statment inside the method. This helps with speed. but hurts readability.

            List<CostCode> codes = new List<CostCode>();

            Application xlApp = new Application();
            Workbooks workBooks = null;
            Workbook workBook = null;
            Sheets sheets = null;
            Worksheet workSheet = null;
            Range range = null;
            Range cols = null;
            string header = "";
            string standard = "";
            try
            {               
                workBooks = xlApp.Workbooks;             
                workBook = workBooks.Open(filePath);           
                sheets = workBook.Worksheets;
                workSheet = sheets[1];
                
                try
                {            
                    range = workSheet.UsedRange;              
                    cols = range.Columns;
                    int cl = cols.Count;
                    //get header
                    StringBuilder hBuilder = new StringBuilder();
                    object[,] cells = (object[,])range.Value2;
                    for (int cCnt = 1; cCnt <= cl; cCnt++)
                    {
                        hBuilder.Append(",");//adds a , inbetween each value and at the start.
                        hBuilder.Append((string)cells[1,cCnt]);

                    }
                    header = hBuilder.ToString().Substring(1);//skip the first, added in loop for delimnation.
                    standard = standardsDic[header];
                
                }
                finally
                {
                    if (cols != null) { Marshal.ReleaseComObject(cols); }
                    if (range != null) { Marshal.ReleaseComObject(range); }
                }
                
                

                if (standard == procoreString)
                {
                    codes = getCostCodesFromProcoreFromExcel(workSheet);
                }
                else if (standard == csi2018String)
                {
                    codes = getCostCodesFromCSI2018FromExcel(workSheet);
                }
                else if (standard == nahbString)
                {
                    codes = getCostCodesFromNAHBFromExcel(workSheet, standardsColDic[nahbString]);
                }
                else
                {
                    //custome format method call here.
                }
                
            }
            finally
            {
                workBook.Close(0);
                workBooks.Close();
                xlApp.Quit();
                //GC.Collect();
                if (workSheet != null) { Marshal.ReleaseComObject(workSheet); }
                if (sheets != null) { Marshal.ReleaseComObject(sheets); }
                if (workBook != null) { Marshal.ReleaseComObject(workBook); }
                if (workBooks != null) { Marshal.ReleaseComObject(workBooks); }
                if (xlApp != null) { Marshal.ReleaseComObject(xlApp); }
            }
            return codes;
        }

        //Procore standard for code
        public static CostCode getCostCodeFromNAHB(List<string> lineData)
        {
            CostCode costCode = new CostCode();

            costCode = getCostCodeFromText(lineData[1], "", "", lineData[2]);

            return costCode;
        }

        public static List<CostCode> getCostCodesFromNAHBFromExcel(Worksheet workSheet, int colCnt)
        {
            Range range = null;
            Range rows = null;
            Range rEnd = null;
            Range cols = null;
            List<CostCode> codes = new List<CostCode>();
            try
            {
                range = workSheet.UsedRange;
                rows = range.Rows;
                cols = range.Columns;
                rEnd = rows.End[XlDirection.xlDown];
                int rw = rEnd.Row;
                int cl = colCnt;
                object[,] cells = (object[,])range.Value2;
                for (int rCnt = 2; rCnt <= rw; rCnt++)
                {
                    List<string> line = new List<string>();
                    for (int cCnt = 1; cCnt <= cl; cCnt++)
                    {
                        line.Add(cells[rCnt, cCnt].ToString());    
                    }                    
                    codes.Add(getCostCodeFromNAHB(line));
                }
            }
            finally
            {
                if (rEnd != null) { Marshal.ReleaseComObject(rEnd); }
                if (cols != null) { Marshal.ReleaseComObject(cols); }
                if (rows != null) { Marshal.ReleaseComObject(rows); }
                if (range != null) { Marshal.ReleaseComObject(range); }
                if (workSheet != null) { Marshal.ReleaseComObject(workSheet); }
            }

            return codes;
        }

        //NAHB standard
        public static List<CostCode> getCostCodesFromNAHB(List<List<string>> csvData)
        {
            List<CostCode> costCodes = new List<CostCode>();

            for (int i = 1; i < csvData.Count; i++)
            {
                List<string> csvLine = csvData[i];
                costCodes.Add(getCostCodeFromNAHB(csvData[i]));
            }

            return costCodes;
        }

        public static CostCode getCostCodeFromCSI2018(List<string> lineData)
        {
            CostCode costCode = new CostCode();

            costCode = getCostCodeFromText(lineData[2], "", "", lineData[3]);

            return costCode;
        }

        public static List<CostCode> getCostCodesFromCSI2018FromExcel(Worksheet workSheet)
        {

            Range range = null;
            Range rows = null;
            Range cols = null;
            List<CostCode> codes = new List<CostCode>();
            try
            {
                
                range = workSheet.UsedRange;
                rows = range.Rows;
                cols = range.Columns;
                int rw = rows.Count;
                int cl = cols.Count;
                object[,] cells = (object[,])range.Value2;
                for (int rCnt = 2; rCnt <= rw; rCnt++)
                {
                    List<string> line = new List<string>();
                    
                    for (int cCnt = 1; cCnt <= cl; cCnt++)
                    {
                        line.Add(cells[rCnt, cCnt].ToString());
                    }
                    codes.Add(getCostCodeFromCSI2018(line));
                }

            }
            finally
            {
                if (cols != null) { Marshal.ReleaseComObject(cols); }
                if (rows != null) { Marshal.ReleaseComObject(rows); }
                if (range != null) { Marshal.ReleaseComObject(range); }
            }
            
            return codes;
        }

        //CSI2018 standard
        public static List<CostCode> getCostCodesFromCSI2018(List<List<string>> csvData)
        {
            List<CostCode> costCodes = new List<CostCode>();

            for (int i = 1; i < csvData.Count; i++)
            {
                List<string> csvLine = csvData[i];
                costCodes.Add(getCostCodeFromCSI2018(csvData[i]));
            }

            return costCodes;
        }

        //Procore standard for code
        public static CostCode getCostCodeFromProcore(List<string> lineData)
        {
            CostCode costCode = new CostCode();

            costCode = getCostCodeFromText(lineData[0], lineData[1], "", lineData[2]);

            return costCode;
        }

        //Procore standard for codS
        public static List<CostCode> getCostCodesFromProcoreFromExcel(Worksheet workSheet)
        {
            Range range = null;
            Range rows = null;
            Range cols = null;
            List<CostCode> codes = new List<CostCode>();
            try
            {
                range = workSheet.UsedRange;
                rows = range.Rows;
                cols = range.Columns;
                int rw = rows.Count;
                int cl = cols.Count;
                object[,] cells = (object[,])range.Value2;
                for (int rCnt = 2; rCnt <= rw; rCnt++)
                {
                    List<string> line = new List<string>();
                    for (int cCnt = 1; cCnt <= cl; cCnt++)
                    {
                        line.Add(cells[rCnt, cCnt].ToString());                     

                    }
                    codes.Add(getCostCodeFromProcore(line));
                }
            }
            finally
            {
                if (cols != null) { Marshal.ReleaseComObject(cols); }
                if (rows != null) { Marshal.ReleaseComObject(rows); }
                if (range != null) { Marshal.ReleaseComObject(range); }
            }

            return codes;
        }

        //Procore standard for codS
        public static List<CostCode> getCostCodesFromProcore(List<List<string>> csvData)
        {
            List<CostCode> costCodes = new List<CostCode>();

            for (int i = 1; i < csvData.Count; i++)
            {
                List<string> csvLine = csvData[i];
                costCodes.Add(getCostCodeFromProcore(csvData[i]));
            }

            return costCodes;
        }
    }
}
