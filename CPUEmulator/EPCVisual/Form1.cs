using CPUEmulator;
using CPUEmulator.Custom;
using CPUEmulator.Internal;
using EPCCompiler;

namespace EPCVisual
{
    public partial class Form1 : Form
    {
        public string CurrentSource = @"";
        public string AssembledSource = @"";
        public int CurrentInstruction = 0;
        public bool SourceInitialized = false;
        public EPC CurrentEPC = new EPC(8, 8);
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files|*.txt|All files|*.*";
        }

        private void btn_changeSource_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filepath = openFileDialog1.FileName;
                /*if () // Valid file
                {
                    // Change current source label
                    SourceInitialized = false;
                }*/
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (Path.Exists(AssembledSource))
            {
                CurrentEPC.RAM.SetAddress(0);
                CurrentEPC.RAM.Clear();
                CurrentEPC.RAM.LoadFromFile(AssembledSource);
            }
        }
    }
}
