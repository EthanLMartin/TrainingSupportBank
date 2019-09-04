using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class CSVParserClass
    {
        public List<List<string>> ParseFile(string directory)
        {
            string[] lines = System.IO.File.ReadAllLines(directory);
            List<List<string>> parsedFile = new List<List<string>>();

            foreach (string line in lines)
            {
                parsedFile.Add(line.Split(',').ToList());
            }
            return parsedFile;
        }
    }
}
