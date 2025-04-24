namespace EPCVisual
{
    partial class FormMemoryAnalizer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lsw_registers = new ListView();
            label1 = new Label();
            lsb_memory = new ListBox();
            label2 = new Label();
            txb_address = new TextBox();
            SuspendLayout();
            // 
            // lsw_registers
            // 
            lsw_registers.GridLines = true;
            lsw_registers.Location = new Point(12, 27);
            lsw_registers.Name = "lsw_registers";
            lsw_registers.Size = new Size(244, 411);
            lsw_registers.TabIndex = 0;
            lsw_registers.UseCompatibleStateImageBehavior = false;
            lsw_registers.View = View.List;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 1;
            label1.Text = "Registers";
            // 
            // lsb_memory
            // 
            lsb_memory.FormattingEnabled = true;
            lsb_memory.Items.AddRange(new object[] { "" });
            lsb_memory.Location = new Point(262, 29);
            lsb_memory.Name = "lsb_memory";
            lsb_memory.Size = new Size(215, 409);
            lsb_memory.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(262, 9);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 3;
            label2.Text = "Memory";
            // 
            // txb_address
            // 
            txb_address.Location = new Point(320, 1);
            txb_address.MaxLength = 8;
            txb_address.Name = "txb_address";
            txb_address.PlaceholderText = "Address...";
            txb_address.Size = new Size(215, 23);
            txb_address.TabIndex = 4;
            txb_address.Text = "WORK IN PROGRESS";
            txb_address.Visible = false;
            txb_address.TextChanged += txb_address_TextChanged;
            // 
            // FormMemoryAnalizer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(489, 450);
            Controls.Add(txb_address);
            Controls.Add(label2);
            Controls.Add(lsb_memory);
            Controls.Add(label1);
            Controls.Add(lsw_registers);
            Name = "FormMemoryAnalizer";
            Text = "Memory and Registers";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lsw_registers;
        private Label label1;
        private ListBox lsb_memory;
        private Label label2;
        private TextBox txb_address;
    }
}