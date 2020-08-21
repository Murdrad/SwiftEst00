/*
 * COMCSVReader
 * Input file path to CSV
 * Outputs List<List<string>>
 * 
 * CSV item parser based on CSV Reader
 * Decided not to use CSV reader because it used DataTable and type conversion.
 * Type conversion seemed like it was uncesary, and costly
 * And I don't need the output in the form of a data table.
 * I wanted a tool that I could not only use here, but on my other personal projects.
 * My ideal tool would read in CSV files as orgnzied text, thus no data is lost or added.
 * Users may do as they like with the unaltered data.
 * 
 * I used Stellman's boolean logic for parsing CSV items.
 * 
 * 
 * CSVReader Citation.
 * CSVReader - a simple open source C# class library to read CSV data
 * by Andrew Stellman - http://www.stellman-greene.com/CSVReader
 * 
 * CSVReader.cs - Class to read CSV data from a string, file or stream
 * 
 * download the latest version: http://svn.stellman-greene.com/CSVReader
 * 
 * (c) 2008, Stellman & Greene Consulting
 * All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SwiftEst00
{
    class COMCSVReader
    {
        private List<string> getLine(string lineText)
        {
            List<string> lineValues = new List<string>();
            int index = 0;
            while(index <= lineText.Length)
            {
                // Check to see if the next value is quoted
                bool quoted = false;
                string remainingText = lineText.Substring(index);
                if (remainingText.StartsWith("\""))
                {
                    quoted = true;
                }

                // Find the end of the next value
                string nextObjectString = "";
                int i = index;
                int len = lineText.Length;
                bool foundEnd = false;
                while (!foundEnd && i <= len)
                {
                    // Check if we've hit the end of the string
                    if ((!quoted && i == len) // non-quoted strings end with a comma or end of line
                        || (!quoted && lineText.Substring(i, 1) == ",")
                        // quoted strings end with a quote followed by a comma or end of line
                        || (quoted && i == len - 1 && lineText.EndsWith("\""))
                        || (quoted && i == len - 2 && lineText.Substring(i, 2) == "\","))
                    {
                        foundEnd = true;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (quoted)
                {
                    if (i > len || !lineText.Substring(i, 1).StartsWith("\""))
                    {
                        throw new FormatException("Invalid CSV format: " + lineText.Substring(0, i));
                    }
                    i++;
                }
                nextObjectString = lineText.Substring(index, i-index).Replace("\"\"", "\"");
                if (quoted)
                {
                    if (nextObjectString.StartsWith("\""))
                    {
                        nextObjectString = nextObjectString.Substring(1);
                    }
                    if (nextObjectString.EndsWith("\""))
                    {
                        nextObjectString = nextObjectString.Substring(0, nextObjectString.Length - 1);
                    }
                }
                lineValues.Add(nextObjectString);
                index = i+1;
            }
            return lineValues;
        }

        public List<List<string>> getData(string filePath)
        {
            List<List<string>> data = new List<List<string>>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                //ends when we are out of lines to read.
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    data.Add(getLine(line));
                }
            }

            return data;
        }


    }
}
