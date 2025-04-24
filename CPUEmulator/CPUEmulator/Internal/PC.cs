using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUEmulator.Internal
{
    public class PC
    {
        int _internalReg = 0;
        int _bits;
        public PC(ushort bits = 0)
        {
            _bits = bits;
        }

        public void SetCurrentLine(int address)
        {
            if (!(address > Math.Pow(2, _bits) - 1))
            {
                _internalReg = address;
            }
        }

        public void Increment()
        {
            if (_internalReg+1 > Math.Pow(2, _bits)-1)
            {
                _internalReg = 0;
            }
            else
            {
                _internalReg = _internalReg+1;
            }
        }

        public int GetCurrentLine()
        {
            return _internalReg;
        }
    }
}
