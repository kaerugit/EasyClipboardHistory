using API;
using ClipboardViewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyClipboardHistory
{
    public partial class Main : Form
    {
        /// <summary>履歴を表示するPopupのショートカットキー</summary>
        public Keys PopupShortcutKey = Keys.LShiftKey;

        /// <summary>履歴を表示するPopupのショートカットキー(sub)</summary>
        /// <remarks>必要ない場合はnullをセット</remarks>
        public Keys? PopupShortcutKeySub = Keys.RShiftKey;

        /// <summary>表示する文字数</summary>
        const int DISPLAY_LENGTH = 40;

        /// <summary>（定型時使用）改行コード</summary>
        const string KAIGYO_CODE = @"[\n]";

        /// <summary>ダブルクリックの判定（時間）</summary>
        private int doubleClickTime = APIList.GetDoubleClickTime();

        private MyClipboardViewer viewer;

        /// <summary>前面のwindowのハンドル</summary>
        private IntPtr ForehWnd = IntPtr.Zero;

        private bool isEndFlag = false;
        private bool isContextMenuStripOpen = false;

        /// <summary>クリップボード履歴の件数</summary>
        /// <remarks></remarks>
        public int ClipboardHistoryCount
        {
            get
            {
                var returnValue = this.contextMenuStrip1.Items.Count;
                if (itemFixedForm != null)
                {
                    returnValue -= 1;
                }
                return returnValue;
            }
        }

        /// <summary>定型のメニュー</summary>
        ToolStripMenuItem itemFixedForm = null;

        List<string> ALLClipData = new List<string>();
        private ToolTip toolTipResult;

        public Main()
        {
            //クリップボード
            viewer = new MyClipboardViewer(this);

            InitializeComponent();

            //クリップボード イベントハンドラを登録
            viewer.ClipboardHandler += this.OnClipBoardChanged;

            toolTipResult = new ToolTip();

        }

        DateTime prev_time = DateTime.MinValue;
        DateTime up_time = DateTime.MinValue;

        private void keyboardHook1_KeyboardHooked(object sender, HongliangSoft.Utilities.Gui.KeyboardHookedEventArgs e)
        {
            //デバッグで実行するには　デバッグ　VS　ホスティングプロセスを有効にする　のチェックを外す
            if (e.KeyCode == this.PopupShortcutKey ||
                (this.PopupShortcutKeySub.HasValue == true && e.KeyCode == this.PopupShortcutKeySub.Value)
            )
            {
                if (e.UpDown == HongliangSoft.Utilities.Gui.KeyboardUpDown.Down)
                {
                    up_time = DateTime.Now;
                }
                else if (e.UpDown == HongliangSoft.Utilities.Gui.KeyboardUpDown.Up)
                {
                    DateTime now = DateTime.Now;

                    TimeSpan ts = now - up_time;
                    //down upの時間(押しっぱなしの場合は無視する) doubleClickTimeの流用は微妙かも・・
                    if (ts.TotalMilliseconds > doubleClickTime)
                    {
                        return;
                    }

                    //左ShiftKey　ダブルクリック
                    ts = now - prev_time;
                    prev_time = now;

                    if (ts.TotalMilliseconds <= doubleClickTime)
                    {

                        //すでに開いている場合は無視
                        if (isContextMenuStripOpen == true)
                        {
                            return;
                        }

                        //フォーカス位置の取得
                        this.ForehWnd = APIList.GetForegroundWindow();
                        var current = APIList.GetCurrentThreadId();
                        var target = APIList.GetWindowThreadProcessId(this.ForehWnd, IntPtr.Zero);
                        Point p;
                        APIList.AttachThreadInput(current, target, true);
                        APIList.GetCaretPos(out p);
                        var fWnd = APIList.GetFocus();
                        APIList.ClientToScreen(fWnd, out p);
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
            }
            else if (e.UpDown == HongliangSoft.Utilities.Gui.KeyboardUpDown.Up && e.KeyCode == Keys.Escape)
            {
                //キャンセルの処理
                if (isContextMenuStripOpen == true)
                {
                    this.contextMenuStrip1.Close();
                }
            }
            else
            {
                //ダブルクリックの間に違うキーが押された場合は一旦キャンセル
                up_time = DateTime.MinValue;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

            this.notifyIcon1.Icon = this.Icon;
            //this.notifyIcon1.BalloonTipText = "";
            //this.notifyIcon1.ShowBalloonTip(500);
            this.notifyIcon1.BalloonTipTitle = "";
            this.notifyIcon1.Text = "shiftキーダブルクリック\n選択でクリップボードにコピー（貼付）";

            //【設定】履歴数
            this.txtRirekiCount.Text = Properties.Settings.Default.RirekiCount.ToString();
            this.txtAllRirekiCount.Text = Properties.Settings.Default.ALLRirekiCount.ToString();

            //【設定】定型
            txtFixedFormRei.BackColor = this.BackColor;
            txtFixedFormRei.Text = "改行は " + KAIGYO_CODE + " をセット";

            if (Properties.Settings.Default.RirekiFixedForm == null)
            {
                Properties.Settings.Default.RirekiFixedForm = new System.Collections.Specialized.StringCollection();
            }
            this.lstFixedForm.Items.Clear();
            this.lstFixedForm.Items.AddRange(Properties.Settings.Default.RirekiFixedForm.OfType<object>().ToArray<object>());

            //【設定】履歴の保存
            //if (Properties.Settings.Default.RerekiSave == null)
            //{
            //Properties.Settings.Default.RerekiSave = true;
            //}
            this.chkClipboardHistorySave.Checked = Properties.Settings.Default.RerekiSave;

            this.chkSameWordAlert.Checked = Properties.Settings.Default.SameWordAlert;

            //popupmenuの初期化
            initContextMenu(false);

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isEndFlag == false)
            {
                e.Cancel = true;
                this.Hide();
                return;
            }

            if (Properties.Settings.Default.RerekiSave == true)
            {
                //履歴の保存
                Properties.Settings.Default.RirekiKeyData.Clear();
                Properties.Settings.Default.RirekiTagData.Clear();

                for (var i = 0; i < this.contextMenuStrip1.Items.Count; i++)
                {
                    ToolStripItem item = this.contextMenuStrip1.Items[i];
                    if (item.Tag != null)
                    {
                        Properties.Settings.Default.RirekiKeyData.Add(item.Text);
                        Properties.Settings.Default.RirekiTagData.Add(item.Tag.ToString());
                    }
                }
            }

            Properties.Settings.Default.Save();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Opacity = 0;
        }

        private Point oldPoint = new Point(0,0);
        private void OnClipBoardChanged(object sender, ClipboardEventArgs args)
        {

            if (Clipboard.ContainsText(System.Windows.Forms.TextDataFormat.Text))
            {
                var clipText = "";
                clipText = (String)System.Windows.Forms.Clipboard.GetData(System.Windows.Forms.DataFormats.StringFormat);

                using (MemoryStream ms = (MemoryStream)System.Windows.Forms.Clipboard.GetData("Csv"))
                {
                    //★特別処理★Excelからのコピー (改行がある場合ダブルコーテーションが追加されるので削除する)
                    //こちらの処理を入れない場合は一旦wordに貼り付けてコピー
                    if (ms != null)
                    {
                        if (clipText.Contains("\n") && clipText.StartsWith(@""""))
                        {
                            clipText = clipText.Replace(@"""""", "\t¶\t");
                            clipText = clipText.Replace(@"""", "");
                            clipText = clipText.Replace("\t¶\t", @"""");
                        }
                    }

                }

                var item = getClipTextItem(clipText);

                if (item == null)
                {
                    return;
                }

                Point p = new Point(0, 0);

                APIList.GetCursorPos(out p);

                try
                {
                    if (this.contextMenuStrip1.Items.Count > 0 && this.contextMenuStrip1.Items[0].Tag != null)
                    {
                        if ((String)(this.contextMenuStrip1.Items[0].Tag) == (String)clipText)
                        {

                            if (Properties.Settings.Default.SameWordAlert == true)
                            {

                                //キャレットの位置が違ったら（マウスの位置で判定しているのでカーソル移動の時は無視される（公認バグ？））
                                if ((oldPoint.X != p.X || oldPoint.Y != p.Y))
                                {

                                    this.Activate();
                                    MessageBox.Show("同じデータが追加されました。");
                                    //this.notifyIcon1.BalloonTipText = "同じデータが追加されました。";
                                    //this.notifyIcon1.ShowBalloonTip(100);

                                }
                            }

                            return;
                        }
                    }

                }
                finally
                {
                    oldPoint = p;
                }
                
                this.contextMenuStrip1.Items.Insert(0, item);

                //履歴数を超えた場合の削除処理
                int MaxCountData = Properties.Settings.Default.RirekiCount;
                if (this.ClipboardHistoryCount > MaxCountData)
                {
                    for (int index = this.contextMenuStrip1.Items.Count - 1; index >= MaxCountData; index--)
                    {
                        if (this.contextMenuStrip1.Items[index].Tag != null)
                        {
                            this.contextMenuStrip1.Items.RemoveAt(index);
                        }
                    }
                }

                //検索用の履歴
                int MaxSerchCountData = Properties.Settings.Default.ALLRirekiCount;

                if (this.ALLClipData.Contains(clipText) == false)
                {
                    this.ALLClipData.Insert(0, clipText);
                    if (this.ALLClipData.Count > MaxSerchCountData)
                    {
                        for (int index = this.ALLClipData.Count - 1; index >= MaxSerchCountData; index--)
                        {
                            this.ALLClipData.RemoveAt(index);
                        }
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
            var tag = ((ToolStripMenuItem)(e.ClickedItem)).Tag;
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

        /// <summary>タスクトレイ（終了）</summary>
        private void toolStripMenuItemEnd_Click(object sender, EventArgs e)
        {
            isEndFlag = true;
            Application.Exit();
        }

        /// <summary>タスクトレイ（設定）</summary>
        private void toolStripMenuItemSetting_Click(object sender, EventArgs e)
        {
            notifyIcon1_DoubleClick(sender, e);
        }

        /// <summary>タスクトレイ（履歴クリア）</summary>
        private void toolStripMenuClipboardHistoryClear_Click(object sender, EventArgs e)
        {
            initContextMenu(true);
        }

        /// <summary>タスクトレイダブルクリック</summary>
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

            //マイナスと100よりも大きい場合
            if (value <= 0 || value > 100)
            {
                Properties.Settings.Default.RirekiCount = 20;
            }
        }

        private void txtAllRirekiCount_TextChanged(object sender, EventArgs e)
        {
            int value = int.MinValue;
            if (this.txtAllRirekiCount.Text.Length > 0)
            {
                if (int.TryParse(this.txtAllRirekiCount.Text, out value))
                {
                    Properties.Settings.Default.ALLRirekiCount = value;
                }
            }

            //マイナスと100よりも大きい場合
            if (value <= 0 || value > 500)
            {
                Properties.Settings.Default.ALLRirekiCount = 100;
            }
        }

        private void chkClipboardHistorySave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RerekiSave = this.chkClipboardHistorySave.Checked;
        }

        private void chkSameWordAlert_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SameWordAlert = this.chkSameWordAlert.Checked ;
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

                saveFixedFormValue(true);
            }
        }

        private void btnFixedFormDelete_Click(object sender, EventArgs e)
        {
            this.lstFixedForm.Items.Remove(this.lstFixedForm.SelectedItem);
            saveFixedFormValue(true);
        }

        private void setClipboard(string text)
        {
            System.Windows.Forms.Clipboard.SetText(text);

            if (this.ForehWnd != IntPtr.Zero)
            {
                this.contextMenuStrip1.Hide();

                APIList.SetActiveWindow(this.ForehWnd);
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

        /// <summary>定型データの保存</summary>
        /// <param name="dataSyncFlag"></param>
        private void saveFixedFormValue(bool dataSyncFlag)
        {

            if (dataSyncFlag)
            {
                Properties.Settings.Default.RirekiFixedForm.Clear();
                Properties.Settings.Default.RirekiFixedForm.AddRange(this.lstFixedForm.Items.OfType<string>().ToArray<string>());
            }

            if (Properties.Settings.Default.RirekiFixedForm.Count == 0)
            {
                if (itemFixedForm != null)
                {
                    this.contextMenuStrip1.Items.Remove(itemFixedForm);
                    itemFixedForm = null;
                }
            }
            else
            {
                if (itemFixedForm == null)
                {
                    itemFixedForm = new ToolStripMenuItem();
                    itemFixedForm.Text = "（定型）";

                    this.contextMenuStrip1.Items.Add(itemFixedForm);
                }
                if (this.contextMenuStrip1.Items.Find(itemFixedForm.Text, false).Length == 0)
                {
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

        /// <summary>popupメニューの初期化</summary>
        /// <param name="initFlag">初期化する場合：true</param>
        private void initContextMenu(bool initFlag)
        {

            if (initFlag == true)
            {
                this.ALLClipData.Clear();
            }

            //履歴データの復元
            if (Properties.Settings.Default.RirekiKeyData == null || initFlag == true)
            {
                Properties.Settings.Default.RirekiKeyData = new System.Collections.Specialized.StringCollection();
            }

            if (Properties.Settings.Default.RirekiTagData == null || initFlag == true)
            {
                Properties.Settings.Default.RirekiTagData = new System.Collections.Specialized.StringCollection();
            }

            this.contextMenuStrip1.Items.Clear();
            if (Properties.Settings.Default.RirekiKeyData.Count > 0)
            {
                for (var i = 0; i < Properties.Settings.Default.RirekiKeyData.Count; i++)
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
            saveFixedFormValue(false);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.lstResult.Items.Clear();

            this.lstResult.Items.AddRange(
                this.ALLClipData.Where(
                (value) =>
                {
                    if (value.ToLower().Contains(this.txtSearch.Text.ToLower()))
                    {
                        return true;
                    }
                    return false;
                }).ToArray()
            );
        }

        private void btnResultCopy_Click(object sender, EventArgs e)
        {
            if (this.lstResult.SelectedItem != null)
            {
                System.Windows.Forms.Clipboard.SetText(this.lstResult.SelectedItem.ToString());
                MessageBox.Show("クリップボードにコピーしました。");
            }
            else
            {
                MessageBox.Show("データを選択してください。");
            }
        }

        private void lstResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ツールチップの表示
            if (this.lstResult.SelectedItem != null)
            {
                var dispText = this.lstResult.SelectedItem.ToString();
                //1000文字以上は...表示
                if (dispText.Length > 1000)
                {
                    dispText = dispText.Substring(0, 1000) + "...";
                }
                toolTipResult.Show(dispText, this.lstResult, 2000); //2秒表示
            }
        }

    }
}
