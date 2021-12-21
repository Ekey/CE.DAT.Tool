using System;

namespace CE.Unpacker
{
    class DatEntry
    {
        public UInt32 dwHash { get; set; }
        public UInt32 dwOffset { get; set; }
        public Int32 dwSize { get; set; }
    }
}
