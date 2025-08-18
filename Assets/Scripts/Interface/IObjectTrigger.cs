using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IObjectTrigger
{
    bool IsActivated { get; }
    ValueChangeFunc OnValueChanged { get; set; }
    
}
public delegate void ValueChangeFunc(bool onOff);

