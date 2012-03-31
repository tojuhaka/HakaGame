namespace TileMapEditor
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
            this.tileDisplay1 = new TileMapEditor.TileDisplay();
            this.SuspendLayout();
            // 
            // tileDisplay1
            // 
            this.tileDisplay1.Location = new System.Drawing.Point(134, 118);
            this.tileDisplay1.Name = "tileDisplay1";
            this.tileDisplay1.Size = new System.Drawing.Size(75, 23);
            this.tileDisplay1.TabIndex = 0;
            this.tileDisplay1.Text = "tileDisplay1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 428);
            this.Controls.Add(this.tileDisplay1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private TileDisplay tileDisplay1;
    }
}

