using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace unisender
{
    internal class TxtFile
    {
        public async void SaveId(string id, string dir)
        {
            string path = System.IO.Path.Combine(dir, "note1.txt");
            string text = id;

            // полная перезапись файла 
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                await writer.WriteLineAsync(text);
            }
        }

        public async Task<string> GetId(string dir)
        {
            string result = string.Empty;
            string path = System.IO.Path.Combine(dir, "note1.txt");
            if (!File.Exists(path)) { return result; }
            // асинхронное чтение
            using (StreamReader reader = new StreamReader(path))
            {
                string text = await reader.ReadToEndAsync();
                result = text;
            }
            return result;
        }

        public void CsvToXml(string inputfile, string outputfile)
        {
            var header = File.ReadLines(inputfile).First().Split(','); //Read header
            var lines = File.ReadAllLines(inputfile).Skip(1); //Skip header row        

            //format the xml how you want
            var xml = new XElement("EmpDetails",
                lines.Select(line => new XElement("Row",
                    line.Split(',').Select((column, index) => new XElement(header[index], column.Replace("\"",string.Empty))))));

            xml.Save(outputfile); //output xml file
        }

        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable("tbResult");
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i].Replace("\"", string.Empty);
                    }
                    dt.Rows.Add(dr);
                }

            }


            return dt;
        }
    }
}
