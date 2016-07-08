namespace Caesar___Entschlüsselung
{
    partial class Caesar
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Caesar));
            this.lbl_encrypted = new System.Windows.Forms.Label();
            this.box_encrypted = new System.Windows.Forms.RichTextBox();
            this.box_decrypted = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_exampleText = new System.Windows.Forms.Label();
            this.box_exampleText = new System.Windows.Forms.RichTextBox();
            this.btn_decrypt = new System.Windows.Forms.Button();
            this.lbl_key = new System.Windows.Forms.Label();
            this.slider_accuracy = new System.Windows.Forms.TrackBar();
            this.lbl_accuracy_adj = new System.Windows.Forms.Label();
            this.lbl_accuracy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.slider_accuracy)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_encrypted
            // 
            this.lbl_encrypted.AutoSize = true;
            this.lbl_encrypted.Location = new System.Drawing.Point(9, 9);
            this.lbl_encrypted.Name = "lbl_encrypted";
            this.lbl_encrypted.Size = new System.Drawing.Size(105, 13);
            this.lbl_encrypted.TabIndex = 0;
            this.lbl_encrypted.Text = "Verschlüsselter Text:";
            // 
            // box_encrypted
            // 
            this.box_encrypted.Location = new System.Drawing.Point(12, 25);
            this.box_encrypted.Name = "box_encrypted";
            this.box_encrypted.Size = new System.Drawing.Size(275, 190);
            this.box_encrypted.TabIndex = 1;
            this.box_encrypted.Text = "";
            // 
            // box_decrypted
            // 
            this.box_decrypted.Location = new System.Drawing.Point(293, 25);
            this.box_decrypted.Name = "box_decrypted";
            this.box_decrypted.ReadOnly = true;
            this.box_decrypted.Size = new System.Drawing.Size(275, 190);
            this.box_decrypted.TabIndex = 3;
            this.box_decrypted.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(290, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Entschlüsselter Text:";
            // 
            // lbl_exampleText
            // 
            this.lbl_exampleText.AutoSize = true;
            this.lbl_exampleText.Location = new System.Drawing.Point(9, 236);
            this.lbl_exampleText.Name = "lbl_exampleText";
            this.lbl_exampleText.Size = new System.Drawing.Size(70, 13);
            this.lbl_exampleText.TabIndex = 4;
            this.lbl_exampleText.Text = "Beispiel Text:";
            // 
            // box_exampleText
            // 
            this.box_exampleText.Location = new System.Drawing.Point(12, 252);
            this.box_exampleText.Name = "box_exampleText";
            this.box_exampleText.Size = new System.Drawing.Size(275, 142);
            this.box_exampleText.TabIndex = 5;
            this.box_exampleText.Text = resources.GetString("box_exampleText.Text");
            // 
            // btn_decrypt
            // 
            this.btn_decrypt.Location = new System.Drawing.Point(12, 400);
            this.btn_decrypt.Name = "btn_decrypt";
            this.btn_decrypt.Size = new System.Drawing.Size(556, 23);
            this.btn_decrypt.TabIndex = 6;
            this.btn_decrypt.Text = "Entschlüsseln";
            this.btn_decrypt.UseVisualStyleBackColor = true;
            this.btn_decrypt.Click += new System.EventHandler(this.btn_decrypt_Click);
            // 
            // lbl_key
            // 
            this.lbl_key.AutoSize = true;
            this.lbl_key.Location = new System.Drawing.Point(293, 255);
            this.lbl_key.Name = "lbl_key";
            this.lbl_key.Size = new System.Drawing.Size(177, 13);
            this.lbl_key.TabIndex = 7;
            this.lbl_key.Text = "Erkannte Verschiebung: Unbekannt";
            // 
            // slider_accuracy
            // 
            this.slider_accuracy.Location = new System.Drawing.Point(293, 332);
            this.slider_accuracy.Name = "slider_accuracy";
            this.slider_accuracy.Size = new System.Drawing.Size(275, 45);
            this.slider_accuracy.TabIndex = 8;
            this.slider_accuracy.Scroll += new System.EventHandler(this.slider_accuracy_Scroll);
            // 
            // lbl_accuracy_adj
            // 
            this.lbl_accuracy_adj.AutoSize = true;
            this.lbl_accuracy_adj.Location = new System.Drawing.Point(293, 316);
            this.lbl_accuracy_adj.Name = "lbl_accuracy_adj";
            this.lbl_accuracy_adj.Size = new System.Drawing.Size(200, 13);
            this.lbl_accuracy_adj.TabIndex = 9;
            this.lbl_accuracy_adj.Text = "Übereinstimmungs Value Anpassung: 0.0";
            // 
            // lbl_accuracy
            // 
            this.lbl_accuracy.AutoSize = true;
            this.lbl_accuracy.Location = new System.Drawing.Point(293, 292);
            this.lbl_accuracy.Name = "lbl_accuracy";
            this.lbl_accuracy.Size = new System.Drawing.Size(150, 13);
            this.lbl_accuracy.TabIndex = 10;
            this.lbl_accuracy.Text = "Übereinstimmungs Value: 0.01";
            // 
            // Caesar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 432);
            this.Controls.Add(this.lbl_accuracy);
            this.Controls.Add(this.lbl_accuracy_adj);
            this.Controls.Add(this.slider_accuracy);
            this.Controls.Add(this.lbl_key);
            this.Controls.Add(this.btn_decrypt);
            this.Controls.Add(this.box_exampleText);
            this.Controls.Add(this.lbl_exampleText);
            this.Controls.Add(this.box_decrypted);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.box_encrypted);
            this.Controls.Add(this.lbl_encrypted);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(595, 471);
            this.MinimumSize = new System.Drawing.Size(595, 471);
            this.Name = "Caesar";
            this.Text = "Caesar Entschlüsselung";
            ((System.ComponentModel.ISupportInitialize)(this.slider_accuracy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_encrypted;
        private System.Windows.Forms.RichTextBox box_encrypted;
        private System.Windows.Forms.RichTextBox box_decrypted;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_exampleText;
        private System.Windows.Forms.RichTextBox box_exampleText;
        private System.Windows.Forms.Button btn_decrypt;
        private System.Windows.Forms.Label lbl_key;
        private System.Windows.Forms.TrackBar slider_accuracy;
        private System.Windows.Forms.Label lbl_accuracy_adj;
        private System.Windows.Forms.Label lbl_accuracy;
    }
}

