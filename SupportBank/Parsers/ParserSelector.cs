using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class ParserSelector
    {
        public Parser SelectParser(string directory)
        {
            string extension = Path.GetExtension(directory);
            Parser parser = null;

            switch (extension)
            {
                case ".json":
                    parser = new JSONParser();
                    break;
                case ".csv":
                    parser = new CSVParser();
                    break;
                default:
                    break;
            }

            return parser;
        }
    }
}
