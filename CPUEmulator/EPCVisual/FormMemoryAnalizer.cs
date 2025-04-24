using CPUEmulator.External;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPCVisual
{
    public partial class FormMemoryAnalizer : Form
    {
        public RAM Memory;
        public Dictionary<string, int> Registers;
        private int currentMemoryIdx;
        public FormMemoryAnalizer(ref RAM memory, Dictionary<string, int> registers)
        {
            Memory = memory;
            Registers = registers;
            currentMemoryIdx = 0;
            InitializeComponent();
            Update();
        }

        public void Update(Dictionary<string, int>? registers = null)
        {
            if(registers != null)
            {
                Registers = registers;
            }
            lsw_registers.Items.Clear();
            lsb_memory.Items.Clear();
            foreach(var item in Registers)
            {
                lsw_registers.Items.Add(new ListViewItem(item.Key + "    " + Convert.ToString(item.Value, 2).PadLeft(8, '0')));
            }
            foreach(var line in Memory.GetMemoryReference())
            {
                lsb_memory.Items.Add($"#{line.Key}  {Convert.ToString(line.Value, 2).PadLeft(8, '0')}");
            }
        }

        private void txb_address_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
