namespace CloudBackup
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxFolderOrig = new System.Windows.Forms.TextBox();
            this.textBoxFolderCrypt = new System.Windows.Forms.TextBox();
            this.buttonFolderOrig = new System.Windows.Forms.Button();
            this.buttonFolderCrypt = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonRemote = new System.Windows.Forms.RadioButton();
            this.radioButtonBackup = new System.Windows.Forms.RadioButton();
            this.textBoxUpdate = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Répertoire fichiers originaux";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Répertoire fichiers cryptés";
            // 
            // textBoxFolderOrig
            // 
            this.textBoxFolderOrig.Location = new System.Drawing.Point(205, 77);
            this.textBoxFolderOrig.Name = "textBoxFolderOrig";
            this.textBoxFolderOrig.ReadOnly = true;
            this.textBoxFolderOrig.Size = new System.Drawing.Size(181, 20);
            this.textBoxFolderOrig.TabIndex = 4;
            // 
            // textBoxFolderCrypt
            // 
            this.textBoxFolderCrypt.Location = new System.Drawing.Point(205, 121);
            this.textBoxFolderCrypt.Name = "textBoxFolderCrypt";
            this.textBoxFolderCrypt.ReadOnly = true;
            this.textBoxFolderCrypt.Size = new System.Drawing.Size(181, 20);
            this.textBoxFolderCrypt.TabIndex = 5;
            // 
            // buttonFolderOrig
            // 
            this.buttonFolderOrig.Location = new System.Drawing.Point(404, 77);
            this.buttonFolderOrig.Name = "buttonFolderOrig";
            this.buttonFolderOrig.Size = new System.Drawing.Size(75, 23);
            this.buttonFolderOrig.TabIndex = 6;
            this.buttonFolderOrig.Text = "...";
            this.buttonFolderOrig.UseVisualStyleBackColor = true;
            this.buttonFolderOrig.Click += new System.EventHandler(this.buttonFolderOrig_Click);
            // 
            // buttonFolderCrypt
            // 
            this.buttonFolderCrypt.Location = new System.Drawing.Point(404, 121);
            this.buttonFolderCrypt.Name = "buttonFolderCrypt";
            this.buttonFolderCrypt.Size = new System.Drawing.Size(75, 23);
            this.buttonFolderCrypt.TabIndex = 7;
            this.buttonFolderCrypt.Text = "...";
            this.buttonFolderCrypt.UseVisualStyleBackColor = true;
            this.buttonFolderCrypt.Click += new System.EventHandler(this.buttonFolderCrypt_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "mot de passe";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(205, 154);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(181, 20);
            this.textBoxPassword.TabIndex = 9;
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(404, 151);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 23);
            this.buttonGo.TabIndex = 10;
            this.buttonGo.Text = "Start";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(44, 200);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(435, 21);
            this.progressBar1.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonRemote);
            this.groupBox1.Controls.Add(this.radioButtonBackup);
            this.groupBox1.Location = new System.Drawing.Point(44, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 65);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type d\'opération";
            // 
            // radioButtonRemote
            // 
            this.radioButtonRemote.AutoSize = true;
            this.radioButtonRemote.Location = new System.Drawing.Point(6, 42);
            this.radioButtonRemote.Name = "radioButtonRemote";
            this.radioButtonRemote.Size = new System.Drawing.Size(57, 17);
            this.radioButtonRemote.TabIndex = 1;
            this.radioButtonRemote.TabStop = true;
            this.radioButtonRemote.Text = "remote";
            this.radioButtonRemote.UseVisualStyleBackColor = true;
            // 
            // radioButtonBackup
            // 
            this.radioButtonBackup.AutoSize = true;
            this.radioButtonBackup.Location = new System.Drawing.Point(6, 19);
            this.radioButtonBackup.Name = "radioButtonBackup";
            this.radioButtonBackup.Size = new System.Drawing.Size(62, 17);
            this.radioButtonBackup.TabIndex = 0;
            this.radioButtonBackup.TabStop = true;
            this.radioButtonBackup.Text = "Backup";
            this.radioButtonBackup.UseVisualStyleBackColor = true;
            // 
            // textBoxUpdate
            // 
            this.textBoxUpdate.Location = new System.Drawing.Point(44, 238);
            this.textBoxUpdate.Multiline = true;
            this.textBoxUpdate.Name = "textBoxUpdate";
            this.textBoxUpdate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxUpdate.Size = new System.Drawing.Size(935, 87);
            this.textBoxUpdate.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 337);
            this.Controls.Add(this.textBoxUpdate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonFolderCrypt);
            this.Controls.Add(this.buttonFolderOrig);
            this.Controls.Add(this.textBoxFolderCrypt);
            this.Controls.Add(this.textBoxFolderOrig);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBoxFolderOrig;
        private System.Windows.Forms.TextBox textBoxFolderCrypt;
        private System.Windows.Forms.Button buttonFolderOrig;
        private System.Windows.Forms.Button buttonFolderCrypt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonRemote;
        private System.Windows.Forms.RadioButton radioButtonBackup;
        private System.Windows.Forms.TextBox textBoxUpdate;
    }
}

