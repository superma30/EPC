using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUEmulator
{
    public static class GlobalData
    {
        public static readonly ushort DEFAULT_BITS = 8;
        public static readonly ushort DEFAULT_ADDRESS_BITS = 8;
        
        public static int Negate(int value, ushort bits)
        {
            string valBin = Convert.ToString(value, 2).PadLeft(bits, '0').Substring(Convert.ToString(value, 2).PadLeft(bits, '0').Length - bits, bits);

            valBin = valBin.Replace('1', '2');
            valBin = valBin.Replace('0', '1');
            valBin = valBin.Replace('2', '0');
            return Convert.ToInt32(valBin, 2);
        }
    }

    public class DictionaryWithDefault<TKey, TValue> : Dictionary<TKey, TValue>
    {
        TValue _default;
        public TValue DefaultValue
        {
            get { return _default; }
            set { _default = value; }
        }
        public DictionaryWithDefault() : base() { }
        public DictionaryWithDefault(TValue defaultValue) : base()
        {
            _default = defaultValue;
        }
        public new TValue this[TKey key]
        {
            get
            {
                TValue t;
                return base.TryGetValue(key, out t) ? t : _default;
            }
            set { base[key] = value; }
        }
    }
}