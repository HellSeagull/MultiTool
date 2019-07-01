namespace Roccus_MultiTool
{
    partial class frmArmorSmith
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.previewBtn = new System.Windows.Forms.Button();
            this.hotfixBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.itemIDTxt = new System.Windows.Forms.TextBox();
            this.iconFileDataIDTxt = new System.Windows.Forms.TextBox();
            this.materialBox = new System.Windows.Forms.ComboBox();
            this.inventoryTypeBox = new System.Windows.Forms.ComboBox();
            this.maxIDBtn = new System.Windows.Forms.Button();
            this.itemAppMaxIDBtn = new System.Windows.Forms.Button();
            this.displayIDTxt = new System.Windows.Forms.TextBox();
            this.itemID2Txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.itemModAppMaxIDBtn = new System.Windows.Forms.Button();
            this.itemID3Txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.itemDescTxt = new System.Windows.Forms.TextBox();
            this.itemNameTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.qualityBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.LegionBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logoBox);
            this.groupBox1.Controls.Add(this.previewBox);
            this.groupBox1.Location = new System.Drawing.Point(383, 413);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 199);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // logoBox
            // 
            this.logoBox.BackgroundImage = global::Roccus_MultiTool.Properties.Resources.temp;
            this.logoBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoBox.Location = new System.Drawing.Point(17, 29);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(57, 54);
            this.logoBox.TabIndex = 1;
            this.logoBox.TabStop = false;
            // 
            // previewBox
            // 
            this.previewBox.Location = new System.Drawing.Point(7, 20);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(307, 173);
            this.previewBox.TabIndex = 0;
            this.previewBox.TabStop = false;
            // 
            // previewBtn
            // 
            this.previewBtn.Location = new System.Drawing.Point(132, 517);
            this.previewBtn.Name = "previewBtn";
            this.previewBtn.Size = new System.Drawing.Size(114, 60);
            this.previewBtn.TabIndex = 1;
            this.previewBtn.Text = "Preview Armor";
            this.previewBtn.UseVisualStyleBackColor = true;
            this.previewBtn.Click += new System.EventHandler(this.previewBtn_Click);
            // 
            // hotfixBtn
            // 
            this.hotfixBtn.Location = new System.Drawing.Point(132, 433);
            this.hotfixBtn.Name = "hotfixBtn";
            this.hotfixBtn.Size = new System.Drawing.Size(114, 60);
            this.hotfixBtn.TabIndex = 2;
            this.hotfixBtn.Text = "Hotfix";
            this.hotfixBtn.UseVisualStyleBackColor = true;
            this.hotfixBtn.Click += new System.EventHandler(this.hotfixBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(91, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "item :";
            // 
            // itemIDTxt
            // 
            this.itemIDTxt.Location = new System.Drawing.Point(59, 88);
            this.itemIDTxt.Name = "itemIDTxt";
            this.itemIDTxt.Size = new System.Drawing.Size(100, 20);
            this.itemIDTxt.TabIndex = 4;
            // 
            // iconFileDataIDTxt
            // 
            this.iconFileDataIDTxt.Enabled = false;
            this.iconFileDataIDTxt.Location = new System.Drawing.Point(59, 143);
            this.iconFileDataIDTxt.Name = "iconFileDataIDTxt";
            this.iconFileDataIDTxt.Size = new System.Drawing.Size(100, 20);
            this.iconFileDataIDTxt.TabIndex = 5;
            // 
            // materialBox
            // 
            this.materialBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialBox.FormattingEnabled = true;
            this.materialBox.Location = new System.Drawing.Point(48, 205);
            this.materialBox.Name = "materialBox";
            this.materialBox.Size = new System.Drawing.Size(121, 21);
            this.materialBox.TabIndex = 6;
            // 
            // inventoryTypeBox
            // 
            this.inventoryTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inventoryTypeBox.FormattingEnabled = true;
            this.inventoryTypeBox.Location = new System.Drawing.Point(48, 267);
            this.inventoryTypeBox.Name = "inventoryTypeBox";
            this.inventoryTypeBox.Size = new System.Drawing.Size(121, 21);
            this.inventoryTypeBox.TabIndex = 7;
            // 
            // maxIDBtn
            // 
            this.maxIDBtn.Location = new System.Drawing.Point(59, 59);
            this.maxIDBtn.Name = "maxIDBtn";
            this.maxIDBtn.Size = new System.Drawing.Size(100, 23);
            this.maxIDBtn.TabIndex = 9;
            this.maxIDBtn.Text = "max ID";
            this.maxIDBtn.UseVisualStyleBackColor = true;
            this.maxIDBtn.Click += new System.EventHandler(this.maxIDBtn_Click);
            // 
            // itemAppMaxIDBtn
            // 
            this.itemAppMaxIDBtn.Location = new System.Drawing.Point(211, 59);
            this.itemAppMaxIDBtn.Name = "itemAppMaxIDBtn";
            this.itemAppMaxIDBtn.Size = new System.Drawing.Size(100, 23);
            this.itemAppMaxIDBtn.TabIndex = 15;
            this.itemAppMaxIDBtn.Text = "max ID";
            this.itemAppMaxIDBtn.UseVisualStyleBackColor = true;
            this.itemAppMaxIDBtn.Click += new System.EventHandler(this.itemAppMaxIDBtn_Click);
            // 
            // displayIDTxt
            // 
            this.displayIDTxt.Location = new System.Drawing.Point(211, 143);
            this.displayIDTxt.Name = "displayIDTxt";
            this.displayIDTxt.Size = new System.Drawing.Size(100, 20);
            this.displayIDTxt.TabIndex = 12;
            // 
            // itemID2Txt
            // 
            this.itemID2Txt.Location = new System.Drawing.Point(211, 88);
            this.itemID2Txt.Name = "itemID2Txt";
            this.itemID2Txt.Size = new System.Drawing.Size(100, 20);
            this.itemID2Txt.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(228, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "item_app :";
            // 
            // itemModAppMaxIDBtn
            // 
            this.itemModAppMaxIDBtn.Location = new System.Drawing.Point(366, 59);
            this.itemModAppMaxIDBtn.Name = "itemModAppMaxIDBtn";
            this.itemModAppMaxIDBtn.Size = new System.Drawing.Size(100, 23);
            this.itemModAppMaxIDBtn.TabIndex = 20;
            this.itemModAppMaxIDBtn.Text = "max ID";
            this.itemModAppMaxIDBtn.UseVisualStyleBackColor = true;
            this.itemModAppMaxIDBtn.Click += new System.EventHandler(this.itemModAppMaxIDBtn_Click);
            // 
            // itemID3Txt
            // 
            this.itemID3Txt.Location = new System.Drawing.Point(366, 88);
            this.itemID3Txt.Name = "itemID3Txt";
            this.itemID3Txt.Size = new System.Drawing.Size(100, 20);
            this.itemID3Txt.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(359, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "item_mod_app :";
            // 
            // itemDescTxt
            // 
            this.itemDescTxt.Location = new System.Drawing.Point(527, 143);
            this.itemDescTxt.Name = "itemDescTxt";
            this.itemDescTxt.Size = new System.Drawing.Size(100, 20);
            this.itemDescTxt.TabIndex = 23;
            // 
            // itemNameTxt
            // 
            this.itemNameTxt.Location = new System.Drawing.Point(527, 88);
            this.itemNameTxt.Name = "itemNameTxt";
            this.itemNameTxt.Size = new System.Drawing.Size(100, 20);
            this.itemNameTxt.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(534, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "item_sparse :";
            // 
            // qualityBox
            // 
            this.qualityBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qualityBox.FormattingEnabled = true;
            this.qualityBox.Location = new System.Drawing.Point(517, 205);
            this.qualityBox.Name = "qualityBox";
            this.qualityBox.Size = new System.Drawing.Size(121, 21);
            this.qualityBox.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(92, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Icon";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(92, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Material";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(72, 251);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Inventory Type";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(238, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Display ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(561, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(561, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Desc";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(555, 189);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "Quality";
            // 
            // LegionBox
            // 
            this.LegionBox.AutoSize = true;
            this.LegionBox.Location = new System.Drawing.Point(12, 456);
            this.LegionBox.Name = "LegionBox";
            this.LegionBox.Size = new System.Drawing.Size(74, 17);
            this.LegionBox.TabIndex = 35;
            this.LegionBox.Text = "735 Mode";
            this.LegionBox.UseVisualStyleBackColor = true;
            // 
            // frmArmorSmith
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 624);
            this.Controls.Add(this.LegionBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.qualityBox);
            this.Controls.Add(this.itemDescTxt);
            this.Controls.Add(this.itemNameTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.itemModAppMaxIDBtn);
            this.Controls.Add(this.itemID3Txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.itemAppMaxIDBtn);
            this.Controls.Add(this.displayIDTxt);
            this.Controls.Add(this.itemID2Txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maxIDBtn);
            this.Controls.Add(this.inventoryTypeBox);
            this.Controls.Add(this.materialBox);
            this.Controls.Add(this.iconFileDataIDTxt);
            this.Controls.Add(this.itemIDTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hotfixBtn);
            this.Controls.Add(this.previewBtn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmArmorSmith";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotfix Armor";
            this.Load += new System.EventHandler(this.frmArmorSmith_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Button previewBtn;
        private System.Windows.Forms.Button hotfixBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox itemIDTxt;
        private System.Windows.Forms.TextBox iconFileDataIDTxt;
        private System.Windows.Forms.ComboBox materialBox;
        private System.Windows.Forms.ComboBox inventoryTypeBox;
        private System.Windows.Forms.Button maxIDBtn;
        private System.Windows.Forms.Button itemAppMaxIDBtn;
        private System.Windows.Forms.TextBox displayIDTxt;
        private System.Windows.Forms.TextBox itemID2Txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button itemModAppMaxIDBtn;
        private System.Windows.Forms.TextBox itemID3Txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox itemDescTxt;
        private System.Windows.Forms.TextBox itemNameTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox qualityBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox LegionBox;
    }
}