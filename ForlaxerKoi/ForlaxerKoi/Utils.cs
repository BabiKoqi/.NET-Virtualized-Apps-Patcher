using System;

namespace ForlaxerKoi
{
	// Token: 0x02000009 RID: 9
	internal static class Utils
	{
        // Token: 0x0600000F RID: 15 RVA: 0x0000264C File Offset: 0x0000084C
        public unsafe static uint ReadCompressedUInt(ref byte* ptr)
        {
            uint num = 0;
            int shift = 0;
            do
            {
                num |= (*ptr & 0x7fu) << shift;
                shift += 7;
            } while ((*ptr++ & 0x80) != 0);
            return num;
        }

        // Token: 0x06000010 RID: 16 RVA: 0x00002690 File Offset: 0x00000890
        public static uint FromCodedToken(uint codedToken)
		{
			uint num = codedToken >> 3;
			uint result;
			switch (codedToken & 7u)
			{
			case 1u:
				result = (num | 33554432u);
				break;
			case 2u:
				result = (num | 16777216u);
				break;
			case 3u:
				result = (num | 452984832u);
				break;
			case 4u:
				result = (num | 167772160u);
				break;
			case 5u:
				result = (num | 100663296u);
				break;
			case 6u:
				result = (num | 67108864u);
				break;
			case 7u:
				result = (num | 721420288u);
				break;
			default:
				result = num;
				break;
			}
			return result;
		}
	}
}
