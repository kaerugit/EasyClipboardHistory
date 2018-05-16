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
            this.toolStripMenuItemEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRirekiCount = new System.Windows.Forms.TextBox();
            this.keyboardHook1 = new HongliangSoft.Utilities.Gui.KeyboardHook();
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
            this.toolStripMenuItemEnd});
            this.contextMenuStripEnd.Name = "contextMenuStripEnd";
            this.contextMenuStripEnd.Size = new System.Drawing.Size(101, 26);
            // 
            // toolStripMenuItemEnd
            // 
            this.toolStripMenuItemEnd.Name = "toolStripMenuItemEnd";
            this.toolStripMenuItemEnd.Size = new System.Drawing.Size(100, 22);
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
            this.txtRirekiCount.Size = new System.Drawing.Size(152, 19);
            this.txtRirekiCount.TabIndex = 3;
            this.txtRirekiCount.TextChanged += new System.EventHandler(this.txtRirekiCount_TextChanged);
            // 
            // keyboardHook1
            // 
            this.keyboardHook1.KeyboardHooked += new HongliangSoft.Utilities.Gui.KeyboardHookedEventHandler(this.keyboardHook1_KeyboardHooked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
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
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
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
    }
}

