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
            tabControl1 = new TabControl();
            tab_emulator = new TabPage();
            splitContainer1 = new SplitContainer();
            lbl_line = new Label();
            label1 = new Label();
            rtb_executionLog = new RichTextBox();
            toolStrip1 = new ToolStrip();
            btn_changeSource = new ToolStripButton();
            lbl_source = new ToolStripLabel();
            toolStripSeparator1 = new ToolStripSeparator();
            btn_start = new ToolStripButton();
            btn_stop = new ToolStripButton();
            btn_restart = new ToolStripButton();
            btn_next = new ToolStripButton();
            btn_nextAll = new ToolStripButton();
            pgb_progess = new ToolStripProgressBar();
            tab_compiler = new TabPage();
            openFileDialog1 = new OpenFileDialog();
            tabControl1.SuspendLayout();
            tab_emulator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            toolStrip1.SuspendLayout();
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
            tabControl1.Size = new Size(933, 519);
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
            tab_emulator.Size = new Size(925, 491);
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
            splitContainer1.Panel2.Controls.Add(rtb_executionLog);
            splitContainer1.Size = new Size(917, 459);
            splitContainer1.SplitterDistance = 46;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 1;
            // 
            // lbl_line
            // 
            lbl_line.AutoSize = true;
            lbl_line.Location = new Point(91, 14);
            lbl_line.Margin = new Padding(4, 0, 4, 0);
            lbl_line.Name = "lbl_line";
            lbl_line.Size = new Size(0, 15);
            lbl_line.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(12, 14);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 0;
            label1.Text = "Current Line: ";
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
            rtb_executionLog.Size = new Size(917, 408);
            rtb_executionLog.TabIndex = 0;
            rtb_executionLog.Text = "aaaa";
            rtb_executionLog.WordWrap = false;
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btn_changeSource, lbl_source, toolStripSeparator1, btn_start, btn_stop, btn_restart, btn_next, btn_nextAll, pgb_progess });
            toolStrip1.Location = new Point(4, 3);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RenderMode = ToolStripRenderMode.System;
            toolStrip1.Size = new Size(917, 26);
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
            btn_start.ToolTipText = "Start";
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
            // 
            // btn_restart
            // 
            btn_restart.BackgroundImage = Properties.Resources.restart_icon;
            btn_restart.BackgroundImageLayout = ImageLayout.Stretch;
            btn_restart.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_restart.ImageTransparentColor = Color.Magenta;
            btn_restart.Name = "btn_restart";
            btn_restart.Size = new Size(23, 23);
            btn_restart.Text = "toolStripButton1";
            btn_restart.ToolTipText = "Restart";
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
            // 
            // pgb_progess
            // 
            pgb_progess.AutoSize = false;
            pgb_progess.AutoToolTip = true;
            pgb_progess.Name = "pgb_progess";
            pgb_progess.Size = new Size(117, 23);
            pgb_progess.ToolTipText = "Code Execution Progress";
            // 
            // tab_compiler
            // 
            tab_compiler.Location = new Point(4, 24);
            tab_compiler.Margin = new Padding(4, 3, 4, 3);
            tab_compiler.Name = "tab_compiler";
            tab_compiler.Padding = new Padding(4, 3, 4, 3);
            tab_compiler.Size = new Size(925, 491);
            tab_compiler.TabIndex = 1;
            tab_compiler.Text = "Compiler";
            tab_compiler.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Title = "Select Source";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(tabControl1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tab_emulator.ResumeLayout(false);
            tab_emulator.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton btn_restart;
        private System.Windows.Forms.ToolStripButton btn_next;
        private System.Windows.Forms.ToolStripButton btn_nextAll;
        private System.Windows.Forms.ToolStripProgressBar pgb_progess;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_line;
        private System.Windows.Forms.RichTextBox rtb_executionLog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
