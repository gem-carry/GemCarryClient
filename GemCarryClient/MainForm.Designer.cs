namespace GemCarryClient
{
    partial class MainForm
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
            this.sendChat_button = new System.Windows.Forms.Button();
            this.chatbox_text = new System.Windows.Forms.TextBox();
            this.console_text = new System.Windows.Forms.TextBox();
            this.login_button = new System.Windows.Forms.Button();
            this.connect_button = new System.Windows.Forms.Button();
            this.username_text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sendChat_button
            // 
            this.sendChat_button.Location = new System.Drawing.Point(327, 416);
            this.sendChat_button.Name = "sendChat_button";
            this.sendChat_button.Size = new System.Drawing.Size(75, 23);
            this.sendChat_button.TabIndex = 0;
            this.sendChat_button.Text = "Send";
            this.sendChat_button.UseVisualStyleBackColor = true;
            this.sendChat_button.Click += new System.EventHandler(this.sendChat_button_Click);
            // 
            // chatbox_text
            // 
            this.chatbox_text.Location = new System.Drawing.Point(13, 416);
            this.chatbox_text.Name = "chatbox_text";
            this.chatbox_text.Size = new System.Drawing.Size(308, 20);
            this.chatbox_text.TabIndex = 1;
            // 
            // console_text
            // 
            this.console_text.Location = new System.Drawing.Point(13, 13);
            this.console_text.Multiline = true;
            this.console_text.Name = "console_text";
            this.console_text.Size = new System.Drawing.Size(389, 397);
            this.console_text.TabIndex = 2;
            // 
            // login_button
            // 
            this.login_button.Location = new System.Drawing.Point(443, 86);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(97, 23);
            this.login_button.TabIndex = 3;
            this.login_button.Text = "Log In";
            this.login_button.UseVisualStyleBackColor = true;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // connect_button
            // 
            this.connect_button.Location = new System.Drawing.Point(443, 57);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(97, 23);
            this.connect_button.TabIndex = 4;
            this.connect_button.Text = "Connect";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // username_text
            // 
            this.username_text.Location = new System.Drawing.Point(408, 22);
            this.username_text.Name = "username_text";
            this.username_text.Size = new System.Drawing.Size(161, 20);
            this.username_text.TabIndex = 5;
            this.username_text.Text = "<Username>";
            this.username_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 451);
            this.Controls.Add(this.username_text);
            this.Controls.Add(this.connect_button);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.console_text);
            this.Controls.Add(this.chatbox_text);
            this.Controls.Add(this.sendChat_button);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendChat_button;
        private System.Windows.Forms.TextBox chatbox_text;
        private System.Windows.Forms.TextBox console_text;
        private System.Windows.Forms.Button login_button;
        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.TextBox username_text;
    }
}