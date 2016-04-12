using PyriteCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyriteCore.CoreStandartActions
{
    internal interface ICoreElement
    {
        Pyrite CurrentPyrite { get; set; }
    }
}
