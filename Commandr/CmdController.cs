using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Commandr
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CmdController : Attribute
    {
        /// <summary>
        /// Execute methods that startup arguments are bound to.
        /// </summary>
        /// <param name="args">Arguments passed further from Main</param>
        public static void Process(string[] commands)
        {
            //No commands to process
            if (commands.Length < 1)
                return;

            Type controllerType = null;

            //Faster than LINQ(?)
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach(Type t in a.GetTypes())
                {
                    if(t.GetCustomAttribute<CmdController>() != null)
                    {
                        controllerType = t;
                        break;
                    }
                }
            }

            //No controller found -> do nothing
            if (controllerType == null)
                return;

            //Find methods inside of the controller with commandline arguments attached to them
            var cmds = commands.ToList();
            foreach( MethodInfo m in controllerType.GetMethods() )
            {
                var hook = m.GetCustomAttribute<CmdHook>();
                if (hook != null)
                {
                    //Argument wasn't used, skip it;
                    if (!cmds.Where(x => x.Equals(hook.Command)).Any())
                        continue;

                    var argIndex = cmds.IndexOf(hook.Command);

                    //Execute method with given amount of parameters
                    var len = m.GetParameters().Length;
                    var prms = new List<string>();
                    for(int i = 1; i <= len; i++)
                    {
                        if (argIndex + i >= cmds.Count)
                            break;

                        if (cmds[argIndex + i].StartsWith("-"))
                            break;

                        prms.Add(cmds[argIndex + i]);
                    }

                    //Fill missing params with nulls
                    for(int i = prms.Count; i < len; i++)
                    {
                        prms.Add(null);
                    }

                    m.Invoke(null, prms.ToArray());
                }
            }
        }
    }
}
