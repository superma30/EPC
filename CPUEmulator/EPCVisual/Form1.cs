using CPUEmulator;
using CPUEmulator.Custom;
using CPUEmulator.Internal;
using EPCCompiler;
using System.Text.RegularExpressions;

namespace EPCVisual
{
    public partial class Form1 : Form
    {
        public string SourceCode = "";
        public string AssembledCode = "";
        public string BinCode = "";
        public int programLength = 0;
        public int CurrentInstruction = 0;
        public bool SourceInitialized = false;
        public EPC CurrentEPC = new EPC(8, 8);
        FormMemoryAnalizer memoryAnalizer;
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files|*.txt|All files|*.*";
        }

        private string getInstructionLogString(string fetchedInstr)
        {
            string appString = $"Instruction #{CurrentEPC.PC.GetCurrentLine()}: {fetchedInstr.Substring(0, 4)} {fetchedInstr.Substring(4, 8)}";
            if (fetchedInstr.Substring(0, 4) == "1000" || fetchedInstr.Substring(0, 4) == "0110")
            {
                appString += $"  |  {new MachineCodeAssembler().GetFromBin(0, fetchedInstr.Substring(0, 4))} {new MachineCodeAssembler().GetFromBin(2, fetchedInstr.Substring(4, 8))}\n";
            }
            else if (fetchedInstr.Substring(0, 4) == "1001")
            {
                appString += $"  |  {new MachineCodeAssembler().GetFromBin(0, fetchedInstr.Substring(0, 4))} {Convert.ToInt32(fetchedInstr.Substring(4, 8), 2)}\n";
            }
            else if (fetchedInstr.Substring(0, 4) == "0010" || fetchedInstr.Substring(0, 4) == "0011")
            {
                appString += $"  |  {new MachineCodeAssembler().GetFromBin(0, fetchedInstr.Substring(0, 4))} {new MachineCodeAssembler().GetFromBin(1, fetchedInstr.Substring(4, 8))}\n";
            }
            else
            {
                appString += $"  |  {new MachineCodeAssembler().GetFromBin(0, fetchedInstr.Substring(0, 4))}\n";
            }
            return appString;
        }

        private void btn_changeSource_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filepath = openFileDialog1.FileName;
                var code = File.ReadAllText(filepath);
                rtb_code.Text = code;
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            btn_start.BackColor = Color.FromArgb(0, 0, 0, 0);

            SourceCode = rtb_code.Text;

            CurrentEPC = new(8, 8);

            MachineCodeAssembler es = new();
            var lexer = new EPCCompiler.Lexer();
            var parser = new EPCCompiler.Parser();
            var compiler = new EPCCompiler.Compiler();
            string lla;
            try
            {
                var tokens = lexer.Tokenize(SourceCode);
                var ast = parser.Parse(tokens);
                lla = compiler.Compile(ast);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unknown compilation error occurred, check the source code ({ex.Message})", "Compilation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (lla.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Last() != "STP")
            {
                MessageBox.Show($"An unknown compilation error occurred (STP not at the end)", "Compilation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AssembledCode = lla;
            SourceInitialized = false;
            rtb_assembledCode.Text = AssembledCode;

            MachineCodeAssembler mcs = new();
            BinCode = mcs.From_low_To_Bin_String(AssembledCode);

            if (this.SourceCode == "")
            {
                return;
            }
            if (BinCode != "")
            {
                CurrentEPC.RAM.SetAddress(0);
                CurrentEPC.RAM.Clear();
                CurrentEPC.RAM.LoadFromString(BinCode);
            }

            CurrentEPC.ProgramCache.TransferDirectlyFullData(ref CurrentEPC.RAM.GetMemoryReference());

            rtb_executionLog.Text = "";
            SourceInitialized = true;
            pgb_progess.Maximum = 100;
            pgb_progess.Value = 0;
            programLength = BinCode.Count(i => i == '\n');

            rtb_executionLog.Text += "Program Loaded into cache\n";
            CurrentInstruction = 0;
            CurrentEPC.PC.SetCurrentLine(CurrentInstruction);
            if (programLength != 0)
            {
                pgb_progess.Value = (int)(((float)CurrentInstruction / (float)programLength) * 100);
            }

            if (!(ReferenceEquals(memoryAnalizer, null) || memoryAnalizer.IsDisposed))
            {
                memoryAnalizer.Update(CurrentEPC.GetRegisters());
            }
        }

        private void btn_nextAll_Click(object sender, EventArgs e)
        {
            if (!SourceInitialized)
            {
                return;
            }
            string fetched = CurrentEPC.CU.InstructionFetch();
            rtb_executionLog.Text += getInstructionLogString(fetched);
            lbl_line.Text = $"#{CurrentInstruction}";
            while (CurrentEPC.CU.ExecuteInstruction(CurrentEPC.CU.DecodeInstruction(CurrentEPC.CU.InstructionSplit(CurrentEPC.CU.InstructionFetch()))))
            {
                CurrentEPC.PC.Increment();
                CurrentInstruction = (int)CurrentEPC.PC.GetCurrentLine();
                if (programLength != 0)
                {
                    pgb_progess.Value = (int)(Math.Round((float)CurrentInstruction / (float)programLength) * 100);
                }
                if (!(ReferenceEquals(memoryAnalizer, null) || memoryAnalizer.IsDisposed))
                {
                    memoryAnalizer.Update(CurrentEPC.GetRegisters());
                }
                fetched = CurrentEPC.CU.InstructionFetch();
                rtb_executionLog.Text += getInstructionLogString(fetched);
                lbl_line.Text = $"#{CurrentInstruction}";
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (!SourceInitialized)
            {
                return;
            }
            string fetched = CurrentEPC.CU.InstructionFetch();
            rtb_executionLog.Text += getInstructionLogString(fetched);
            lbl_line.Text = $"#{CurrentInstruction}";
            if (CurrentEPC.CU.ExecuteInstruction(CurrentEPC.CU.DecodeInstruction(CurrentEPC.CU.InstructionSplit(CurrentEPC.CU.InstructionFetch()))))
            {
                CurrentEPC.PC.Increment();
                CurrentInstruction = (int)CurrentEPC.PC.GetCurrentLine();
                if (programLength != 0)
                {
                    pgb_progess.Value = (int)(((float)CurrentInstruction / (float)programLength) * 100);
                }
                if (!(ReferenceEquals(memoryAnalizer, null) || memoryAnalizer.IsDisposed))
                {
                    memoryAnalizer.Update(CurrentEPC.GetRegisters());
                }
            }
            else
            {
                SourceInitialized = false;
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            SourceInitialized = false;
            if (programLength != 0)
            {
                pgb_progess.Value = (int)(((float)CurrentInstruction / (float)programLength) * 100);
            }
            if (!(ReferenceEquals(memoryAnalizer, null) || memoryAnalizer.IsDisposed))
            {
                memoryAnalizer.Update(CurrentEPC.GetRegisters());
            }
        }

        private void btn_memoryAnalizer_Click(object sender, EventArgs e)
        {
            if (ReferenceEquals(memoryAnalizer, null) || memoryAnalizer.IsDisposed)
            {
                memoryAnalizer = new FormMemoryAnalizer(ref CurrentEPC.RAM, CurrentEPC.GetRegisters());
            }
            memoryAnalizer.Show();
        }

        private void rtb_code_TextChanged(object sender, EventArgs e)
        {
            btn_start.BackColor = Color.FromArgb(192, 192, 255);
        }
    }
}
