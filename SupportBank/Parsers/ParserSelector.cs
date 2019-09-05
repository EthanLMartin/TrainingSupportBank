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
                    parser = CreateParser(UserSelectFileType.JSON);
                    break;
                case ".csv":
                    parser = CreateParser(UserSelectFileType.CSV);
                    break;
                case ".xml":
                    parser = CreateParser(UserSelectFileType.XML);
                    break;
                default:
                    UserSelectFileType choice = consoleInterface.PromptFileTypeSelection("File type not recongised.");
                    if (choice != UserSelectFileType.Return)
                    {
                        parser = CreateParser(choice);
                    }
                    break;
            }
            
            return parser;
        }

        private IParser CreateParser(UserSelectFileType choice)
        {
            switch (choice)
            {
                case UserSelectFileType.JSON:
                    return new JSONParser();
                case UserSelectFileType.CSV:
                    return new CSVParser();
                case UserSelectFileType.XML:
                    return new XMLParser();
            }
            return null;
        }
    }
}
