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
            ConsoleInterface consoleInterface = new ConsoleInterface();
            string extension = Path.GetExtension(directory);
            IParser parser = null;

            switch (extension)
            {
                case ".json":
                    parser = CreateParser(UserSelectParser.JSON);
                    break;
                case ".csv":
                    parser = CreateParser(UserSelectParser.CSV);
                    break;
                case ".xml":
                    parser = CreateParser(UserSelectParser.XML);
                    break;
                default:
                    UserSelectParser choice = consoleInterface.PromptParserSelection();
                    if (choice != UserSelectParser.Return)
                    {
                        parser = CreateParser(choice);
                    }
                    break;
            }
            
            return parser;
        }

        private IParser CreateParser(UserSelectParser choice)
        {
            switch (choice)
            {
                case UserSelectParser.JSON:
                    return new JSONParser();
                case UserSelectParser.CSV:
                    return new CSVParser();
                case UserSelectParser.XML:
                    return new XMLParser();
            }
            return null;
        }
    }
}
