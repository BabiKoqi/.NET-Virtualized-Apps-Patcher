using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace Forlaxer
{
    public class Forlaxer
    {
        public static object HandleInvoke(MethodBase obj, object obj2, object[] Parameters)
        {
            object result;
            try
            {
                if (obj.Name.ToString() == "op_Equality" && Parameters[0] is string && Parameters[0].ToString().Contains("4"))
                {
                    return true;
                }
                else if (obj.Name.ToString() == "op_Inequality" && Parameters[0] is string)
                {
                    return false;
                }
                result = obj.Invoke(obj2, Parameters);
            }
            catch
            {
                result = null;
            }
            return result;
        }
        public static string[] array = new string[]
        {
                "codecracker",
                "x32dbg",
                "x64dbg",
                "dnspy",
                "simpleassembly",
                "httpanalyzer",
                "fiddler",
                "wireshark",
                "mitmproxy",
                "pc-ret",
                "cracked.to",
                "burpsuite",
                "burpsuitecommunity",
                "python",
                "de4dot",
                "proxifier",
                "windbg",
                "krawk",
                "dumper",
                "eaz",
                "httpdebug",
                "ieinspector",
                "peek",
                "charles",
                "ida -"

        };
        public static object debugInvoke(MethodBase obj, object obj2, object[] Parameters)
        {
            bool bypass = false;
            object op = obj.Invoke(obj2, Parameters);
            try
            {
                string debug = "";
                if (obj2 == null)
                {
                    debug = "MethodDeclaringType: [ " + obj.DeclaringType + " ]" + "\nMethodName: ( " + obj.Name.ToString() + " ) MethodType: " + obj.GetType() + "\nObject Value: ( null ) Type: null\nParameters: " + Parameters.ToString();
                }
                else
                {
                    debug = "MethodDeclaringType: [ " + obj.DeclaringType + " ]" + "\nMethodName: ( " + obj.Name.ToString() + " ) MethodType: " + obj.GetType() + "\nObject Value: ( " + obj2.ToString() + " ) Type: " + obj2.GetType() + "\nParameters: " + Parameters.ToString();
                }
                int num = 0;
                foreach (object sexy in Parameters)
                {
                    debug = debug + "\nParameter[" + num + "] Value: ( " + sexy + " ) Type: " + sexy.GetType();
                    num++;
                }
                debug = debug + "\nReturns: " + op.ToString() + "\nRes Type: " + op.GetType();

                System.IO.File.AppendAllText("forlxerTracer.txt", debug + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            }
            catch { }

            return op;
        }

        public static object dbgInvokeHandler(MethodBase obj, object obj2, object[] Parameters)
        {
            object result;
            try
            {
                string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*_config.json");
                dynamic jsonElements = Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText(filePaths[0]));
                foreach (dynamic element in jsonElements.functions)
                {
                    string searchBy = element.searchBy;
                    string searchFor = element.searchFor;
                    string parameters = element.parameters;

                    string searchBy2 = element.continueAdvanced.searchBy;
                    string searchFor2 = element.continueAdvanced.searchFor;
                    object replaceWith = element.continueAdvanced.replaceWith;
                    object replaceParWith = element.continueAdvanced.replaceParWith;
                    int par2 = element.continueAdvanced.parameter;

                    if (searchBy == "MethodName" && obj.Name.ToString() == searchFor && Parameters.ToString() == parameters)
                    {
                        if (searchBy2 != "None" && searchBy2 == "Parameter" && Parameters[par2].ToString().Contains(searchFor2))
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                        else if (searchBy2 != "None" && searchBy2 == "ParameterType" && Parameters[par2].GetType().ToString() == searchFor2)
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }

                    }
                    else if (searchBy == "MethodDeclaringType" && obj.DeclaringType.ToString() == searchFor && Parameters.ToString() == parameters)
                    {
                        if (searchBy2 != "None" && searchBy2 == "Parameter" && Parameters[par2].ToString().Contains(searchFor2))
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                        else if (searchBy2 != "None" && searchBy2 == "ParameterType" && Parameters[par2].GetType().ToString() == searchFor2)
                        {

                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                    }
                    else if (searchBy == "MethodType" && obj.GetType().ToString() == searchFor && Parameters.ToString() == parameters)
                    {
                        if (searchBy2 != "None" && searchBy2 == "Parameter" && Parameters[par2].ToString().Contains(searchFor2))
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                        else if (searchBy2 != "None" && searchBy2 == "ParameterType" && Parameters[par2].GetType().ToString() == searchFor2)
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                    }
                    else if (searchBy == "Object" && obj2.ToString() == searchFor && Parameters.ToString() == parameters)
                    {
                        if (searchBy2 != "None" && searchBy2 == "Parameter" && Parameters[par2].ToString().Contains(searchFor2))
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                        else if (searchBy2 != "None" && searchBy2 == "ParameterType" && Parameters[par2].GetType().ToString() == searchFor2)
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;

                            }
                        }
                    }
                    else if (searchBy == "ObjectType" && obj2.GetType().ToString() == searchFor && Parameters.ToString() == parameters)
                    {
                        if (searchBy2 != "None" && searchBy2 == "Parameter" && Parameters[par2].ToString().Contains(searchFor2))
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                        else if (searchBy2 != "None" && searchBy2 == "ParameterType" && Parameters[par2].GetType().ToString() == searchFor2)
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                    }
                }
                result = obj.Invoke(obj2, Parameters);
                try
                {
                    string debug = "";
                    if (obj2 == null)
                    {
                        debug = "MethodDeclaringType: [ " + obj.DeclaringType + " ]" + "\nMethodName: ( " + obj.Name.ToString() + " ) MethodType: " + obj.GetType() + "\nObject Value: ( null ) Type: null\nParameters: " + Parameters.ToString();
                    }
                    else
                    {
                        debug = "MethodDeclaringType: [ " + obj.DeclaringType + " ]" + "\nMethodName: ( " + obj.Name.ToString() + " ) MethodType: " + obj.GetType() + "\nObject Value: ( " + obj2.ToString() + " ) Type: " + obj2.GetType() + "\nParameters: " + Parameters.ToString();
                    }
                    int num = 0;
                    foreach (object sexy in Parameters)
                    {
                        debug = debug + "\nParameter[" + num + "] Value: ( " + sexy + " ) Type: " + sexy.GetType();
                        num++;
                    }
                    debug = debug + "\nReturns: " + result.ToString() + "\nRes Type: " + result.GetType();
                    System.IO.File.AppendAllText("forlxerTracer.txt", debug + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                }
                catch { }
            }
            catch
            {
                result = obj.Invoke(obj2, Parameters);
                try
                {
                    string debug = "";
                    if (obj2 == null)
                    {
                        debug = "MethodDeclaringType: [ " + obj.DeclaringType + " ]" + "\nMethodName: ( " + obj.Name.ToString() + " ) MethodType: " + obj.GetType() + "\nObject Value: ( null ) Type: null\nParameters: " + Parameters.ToString();
                    }
                    else
                    {
                        debug = "MethodDeclaringType: [ " + obj.DeclaringType + " ]" + "\nMethodName: ( " + obj.Name.ToString() + " ) MethodType: " + obj.GetType() + "\nObject Value: ( " + obj2.ToString() + " ) Type: " + obj2.GetType() + "\nParameters: " + Parameters.ToString();
                    }
                    int num = 0;
                    foreach (object sexy in Parameters)
                    {
                        debug = debug + "\nParameter[" + num + "] Value: ( " + sexy + " ) Type: " + sexy.GetType();
                        num++;
                    }
                    debug = debug + "\nReturns: " + result.ToString() + "\nRes Type: " + result.GetType();
                    System.IO.File.AppendAllText("forlxerTracer.txt", debug + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                }
                catch { }
            }
            return result;
        }

        public static object InvokeHandler(MethodBase obj, object obj2, object[] Parameters)
        {
            object result;
            try
            {
                string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*_config.json");
                dynamic jsonElements = Newtonsoft.Json.JsonConvert.DeserializeObject(File.ReadAllText(filePaths[0]));
                foreach (dynamic element in jsonElements.functions)
                {
                    string searchBy = element.searchBy;
                    string searchFor = element.searchFor;
                    string parameters = element.parameters;

                    string searchBy2 = element.continueAdvanced.searchBy;
                    string searchFor2 = element.continueAdvanced.searchFor;
                    object replaceWith = element.continueAdvanced.replaceWith;
                    object replaceParWith = element.continueAdvanced.replaceParWith;
                    int par2 = element.continueAdvanced.parameter;

                    if (searchBy == "MethodName" && obj.Name.ToString() == searchFor && Parameters.ToString() == parameters)
                    {
                        if (searchBy2 != "None" && searchBy2 == "Parameter" && Parameters[par2].ToString().Contains(searchFor2))
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                        else if (searchBy2 != "None" && searchBy2 == "ParameterType" && Parameters[par2].GetType().ToString() == searchFor2)
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }

                    }
                    else if (searchBy == "MethodDeclaringType" && obj.DeclaringType.ToString() == searchFor && Parameters.ToString() == parameters)
                    {
                        if (searchBy2 != "None" && searchBy2 == "Parameter" && Parameters[par2].ToString().Contains(searchFor2))
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                        else if (searchBy2 != "None" && searchBy2 == "ParameterType" && Parameters[par2].GetType().ToString() == searchFor2)
                        {

                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                    }
                    else if (searchBy == "MethodType" && obj.GetType().ToString() == searchFor && Parameters.ToString() == parameters)
                    {
                        if (searchBy2 != "None" && searchBy2 == "Parameter" && Parameters[par2].ToString().Contains(searchFor2))
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                        else if (searchBy2 != "None" && searchBy2 == "ParameterType" && Parameters[par2].GetType().ToString() == searchFor2)
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                    }
                    else if (searchBy == "Object" && obj2.ToString() == searchFor && Parameters.ToString() == parameters)
                    {
                        if (searchBy2 != "None" && searchBy2 == "Parameter" && Parameters[par2].ToString().Contains(searchFor2))
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                        else if (searchBy2 != "None" && searchBy2 == "ParameterType" && Parameters[par2].GetType().ToString() == searchFor2)
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;

                            }
                        }
                    }
                    else if (searchBy == "ObjectType" && obj2.GetType().ToString() == searchFor && Parameters.ToString() == parameters)
                    {
                        if (searchBy2 != "None" && searchBy2 == "Parameter" && Parameters[par2].ToString().Contains(searchFor2))
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                        else if (searchBy2 != "None" && searchBy2 == "ParameterType" && Parameters[par2].GetType().ToString() == searchFor2)
                        {
                            if (replaceParWith.ToString() != "None")
                            {
                                Parameters[par2] = replaceParWith;
                            }
                            if (replaceWith.ToString() != "None")
                            {
                                if (replaceWith.ToString().ToLower() == "true")
                                {
                                    return true;
                                }
                                if (replaceWith.ToString().ToLower() == "false")
                                {
                                    return false;
                                }
                                return replaceWith;
                            }
                        }
                    }
                }
                result = obj.Invoke(obj2, Parameters);
            }
            catch
            {
                result = obj.Invoke(obj2, Parameters);
            }
            return result;
        }

    }
}
