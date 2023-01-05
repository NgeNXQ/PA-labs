namespace DataTable
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
            this.indexRecords = new System.Windows.Forms.DataGridView();
            this.mainRecords = new System.Windows.Forms.DataGridView();
            this.overflowRecords = new System.Windows.Forms.DataGridView();
            this.Get = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.indexRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.overflowRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // indexRecords
            // 
            this.indexRecords.AllowUserToAddRows = false;
            this.indexRecords.AllowUserToDeleteRows = false;
            this.indexRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.indexRecords.Location = new System.Drawing.Point(12, 36);
            this.indexRecords.Name = "indexRecords";
            this.indexRecords.ReadOnly = true;
            this.indexRecords.Size = new System.Drawing.Size(243, 360);
            this.indexRecords.TabIndex = 0;
            // 
            // mainRecords
            // 
            this.mainRecords.AllowUserToAddRows = false;
            this.mainRecords.AllowUserToDeleteRows = false;
            this.mainRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainRecords.Location = new System.Drawing.Point(510, 36);
            this.mainRecords.Name = "mainRecords";
            this.mainRecords.ReadOnly = true;
            this.mainRecords.Size = new System.Drawing.Size(243, 360);
            this.mainRecords.TabIndex = 1;
            // 
            // overflowRecords
            // 
            this.overflowRecords.AllowUserToAddRows = false;
            this.overflowRecords.AllowUserToDeleteRows = false;
            this.overflowRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.overflowRecords.Location = new System.Drawing.Point(261, 36);
            this.overflowRecords.Name = "overflowRecords";
            this.overflowRecords.ReadOnly = true;
            this.overflowRecords.Size = new System.Drawing.Size(243, 360);
            this.overflowRecords.TabIndex = 2;
            // 
            // Get
            // 
            this.Get.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Get.Location = new System.Drawing.Point(769, 112);
            this.Get.Name = "Get";
            this.Get.Size = new System.Drawing.Size(113, 58);
            this.Get.TabIndex = 3;
            this.Get.Text = "Get";
            this.Get.UseVisualStyleBackColor = true;
            this.Get.Click += new System.EventHandler(this.Get_Click);
            // 
            // Edit
            // 
            this.Edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Edit.Location = new System.Drawing.Point(769, 187);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(113, 58);
            this.Edit.TabIndex = 4;
            this.Edit.Text = "Edit";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // Remove
            // 
            this.Remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Remove.Location = new System.Drawing.Point(769, 264);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(113, 58);
            this.Remove.TabIndex = 5;
            this.Remove.Text = "Remove";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // Clear
            // 
            this.Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear.Location = new System.Drawing.Point(769, 338);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(113, 58);
            this.Clear.TabIndex = 6;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Add
            // 
            this.Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add.Location = new System.Drawing.Point(769, 36);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(113, 58);
            this.Add.TabIndex = 7;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Index File Content";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(299, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Overflow File Content";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(563, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Main File Content";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 411);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.Edit);
            this.Controls.Add(this.Get);
            this.Controls.Add(this.overflowRecords);
            this.Controls.Add(this.mainRecords);
            this.Controls.Add(this.indexRecords);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(915, 450);
            this.MinimumSize = new System.Drawing.Size(915, 450);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.indexRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.overflowRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView indexRecords;
        private System.Windows.Forms.DataGridView mainRecords;
        private System.Windows.Forms.DataGridView overflowRecords;
        private System.Windows.Forms.Button Get;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}