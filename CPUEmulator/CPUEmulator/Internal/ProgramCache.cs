using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CPUEmulator.Internal
{
    // Used for ease-of-programming but could be removed and use the RAM instead
    public class ProgramCache
    {
        // Phisical Methods
        private ushort _bits;
        private ushort _addressBits;
        private DictionaryWithDefault<uint, int> _memory = new DictionaryWithDefault<uint, int>(0);
        private uint _idx = 0;
        public ProgramCache(ushort addressBits = 0, ushort memoryBits = 0)
        {
            if (memoryBits == 0)
            {
                _bits = GlobalData.DEFAULT_BITS;
            }
            else
            {
                _bits = memoryBits;
            }
            if(addressBits == 0)
            {
                _addressBits = GlobalData.DEFAULT_ADDRESS_BITS;
            }
            else
            {
                _addressBits = addressBits;
            }
        }
        public void SetAddress(uint address)
        {
            if (address < -Math.Pow(2, _addressBits - 1) || address > Math.Pow(2, _addressBits-1)-1)
            {
                throw new IndexOutOfRangeException($"Address:{address}/{Convert.ToString(address, 2)},AddressBits:{_addressBits}");
            }
            else
            {
                _idx = address;
            }
        }
        public void Write(int newData)
        {
            if (newData < -Math.Pow(2, _bits - 1) || newData > Math.Pow(2, _bits - 1) - 1)
            {
                throw new ArgumentOutOfRangeException($"Data:{newData}/{Convert.ToString(newData, 2)},Bits:{_bits}");
            }
            else
            {
                _memory[_idx] = newData;
            }
        }
        public int Get()
        {
            return _memory[_idx];
        }

        // Abstract Methods
        public void Clear()
        {
            _memory.Clear();
        }

        public void TransferDirectlyFullData(ref Dictionary<uint, int> dataPointer)
        {
            foreach(var item in dataPointer)
            {
                _memory[item.Key] = item.Value;
            }
        }
    }
}
