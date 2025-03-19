namespace Perlin
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
            saveFileDialog1 = new SaveFileDialog();
            pctBox = new PictureBox();
            btnStart = new Button();
            txtSeed = new TextBox();
            label1 = new Label();
            btnRandomSeed = new Button();
            ((System.ComponentModel.ISupportInitialize)pctBox).BeginInit();
            SuspendLayout();
            // 
            // pctBox
            // 
            pctBox.Location = new Point(12, 12);
            pctBox.Name = "pctBox";
            pctBox.Size = new Size(509, 426);
            pctBox.TabIndex = 0;
            pctBox.TabStop = false;
            pctBox.Click += pctBox_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(527, 415);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(261, 23);
            btnStart.TabIndex = 1;
            btnStart.Text = "Generate";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // txtSeed
            // 
            txtSeed.Location = new Point(578, 365);
            txtSeed.Name = "txtSeed";
            txtSeed.Size = new Size(124, 23);
            txtSeed.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(534, 368);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 3;
            label1.Text = "Seed";
            // 
            // btnRandomSeed
            // 
            btnRandomSeed.Location = new Point(708, 365);
            btnRandomSeed.Name = "btnRandomSeed";
            btnRandomSeed.Size = new Size(78, 23);
            btnRandomSeed.TabIndex = 4;
            btnRandomSeed.Text = "Randomise";
            btnRandomSeed.UseVisualStyleBackColor = true;
            btnRandomSeed.Click += btnRandomSeed_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRandomSeed);
            Controls.Add(label1);
            Controls.Add(txtSeed);
            Controls.Add(btnStart);
            Controls.Add(pctBox);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pctBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SaveFileDialog saveFileDialog1;
        private PictureBox pctBox;
        private Button btnStart;
        private TextBox txtSeed;
        private Label label1;
        private Button btnRandomSeed;
    }
}
