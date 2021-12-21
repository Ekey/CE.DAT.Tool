using System;
using System.IO;
using System.Collections.Generic;

namespace CE.Unpacker
{
    class DatUnpack
    {
        static List<DatEntry> m_EntryTable = new List<DatEntry>();

        public static void iDoIt(String m_Archive, String m_DstFolder)
        {
            DatHashList.iLoadProject();
            using (FileStream TDatStream = File.OpenRead(m_Archive))
            {
                m_EntryTable.Clear();

                for (;;)
                {
                    UInt32 dwHash = TDatStream.ReadUInt32();
                    UInt32 dwOffset = TDatStream.ReadUInt32();
                    Int32 dwSize = TDatStream.ReadInt32();

                    if (dwHash == 0)
                    {
                        break;
                    }

                    var TEntry = new DatEntry
                    {
                        dwHash = dwHash,
                        dwOffset = dwOffset,
                        dwSize = dwSize,
                    };

                    m_EntryTable.Add(TEntry);
                }

                TDatStream.Dispose();
            }

            foreach (var m_Entry in m_EntryTable)
            {
                String m_FileName = DatHashList.iGetNameFromHashList(m_Entry.dwHash);
                String m_FullPath = m_DstFolder + m_FileName;

                Utils.iSetInfo("[UNPACKING]: " + m_FileName);
                Utils.iCreateDirectory(m_FullPath);

                DatHelpers.ReadWriteFile(m_Archive, m_FullPath, m_Entry.dwOffset, m_Entry.dwSize);
            }
        }
    }
}
