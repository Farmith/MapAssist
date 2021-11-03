
namespace MapAssist
{
    partial class DebugWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.playerPosX = new System.Windows.Forms.TextBox();
            this.playerPosY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.playerArea = new System.Windows.Forms.TextBox();
            this.playerDifficulty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.playerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.entityList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Player Y";
            // 
            // playerPosX
            // 
            this.playerPosX.Location = new System.Drawing.Point(81, 50);
            this.playerPosX.Name = "playerPosX";
            this.playerPosX.Size = new System.Drawing.Size(100, 20);
            this.playerPosX.TabIndex = 3;
            // 
            // playerPosY
            // 
            this.playerPosY.Location = new System.Drawing.Point(81, 84);
            this.playerPosY.Name = "playerPosY";
            this.playerPosY.Size = new System.Drawing.Size(100, 20);
            this.playerPosY.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Area";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // playerArea
            // 
            this.playerArea.Location = new System.Drawing.Point(265, 50);
            this.playerArea.Name = "playerArea";
            this.playerArea.Size = new System.Drawing.Size(100, 20);
            this.playerArea.TabIndex = 7;
            // 
            // playerDifficulty
            // 
            this.playerDifficulty.Location = new System.Drawing.Point(265, 80);
            this.playerDifficulty.Name = "playerDifficulty";
            this.playerDifficulty.Size = new System.Drawing.Size(100, 20);
            this.playerDifficulty.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Difficulty";
            // 
            // playerName
            // 
            this.playerName.Location = new System.Drawing.Point(81, 21);
            this.playerName.Name = "playerName";
            this.playerName.Size = new System.Drawing.Size(100, 20);
            this.playerName.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Player Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Entities:";
            // 
            // entityList
            // 
            this.entityList.FormattingEnabled = true;
            this.entityList.Location = new System.Drawing.Point(16, 135);
            this.entityList.Name = "entityList";
            this.entityList.Size = new System.Drawing.Size(377, 225);
            this.entityList.TabIndex = 13;
            // 
            // DebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 390);
            this.Controls.Add(this.entityList);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.playerName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.playerDifficulty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.playerArea);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.playerPosY);
            this.Controls.Add(this.playerPosX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DebugWindow";
            this.Text = "DebugWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox playerPosX;
        private System.Windows.Forms.TextBox playerPosY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox playerArea;
        private System.Windows.Forms.TextBox playerDifficulty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox playerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox entityList;
    }
}