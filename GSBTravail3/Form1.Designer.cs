namespace GSBTravail3
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Fiche = new System.Windows.Forms.Label();
            this.etatLabel = new System.Windows.Forms.Label();
            this.majButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(2, 37);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(644, 220);
            this.dataGridView.TabIndex = 0;
            // 
            // Fiche
            // 
            this.Fiche.AutoSize = true;
            this.Fiche.Font = new System.Drawing.Font("Monotype Corsiva", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fiche.Location = new System.Drawing.Point(217, 9);
            this.Fiche.Name = "Fiche";
            this.Fiche.Size = new System.Drawing.Size(190, 25);
            this.Fiche.TabIndex = 2;
            this.Fiche.Text = "Fiche du mois dernier";
            // 
            // etatLabel
            // 
            this.etatLabel.AutoSize = true;
            this.etatLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.etatLabel.ForeColor = System.Drawing.Color.DimGray;
            this.etatLabel.Location = new System.Drawing.Point(174, 282);
            this.etatLabel.Name = "etatLabel";
            this.etatLabel.Size = new System.Drawing.Size(0, 19);
            this.etatLabel.TabIndex = 3;
            this.etatLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // majButton
            // 
            this.majButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.majButton.ForeColor = System.Drawing.Color.Black;
            this.majButton.Location = new System.Drawing.Point(266, 317);
            this.majButton.Name = "majButton";
            this.majButton.Size = new System.Drawing.Size(83, 30);
            this.majButton.TabIndex = 4;
            this.majButton.Text = "Mettre à jour";
            this.majButton.UseVisualStyleBackColor = false;
            this.majButton.Click += new System.EventHandler(this.majButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 379);
            this.Controls.Add(this.majButton);
            this.Controls.Add(this.etatLabel);
            this.Controls.Add(this.Fiche);
            this.Controls.Add(this.dataGridView);
            this.Name = "Form1";
            this.Text = "GSB - Gestion des frais";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label Fiche;
        private System.Windows.Forms.Label etatLabel;
        private System.Windows.Forms.Button majButton;
    }
}

