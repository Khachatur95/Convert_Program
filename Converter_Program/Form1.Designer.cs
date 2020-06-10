namespace Converter_Program
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
            this.svo_Convert = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.svo_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.svo_SelectSourcePath = new System.Windows.Forms.Button();
            this.svo_SelectSource = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.svo_ListBox = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.svo_dtPicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // svo_Convert
            // 
            this.svo_Convert.Location = new System.Drawing.Point(960, 384);
            this.svo_Convert.Name = "svo_Convert";
            this.svo_Convert.Size = new System.Drawing.Size(75, 23);
            this.svo_Convert.TabIndex = 0;
            this.svo_Convert.Text = "Convert";
            this.svo_Convert.UseVisualStyleBackColor = true;
            this.svo_Convert.Click += new System.EventHandler(this.button1_Click);
            // 
            // svo_ProgressBar
            // 
            this.svo_ProgressBar.Location = new System.Drawing.Point(12, 432);
            this.svo_ProgressBar.Name = "svo_ProgressBar";
            this.svo_ProgressBar.Size = new System.Drawing.Size(1036, 23);
            this.svo_ProgressBar.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(593, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source Files Path";
            // 
            // svo_SelectSourcePath
            // 
            this.svo_SelectSourcePath.Location = new System.Drawing.Point(548, 279);
            this.svo_SelectSourcePath.Name = "svo_SelectSourcePath";
            this.svo_SelectSourcePath.Size = new System.Drawing.Size(39, 25);
            this.svo_SelectSourcePath.TabIndex = 4;
            this.svo_SelectSourcePath.Text = "...";
            this.svo_SelectSourcePath.UseVisualStyleBackColor = true;
            this.svo_SelectSourcePath.Click += new System.EventHandler(this.button2_Click);
            // 
            // svo_SelectSource
            // 
            this.svo_SelectSource.Location = new System.Drawing.Point(548, 232);
            this.svo_SelectSource.Name = "svo_SelectSource";
            this.svo_SelectSource.Size = new System.Drawing.Size(39, 25);
            this.svo_SelectSource.TabIndex = 5;
            this.svo_SelectSource.Text = "...";
            this.svo_SelectSource.UseVisualStyleBackColor = true;
            this.svo_SelectSource.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(593, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Source Files ";
            // 
            // svo_ListBox
            // 
            this.svo_ListBox.FormattingEnabled = true;
            this.svo_ListBox.Location = new System.Drawing.Point(12, 44);
            this.svo_ListBox.Name = "svo_ListBox";
            this.svo_ListBox.Size = new System.Drawing.Size(243, 277);
            this.svo_ListBox.TabIndex = 7;
            this.svo_ListBox.Click += new System.EventHandler(this.svo_ListBox_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1060, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // svo_dtPicker
            // 
            this.svo_dtPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.svo_dtPicker.CustomFormat = "dd/MM/yyyy";
            this.svo_dtPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.svo_dtPicker.Location = new System.Drawing.Point(22, 5);
            this.svo_dtPicker.Name = "svo_dtPicker";
            this.svo_dtPicker.Size = new System.Drawing.Size(128, 20);
            this.svo_dtPicker.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 467);
            this.Controls.Add(this.svo_dtPicker);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.svo_ListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.svo_SelectSource);
            this.Controls.Add(this.svo_SelectSourcePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.svo_ProgressBar);
            this.Controls.Add(this.svo_Convert);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button svo_Convert;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar svo_ProgressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button svo_SelectSourcePath;
        private System.Windows.Forms.Button svo_SelectSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox svo_ListBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DateTimePicker svo_dtPicker;
    }
}

