﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    abstract class Parser 
    {
        public abstract List<Transaction> ParseFile(string directory);
    }
}
