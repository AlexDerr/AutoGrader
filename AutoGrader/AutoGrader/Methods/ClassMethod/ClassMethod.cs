using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGrader.Methods.ClassMethod
{
    public class ClassMethod
    {
        public static string GenerateUniqueKey()
        {
            return Guid.NewGuid().ToString().Substring(0,5) + DateTime.Now.ToString("MMddyyyyHHmmss");
        }
    }
}
