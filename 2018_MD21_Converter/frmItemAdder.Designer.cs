namespace Roccus_MultiTool
{
    partial class frmItemAdder
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableList = new System.Windows.Forms.ComboBox();
            this.DBTableBtn = new System.Windows.Forms.Button();
            this.armorHotfixBtn = new System.Windows.Forms.Button();
            this.weaponHotfixBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableList);
            this.groupBox1.Controls.Add(this.DBTableBtn);
            this.groupBox1.Location = new System.Drawing.Point(366, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 261);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DB Table Backup";
            // 
            // tableList
            // 
            this.tableList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tableList.FormattingEnabled = true;
            this.tableList.Location = new System.Drawing.Point(127, 105);
            this.tableList.Name = "tableList";
            this.tableList.Size = new System.Drawing.Size(146, 21);
            this.tableList.TabIndex = 3;
            // 
            // DBTableBtn
            // 
            this.DBTableBtn.Location = new System.Drawing.Point(111, 156);
            this.DBTableBtn.Name = "DBTableBtn";
            this.DBTableBtn.Size = new System.Drawing.Size(179, 80);
            this.DBTableBtn.TabIndex = 2;
            this.DBTableBtn.Text = "DB Table Backup";
            this.DBTableBtn.UseVisualStyleBackColor = true;
            this.DBTableBtn.Click += new System.EventHandler(this.DBTableBtn_Click);
            // 
            // armorHotfixBtn
            // 
            this.armorHotfixBtn.Location = new System.Drawing.Point(12, 12);
            this.armorHotfixBtn.Name = "armorHotfixBtn";
            this.armorHotfixBtn.Size = new System.Drawing.Size(348, 126);
            this.armorHotfixBtn.TabIndex = 0;
            this.armorHotfixBtn.Text = "Armor Hotfix";
            this.armorHotfixBtn.UseVisualStyleBackColor = true;
            this.armorHotfixBtn.Click += new System.EventHandler(this.armorHotfixBtn_Click);
            // 
            // weaponHotfixBtn
            // 
            this.weaponHotfixBtn.Location = new System.Drawing.Point(12, 144);
            this.weaponHotfixBtn.Name = "weaponHotfixBtn";
            this.weaponHotfixBtn.Size = new System.Drawing.Size(348, 129);
            this.weaponHotfixBtn.TabIndex = 1;
            this.weaponHotfixBtn.Text = "Weapon Hotfix";
            this.weaponHotfixBtn.UseVisualStyleBackColor = true;
            this.weaponHotfixBtn.Click += new System.EventHandler(this.weaponHotfixBtn_Click);
            // 
            // frmItemAdder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 285);
            this.Controls.Add(this.weaponHotfixBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.armorHotfixBtn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmItemAdder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface";
            this.Load += new System.EventHandler(this.frmItemAdder_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button DBTableBtn;
        private System.Windows.Forms.ComboBox tableList;
        private System.Windows.Forms.Button armorHotfixBtn;
        private System.Windows.Forms.Button weaponHotfixBtn;
    }
}