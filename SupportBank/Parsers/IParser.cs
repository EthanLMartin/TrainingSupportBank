﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    interface IParser 
    {
        List<Transaction> ParseFile(string directory);
    }
}
