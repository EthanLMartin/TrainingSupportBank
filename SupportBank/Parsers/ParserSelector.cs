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
                    parser = CreateParser(UserChoice.JSON);
                    break;
                case ".csv":
                    parser = CreateParser(UserChoice.CSV);
                    break;
                case ".xml":
                    parser = CreateParser(UserChoice.XML);
                    break;
                default:
                    UserChoice choice = consoleInterface.PromptParserSelection();
                    if (choice != UserChoice.Exit)
                    {
                        parser = CreateParser(choice);
                    }
                    break;
            }
            
            return parser;
        }

        private IParser CreateParser(UserChoice choice)
        {
            switch (choice)
            {
                case UserChoice.JSON:
                    return new JSONParser();
                case UserChoice.CSV:
                    return new CSVParser();
                case UserChoice.XML:
                    return new XMLParser();
            }
            return null;
        }
    }
}
