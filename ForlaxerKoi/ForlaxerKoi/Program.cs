using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Writer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ForlaxerKoi.Tobatoes;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace ForlaxerKoi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.ASCII;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            //string path = args[0];
            Console.WriteLine("Drag n Drop the Exe: ");

            string path = Console.ReadLine();
            try
            {
                path = path.Replace("\"", "");
            }
            catch { }
            string fileDir = System.IO.Path.GetDirectoryName(path);
            fileDir = fileDir.Replace("\"", "");
            string agilePath = fileDir + "\\AgileDotNet.VMRuntime.dll";
            string obf = "";
            Console.Clear();
            ModuleDefMD module = ModuleDefMD.Load(path);

            Console.WriteLine("ForlaxerKoi v1.0");
            Console.WriteLine("ReCoded by: Forlax (Originaly Made by Team Venturi77)\n");
            info($"Processing module '{module.Name}'...");
            info($"Resolving dependencies...");

            foreach (var data in module.GetAssemblyRefs())
            {
                if (data.ToString().Contains("AgileDotNet.VMRuntime"))
                    obf = "Agile.NET";
                dbg($"Resolved  '{data.ToString()}'");
            }
            success("Resolved all dependencies");
			Colorful.Console.Write("\n[DEBUG]    ", Color.LightSkyBlue);
            Colorful.Console.Write("Pick an option [1: Auto Detection |2: Manual Detection] ");
            stopwatch.Stop();
            int detection = int.Parse(Console.ReadLine());
            stopwatch.Start();
			if (detection == 1)
			{
				try
				{

					if (obf == "Agile.NET")
					{
						info("Detected Agile.NET as Obfuscator");
						obf = "AgileVM";
						goto Agile;
					}
					else if (module.FullName.Contains("вє∂ѕ ρяσтє¢тσя"))
					{
						info("Detected Beds Protector as Obfuscator");
						obf = "Beds Protector with KoiVM";
					}
					else if (module.Assembly.Modules[0].CustomAttributes[0].AttributeType.ToString().Contains("ConfusedBy") || module.Assembly.Modules[0].CustomAttributes[1].AttributeType.ToString().Contains("ConfusedBy"))
					{
						info("Detected ConfuserEx as Obfuscator");
						obf = "ConfuserEX with KoiVM";
					}
					else
					{
						info("Detected EazFuscator as Obfuscator");
						obf = "EazVM";
						goto EAZ;
					}
				}
				catch
				{
					info("Detected EazFuscator as Obfuscator");
					obf = "EazVM";
					goto EAZ;
				}
			}
			else 
			{
				Colorful.Console.Write("[DEBUG]    ", Color.LightSkyBlue);
				Colorful.Console.Write("Pick a virtualizer [1: Koi |2: Eaz |3: Agile] ");
				stopwatch.Stop();
				int vmOption = int.Parse(Console.ReadLine());
				stopwatch.Start();
				if (vmOption == 1)
				{
					obf = "KoiVM";
				}
				else if (vmOption == 2)
				{
					obf = "EazVM";
					goto EAZ;
				}
				else
				{
					obf = "AgileVM";
					goto Agile;
				}
			}
            dbg("Looking for KoiVM data");
            bool isFound = false;
            foreach (var stream in module.Metadata.AllStreams)
            {
                if (stream.Name == "#Koi" || stream.Name == "#Bed" || stream.Name == "Eddy^CZ")
                {
                    isFound = true;
                    info($"Found KoiVM data '{stream.Name}'");
                    dbg($"Heap offset: {ToHex((int)stream.StreamHeader.StartOffset)}");
                    dbg($"Heap size: {ToHex((int)stream.StreamHeader.StreamSize)}");
                    var read = stream.CreateReader();
                    byte[] bytes = read.ToArray();
                    System.IO.File.WriteAllText(path + "_" + stream.Name.Replace("#", "") + "_unicode.bin", System.Text.Encoding.Unicode.GetString(bytes));
                    System.IO.File.WriteAllBytes(path + "_" + stream.Name.Replace("#", "") + ".bin", bytes);
                    success("Exported KoiVM data");
                    dbg("Hooking Forlaxer...");
                    try
                    {
                        //File.Copy(@"C:\Users\ForlaxPy\source\repos\Forlaxer\Forlaxer\bin\Debug\Forlaxer.dll", fileDir + "\\Forlaxer.dll");
                    }
                    catch { }
                    try
                    {
                        Colorful.Console.Write("\n[DEBUG]    ", Color.LightSkyBlue);
                        Colorful.Console.Write("Pick a version [1: DBG & BNG |2: BNG] ");
                        stopwatch.Stop();
                        int pick = int.Parse(Console.ReadLine());
                        stopwatch.Start();
                        InjectForlaxerKoi(path, bytes, pick);
                        dbg("Generating the JSON config...");
                        generateJson(path, obf);
                    }
                    catch (ArgumentNullException ex)
                    {
                        File.WriteAllText("traces.txt", ex.Message.ToString());
                        fail("Failed to Hook Forlaxer ");
                    }
                    int VMmethods = 0;
                    try
                    {
                        VMmethods = detectingVMedMethods(path);
                        success($"Parsed {VMmethods} virtualized methods");
                    }
                    catch { }

                    //VMData koi = new VMData(stream.StreamHeader);
                    //System.Reflection.Module m = System.Reflection.Assembly
                    //System.Reflection.Module m = Type.GetTypeFromHandle(typeof(Fox).TypeHandle).Module;
                }
            }
            if (!isFound)
                fail("Couldn't find KoiVM data");
            goto Done;

        EAZ:
            dbg("Hooking Forlaxer...");
            try
            {
                //File.Copy(@"C:\Users\ForlaxPy\source\repos\Forlaxer\Forlaxer\bin\Debug\Forlaxer.dll", fileDir + "\\Forlaxer.dll");
            }
            catch { }
            try
            {
                Colorful.Console.Write("\n[DEBUG]    ", Color.LightSkyBlue);
                Colorful.Console.Write("Pick a version [1: DBG & BNG |2: BNG] ");
                stopwatch.Stop();
                int pick = int.Parse(Console.ReadLine());
                stopwatch.Start();
                InjectForlaxerEaz(path, pick);
                dbg("Generating the JSON config...");
                generateJson(path, obf);
            }
            catch (ArgumentNullException ex)
            {
                File.WriteAllText("traces.txt", ex.Message.ToString());
                fail("Failed to Hook Forlaxer ");
            }
            goto Done;

        Agile:
            dbg("Hooking Forlaxer...");
            try
            {
                //File.Copy(@"C:\Users\ForlaxPy\source\repos\Forlaxer\Forlaxer\bin\Debug\Forlaxer.dll", fileDir + "\\Forlaxer.dll");
            }
            catch { }
            try
            {
                Colorful.Console.Write("\n[DEBUG]    ", Color.LightSkyBlue);
                Colorful.Console.Write("Pick a version [1: DBG & BNG |2: BNG] ");
                stopwatch.Stop();
                int pick = int.Parse(Console.ReadLine());
                stopwatch.Start();
                InjectForlaxerAgile(agilePath, pick);
                File.Move(agilePath, agilePath + ".bak");
                File.Move(agilePath + ".temp", agilePath);
                success("Forlaxer Hooked Succesfully!");
                dbg("Generating the JSON config...");
                generateJson(path, obf);
            }
            catch (ArgumentNullException ex)
            {
                File.WriteAllText("traces.txt", ex.Message.ToString());
                fail("Failed to Hook Forlaxer ");
            }
            goto Done;

        Done:
            stopwatch.Stop();
            Console.WriteLine("\n\n\nElapsed time: " + stopwatch.Elapsed.TotalSeconds.ToString() + " seconds.");
            Console.ReadKey();

        }
        public static void InjectForlaxerAgile(string path, int pick)
        {
            ModuleDefMD md = ModuleDefMD.Load(path);
            foreach (TypeDef type in md.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (method.HasBody && method.Body.HasInstructions && method.Body.Instructions.Count() < 300)
                    {
                        for (int i = 0; i < method.Body.Instructions.Count(); i++)
                        {
                            if (method.Body.Instructions[i].OpCode == OpCodes.Callvirt)
                            {
                                string operand = method.Body.Instructions[i].Operand.ToString();
                                if (operand.Contains("System.Object System.Reflection.MethodBase::Invoke(System.Object,System.Object[])") && method.Body.Instructions[i + 1].IsStloc() && method.Body.Instructions[i - 1].IsLdarg() && method.Body.Instructions[i - 3].IsLdarg())
                                {
                                    MDToken var = method.MDToken;
                                    method.Body.Instructions[i].OpCode = OpCodes.Nop;
                                    Importer importer = new Importer(md);
                                    IMethod myMethod;
                                    if (pick == 1)
                                    {
                                        myMethod = importer.Import(typeof(Forlaxer.Forlaxer).GetMethod("dbgInvokeHandler"));
                                    }
                                    else
                                    {
                                        myMethod = importer.Import(typeof(Forlaxer.Forlaxer).GetMethod("InvokeHandler"));
                                    }
                                    method.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, md.Import(myMethod)));
                                    i += 1;

                                }
                            }
                        }
                    }
                }
            }
        BLOCK1:
            ModuleWriterOptions moduleWriterOptions = new ModuleWriterOptions(md);
            moduleWriterOptions.MetadataOptions.Flags = MetadataFlags.KeepOldMaxStack;
            moduleWriterOptions.Logger = DummyLogger.NoThrowInstance;
            moduleWriterOptions.MetadataOptions.Flags = MetadataFlags.PreserveAll;
            moduleWriterOptions.MetadataOptions.PreserveHeapOrder(md, true);
            moduleWriterOptions.Cor20HeaderOptions.Flags = new ComImageFlags?(ComImageFlags.ILOnly | ComImageFlags.Bit32Required);
            md.Write(path + ".temp", moduleWriterOptions);
        }
        public static void InjectForlaxerEaz(string path, int pick)
        {
            ModuleDefMD md = ModuleDefMD.Load(path);
            foreach (TypeDef type in md.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (method.HasBody && method.Body.HasInstructions && method.Body.Instructions.Count() < 200)
                    {
                        for (int i = 0; i < method.Body.Instructions.Count(); i++)
                        {
                            if (method.Body.Instructions[i].OpCode == OpCodes.Callvirt)
                            {
                                string operand = method.Body.Instructions[i].Operand.ToString();
                                if (operand.Contains("System.Object System.Reflection.MethodBase::Invoke(System.Object,System.Object[])") && method.Body.Instructions[i - 1].IsLdarg() && method.Body.Instructions[i - 2].IsLdarg() && method.Body.Instructions[i - 3].IsLdarg())
                                {
                                    MDToken var = method.MDToken;
                                    Colorful.Console.Write("[INFO]     ", Color.Purple);
                                    Colorful.Console.Write("Invoke MDToken: " + var.Raw.ToString());
                                    method.Body.Instructions[i].OpCode = OpCodes.Nop;
                                    Importer importer = new Importer(md);
                                    IMethod myMethod;
                                    if (pick == 1)
                                    {
                                        myMethod = importer.Import(typeof(Forlaxer.Forlaxer).GetMethod("dbgInvokeHandler"));
                                    }
                                    else
                                    {
                                        myMethod = importer.Import(typeof(Forlaxer.Forlaxer).GetMethod("InvokeHandler"));
                                    }
                                    method.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, md.Import(myMethod)));
                                    i += 1;
                                    success("Forlaxer Hooked Succesfully!");
                                }
                            }
                        }
                    }
                }
            }
        BLOCK1:
            ModuleWriterOptions moduleWriterOptions = new ModuleWriterOptions(md);
            moduleWriterOptions.MetadataOptions.Flags = MetadataFlags.KeepOldMaxStack;
            moduleWriterOptions.Logger = DummyLogger.NoThrowInstance;
            moduleWriterOptions.MetadataOptions.Flags = MetadataFlags.PreserveAll;
            moduleWriterOptions.MetadataOptions.PreserveHeapOrder(md, true);
            moduleWriterOptions.Cor20HeaderOptions.Flags = new ComImageFlags?(ComImageFlags.ILOnly | ComImageFlags.Bit32Required);
            md.Write(path + "_Hooked_.exe", moduleWriterOptions);
        }
        public static void InjectForlaxerKoi(string path, byte[] koi, int pick)
        {
            ModuleDefMD md = ModuleDefMD.Load(path);
            foreach (TypeDef type in md.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (method.HasBody && method.Body.HasInstructions && method.Body.Instructions.Count() > 150)
                    {
                        for (int i = 0; i < method.Body.Instructions.Count(); i++)
                        {
                            if (method.Body.Instructions[i].OpCode == OpCodes.Callvirt)
                            {
                                string operand = method.Body.Instructions[i].Operand.ToString();
                                if (operand.Contains("System.Object System.Reflection.MethodBase::Invoke(System.Object,System.Object[])") && method.Body.Instructions[i + 1].IsStloc() && method.Body.Instructions[i - 2].IsLdloc())
                                {
                                    MDToken var = method.MDToken;
                                    Colorful.Console.Write("[INFO]     ", Color.Purple);
                                    Colorful.Console.Write("Invoke MDToken: " + var.Raw.ToString());
                                    method.Body.Instructions[i].OpCode = OpCodes.Nop;
                                    Importer importer = new Importer(md);
                                    IMethod myMethod;
                                    if (pick == 1)
                                    {
                                        myMethod = importer.Import(typeof(Forlaxer.Forlaxer).GetMethod("dbgInvokeHandler"));
                                    }
                                    else
                                    {
                                        myMethod = importer.Import(typeof(Forlaxer.Forlaxer).GetMethod("InvokeHandler"));
                                    }
                                    method.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, md.Import(myMethod)));
                                    i += 1;
                                    success("Forlaxer Hooked Succesfully!");
                                }
                            }
                        }
                    }
                }
            }
        BLOCK1:
            dbg("Preparing to move KoiVM Data...");
            MethodDef methodDef = stuffs.GetMethod(md);
            MethodDef methodDef2 = stuffs.GetMethod2(md);
            try
            {
                methodDef.Body.Instructions.Clear();
            }
            catch { }

            ModuleDefMD mod = ModuleDefMD.Load("ForlaxerKoi.exe");
            MethodDef testMethod = stuffs.GetTestMethod(mod);
            foreach (Instruction item in testMethod.Body.Instructions)
            {
                methodDef.Body.Instructions.Add(item);
            }
            TypeRef type2 = new TypeRefUser(md, "System.IO", "UnmanagedMemoryStream", md.CorLibTypes.AssemblyRef);
            methodDef.Body.Variables[0].Type = type2.ToTypeSig(true);
            if (methodDef.Body.Instructions[11].OpCode == OpCodes.Call)
            {
                methodDef.Body.Instructions[11].Operand = methodDef2.ResolveMethodDef();
            }
            dbg("Adding VM to Resources");
            md.Resources.Add(new EmbeddedResource("VM", koi, ManifestResourceAttributes.Private));
            ModuleWriterOptions moduleWriterOptions = new ModuleWriterOptions(md);
            moduleWriterOptions.MetadataOptions.Flags = MetadataFlags.KeepOldMaxStack;
            moduleWriterOptions.Logger = DummyLogger.NoThrowInstance;
            moduleWriterOptions.MetadataOptions.Flags = MetadataFlags.PreserveAll;
            moduleWriterOptions.MetadataOptions.PreserveHeapOrder(md, true);
            moduleWriterOptions.Cor20HeaderOptions.Flags = new ComImageFlags?(ComImageFlags.ILOnly | ComImageFlags.Bit32Required);
            success("VM Resource injected");
            md.Write(path + "_Hooked_.exe", moduleWriterOptions);
        }
        public static int detectingVMedMethods(string path)
        {
            int result = 0;
            ModuleDefMD md = ModuleDefMD.Load(path);
            foreach (TypeDef type in md.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (method.HasBody && method.Body.HasInstructions && method.Body.Instructions.Count() < 100)
                    {
                        for (int i = 0; i < method.Body.Instructions.Count(); i++)
                        {
                            if (method.Body.Instructions[i].OpCode == OpCodes.Call && method.Body.Instructions[i].Operand.ToString().Contains("System.Object KoiVM.Runtime.VMEntry::Run(System.RuntimeTypeHandle,System.UInt32,System.Object[])") && method.Body.Instructions[i - 1].OpCode.ToString().Contains("newarr"))
                            {
                                result++;
                                dbg($"Found virtualized method: {method.FullName} Id: {result}");
                            }
                        }
                    }
                }
            }
            return result;
        }
        public static void generateJson(string path, string obfuscator)
        {
            JObject json =
                    new JObject(
                        new JProperty("obfuscator", obfuscator),
                        new JProperty("getMethodByMDToken", 0),
                        new JProperty("functions",
                                new JArray(
                                        new JObject(
                                                new JProperty("searchBy", "MethodName | MethodDeclaringType | MethodType | Object | ObjectType"),
                                                new JProperty("searchFor", "searchBy => Value"),
                                                new JProperty("parameters", "Parameters Value"),
                                                new JProperty("continueAdvanced",
                                                    new JObject(
                                                        new JProperty("searchBy", "None | Parameter | ParameterType"),
                                                        new JProperty("searchFor", "searchBy => Value"),
                                                        new JProperty("replaceWith", "None | Object => Value"),
                                                        new JProperty("replaceParWith", "None | Parameter => Value"),
                                                        new JProperty("parameter", "searchBy => Parameter id(int)")
                                                    )
                                                )
                                            ),
                                        new JObject(
                                                new JProperty("searchBy", "MethodName | MethodDeclaringType | MethodType | Object | ObjectType"),
                                                new JProperty("searchFor", "searchBy => Value"),
                                                new JProperty("parameters", "Parameters Value"),
                                                new JProperty("continueAdvanced",
                                                    new JObject(
                                                        new JProperty("searchBy", "None | Parameter | ParameterType"),
                                                        new JProperty("searchFor", "searchBy => Value"),
                                                        new JProperty("replaceWith", "None | Object => Value"),
                                                        new JProperty("replaceParWith", "None | Parameter => Value"),
                                                        new JProperty("parameter", "searchBy => Parameter id(int)")
                                                    )
                                                )
                                            )
                                    )
                            )
                        );
            File.WriteAllText(path + "_config.json", json.ToString());
            success("Generated the JSON config");

        }
        public static void dbg(string str)
        {
            Colorful.Console.Write("\n[DEBUG]    ", Color.LightSkyBlue);
            Colorful.Console.Write(str);
        }
        public static void info(string str)
        {
            Colorful.Console.Write("\n[INFO]     ", Color.Purple);
            Colorful.Console.Write(str);
        }
        public static void success(string str)
        {
            Colorful.Console.Write("\n[SUCCESS]  ", Color.Green);
            Colorful.Console.Write(str);
        }
        public static void error(string str)
        {
            Colorful.Console.Write("\n[ERROR]    ", Color.Gray);
            Colorful.Console.Write(str);
        }
        public static void fail(string str)
        {
            Colorful.Console.Write("\n[FAILURE]  ", Color.Red);
            Colorful.Console.Write(str);
        }
        public static string ToHex(int value)
        {
            return String.Format("0x{0:X}", value);
        }
    }
}
