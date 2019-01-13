namespace DoodleClassifier
{
    partial class Draw
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
            this.picBoxDrawing = new System.Windows.Forms.PictureBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRecognize = new System.Windows.Forms.Button();
            this.lblCar = new System.Windows.Forms.Label();
            this.lblCat = new System.Windows.Forms.Label();
            this.txtCar = new System.Windows.Forms.TextBox();
            this.txtCat = new System.Windows.Forms.TextBox();
            this.picBoxScaled = new System.Windows.Forms.PictureBox();
            this.btnSaveDrawing = new System.Windows.Forms.Button();
            this.btnOpenDrawing = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxDrawing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxScaled)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxDrawing
            // 
            this.picBoxDrawing.Location = new System.Drawing.Point(12, 12);
            this.picBoxDrawing.Name = "picBoxDrawing";
            this.picBoxDrawing.Size = new System.Drawing.Size(448, 448);
            this.picBoxDrawing.TabIndex = 0;
            this.picBoxDrawing.TabStop = false;
            this.picBoxDrawing.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingBox_Paint);
            this.picBoxDrawing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingBox_MouseDown);
            this.picBoxDrawing.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingBox_MouseMove);
            this.picBoxDrawing.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingBox_MouseUp);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(467, 80);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 28);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRecognize
            // 
            this.btnRecognize.Location = new System.Drawing.Point(466, 114);
            this.btnRecognize.Name = "btnRecognize";
            this.btnRecognize.Size = new System.Drawing.Size(121, 28);
            this.btnRecognize.TabIndex = 3;
            this.btnRecognize.Text = "Recognize";
            this.btnRecognize.UseVisualStyleBackColor = true;
            this.btnRecognize.Click += new System.EventHandler(this.btnRecognize_Click);
            // 
            // lblCar
            // 
            this.lblCar.AutoSize = true;
            this.lblCar.Location = new System.Drawing.Point(467, 177);
            this.lblCar.Name = "lblCar";
            this.lblCar.Size = new System.Drawing.Size(23, 13);
            this.lblCar.TabIndex = 10;
            this.lblCar.Text = "Car";
            // 
            // lblCat
            // 
            this.lblCat.AutoSize = true;
            this.lblCat.Location = new System.Drawing.Point(467, 151);
            this.lblCat.Name = "lblCat";
            this.lblCat.Size = new System.Drawing.Size(23, 13);
            this.lblCat.TabIndex = 9;
            this.lblCat.Text = "Cat";
            // 
            // txtCar
            // 
            this.txtCar.Location = new System.Drawing.Point(505, 174);
            this.txtCar.Name = "txtCar";
            this.txtCar.Size = new System.Drawing.Size(82, 20);
            this.txtCar.TabIndex = 8;
            // 
            // txtCat
            // 
            this.txtCat.Location = new System.Drawing.Point(505, 148);
            this.txtCat.Name = "txtCat";
            this.txtCat.Size = new System.Drawing.Size(82, 20);
            this.txtCat.TabIndex = 7;
            // 
            // picBoxScaled
            // 
            this.picBoxScaled.Location = new System.Drawing.Point(559, 200);
            this.picBoxScaled.Name = "picBoxScaled";
            this.picBoxScaled.Size = new System.Drawing.Size(28, 28);
            this.picBoxScaled.TabIndex = 11;
            this.picBoxScaled.TabStop = false;
            // 
            // btnSaveDrawing
            // 
            this.btnSaveDrawing.Location = new System.Drawing.Point(466, 12);
            this.btnSaveDrawing.Name = "btnSaveDrawing";
            this.btnSaveDrawing.Size = new System.Drawing.Size(120, 28);
            this.btnSaveDrawing.TabIndex = 12;
            this.btnSaveDrawing.Text = "Save Drawing";
            this.btnSaveDrawing.UseVisualStyleBackColor = true;
            this.btnSaveDrawing.Click += new System.EventHandler(this.btnSaveDrawing_Click);
            // 
            // btnOpenDrawing
            // 
            this.btnOpenDrawing.Location = new System.Drawing.Point(466, 46);
            this.btnOpenDrawing.Name = "btnOpenDrawing";
            this.btnOpenDrawing.Size = new System.Drawing.Size(120, 28);
            this.btnOpenDrawing.TabIndex = 13;
            this.btnOpenDrawing.Text = "Open Drawing";
            this.btnOpenDrawing.UseVisualStyleBackColor = true;
            this.btnOpenDrawing.Click += new System.EventHandler(this.btnOpenDrawing_Click);
            // 
            // Draw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 475);
            this.Controls.Add(this.btnOpenDrawing);
            this.Controls.Add(this.btnSaveDrawing);
            this.Controls.Add(this.picBoxScaled);
            this.Controls.Add(this.lblCar);
            this.Controls.Add(this.lblCat);
            this.Controls.Add(this.txtCar);
            this.Controls.Add(this.txtCat);
            this.Controls.Add(this.btnRecognize);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.picBoxDrawing);
            this.Name = "Draw";
            this.Text = "Draw";
            ((System.ComponentModel.ISupportInitialize)(this.picBoxDrawing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxScaled)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxDrawing;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRecognize;
        private System.Windows.Forms.Label lblCar;
        private System.Windows.Forms.Label lblCat;
        private System.Windows.Forms.TextBox txtCar;
        private System.Windows.Forms.TextBox txtCat;
        private System.Windows.Forms.PictureBox picBoxScaled;
        private System.Windows.Forms.Button btnSaveDrawing;
        private System.Windows.Forms.Button btnOpenDrawing;
    }
}