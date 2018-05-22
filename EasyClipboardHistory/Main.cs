using API;
using ClipboardViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyClipboardHistory
{
    public partial class Main : Form
    {
        private MyClipboardViewer viewer;

        private IntPtr ForehWnd = IntPtr.Zero;

        private bool IsEndFlag = false;
        private bool isContextMenuStripOpen = false;

        /// <summary>表示する文字数</summary>
    
        const int DISPLAY_LENGTH = 40;

        /// <summary>（定型時使用）改行コード</summary>
        const string KAIGYO_CODE = @"[\n]";

        private bool isFixedFormExists = false;

        /// <summary>データ件数をマイナス</summary>
        public int CountMinus {
            get{
                var returnValue = 0;
                if (isFixedFormExists == true)
                {
                    returnValue += 1;
                }
                return returnValue;
            }
        }

        ToolStripMenuItem itemFixedForm = null;

        public Main()
        {
            //クリップボード
            viewer = new MyClipboardViewer(this);

            InitializeComponent();

            //クリップボード イベントハンドラを登録
            viewer.ClipboardHandler += this.OnClipBoardChanged;

        }
        DateTime prev_time = DateTime.Now;

        private void keyboardHook1_KeyboardHooked(object sender, HongliangSoft.Utilities.Gui.KeyboardHookedEventArgs e)
        {
            //デバッグで実行するには　デバッグ　VS　ホスティングプロセスを有効にする　のチェックを外す

            //左ShiftKey　ダブルクリック
            if (e.UpDown == HongliangSoft.Utilities.Gui.KeyboardUpDown.Up && e.KeyCode == Keys.LShiftKey)
            {
                DateTime now = DateTime.Now;
                TimeSpan ts = now - prev_time;
                prev_time = now;

                var doubleClickTime = APIList.GetDoubleClickTime();


                if (ts.TotalMilliseconds <= doubleClickTime)
                {

                    //すでに開いている場合は無視
                    if (isContextMenuStripOpen == true)
                    {
                        return;
                    }

                    //フォーカス位置の取得
                    ForehWnd = APIList.GetForegroundWindow();
                    var current = APIList.GetCurrentThreadId();
                    var target = APIList.GetWindowThreadProcessId(ForehWnd, IntPtr.Zero);
                    Point p;
                    APIList.AttachThreadInput(current, target, true);
                    APIList.GetCaretPos(out p);
                    var fWnd = APIList.GetFocus();
                    APIList.ClientToScreen(fWnd,out p);
                    APIList.AttachThreadInput(current, target, false);

                    //ダブルクリック処理
                    var task = Task.Factory.StartNew(
                        () =>
                        {
                            this.Invoke(
                                    (MethodInvoker)(() =>
                                    {
                                        Thread.Sleep(200);
                    
                                        //APIList.SetTopMost(this.Handle);
                                        this.contextMenuStrip1.Show(p.X, p.Y);

                                        this.contextMenuStrip1.Focus();
                                        APIList.SetForegroundWindow(this.contextMenuStrip1.Handle);

                                        if (this.contextMenuStrip1.Items.Count > 0)
                                        {
                                            this.contextMenuStrip1.Items[0].Select();
                                        }
                                    }
                                    )
                                );

                        }
                    );

                }
            }
            else if (e.UpDown == HongliangSoft.Utilities.Gui.KeyboardUpDown.Up && e.KeyCode == Keys.Escape)
            {
                //キャンセルの処理
                if (isContextMenuStripOpen == true)
                {
                    this.contextMenuStrip1.Close();
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Icon = this.Icon;
            this.notifyIcon1.BalloonTipText = "（左）shiftキーダブルクリック\n選択でクリップボードにコピー（貼付）";
            this.notifyIcon1.BalloonTipTitle = "";
            this.notifyIcon1.Text = "";

            this.notifyIcon1.ShowBalloonTip(500);

            this.txtRirekiCount.Text = Properties.Settings.Default.RirekiCount.ToString();

            txtFixedFormRei.BackColor = this.BackColor;
            txtFixedFormRei.Text = "改行は " + KAIGYO_CODE + " をセット";

            //履歴データの復元
            if (Properties.Settings.Default.RirekiKeyData == null)
            {
                Properties.Settings.Default.RirekiKeyData = new System.Collections.Specialized.StringCollection();
            }

            if (Properties.Settings.Default.RirekiTagData == null)
            {
                Properties.Settings.Default.RirekiTagData = new System.Collections.Specialized.StringCollection();
            }

            if (Properties.Settings.Default.RirekiKeyData.Count > 0) { 
                this.contextMenuStrip1.Items.Clear();
                for (var i = 0; i < Properties.Settings.Default.RirekiKeyData.Count ; i++)
                {
                    try
                    {
                        var item = new ToolStripMenuItem();

                        item.Text = Properties.Settings.Default.RirekiKeyData[i];
                        item.Tag = Properties.Settings.Default.RirekiTagData[i];

                        this.contextMenuStrip1.Items.Add(item);
                    }
                    catch { }
                }
            }

            //定型のセット
            saveFixedFormValue(true);
          
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsEndFlag == false)
            {
                e.Cancel = true;
                this.Hide();
                return;
            }

            //履歴の保存
            Properties.Settings.Default.RirekiKeyData.Clear();
            Properties.Settings.Default.RirekiTagData.Clear();

            for (var i = 0; i < this.contextMenuStrip1.Items.Count ; i++)
            {
                ToolStripItem item = this.contextMenuStrip1.Items[i];
                if (item.Tag != null)
                {
                    Properties.Settings.Default.RirekiKeyData.Add(item.Text);
                    Properties.Settings.Default.RirekiTagData.Add(item.Tag.ToString());
                }
            }
            
            Properties.Settings.Default.Save();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Opacity = 0;
        }

        private void OnClipBoardChanged(object sender, ClipboardEventArgs args)
        {

            if (Clipboard.ContainsText(System.Windows.Forms.TextDataFormat.Text))
            {
                var clipText = (String)System.Windows.Forms.Clipboard.GetData(System.Windows.Forms.DataFormats.StringFormat);

                var item = getClipTextItem(clipText);

                if (item == null)
                {
                    return;
                }

                if (this.contextMenuStrip1.Items.Count > 0 && this.contextMenuStrip1.Items[0].Tag !=null)
                {
                    if ((String)(this.contextMenuStrip1.Items[0].Tag) == (String)clipText)
                    {
                        return;
                    }
                }
                this.contextMenuStrip1.Items.Insert(0, item);

                //履歴数を超えた場合の削除処理
                int MaxCountData = Properties.Settings.Default.RirekiCount;
                if (this.contextMenuStrip1.Items.Count - this.CountMinus > MaxCountData)
                {
                    for (int index = this.contextMenuStrip1.Items.Count - 1; index >= MaxCountData; index--)
                    {
                        this.contextMenuStrip1.Items.RemoveAt(index);
                    }
                }
            }
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            isContextMenuStripOpen = true;
        }

        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            isContextMenuStripOpen = false;
        }

        //private bool IsClipCopy = false;
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var tag =((ToolStripMenuItem)(e.ClickedItem)).Tag;
            if (tag == null)
            {
                return;
            }

            var text = (string)tag;
            this.contextMenuStrip1.Items.Remove((ToolStripMenuItem)(e.ClickedItem));    //削除

            setClipboard(text);
            //this.notifyIcon1.BalloonTipText = "クリップボードにコピーしました。";
            //this.notifyIcon1.ShowBalloonTip(50);
        }

        private void toolStripMenuItemEnd_Click(object sender, EventArgs e)
        {
            IsEndFlag = true;
            Application.Exit();
        }

        private void toolStripMenuItemSetting_Click(object sender, EventArgs e)
        {
            notifyIcon1_DoubleClick(sender, e);
        }

        /// <summary>タスクトレイ</summary>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Opacity = 100;
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;

            //this.Update();

            this.Activate();
        }

        private void txtRirekiCount_TextChanged(object sender, EventArgs e)
        {
            int value = int.MinValue;
            if (this.txtRirekiCount.Text.Length > 0)
            {
                if (int.TryParse(this.txtRirekiCount.Text, out value))
                {
                    Properties.Settings.Default.RirekiCount = value;
                }
            }

            if (value <= 0)
            {
                Properties.Settings.Default.RirekiCount = 20;
            }
        }


        private void setClipboard(string text)
        {
            System.Windows.Forms.Clipboard.SetText(text);

            if (ForehWnd != IntPtr.Zero)
            {
                this.contextMenuStrip1.Hide();

                APIList.SetActiveWindow(ForehWnd);
                Thread.Sleep(50);

                //貼付処理
                //SendKeys.Send("^V");
                var VK_CONTROL = 0x11;
                var KEYEVENTF_KEYDOWN = 0x0;
                var KEYEVENTF_KEYUP = 0x2;
                APIList.keybd_event((byte)VK_CONTROL, (byte)0, (uint)KEYEVENTF_KEYDOWN, (UIntPtr)0);
                APIList.keybd_event((byte)Keys.V, (byte)0, (uint)KEYEVENTF_KEYDOWN, (UIntPtr)0);
                Thread.Sleep(50);
                APIList.keybd_event((byte)Keys.V, (byte)0, (uint)KEYEVENTF_KEYUP, (UIntPtr)0);
                APIList.keybd_event((byte)VK_CONTROL, (byte)0, (uint)KEYEVENTF_KEYUP, (UIntPtr)0);

            }
        }

        private void btnFixedFormAdd_Click(object sender, EventArgs e)
        {
            if (this.txtFixedForm.Text.Equals("") == false)
            {
                for (var i = 0; i < this.lstFixedForm.Items.Count; i++)
                {
                    if (this.txtFixedForm.Text.Equals(this.lstFixedForm.Items[i].ToString()))
                    {
                        MessageBox.Show("既に追加済です。");
                        return;
                    }
                }
                this.lstFixedForm.Items.Add(this.txtFixedForm.Text);
                saveFixedFormValue(false);
            }
        }

        private void btnFixedFormDelete_Click(object sender, EventArgs e)
        {
            this.lstFixedForm.Items.Remove(this.lstFixedForm.SelectedItem);
            saveFixedFormValue(false);
        }

        private void saveFixedFormValue(bool firstFlag )
        {

            //最初はListboxに追加するだけ
            if (firstFlag == true)
            {
                if (Properties.Settings.Default.RirekiFixedForm == null)
                {
                    Properties.Settings.Default.RirekiFixedForm = new System.Collections.Specialized.StringCollection();
                }
                this.lstFixedForm.Items.Clear();
                this.lstFixedForm.Items.AddRange(Properties.Settings.Default.RirekiFixedForm.OfType<object>().ToArray<object>());

            }
            else
            {
                Properties.Settings.Default.RirekiFixedForm.Clear();
                Properties.Settings.Default.RirekiFixedForm.AddRange(this.lstFixedForm.Items.OfType<string>().ToArray<string>());
            }


            if (Properties.Settings.Default.RirekiFixedForm.Count == 0)
            {
                isFixedFormExists = false;

                if (itemFixedForm != null)
                {
                    this.contextMenuStrip1.Items.Remove(itemFixedForm);
                    itemFixedForm = null;
                }
            }
            else {
                isFixedFormExists = true;

                if (itemFixedForm == null)
                {
                    itemFixedForm = new ToolStripMenuItem();
                    itemFixedForm.Text = "（定型）";

                    this.contextMenuStrip1.Items.Add(itemFixedForm);
                }

                itemFixedForm.DropDownItems.Clear();

                for (var i = 0; i < Properties.Settings.Default.RirekiFixedForm.Count; i++)
                {
                    var value = Properties.Settings.Default.RirekiFixedForm[i];

                    value = value.Replace(KAIGYO_CODE, "\n");
                    
                    var item = getClipTextItem(value);

                    if (item == null)
                    {
                        continue;
                    }

                    item.Click += (parasender, parae) =>
                    {
                        var tag = ((ToolStripMenuItem)(parasender)).Tag;
                        if (tag == null)
                        {
                            return;
                        }
                        var text = (string)(tag);
                        setClipboard(text);
                    };

                    itemFixedForm.DropDownItems.Add(item);
                }
            }
        }

        private ToolStripMenuItem getClipTextItem(string clipText)
        {
            var dispText = clipText.Trim().Replace("\r", "").Replace("\n", "").Replace("\t", "");

            if (dispText.Length == 0)
            {
                return null;
            }

            //ポップアップに表示する文字数（本来は全角半角考慮したほうがよい・・）
            if (dispText.Length > DISPLAY_LENGTH)
            {
                dispText = dispText.Substring(0, DISPLAY_LENGTH);
            }

            var item = new ToolStripMenuItem();

            item.Text = dispText;
            item.Tag = clipText;

            return item;
        }

 
    }
}
