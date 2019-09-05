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
        public IParser SelectParser(string directory)
        {
            string extension = Path.GetExtension(directory);
            IParser parser = null;

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
