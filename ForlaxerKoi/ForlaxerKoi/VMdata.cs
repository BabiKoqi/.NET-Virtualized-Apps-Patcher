using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ForlaxerKoi
{
    internal class VMData
    {
        // Token: 0x060001CA RID: 458 RVA: 0x0000BED8 File Offset: 0x0000A0D8
        public unsafe VMData(void* data)
        {
            var header = (VMDAT_HEADER*)data;
            if (header->MAGIC != 0x68736966)
                throw new InvalidProgramException();

            references = new Dictionary<uint, RefInfo>();
            strings = new Dictionary<uint, string>();

            var ptr = (byte*)(header + 1);

            for (int i = 0; i < header->STR_COUNT; i++)
            {
                var id = Utils.ReadCompressedUInt(ref ptr);
                var len = Utils.ReadCompressedUInt(ref ptr);
                strings[id] = new string((char*)ptr, 0, (int)len);
                ptr += len << 1;
            }

            KoiSection = (byte*)data;

        }

        // Token: 0x17000064 RID: 100
        // (get) Token: 0x060001CB RID: 459 RVA: 0x00002627 File Offset: 0x00000827
        public Module Module { get; }

        // Token: 0x17000065 RID: 101
        // (get) Token: 0x060001CC RID: 460 RVA: 0x0000262F File Offset: 0x0000082F
        // (set) Token: 0x060001CD RID: 461 RVA: 0x00002637 File Offset: 0x00000837
        public unsafe byte* KoiSection { get; set; }



        // Token: 0x060001CF RID: 463 RVA: 0x0000C094 File Offset: 0x0000A294
        public MemberInfo LookupReference(uint id)
        {
            return this.references[id].Member;
        }

        // Token: 0x060001D0 RID: 464 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
        public string LookupString(uint id)
        {
            bool flag = id == 0u;
            string result;
            if (flag)
            {
                result = null;
            }
            else
            {
                result = this.strings[id];
            }
            return result;
        }



        // Token: 0x040000D7 RID: 215
        private static readonly Dictionary<Module, VMData> moduleVMData = new Dictionary<Module, VMData>();

        // Token: 0x040000D8 RID: 216


        // Token: 0x040000D9 RID: 217
        private readonly Dictionary<uint, VMData.RefInfo> references;

        // Token: 0x040000DA RID: 218
        private readonly Dictionary<uint, string> strings;

        // Token: 0x02000089 RID: 137
        private struct VMDAT_HEADER
        {
            // Token: 0x040000DD RID: 221
            public readonly uint MAGIC;

            // Token: 0x040000DE RID: 222
            public readonly uint MD_COUNT;

            // Token: 0x040000DF RID: 223
            public readonly uint STR_COUNT;

            // Token: 0x040000E0 RID: 224
            public readonly uint EXP_COUNT;
        }

        // Token: 0x0200008A RID: 138
        private class RefInfo
        {
            // Token: 0x17000066 RID: 102
            // (get) Token: 0x060001D3 RID: 467 RVA: 0x0000C104 File Offset: 0x0000A304
            public MemberInfo Member
            {
                get
                {
                    MemberInfo result;
                    if ((result = this.resolved) == null)
                    {
                        result = (this.resolved = this.module.ResolveMember(this.token));
                    }
                    return result;
                }
            }

            // Token: 0x040000E1 RID: 225
            public Module module;

            // Token: 0x040000E2 RID: 226
            public MemberInfo resolved;

            // Token: 0x040000E3 RID: 227
            public int token;
        }
    }
}
