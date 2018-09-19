using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Commandr
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Delegate)]
    public class CmdHook : Attribute
    {
        public string Command { get; set; }

        public CmdHook(string cmd)
        {
            Command = cmd;
        }
    }
}
