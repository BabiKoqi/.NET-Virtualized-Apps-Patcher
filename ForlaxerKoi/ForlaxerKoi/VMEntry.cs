using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ForlaxerKoi
{
    class VMEntry
    {
        public static object Run(RuntimeTypeHandle type, uint id, object[] args)
        {
            Module module = Type.GetTypeFromHandle(type).Module;
            return null;
        }
    }
}
