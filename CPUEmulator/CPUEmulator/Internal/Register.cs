using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUEmulator.Internal
{
    public class Register
    {
        private ushort _bits;
        private int _data = 0b0;
        public Register(ushort bits = 0)
        {
            if (bits == 0)
            {
                _bits = GlobalData.DEFAULT_BITS;
            }
            else
            {
                _bits = bits;
            }
        }
        public void Write(int newData)
        {
            if(newData < -Math.Pow(2, _bits-1) || newData > Math.Pow(2, _bits-1) - 1)
            {
                throw new ArgumentOutOfRangeException($"Data:{newData}/{Convert.ToString(newData, 2)},Bits:{_bits}");
            }
            else
            {
                _data = newData;
            }
        }
        public int Get()
        {
            return _data;
        }
    }
}
