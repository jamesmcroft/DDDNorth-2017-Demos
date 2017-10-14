namespace MyEnterpriseWinFormsApp.Forms
{
    partial class MainForm
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.header = new System.Windows.Forms.Label();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.saveLabel = new System.Windows.Forms.Label();
            this.editLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGrid.Location = new System.Drawing.Point(19, 74);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.Size = new System.Drawing.Size(846, 546);
            this.dataGrid.TabIndex = 1;
            // 
            // header
            // 
            this.header.AutoSize = true;
            this.header.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.header.Location = new System.Drawing.Point(12, 9);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(155, 39);
            this.header.TabIndex = 2;
            this.header.Text = "Products";
            // 
            // headerPanel
            // 
            this.headerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerPanel.BackColor = System.Drawing.Color.DarkOrange;
            this.headerPanel.Controls.Add(this.saveLabel);
            this.headerPanel.Controls.Add(this.editLabel);
            this.headerPanel.Controls.Add(this.header);
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(886, 59);
            this.headerPanel.TabIndex = 3;
            // 
            // saveLabel
            // 
            this.saveLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveLabel.AutoSize = true;
            this.saveLabel.Font = new System.Drawing.Font("Segoe MDL2 Assets", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.saveLabel.Location = new System.Drawing.Point(815, 13);
            this.saveLabel.Name = "saveLabel";
            this.saveLabel.Size = new System.Drawing.Size(50, 35);
            this.saveLabel.TabIndex = 4;
            this.saveLabel.Text = "";
            this.saveLabel.Visible = false;
            this.saveLabel.Click += new System.EventHandler(this.OnSaveClicked);
            // 
            // editLabel
            // 
            this.editLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editLabel.AutoSize = true;
            this.editLabel.Font = new System.Drawing.Font("Segoe MDL2 Assets", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.editLabel.Location = new System.Drawing.Point(815, 13);
            this.editLabel.Name = "editLabel";
            this.editLabel.Size = new System.Drawing.Size(50, 35);
            this.editLabel.TabIndex = 3;
            this.editLabel.Text = "";
            this.editLabel.Click += new System.EventHandler(this.OnEditClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 632);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.headerPanel);
            this.Name = "MainForm";
            this.Text = "Our Company Products";
            this.Load += new System.EventHandler(this.OnFormLoaded);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label editLabel;
        private System.Windows.Forms.Label saveLabel;
    }
}

