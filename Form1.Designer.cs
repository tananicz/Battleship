namespace Battleship
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
            this.startBtn = new System.Windows.Forms.Button();
            this.gameStatusTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(231, 394);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(123, 31);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // gameStatusTextBox
            // 
            this.gameStatusTextBox.Location = new System.Drawing.Point(32, 313);
            this.gameStatusTextBox.Multiline = true;
            this.gameStatusTextBox.Name = "gameStatusTextBox";
            this.gameStatusTextBox.Size = new System.Drawing.Size(534, 63);
            this.gameStatusTextBox.TabIndex = 1;
            this.gameStatusTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 467);
            this.Controls.Add(this.gameStatusTextBox);
            this.Controls.Add(this.startBtn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Battleships";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox gameStatusTextBox;
        private Button startBtn;
    }
}