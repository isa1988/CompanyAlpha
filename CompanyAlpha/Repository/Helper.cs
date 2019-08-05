using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAlpha.Repository
{
    static class Helper
    {
        public static string GetNotNull(this string line)
        {
            if (line == null) line = string.Empty;
            return line.Trim();
        }
    }
}
