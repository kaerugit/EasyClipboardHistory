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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRirekiCount = new System.Windows.Forms.TextBox();
            this.txtFixedForm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFixedFormAdd = new System.Windows.Forms.Button();
            this.lstFixedForm = new System.Windows.Forms.ListBox();
            this.btnFixedFormDelete = new System.Windows.Forms.Button();
            this.txtFixedFormRei = new System.Windows.Forms.TextBox();
            this.keyboardHook1 = new HongliangSoft.Utilities.Gui.KeyboardHook();
            this.chkClipboardHistorySave = new System.Windows.Forms.CheckBox();
            this.contextMenuStripEnd.SuspendLayout();
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
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "履歴件数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRirekiCount
            // 
            this.txtRirekiCount.Location = new System.Drawing.Point(92, 9);
            this.txtRirekiCount.Name = "txtRirekiCount";
            this.txtRirekiCount.Size = new System.Drawing.Size(99, 19);
            this.txtRirekiCount.TabIndex = 3;
            this.txtRirekiCount.TextChanged += new System.EventHandler(this.txtRirekiCount_TextChanged);
            // 
            // txtFixedForm
            // 
            this.txtFixedForm.Location = new System.Drawing.Point(92, 74);
            this.txtFixedForm.Name = "txtFixedForm";
            this.txtFixedForm.Size = new System.Drawing.Size(99, 19);
            this.txtFixedForm.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "定型";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFixedFormAdd
            // 
            this.btnFixedFormAdd.Location = new System.Drawing.Point(197, 72);
            this.btnFixedFormAdd.Name = "btnFixedFormAdd";
            this.btnFixedFormAdd.Size = new System.Drawing.Size(75, 23);
            this.btnFixedFormAdd.TabIndex = 6;
            this.btnFixedFormAdd.Text = "追加";
            this.btnFixedFormAdd.UseVisualStyleBackColor = true;
            this.btnFixedFormAdd.Click += new System.EventHandler(this.btnFixedFormAdd_Click);
            // 
            // lstFixedForm
            // 
            this.lstFixedForm.FormattingEnabled = true;
            this.lstFixedForm.ItemHeight = 12;
            this.lstFixedForm.Location = new System.Drawing.Point(14, 106);
            this.lstFixedForm.Name = "lstFixedForm";
            this.lstFixedForm.Size = new System.Drawing.Size(258, 88);
            this.lstFixedForm.TabIndex = 7;
            // 
            // btnFixedFormDelete
            // 
            this.btnFixedFormDelete.Location = new System.Drawing.Point(14, 200);
            this.btnFixedFormDelete.Name = "btnFixedFormDelete";
            this.btnFixedFormDelete.Size = new System.Drawing.Size(75, 23);
            this.btnFixedFormDelete.TabIndex = 8;
            this.btnFixedFormDelete.Text = "削除";
            this.btnFixedFormDelete.UseVisualStyleBackColor = true;
            this.btnFixedFormDelete.Click += new System.EventHandler(this.btnFixedFormDelete_Click);
            // 
            // txtFixedFormRei
            // 
            this.txtFixedFormRei.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFixedFormRei.Location = new System.Drawing.Point(95, 204);
            this.txtFixedFormRei.Name = "txtFixedFormRei";
            this.txtFixedFormRei.Size = new System.Drawing.Size(99, 12);
            this.txtFixedFormRei.TabIndex = 9;
            // 
            // keyboardHook1
            // 
            this.keyboardHook1.KeyboardHooked += new HongliangSoft.Utilities.Gui.KeyboardHookedEventHandler(this.keyboardHook1_KeyboardHooked);
            // 
            // chkClipboardHistorySave
            // 
            this.chkClipboardHistorySave.AutoSize = true;
            this.chkClipboardHistorySave.Location = new System.Drawing.Point(22, 39);
            this.chkClipboardHistorySave.Name = "chkClipboardHistorySave";
            this.chkClipboardHistorySave.Size = new System.Drawing.Size(100, 16);
            this.chkClipboardHistorySave.TabIndex = 10;
            this.chkClipboardHistorySave.Text = "履歴を保存する";
            this.chkClipboardHistorySave.UseVisualStyleBackColor = true;
            this.chkClipboardHistorySave.Click += new System.EventHandler(this.chkClipboardHistorySave_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 241);
            this.Controls.Add(this.chkClipboardHistorySave);
            this.Controls.Add(this.txtFixedFormRei);
            this.Controls.Add(this.btnFixedFormDelete);
            this.Controls.Add(this.lstFixedForm);
            this.Controls.Add(this.btnFixedFormAdd);
            this.Controls.Add(this.txtFixedForm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRirekiCount);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.contextMenuStripEnd.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HongliangSoft.Utilities.Gui.KeyboardHook keyboardHook1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEnd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRirekiCount;
        private System.Windows.Forms.TextBox txtFixedForm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFixedFormAdd;
        private System.Windows.Forms.ListBox lstFixedForm;
        private System.Windows.Forms.Button btnFixedFormDelete;
        private System.Windows.Forms.TextBox txtFixedFormRei;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSetting;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuClipboardHistoryClear;
        private System.Windows.Forms.CheckBox chkClipboardHistorySave;
    }
}

