using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUEmulator.External
{
    public class HardDriveDisk
    {
        // Phisical Methods
        private ushort _bits;
        private uint _capacity;
        private DictionaryWithDefault<uint, int> _memory = new DictionaryWithDefault<uint, int>(0);
        private uint _idx = 0;
        public HardDriveDisk(uint capacity, ushort bits = 0)
        {
            _capacity = capacity;
            if (bits == 0)
            {
                _bits = GlobalData.DEFAULT_BITS;
            }
            else
            {
                _bits = bits;
            }
        }
        public void SetAddress(uint address)
        {
            _idx = address;
        }
        public void Write(int newData)
        {
            _memory[_idx] = newData;
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
            foreach (var item in dataPointer)
            {
                _memory[item.Key] = item.Value;
            }
        }

        public void LoadFromFile(string filepath)
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                uint idx = _idx;
                while (!sr.EndOfStream)
                {
                    string data = sr.ReadLine().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    _memory[idx] = Convert.ToInt32(data, 2);
                    idx++;
                }
            }
        }
    }
}
