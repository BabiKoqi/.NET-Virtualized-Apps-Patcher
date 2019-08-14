using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ForlaxerKoi
{
    class Data
    {
        internal unsafe static VMData GetData(Module module)
        {
            byte* moduleBase = (byte*)((void*)Marshal.GetHINSTANCE(module));
            string fullyQualifiedName = module.FullyQualifiedName;
            bool flag2 = fullyQualifiedName.Length > 0 && fullyQualifiedName[0] == '<';
            bool flag3 = flag2;
            VMData result;
            if (flag3)
            {
                result = new VMData(GetKoiStreamFlat(moduleBase));
            }
            else
            {
                result = new VMData(GetKoiStreamMapped(moduleBase));
            }
            return result;
        }

        private unsafe static void* GetKoiStreamMapped(byte* moduleBase)
        {
            byte* ptr = moduleBase + 0x3c;
            byte* ptr2;
            ptr = ptr2 = moduleBase + *(uint*)ptr;
            ptr += 0x6;
            ushort sectNum = *(ushort*)ptr;
            ptr += 14;
            ushort optSize = *(ushort*)ptr;
            ptr = ptr2 = ptr + 0x4 + optSize;

            byte* mdDir = moduleBase + *(uint*)(ptr - 16);
            byte* mdHdr = moduleBase + *(uint*)(mdDir + 8);
            mdHdr += 12;
            mdHdr += *(uint*)mdHdr;
            mdHdr = (byte*)(((ulong)mdHdr + 7) & ~3UL);
            mdHdr += 2;
            ushort numOfStream = *mdHdr;
            mdHdr += 2;
            var streamName = new StringBuilder();
            for (int i = 0; i < numOfStream; i++)
            {
                uint offset = *(uint*)mdHdr;
                uint len = *(uint*)(mdHdr + 4);
                mdHdr += 8;
                streamName.Length = 0;
                for (int ii = 0; ii < 8; ii++)
                {
                    streamName.Append((char)*mdHdr++);
                    if (*mdHdr == 0)
                    {
                        mdHdr += 3;
                        break;
                    }
                    streamName.Append((char)*mdHdr++);
                    if (*mdHdr == 0)
                    {
                        mdHdr += 2;
                        break;
                    }
                    streamName.Append((char)*mdHdr++);
                    if (*mdHdr == 0)
                    {
                        mdHdr += 1;
                        break;
                    }
                    streamName.Append((char)*mdHdr++);
                    if (*mdHdr == 0)
                        break;
                }
                if (streamName.ToString() == "#Koi")
                    return AllocateKoi(moduleBase + *(uint*)(mdDir + 8) + offset, len);
            }
            return null;
        }

        private unsafe static void* GetKoiStreamFlat(byte* moduleBase)
        {
            byte* ptr = moduleBase + 0x3c;
            byte* ptr2;
            ptr = ptr2 = moduleBase + *(uint*)ptr;
            ptr += 0x6;
            ushort sectNum = *(ushort*)ptr;
            ptr += 14;
            ushort optSize = *(ushort*)ptr;
            ptr = ptr2 = ptr + 0x4 + optSize;

            uint mdDir = *(uint*)(ptr - 16);

            var vAdrs = new uint[sectNum];
            var vSizes = new uint[sectNum];
            var rAdrs = new uint[sectNum];
            for (int i = 0; i < sectNum; i++)
            {
                vAdrs[i] = *(uint*)(ptr + 12);
                vSizes[i] = *(uint*)(ptr + 8);
                rAdrs[i] = *(uint*)(ptr + 20);
                ptr += 0x28;
            }

            for (int i = 0; i < sectNum; i++)
                if (vAdrs[i] <= mdDir && mdDir < vAdrs[i] + vSizes[i])
                {
                    mdDir = mdDir - vAdrs[i] + rAdrs[i];
                    break;
                }
            byte* mdDirPtr = moduleBase + mdDir;
            uint mdHdr = *(uint*)(mdDirPtr + 8);
            for (int i = 0; i < sectNum; i++)
                if (vAdrs[i] <= mdHdr && mdHdr < vAdrs[i] + vSizes[i])
                {
                    mdHdr = mdHdr - vAdrs[i] + rAdrs[i];
                    break;
                }


            byte* mdHdrPtr = moduleBase + mdHdr;
            mdHdrPtr += 12;
            mdHdrPtr += *(uint*)mdHdrPtr;
            mdHdrPtr = (byte*)(((ulong)mdHdrPtr + 7) & ~3UL);
            mdHdrPtr += 2;
            ushort numOfStream = *mdHdrPtr;
            mdHdrPtr += 2;
            var streamName = new StringBuilder();
            for (int i = 0; i < numOfStream; i++)
            {
                uint offset = *(uint*)mdHdrPtr;
                uint len = *(uint*)(mdHdrPtr + 4);
                streamName.Length = 0;
                mdHdrPtr += 8;
                for (int ii = 0; ii < 8; ii++)
                {
                    streamName.Append((char)*mdHdrPtr++);
                    if (*mdHdrPtr == 0)
                    {
                        mdHdrPtr += 3;
                        break;
                    }
                    streamName.Append((char)*mdHdrPtr++);
                    if (*mdHdrPtr == 0)
                    {
                        mdHdrPtr += 2;
                        break;
                    }
                    streamName.Append((char)*mdHdrPtr++);
                    if (*mdHdrPtr == 0)
                    {
                        mdHdrPtr += 1;
                        break;
                    }
                    streamName.Append((char)*mdHdrPtr++);
                    if (*mdHdrPtr == 0)
                        break;
                }
                if (streamName.ToString() == "#Koi")
                    return AllocateKoi(moduleBase + mdHdr + offset, len);
            }
            return null;
        }

        [DllImport("kernel32.dll")]
        private unsafe static extern void CopyMemory(void* dest, void* src, uint count);

        private unsafe static void* AllocateKoi(void* ptr, uint len)
        {
            var koi = (void*)Marshal.AllocHGlobal((int)len);
            CopyMemory(koi, ptr, len);
            return koi;
        }
    }
}
