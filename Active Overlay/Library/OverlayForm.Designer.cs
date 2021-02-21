namespace ActiveOverlayForm {
    partial class OverlayForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.graphicsBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.graphicsBox)).BeginInit();
            this.SuspendLayout();
            // 
            // graphicsBox
            // 
            this.graphicsBox.Location = new System.Drawing.Point(419, 276);
            this.graphicsBox.Name = "graphicsBox";
            this.graphicsBox.Size = new System.Drawing.Size(212, 119);
            this.graphicsBox.TabIndex = 3;
            this.graphicsBox.TabStop = false;
            // 
            // OverlayForm
            // 
            this.ClientSize = new System.Drawing.Size(743, 437);
            this.Controls.Add(this.graphicsBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OverlayForm";
            this.Opacity = 0.7D;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.graphicsBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox graphicsBox;
    }
}

