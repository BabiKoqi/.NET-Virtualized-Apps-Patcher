using System;
using System.IO;
using System.Linq;
using System.Reflection;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Writer;

namespace ForlaxerKoi.Tobatoes
{
    class stuffs
    {
        public static MethodDef GetTestMethod(ModuleDefMD mod)
        {
            TypeDef[] array = mod.Types.ToArray<TypeDef>();
            for (int i = 0; i < array.Length; i++)
            {
                foreach (MethodDef methodDef in array[i].Methods.ToArray<MethodDef>())
                {
                    bool flag = methodDef.FullName.Contains("Test123");
                    if (flag)
                    {
                        return methodDef;
                    }
                }
            }
            return null;
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00002424 File Offset: 0x00000624
        public static MethodDef GetMethod(ModuleDefMD md)
        {
            TypeDef[] array = md.Types.ToArray<TypeDef>();
            for (int i = 0; i < array.Length; i++)
            {
                foreach (MethodDef methodDef in array[i].Methods.ToArray<MethodDef>())
                {
                    bool flag = methodDef.HasBody && methodDef.Body.HasInstructions;
                    if (flag)
                    {
                        bool flag2 = methodDef.Body.Instructions.Count<Instruction>() > 205;
                        if (flag2)
                        {
                            bool flag3 = methodDef.Body.Instructions[204].OpCode == OpCodes.Ldstr;
                            if (flag3)
                            {
                                bool flag4 = methodDef.Body.Instructions[205].OpCode == OpCodes.Call;
                                if (flag4)
                                {
                                    bool flag5 = methodDef.Body.Instructions[203].OpCode == OpCodes.Callvirt;
                                    if (flag5)
                                    {
                                        return methodDef;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        // Token: 0x06000004 RID: 4 RVA: 0x00002558 File Offset: 0x00000758
        private unsafe static void* Test123(byte* A_0)
        {
            UnmanagedMemoryStream unmanagedMemoryStream = (UnmanagedMemoryStream)Assembly.GetExecutingAssembly().GetManifestResourceStream("VM");
            return Test2((void*)unmanagedMemoryStream.PositionPointer, (uint)unmanagedMemoryStream.Length);
        }

        // Token: 0x06000005 RID: 5 RVA: 0x00002594 File Offset: 0x00000794
        private unsafe static void* Test2(void* A_0, uint A_1)
        {
            return null;
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000025A8 File Offset: 0x000007A8
        public static MethodDef GetMethod2(ModuleDefMD md)
        {
            TypeDef[] array = md.Types.ToArray<TypeDef>();
            for (int i = 0; i < array.Length; i++)
            {
                foreach (MethodDef methodDef in array[i].Methods.ToArray<MethodDef>())
                {
                    bool flag = methodDef.HasBody && methodDef.Body.HasInstructions;
                    if (flag)
                    {
                        bool flag2 = methodDef.Body.Instructions.Count<Instruction>() > 12;
                        if (flag2)
                        {
                            bool flag3 = methodDef.Body.Instructions[2].OpCode == OpCodes.Call;
                            if (flag3)
                            {
                                bool flag4 = methodDef.Body.Instructions[2].ToString().ToUpper().Contains("ALLOC");
                                if (flag4)
                                {
                                    bool flag5 = methodDef.Body.Instructions[8].OpCode == OpCodes.Call;
                                    if (flag5)
                                    {
                                        return methodDef;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        // Token: 0x06000007 RID: 7 RVA: 0x000026D8 File Offset: 0x000008D8
        public static MethodDef GetMethodFromMD(int token, ModuleDefMD md)
        {
            TypeDef[] array = md.Types.ToArray<TypeDef>();
            for (int i = 0; i < array.Length; i++)
            {
                foreach (MethodDef methodDef in array[i].Methods.ToArray<MethodDef>())
                {
                    bool flag = methodDef.MDToken.ToInt32() == token;
                    if (flag)
                    {
                        return methodDef;
                    }
                }
            }
            return null;
        }
    }
}
