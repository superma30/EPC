namespace EPCVisual
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tabControl1 = new TabControl();
            tab_emulator = new TabPage();
            splitContainer1 = new SplitContainer();
            lbl_line = new Label();
            label1 = new Label();
            splitContainer2 = new SplitContainer();
            splitContainer3 = new SplitContainer();
            label3 = new Label();
            rtb_code = new RichTextBox();
            label5 = new Label();
            rtb_assembledCode = new RichTextBox();
            label4 = new Label();
            rtb_executionLog = new RichTextBox();
            toolStrip1 = new ToolStrip();
            btn_changeSource = new ToolStripButton();
            lbl_source = new ToolStripLabel();
            toolStripSeparator1 = new ToolStripSeparator();
            btn_start = new ToolStripButton();
            btn_stop = new ToolStripButton();
            btn_next = new ToolStripButton();
            btn_nextAll = new ToolStripButton();
            btn_memoryAnalizer = new ToolStripButton();
            pgb_progess = new ToolStripProgressBar();
            toolStripButton1 = new ToolStripButton();
            tab_compiler = new TabPage();
            tlp_compile = new TableLayoutPanel();
            rtb_code_compiler = new RichTextBox();
            rtb_code_bin = new RichTextBox();
            lbl_compiler_message = new Label();
            openFileDialog1 = new OpenFileDialog();
            tabControl1.SuspendLayout();
            tab_emulator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            toolStrip1.SuspendLayout();
            tab_compiler.SuspendLayout();
            tlp_compile.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tab_emulator);
            tabControl1.Controls.Add(tab_compiler);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(984, 561);
            tabControl1.TabIndex = 0;
            // 
            // tab_emulator
            // 
            tab_emulator.Controls.Add(splitContainer1);
            tab_emulator.Controls.Add(toolStrip1);
            tab_emulator.Location = new Point(4, 24);
            tab_emulator.Margin = new Padding(4, 3, 4, 3);
            tab_emulator.Name = "tab_emulator";
            tab_emulator.Padding = new Padding(4, 3, 4, 3);
            tab_emulator.Size = new Size(976, 533);
            tab_emulator.TabIndex = 0;
            tab_emulator.Text = "Emulator";
            tab_emulator.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(4, 29);
            splitContainer1.Margin = new Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lbl_line);
            splitContainer1.Panel1.Controls.Add(label1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(968, 501);
            splitContainer1.SplitterDistance = 43;
            splitContainer1.TabIndex = 1;
            // 
            // lbl_line
            // 
            lbl_line.AutoSize = true;
            lbl_line.Location = new Point(95, 11);
            lbl_line.Margin = new Padding(4, 0, 4, 0);
            lbl_line.Name = "lbl_line";
            lbl_line.Size = new Size(0, 15);
            lbl_line.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 0;
            label1.Text = "Current Line:";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(label4);
            splitContainer2.Panel2.Controls.Add(rtb_executionLog);
            splitContainer2.Size = new Size(968, 454);
            splitContainer2.SplitterDistance = 500;
            splitContainer2.TabIndex = 2;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(label3);
            splitContainer3.Panel1.Controls.Add(rtb_code);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(label5);
            splitContainer3.Panel2.Controls.Add(rtb_assembledCode);
            splitContainer3.Size = new Size(500, 454);
            splitContainer3.SplitterDistance = 157;
            splitContainer3.TabIndex = 4;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(122, 0);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 3;
            label3.Text = "Code";
            // 
            // rtb_code
            // 
            rtb_code.BackColor = Color.Black;
            rtb_code.BorderStyle = BorderStyle.None;
            rtb_code.Cursor = Cursors.IBeam;
            rtb_code.Dock = DockStyle.Fill;
            rtb_code.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtb_code.ForeColor = Color.White;
            rtb_code.Location = new Point(0, 0);
            rtb_code.Margin = new Padding(4, 3, 4, 3);
            rtb_code.Name = "rtb_code";
            rtb_code.ShowSelectionMargin = true;
            rtb_code.Size = new Size(157, 454);
            rtb_code.TabIndex = 2;
            rtb_code.Text = "";
            rtb_code.WordWrap = false;
            rtb_code.TextChanged += rtb_code_TextChanged;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(243, 0);
            label5.Name = "label5";
            label5.Size = new Size(96, 15);
            label5.TabIndex = 4;
            label5.Text = "Assembled Code";
            // 
            // rtb_assembledCode
            // 
            rtb_assembledCode.BackColor = Color.Black;
            rtb_assembledCode.BorderStyle = BorderStyle.None;
            rtb_assembledCode.Cursor = Cursors.IBeam;
            rtb_assembledCode.Dock = DockStyle.Fill;
            rtb_assembledCode.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtb_assembledCode.ForeColor = Color.White;
            rtb_assembledCode.Location = new Point(0, 0);
            rtb_assembledCode.Margin = new Padding(4, 3, 4, 3);
            rtb_assembledCode.Name = "rtb_assembledCode";
            rtb_assembledCode.ReadOnly = true;
            rtb_assembledCode.ShowSelectionMargin = true;
            rtb_assembledCode.Size = new Size(339, 454);
            rtb_assembledCode.TabIndex = 3;
            rtb_assembledCode.Text = "";
            rtb_assembledCode.WordWrap = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(382, 0);
            label4.Name = "label4";
            label4.Size = new Size(81, 15);
            label4.TabIndex = 2;
            label4.Text = "Execution Log";
            // 
            // rtb_executionLog
            // 
            rtb_executionLog.BackColor = Color.Black;
            rtb_executionLog.BorderStyle = BorderStyle.None;
            rtb_executionLog.Cursor = Cursors.IBeam;
            rtb_executionLog.Dock = DockStyle.Fill;
            rtb_executionLog.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtb_executionLog.ForeColor = Color.White;
            rtb_executionLog.Location = new Point(0, 0);
            rtb_executionLog.Margin = new Padding(4, 3, 4, 3);
            rtb_executionLog.Name = "rtb_executionLog";
            rtb_executionLog.ReadOnly = true;
            rtb_executionLog.ShowSelectionMargin = true;
            rtb_executionLog.Size = new Size(464, 454);
            rtb_executionLog.TabIndex = 1;
            rtb_executionLog.Text = "";
            rtb_executionLog.WordWrap = false;
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btn_changeSource, lbl_source, toolStripSeparator1, btn_start, btn_stop, btn_next, btn_nextAll, btn_memoryAnalizer, pgb_progess, toolStripButton1 });
            toolStrip1.Location = new Point(4, 3);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RenderMode = ToolStripRenderMode.System;
            toolStrip1.Size = new Size(968, 26);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btn_changeSource
            // 
            btn_changeSource.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_changeSource.Image = Properties.Resources.open_icon;
            btn_changeSource.ImageTransparentColor = Color.Magenta;
            btn_changeSource.Name = "btn_changeSource";
            btn_changeSource.Size = new Size(23, 23);
            btn_changeSource.Text = "Change Source";
            btn_changeSource.Click += btn_changeSource_Click;
            // 
            // lbl_source
            // 
            lbl_source.AutoToolTip = true;
            lbl_source.Name = "lbl_source";
            lbl_source.Size = new Size(109, 23);
            lbl_source.Text = "No Source Selected";
            lbl_source.ToolTipText = "Current Source";
            lbl_source.Click += btn_changeSource_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 26);
            // 
            // btn_start
            // 
            btn_start.BackgroundImageLayout = ImageLayout.Zoom;
            btn_start.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_start.Image = Properties.Resources.start_icon;
            btn_start.ImageTransparentColor = Color.Magenta;
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(23, 23);
            btn_start.Text = "toolStripButton1";
            btn_start.ToolTipText = "Compile";
            btn_start.Click += btn_start_Click;
            // 
            // btn_stop
            // 
            btn_stop.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_stop.Image = Properties.Resources.stop_icon;
            btn_stop.ImageTransparentColor = Color.Magenta;
            btn_stop.Name = "btn_stop";
            btn_stop.Size = new Size(23, 23);
            btn_stop.Text = "toolStripButton1";
            btn_stop.ToolTipText = "Stop";
            btn_stop.Click += btn_stop_Click;
            // 
            // btn_next
            // 
            btn_next.BackgroundImage = Properties.Resources.next_icon;
            btn_next.BackgroundImageLayout = ImageLayout.Zoom;
            btn_next.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_next.ImageTransparentColor = Color.Magenta;
            btn_next.Name = "btn_next";
            btn_next.Size = new Size(23, 23);
            btn_next.Text = "toolStripButton1";
            btn_next.ToolTipText = "Next";
            btn_next.Click += btn_next_Click;
            // 
            // btn_nextAll
            // 
            btn_nextAll.AutoSize = false;
            btn_nextAll.BackgroundImage = Properties.Resources.next_all_icon;
            btn_nextAll.BackgroundImageLayout = ImageLayout.Stretch;
            btn_nextAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_nextAll.ImageTransparentColor = Color.Magenta;
            btn_nextAll.Name = "btn_nextAll";
            btn_nextAll.Size = new Size(23, 22);
            btn_nextAll.Text = "toolStripButton1";
            btn_nextAll.ToolTipText = "Next all";
            btn_nextAll.Click += btn_nextAll_Click;
            // 
            // btn_memoryAnalizer
            // 
            btn_memoryAnalizer.BackgroundImageLayout = ImageLayout.Zoom;
            btn_memoryAnalizer.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_memoryAnalizer.Image = Properties.Resources.memory_icon;
            btn_memoryAnalizer.ImageTransparentColor = Color.Magenta;
            btn_memoryAnalizer.Name = "btn_memoryAnalizer";
            btn_memoryAnalizer.Size = new Size(23, 23);
            btn_memoryAnalizer.Text = "toolStripButton1";
            btn_memoryAnalizer.ToolTipText = "Open Memory Analizer";
            btn_memoryAnalizer.Click += btn_memoryAnalizer_Click;
            // 
            // pgb_progess
            // 
            pgb_progess.AutoSize = false;
            pgb_progess.AutoToolTip = true;
            pgb_progess.Name = "pgb_progess";
            pgb_progess.Size = new Size(117, 23);
            pgb_progess.ToolTipText = "Code Execution Progress";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 23);
            toolStripButton1.Text = "toolStripButton1";
            toolStripButton1.Visible = false;
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // tab_compiler
            // 
            tab_compiler.Controls.Add(tlp_compile);
            tab_compiler.Controls.Add(lbl_compiler_message);
            tab_compiler.Location = new Point(4, 24);
            tab_compiler.Margin = new Padding(4, 3, 4, 3);
            tab_compiler.Name = "tab_compiler";
            tab_compiler.Padding = new Padding(4, 3, 4, 3);
            tab_compiler.Size = new Size(976, 533);
            tab_compiler.TabIndex = 1;
            tab_compiler.Text = "Compiler";
            tab_compiler.UseVisualStyleBackColor = true;
            tab_compiler.Enter += tab_compiler_Enter;
            // 
            // tlp_compile
            // 
            tlp_compile.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tlp_compile.ColumnCount = 2;
            tlp_compile.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_compile.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_compile.Controls.Add(rtb_code_compiler, 0, 0);
            tlp_compile.Controls.Add(rtb_code_bin, 1, 0);
            tlp_compile.Location = new Point(0, 30);
            tlp_compile.Name = "tlp_compile";
            tlp_compile.RowCount = 1;
            tlp_compile.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_compile.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlp_compile.Size = new Size(973, 500);
            tlp_compile.TabIndex = 4;
            // 
            // rtb_code_compiler
            // 
            rtb_code_compiler.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtb_code_compiler.BackColor = Color.Black;
            rtb_code_compiler.BorderStyle = BorderStyle.None;
            rtb_code_compiler.Cursor = Cursors.IBeam;
            rtb_code_compiler.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtb_code_compiler.ForeColor = Color.White;
            rtb_code_compiler.Location = new Point(4, 3);
            rtb_code_compiler.Margin = new Padding(4, 3, 4, 3);
            rtb_code_compiler.Name = "rtb_code_compiler";
            rtb_code_compiler.ShowSelectionMargin = true;
            rtb_code_compiler.Size = new Size(478, 494);
            rtb_code_compiler.TabIndex = 4;
            rtb_code_compiler.Text = "";
            rtb_code_compiler.WordWrap = false;
            // 
            // rtb_code_bin
            // 
            rtb_code_bin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtb_code_bin.BackColor = Color.Black;
            rtb_code_bin.BorderStyle = BorderStyle.None;
            rtb_code_bin.Cursor = Cursors.IBeam;
            rtb_code_bin.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtb_code_bin.ForeColor = Color.White;
            rtb_code_bin.Location = new Point(490, 3);
            rtb_code_bin.Margin = new Padding(4, 3, 4, 3);
            rtb_code_bin.Name = "rtb_code_bin";
            rtb_code_bin.ShowSelectionMargin = true;
            rtb_code_bin.Size = new Size(479, 494);
            rtb_code_bin.TabIndex = 3;
            rtb_code_bin.Text = "";
            rtb_code_bin.WordWrap = false;
            // 
            // lbl_compiler_message
            // 
            lbl_compiler_message.Location = new Point(3, 3);
            lbl_compiler_message.Name = "lbl_compiler_message";
            lbl_compiler_message.Size = new Size(970, 24);
            lbl_compiler_message.TabIndex = 0;
            lbl_compiler_message.Text = "The compiler has now been integrated into the previous window, so this one is pretty useless";
            // 
            // openFileDialog1
            // 
            openFileDialog1.Title = "Select Source";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(tabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EPC Emulator and Compiler";
            tabControl1.ResumeLayout(false);
            tab_emulator.ResumeLayout(false);
            tab_emulator.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel1.PerformLayout();
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tab_compiler.ResumeLayout(false);
            tlp_compile.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_emulator;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabPage tab_compiler;
        private System.Windows.Forms.ToolStripButton btn_changeSource;
        private System.Windows.Forms.ToolStripLabel lbl_source;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btn_start;
        private System.Windows.Forms.ToolStripButton btn_stop;
        private System.Windows.Forms.ToolStripButton btn_next;
        private System.Windows.Forms.ToolStripButton btn_nextAll;
        private System.Windows.Forms.ToolStripProgressBar pgb_progess;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_line;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Label lbl_compiler_message;
        private SplitContainer splitContainer2;
        private RichTextBox rtb_code;
        private RichTextBox rtb_executionLog;
        private Label label3;
        private Label label4;
        private SplitContainer splitContainer3;
        private RichTextBox rtb_assembledCode;
        private Label label5;
        private ToolStripButton btn_memoryAnalizer;
        private ToolStripButton toolStripButton1;
        private TableLayoutPanel tlp_compile;
        private RichTextBox rtb_code_compiler;
        private RichTextBox rtb_code_bin;
    }
}
