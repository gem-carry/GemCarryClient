namespace GemCarryClient
{
    partial class CreateUserForm
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
            this.username_text = new System.Windows.Forms.TextBox();
            this.email_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.password_text = new System.Windows.Forms.TextBox();
            this.password_label = new System.Windows.Forms.Label();
            this.repassword_label = new System.Windows.Forms.Label();
            this.repassword_text = new System.Windows.Forms.TextBox();
            this.diagnostic_message_label = new System.Windows.Forms.Label();
            this.submit_user_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // username_text
            // 
            this.username_text.Location = new System.Drawing.Point(161, 87);
            this.username_text.Name = "username_text";
            this.username_text.Size = new System.Drawing.Size(161, 20);
            this.username_text.TabIndex = 6;
            // 
            // email_label
            // 
            this.email_label.AutoSize = true;
            this.email_label.Location = new System.Drawing.Point(82, 90);
            this.email_label.Name = "email_label";
            this.email_label.Size = new System.Drawing.Size(76, 13);
            this.email_label.TabIndex = 7;
            this.email_label.Text = "Email Address:";
            this.email_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(394, 55);
            this.label2.TabIndex = 8;
            this.label2.Text = "Create New User";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // password_text
            // 
            this.password_text.Location = new System.Drawing.Point(161, 113);
            this.password_text.Name = "password_text";
            this.password_text.Size = new System.Drawing.Size(161, 20);
            this.password_text.TabIndex = 9;
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(102, 113);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(56, 13);
            this.password_label.TabIndex = 10;
            this.password_label.Text = "Password:";
            this.password_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // repassword_label
            // 
            this.repassword_label.AutoSize = true;
            this.repassword_label.Location = new System.Drawing.Point(61, 139);
            this.repassword_label.Name = "repassword_label";
            this.repassword_label.Size = new System.Drawing.Size(100, 13);
            this.repassword_label.TabIndex = 11;
            this.repassword_label.Text = "Re-Type Password:";
            this.repassword_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // repassword_text
            // 
            this.repassword_text.Location = new System.Drawing.Point(161, 139);
            this.repassword_text.Name = "repassword_text";
            this.repassword_text.Size = new System.Drawing.Size(161, 20);
            this.repassword_text.TabIndex = 12;
            // 
            // diagnostic_message_label
            // 
            this.diagnostic_message_label.AutoSize = true;
            this.diagnostic_message_label.Location = new System.Drawing.Point(61, 199);
            this.diagnostic_message_label.Name = "diagnostic_message_label";
            this.diagnostic_message_label.Size = new System.Drawing.Size(0, 13);
            this.diagnostic_message_label.TabIndex = 13;
            this.diagnostic_message_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // submit_user_button
            // 
            this.submit_user_button.Location = new System.Drawing.Point(161, 165);
            this.submit_user_button.Name = "submit_user_button";
            this.submit_user_button.Size = new System.Drawing.Size(161, 23);
            this.submit_user_button.TabIndex = 14;
            this.submit_user_button.Text = "Submit";
            this.submit_user_button.UseVisualStyleBackColor = true;
            this.submit_user_button.Click += new System.EventHandler(this.submit_user_button_Click);
            // 
            // CreateUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 254);
            this.Controls.Add(this.submit_user_button);
            this.Controls.Add(this.diagnostic_message_label);
            this.Controls.Add(this.repassword_text);
            this.Controls.Add(this.repassword_label);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.password_text);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.email_label);
            this.Controls.Add(this.username_text);
            this.Name = "CreateUserForm";
            this.Text = "CreateUserForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox username_text;
        private System.Windows.Forms.Label email_label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox password_text;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.Label repassword_label;
        private System.Windows.Forms.TextBox repassword_text;
        private System.Windows.Forms.Label diagnostic_message_label;
        private System.Windows.Forms.Button submit_user_button;
    }
}