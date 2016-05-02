using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveActionUI.ActionPanels
{
    public interface ISetterControl
    {
        SetterImpl Setter { get; }
    }
}
