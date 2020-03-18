namespace DBX2_JP_MOD_TOOL
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDB = new System.Windows.Forms.Label();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.btnDB = new System.Windows.Forms.Button();
            this.btnXlsx = new System.Windows.Forms.Button();
            this.txtXlsx = new System.Windows.Forms.TextBox();
            this.lblXlsx = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(12, 9);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(213, 12);
            this.lblDB.TabIndex = 0;
            this.lblDB.Text = "Dragon Ball Xenoverse 2 のインストール先";
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(12, 24);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(275, 19);
            this.txtDB.TabIndex = 1;
            // 
            // btnDB
            // 
            this.btnDB.Location = new System.Drawing.Point(293, 22);
            this.btnDB.Name = "btnDB";
            this.btnDB.Size = new System.Drawing.Size(75, 23);
            this.btnDB.TabIndex = 2;
            this.btnDB.Text = "参照";
            this.btnDB.UseVisualStyleBackColor = true;
            this.btnDB.Click += new System.EventHandler(this.btnDB_Click);
            // 
            // btnXlsx
            // 
            this.btnXlsx.Location = new System.Drawing.Point(291, 69);
            this.btnXlsx.Name = "btnXlsx";
            this.btnXlsx.Size = new System.Drawing.Size(75, 23);
            this.btnXlsx.TabIndex = 5;
            this.btnXlsx.Text = "参照";
            this.btnXlsx.UseVisualStyleBackColor = true;
            this.btnXlsx.Click += new System.EventHandler(this.btnXlsx_Click);
            // 
            // txtXlsx
            // 
            this.txtXlsx.Location = new System.Drawing.Point(10, 71);
            this.txtXlsx.Name = "txtXlsx";
            this.txtXlsx.Size = new System.Drawing.Size(275, 19);
            this.txtXlsx.TabIndex = 4;
            // 
            // lblXlsx
            // 
            this.lblXlsx.AutoSize = true;
            this.lblXlsx.Location = new System.Drawing.Point(10, 56);
            this.lblXlsx.Name = "lblXlsx";
            this.lblXlsx.Size = new System.Drawing.Size(113, 12);
            this.lblXlsx.TabIndex = 3;
            this.lblXlsx.Text = "作業所のExcelファイル";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(10, 118);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(356, 216);
            this.txtLog.TabIndex = 6;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(12, 103);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(23, 12);
            this.lblLog.TabIndex = 7;
            this.lblLog.Text = "ログ";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(293, 340);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "開始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 375);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnXlsx);
            this.Controls.Add(this.txtXlsx);
            this.Controls.Add(this.lblXlsx);
            this.Controls.Add(this.btnDB);
            this.Controls.Add(this.txtDB);
            this.Controls.Add(this.lblDB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Dragon Ball Xenoverse JP MOD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Button btnDB;
        private System.Windows.Forms.Button btnXlsx;
        private System.Windows.Forms.TextBox txtXlsx;
        private System.Windows.Forms.Label lblXlsx;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnStart;
    }
}

