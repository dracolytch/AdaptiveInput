namespace AdaptiveInput
{
    partial class InputWindow
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
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonPanel
            // 
            this.buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPanel.BackColor = System.Drawing.Color.Black;
            this.buttonPanel.Location = new System.Drawing.Point(12, 12);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(260, 237);
            this.buttonPanel.TabIndex = 0;
            // 
            // InputWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.buttonPanel);
            this.Name = "InputWindow";
            this.Text = "InputWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.InputWindow_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InputWindow_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel buttonPanel;
    }
}