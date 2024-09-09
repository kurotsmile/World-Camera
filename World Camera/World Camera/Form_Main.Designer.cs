namespace World_Camera
{
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            listBox_ip_scan = new ListBox();
            button_scan_ip = new Button();
            SuspendLayout();
            // 
            // listBox_ip_scan
            // 
            listBox_ip_scan.FormattingEnabled = true;
            listBox_ip_scan.ItemHeight = 15;
            listBox_ip_scan.Location = new Point(12, 12);
            listBox_ip_scan.Name = "listBox_ip_scan";
            listBox_ip_scan.Size = new Size(386, 334);
            listBox_ip_scan.TabIndex = 0;
            // 
            // button_scan_ip
            // 
            button_scan_ip.Location = new Point(12, 361);
            button_scan_ip.Name = "button_scan_ip";
            button_scan_ip.Size = new Size(140, 43);
            button_scan_ip.TabIndex = 1;
            button_scan_ip.Text = "Scan";
            button_scan_ip.UseVisualStyleBackColor = true;
            // 
            // Form_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(411, 417);
            Controls.Add(button_scan_ip);
            Controls.Add(listBox_ip_scan);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form_Main";
            Text = "World Camera";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox_ip_scan;
        private Button button_scan_ip;
    }
}
