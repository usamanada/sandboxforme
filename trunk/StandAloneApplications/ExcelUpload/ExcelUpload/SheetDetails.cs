using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUpload
{
    public class SheetDetails
    {
        public SheetDetails(string sheetName)
        {
            SheetName = sheetName.Replace("'", "");
            Name = sheetName.Replace("'", "").Replace("$", "");
        }

        public string Name { get; private set; }
        public string SheetName { get; private set; }

    }
}
