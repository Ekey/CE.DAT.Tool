using System;
using System.IO;
using System.Collections.Generic;

namespace CE.Unpacker
{
    class DatHashList
    {
        static String m_Path = Utils.iGetApplicationPath() + @"\Projects\";
        static String m_ProjectFile = "FileNames.list";
        static String m_ProjectFilePath = m_Path + m_ProjectFile;
        static Dictionary<UInt32, String> m_HashList = new Dictionary<UInt32, String>();

        public static void iLoadProject()
        {
            String m_Line = null;
            if (!File.Exists(m_ProjectFilePath))
            {
                Utils.iSetError("[ERROR]: Unable to load project file " + m_ProjectFile);
            }

            Int32 i = 0;
            m_HashList.Clear();

            StreamReader TProjectFile = new StreamReader(m_ProjectFilePath);
            while ((m_Line = TProjectFile.ReadLine()) != null)
            {
                UInt32 dwHashLower = DatHash.iGetHash(m_Line.ToLower());
                UInt32 dwHashUpper = DatHash.iGetHash(m_Line.ToUpper());

                if (m_HashList.ContainsKey(dwHashLower))
                {
                    String m_Collision = null;
                    m_HashList.TryGetValue(dwHashLower, out m_Collision);
                    Console.WriteLine("[COLLISION]: at line {0} => {1} <-> {2}", i, m_Collision, m_Line);
                }

                m_HashList.Add(dwHashLower, m_Line);

                if (m_HashList.ContainsKey(dwHashUpper))
                {
                    String m_Collision = null;
                    m_HashList.TryGetValue(dwHashUpper, out m_Collision);
                    Console.WriteLine("[COLLISION]: at line {0} => {1} <-> {2}", i, m_Collision, m_Line);
                }

                m_HashList.Add(dwHashUpper, m_Line);

                i++;
            }

            TProjectFile.Close();
            Console.WriteLine("[INFO]: Project File Loaded: {0}", i);
            Console.WriteLine();
        }

        public static String iGetNameFromHashList(UInt32 dwHash)
        {
            String m_FileName = null;

            if (m_HashList.ContainsKey(dwHash))
            {
                m_HashList.TryGetValue(dwHash, out m_FileName);
            }
            else
            {
                m_FileName = @"__Unknown\" + dwHash.ToString("X8");
            }

            return m_FileName;
        }
    }
}
