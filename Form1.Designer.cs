namespace AppTTS
{
    partial class Form1
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
            txtWindow = new TextBox();
            txtCustomer = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnPlay = new Button();
            SuspendLayout();
            // 
            // txtWindow
            // 
            txtWindow.Location = new Point(175, 58);
            txtWindow.Margin = new Padding(4, 3, 4, 3);
            txtWindow.Name = "txtWindow";
            txtWindow.Size = new Size(116, 23);
            txtWindow.TabIndex = 0;
            txtWindow.Text = "Window number";
            txtWindow.Visible = true;  // Set to true
                                       // 
                                       // txtCustomer
                                       // 
            txtCustomer.Location = new Point(175, 115);
            txtCustomer.Margin = new Padding(4, 3, 4, 3);
            txtCustomer.Name = "txtCustomer";
            txtCustomer.Size = new Size(116, 23);
            txtCustomer.TabIndex = 1;
            txtCustomer.Text = "Customer name";
            txtCustomer.Visible = true;  // Set to true
            txtCustomer.TextChanged += txtCustomer_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(58, 61);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(116, 15);
            label1.TabIndex = 2;
            label1.Text = "Set window number:";
            label1.Visible = true;  // Set to true
                                    // 
                                    // label2
                                    // 
            label2.AutoSize = true;
            label2.Location = new Point(58, 119);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(112, 15);
            label2.TabIndex = 3;
            label2.Text = "Set customer name:";
            label2.Visible = true;  // Set to true
                                    // 
                                    // btnPlay
                                    // 
            btnPlay.Location = new Point(175, 173);
            btnPlay.Margin = new Padding(4, 3, 4, 3);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(88, 27);
            btnPlay.TabIndex = 4;
            btnPlay.Text = "Play";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Visible = true;  // Set to true
            btnPlay.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(331, 301);
            Controls.Add(btnPlay);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtCustomer);
            Controls.Add(txtWindow);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "TTS and Audio Player";
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private System.Windows.Forms.TextBox txtWindow;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPlay;
    }
}
