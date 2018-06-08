namespace EasyClipboardHistory
{
    partial class Main
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripEnd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuClipboardHistoryClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAllRirekiCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkClipboardHistorySave = new System.Windows.Forms.CheckBox();
            this.txtFixedFormRei = new System.Windows.Forms.TextBox();
            this.btnFixedFormDelete = new System.Windows.Forms.Button();
            this.lstFixedForm = new System.Windows.Forms.ListBox();
            this.btnFixedFormAdd = new System.Windows.Forms.Button();
            this.txtFixedForm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRirekiCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnResultCopy = new System.Windows.Forms.Button();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.keyboardHook1 = new HongliangSoft.Utilities.Gui.KeyboardHook();
            this.contextMenuStripEnd.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStrip1_Closed);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStripEnd;
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStripEnd
            // 
            this.contextMenuStripEnd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuClipboardHistoryClear,
            this.toolStripMenuItemSetting,
            this.toolStripMenuItemEnd});
            this.contextMenuStripEnd.Name = "contextMenuStripEnd";
            this.contextMenuStripEnd.Size = new System.Drawing.Size(135, 70);
            // 
            // toolStripMenuClipboardHistoryClear
            // 
            this.toolStripMenuClipboardHistoryClear.Name = "toolStripMenuClipboardHistoryClear";
            this.toolStripMenuClipboardHistoryClear.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuClipboardHistoryClear.Text = "履歴のクリア";
            this.toolStripMenuClipboardHistoryClear.Click += new System.EventHandler(this.toolStripMenuClipboardHistoryClear_Click);
            // 
            // toolStripMenuItemSetting
            // 
            this.toolStripMenuItemSetting.Name = "toolStripMenuItemSetting";
            this.toolStripMenuItemSetting.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItemSetting.Text = "設定";
            this.toolStripMenuItemSetting.Click += new System.EventHandler(this.toolStripMenuItemSetting_Click);
            // 
            // toolStripMenuItemEnd
            // 
            this.toolStripMenuItemEnd.Name = "toolStripMenuItemEnd";
            this.toolStripMenuItemEnd.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItemEnd.Text = "終了";
            this.toolStripMenuItemEnd.Click += new System.EventHandler(this.toolStripMenuItemEnd_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(319, 318);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtAllRirekiCount);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.chkClipboardHistorySave);
            this.tabPage1.Controls.Add(this.txtFixedFormRei);
            this.tabPage1.Controls.Add(this.btnFixedFormDelete);
            this.tabPage1.Controls.Add(this.lstFixedForm);
            this.tabPage1.Controls.Add(this.btnFixedFormAdd);
            this.tabPage1.Controls.Add(this.txtFixedForm);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtRirekiCount);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(311, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "設定";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(125, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 19);
            this.label5.TabIndex = 22;
            this.label5.Text = "※履歴検索タブで検索可能";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAllRirekiCount
            // 
            this.txtAllRirekiCount.Location = new System.Drawing.Point(187, 64);
            this.txtAllRirekiCount.Name = "txtAllRirekiCount";
            this.txtAllRirekiCount.Size = new System.Drawing.Size(99, 19);
            this.txtAllRirekiCount.TabIndex = 21;
            this.txtAllRirekiCount.TextChanged += new System.EventHandler(this.txtAllRirekiCount_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 19);
            this.label4.TabIndex = 20;
            this.label4.Text = "履歴検索用保存件数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkClipboardHistorySave
            // 
            this.chkClipboardHistorySave.AutoSize = true;
            this.chkClipboardHistorySave.Location = new System.Drawing.Point(62, 38);
            this.chkClipboardHistorySave.Name = "chkClipboardHistorySave";
            this.chkClipboardHistorySave.Size = new System.Drawing.Size(100, 16);
            this.chkClipboardHistorySave.TabIndex = 19;
            this.chkClipboardHistorySave.Text = "履歴を保存する";
            this.chkClipboardHistorySave.UseVisualStyleBackColor = true;
            this.chkClipboardHistorySave.Click += new System.EventHandler(this.chkClipboardHistorySave_Click);
            // 
            // txtFixedFormRei
            // 
            this.txtFixedFormRei.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFixedFormRei.Location = new System.Drawing.Point(109, 258);
            this.txtFixedFormRei.Name = "txtFixedFormRei";
            this.txtFixedFormRei.Size = new System.Drawing.Size(99, 12);
            this.txtFixedFormRei.TabIndex = 18;
            // 
            // btnFixedFormDelete
            // 
            this.btnFixedFormDelete.Location = new System.Drawing.Point(28, 254);
            this.btnFixedFormDelete.Name = "btnFixedFormDelete";
            this.btnFixedFormDelete.Size = new System.Drawing.Size(75, 23);
            this.btnFixedFormDelete.TabIndex = 17;
            this.btnFixedFormDelete.Text = "削除";
            this.btnFixedFormDelete.UseVisualStyleBackColor = true;
            this.btnFixedFormDelete.Click += new System.EventHandler(this.btnFixedFormDelete_Click);
            // 
            // lstFixedForm
            // 
            this.lstFixedForm.FormattingEnabled = true;
            this.lstFixedForm.ItemHeight = 12;
            this.lstFixedForm.Location = new System.Drawing.Point(28, 160);
            this.lstFixedForm.Name = "lstFixedForm";
            this.lstFixedForm.Size = new System.Drawing.Size(258, 88);
            this.lstFixedForm.TabIndex = 16;
            // 
            // btnFixedFormAdd
            // 
            this.btnFixedFormAdd.Location = new System.Drawing.Point(211, 126);
            this.btnFixedFormAdd.Name = "btnFixedFormAdd";
            this.btnFixedFormAdd.Size = new System.Drawing.Size(75, 23);
            this.btnFixedFormAdd.TabIndex = 15;
            this.btnFixedFormAdd.Text = "追加";
            this.btnFixedFormAdd.UseVisualStyleBackColor = true;
            this.btnFixedFormAdd.Click += new System.EventHandler(this.btnFixedFormAdd_Click);
            // 
            // txtFixedForm
            // 
            this.txtFixedForm.Location = new System.Drawing.Point(106, 128);
            this.txtFixedForm.Name = "txtFixedForm";
            this.txtFixedForm.Size = new System.Drawing.Size(99, 19);
            this.txtFixedForm.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(26, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "定型";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRirekiCount
            // 
            this.txtRirekiCount.Location = new System.Drawing.Point(187, 14);
            this.txtRirekiCount.Name = "txtRirekiCount";
            this.txtRirekiCount.Size = new System.Drawing.Size(99, 19);
            this.txtRirekiCount.TabIndex = 12;
            this.txtRirekiCount.TextChanged += new System.EventHandler(this.txtRirekiCount_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "履歴保存件数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnResultCopy);
            this.tabPage2.Controls.Add(this.lstResult);
            this.tabPage2.Controls.Add(this.btnSearch);
            this.tabPage2.Controls.Add(this.txtSearch);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(311, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "履歴検索";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnResultCopy
            // 
            this.btnResultCopy.Location = new System.Drawing.Point(8, 261);
            this.btnResultCopy.Name = "btnResultCopy";
            this.btnResultCopy.Size = new System.Drawing.Size(295, 23);
            this.btnResultCopy.TabIndex = 21;
            this.btnResultCopy.Text = "コピー";
            this.btnResultCopy.UseVisualStyleBackColor = true;
            this.btnResultCopy.Click += new System.EventHandler(this.btnResultCopy_Click);
            // 
            // lstResult
            // 
            this.lstResult.FormattingEnabled = true;
            this.lstResult.ItemHeight = 12;
            this.lstResult.Location = new System.Drawing.Point(8, 34);
            this.lstResult.Name = "lstResult";
            this.lstResult.Size = new System.Drawing.Size(295, 220);
            this.lstResult.TabIndex = 20;
            this.lstResult.SelectedIndexChanged += new System.EventHandler(this.lstResult_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(228, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(86, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(136, 19);
            this.txtSearch.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 17;
            this.label3.Text = "検索ワード";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // keyboardHook1
            // 
            this.keyboardHook1.KeyboardHooked += new HongliangSoft.Utilities.Gui.KeyboardHookedEventHandler(this.keyboardHook1_KeyboardHooked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 318);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.contextMenuStripEnd.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private HongliangSoft.Utilities.Gui.KeyboardHook keyboardHook1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEnd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEnd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSetting;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuClipboardHistoryClear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox chkClipboardHistorySave;
        private System.Windows.Forms.TextBox txtFixedFormRei;
        private System.Windows.Forms.Button btnFixedFormDelete;
        private System.Windows.Forms.ListBox lstFixedForm;
        private System.Windows.Forms.Button btnFixedFormAdd;
        private System.Windows.Forms.TextBox txtFixedForm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRirekiCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lstResult;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnResultCopy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAllRirekiCount;
        private System.Windows.Forms.Label label4;
    }
}

