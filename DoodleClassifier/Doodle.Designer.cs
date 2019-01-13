namespace DoodleClassifier
{
    partial class Doodle
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnRecognize = new System.Windows.Forms.Button();
            this.txtCat = new System.Windows.Forms.TextBox();
            this.txtCar = new System.Windows.Forms.TextBox();
            this.lblCat = new System.Windows.Forms.Label();
            this.lblCar = new System.Windows.Forms.Label();
            this.lblAnswer = new System.Windows.Forms.Label();
            this.btnExportBrain = new System.Windows.Forms.Button();
            this.btnImportBrain = new System.Windows.Forms.Button();
            this.lblTrainingDS = new System.Windows.Forms.Label();
            this.btnCreateBrain = new System.Windows.Forms.Button();
            this.lblTestingDS = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(28, 28);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // btnTrain
            // 
            this.btnTrain.Enabled = false;
            this.btnTrain.Location = new System.Drawing.Point(47, 12);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(82, 28);
            this.btnTrain.TabIndex = 1;
            this.btnTrain.Text = "Train";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // btnRecognize
            // 
            this.btnRecognize.Enabled = false;
            this.btnRecognize.Location = new System.Drawing.Point(47, 46);
            this.btnRecognize.Name = "btnRecognize";
            this.btnRecognize.Size = new System.Drawing.Size(82, 28);
            this.btnRecognize.TabIndex = 2;
            this.btnRecognize.Text = "Recognize";
            this.btnRecognize.UseVisualStyleBackColor = true;
            this.btnRecognize.Click += new System.EventHandler(this.btnRecognize_Click);
            // 
            // txtCat
            // 
            this.txtCat.Location = new System.Drawing.Point(47, 81);
            this.txtCat.Name = "txtCat";
            this.txtCat.Size = new System.Drawing.Size(82, 20);
            this.txtCat.TabIndex = 3;
            // 
            // txtCar
            // 
            this.txtCar.Location = new System.Drawing.Point(47, 107);
            this.txtCar.Name = "txtCar";
            this.txtCar.Size = new System.Drawing.Size(82, 20);
            this.txtCar.TabIndex = 4;
            // 
            // lblCat
            // 
            this.lblCat.AutoSize = true;
            this.lblCat.Location = new System.Drawing.Point(9, 84);
            this.lblCat.Name = "lblCat";
            this.lblCat.Size = new System.Drawing.Size(23, 13);
            this.lblCat.TabIndex = 5;
            this.lblCat.Text = "Cat";
            // 
            // lblCar
            // 
            this.lblCar.AutoSize = true;
            this.lblCar.Location = new System.Drawing.Point(9, 110);
            this.lblCar.Name = "lblCar";
            this.lblCar.Size = new System.Drawing.Size(23, 13);
            this.lblCar.TabIndex = 6;
            this.lblCar.Text = "Car";
            // 
            // lblAnswer
            // 
            this.lblAnswer.AutoSize = true;
            this.lblAnswer.Location = new System.Drawing.Point(16, 133);
            this.lblAnswer.Name = "lblAnswer";
            this.lblAnswer.Size = new System.Drawing.Size(106, 13);
            this.lblAnswer.TabIndex = 7;
            this.lblAnswer.Text = "The answer was a ...";
            this.lblAnswer.Visible = false;
            // 
            // btnExportBrain
            // 
            this.btnExportBrain.Enabled = false;
            this.btnExportBrain.Location = new System.Drawing.Point(135, 12);
            this.btnExportBrain.Name = "btnExportBrain";
            this.btnExportBrain.Size = new System.Drawing.Size(92, 28);
            this.btnExportBrain.TabIndex = 8;
            this.btnExportBrain.Text = "Export Brain";
            this.btnExportBrain.UseVisualStyleBackColor = true;
            this.btnExportBrain.Click += new System.EventHandler(this.btnExportBrain_Click);
            // 
            // btnImportBrain
            // 
            this.btnImportBrain.Location = new System.Drawing.Point(135, 46);
            this.btnImportBrain.Name = "btnImportBrain";
            this.btnImportBrain.Size = new System.Drawing.Size(92, 28);
            this.btnImportBrain.TabIndex = 9;
            this.btnImportBrain.Text = "Import Brain";
            this.btnImportBrain.UseVisualStyleBackColor = true;
            this.btnImportBrain.Click += new System.EventHandler(this.btnImportBrain_Click);
            // 
            // lblTrainingDS
            // 
            this.lblTrainingDS.AutoSize = true;
            this.lblTrainingDS.Location = new System.Drawing.Point(16, 156);
            this.lblTrainingDS.Name = "lblTrainingDS";
            this.lblTrainingDS.Size = new System.Drawing.Size(109, 13);
            this.lblTrainingDS.TabIndex = 10;
            this.lblTrainingDS.Text = "Training dataset : ...%";
            // 
            // btnCreateBrain
            // 
            this.btnCreateBrain.Location = new System.Drawing.Point(135, 80);
            this.btnCreateBrain.Name = "btnCreateBrain";
            this.btnCreateBrain.Size = new System.Drawing.Size(92, 28);
            this.btnCreateBrain.TabIndex = 11;
            this.btnCreateBrain.Text = "Create Brain";
            this.btnCreateBrain.UseVisualStyleBackColor = true;
            this.btnCreateBrain.Click += new System.EventHandler(this.btnCreateBrain_Click);
            // 
            // lblTestingDS
            // 
            this.lblTestingDS.AutoSize = true;
            this.lblTestingDS.Location = new System.Drawing.Point(16, 169);
            this.lblTestingDS.Name = "lblTestingDS";
            this.lblTestingDS.Size = new System.Drawing.Size(106, 13);
            this.lblTestingDS.TabIndex = 12;
            this.lblTestingDS.Text = "Testing dataset : ...%";
            // 
            // Doodle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 186);
            this.Controls.Add(this.lblTestingDS);
            this.Controls.Add(this.btnCreateBrain);
            this.Controls.Add(this.lblTrainingDS);
            this.Controls.Add(this.btnImportBrain);
            this.Controls.Add(this.btnExportBrain);
            this.Controls.Add(this.lblAnswer);
            this.Controls.Add(this.lblCar);
            this.Controls.Add(this.lblCat);
            this.Controls.Add(this.txtCar);
            this.Controls.Add(this.txtCat);
            this.Controls.Add(this.btnRecognize);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.pictureBox);
            this.Name = "Doodle";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.Button btnRecognize;
        private System.Windows.Forms.TextBox txtCat;
        private System.Windows.Forms.TextBox txtCar;
        private System.Windows.Forms.Label lblCat;
        private System.Windows.Forms.Label lblCar;
        private System.Windows.Forms.Label lblAnswer;
        private System.Windows.Forms.Button btnExportBrain;
        private System.Windows.Forms.Button btnImportBrain;
        private System.Windows.Forms.Label lblTrainingDS;
        private System.Windows.Forms.Button btnCreateBrain;
        private System.Windows.Forms.Label lblTestingDS;
    }
}

