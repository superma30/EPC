using CPUEmulator.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUEmulator.Internal
{
    public class MMU
    {
        Dictionary<uint, uint> _addressConversionTable = new Dictionary<uint, uint>();
        private RAM _ram;
        private HardDriveDisk _hdd;

        public MMU(ref RAM ram, ref HardDriveDisk HDD)
        {
            _ram = ram;
            _hdd = HDD;
        }

        public uint ConvertLogicAddress(uint logicAddress)
        {
            if (_addressConversionTable.ContainsKey(logicAddress))
            {
                return _addressConversionTable[logicAddress];
            }
            else
            {
                // Idk
                return logicAddress;
            }
        }
    }
}
