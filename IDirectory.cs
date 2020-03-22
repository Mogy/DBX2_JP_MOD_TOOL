using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBX2_JP_MOD_TOOL
{
    public interface IDirectory
    {
        string fullPath { get; }
        bool Exists();
    }
}
