using API;
using ClipboardViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public static ToolStripItemCollection HistoryToolStripCollection;
        private MyClipboardViewer viewer;

        private IntPtr ForehWnd = IntPtr.Zero;
        private bool IsEndFlag = false;

        const int DISPLAY_LENGTH = 40;

        public Main()
        {
            //クリップボード
            viewer = new MyClipboardViewer(this);

            InitializeComponent();

            //アイテムを連動
            HistoryToolStripCollection = this.contextMenuStrip1.Items; //= m_history.Items;


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
                if (IsContextMenuStripOpen == true)
                {
                    this.contextMenuStrip1.Close();
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Icon = this.Icon;
            this.notifyIcon1.BalloonTipText = "shiftキーダブルクリック\n選択でクリップボードにコピー（貼付）";
            this.notifyIcon1.BalloonTipTitle = "";
            this.notifyIcon1.Text = "";

            this.notifyIcon1.ShowBalloonTip(500);

            this.txtRirekiCount.Text = Properties.Settings.Default.RirekiCount.ToString();
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.Opacity = 0;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsEndFlag == false)
            {
                e.Cancel = true;
                this.Hide();
            }
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

                var dispText = clipText.Trim().Replace("\r", "").Replace("\n", "").Replace("\t", "");
                
                if (dispText.Length == 0)
                {
                    return;
                }

                //ポップアップに表示する文字数
                if (dispText.Length > DISPLAY_LENGTH)
                {
                    dispText = dispText.Substring(0, DISPLAY_LENGTH);
                }

                var item = new ToolStripMenuItem();

                item.Text = dispText;
                item.Tag = clipText;

                if (HistoryToolStripCollection.Count > 0)
                {
                    if ((String)(HistoryToolStripCollection[0].Tag) == (String)clipText)
                    {
                        return;
                    }

                }
                HistoryToolStripCollection.Insert(0, item);

                //履歴数を超えた場合の削除処理
                int MaxCountData = Properties.Settings.Default.RirekiCount;
                if (HistoryToolStripCollection.Count > MaxCountData)
                {
                    for (int index = HistoryToolStripCollection.Count - 1; index >= MaxCountData; index--)
                    {
                        HistoryToolStripCollection.RemoveAt(index);
                    }
                }
            }
        }

        bool IsContextMenuStripOpen = false;
        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            IsContextMenuStripOpen = true;
        }

        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (ForehWnd != IntPtr.Zero)
            {
                APIList.SetActiveWindow(ForehWnd);
                if (IsClipCopy == true)
                {
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

            IsContextMenuStripOpen = false;
            IsClipCopy = false;

        }

        private bool IsClipCopy = false;
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText((string)(((ToolStripMenuItem)(e.ClickedItem)).Tag));
            IsClipCopy = true;
            //this.notifyIcon1.BalloonTipText = "クリップボードにコピーしました。";
            //this.notifyIcon1.ShowBalloonTip(50);
        }

        private void toolStripMenuItemEnd_Click(object sender, EventArgs e)
        {
            IsEndFlag = true;
            Properties.Settings.Default.Save();
            Application.Exit();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Opacity = 100;
            this.Visible = true;

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
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
    }
}
