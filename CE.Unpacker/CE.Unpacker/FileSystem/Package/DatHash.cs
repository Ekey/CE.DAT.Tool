using System;

namespace CE.Unpacker
{
    class DatHash
    {
        public static UInt32 iGetHash(String m_String)
        {
            Int32 j = 0;
            Int32 dwHash = 1;
            Byte bCounter = 1;
            Int32 dwBlocks = 8 * m_String.Length;

            if (0 < dwBlocks)
            {
                for (Int32 i = 0; i < dwBlocks; i++)
                {
                    Boolean A = (dwHash & 0x200000) != 0;
                    Boolean B = (dwHash & 2) != 0;
                    Boolean C = Convert.ToBoolean(dwHash & 1);
                    Boolean D = Convert.ToBoolean(dwHash < 0);

                    dwHash *= 2;

                    Boolean X = Convert.ToBoolean(m_String[j] & bCounter);

                    if (D ^ (A ^ B ^ C ^ (X != false)))
                    {
                        dwHash |= 1;
                    }

                    bCounter *= 2;
                    if (!Convert.ToBoolean(bCounter))
                    {
                        ++j;
                        bCounter = 1;
                    }
                }
            }
            return (UInt32)dwHash;
        }
    }
}
