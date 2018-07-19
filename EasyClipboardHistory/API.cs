using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;

namespace ClipboardViewer
{
    public class ClipboardEventArgs : EventArgs
    {
        private string text;

        public string Text
        {
            get { return this.text; }
        }

        public ClipboardEventArgs(string str)
        {
            this.text = str;
        }
    }

    // http://www.atmarkit.co.jp/fdotnet/dotnettips/848cbviewer/cbviewer.html 参照（というかコピペ）
    public delegate void cbEventHandler(
                            object sender, ClipboardEventArgs ev);

    [System.Security.Permissions.PermissionSet(
          System.Security.Permissions.SecurityAction.Demand,
          Name = "FullTrust")]
    internal class MyClipboardViewer : NativeWindow
    {
        [DllImport("user32")]
        public static extern IntPtr SetClipboardViewer(
                IntPtr hWndNewViewer);

        [DllImport("user32")]
        public static extern bool ChangeClipboardChain(
                IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32")]
        public extern static int SendMessage(
                IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_DRAWCLIPBOARD = 0x0308;
        private const int WM_CHANGECBCHAIN = 0x030D;
        private IntPtr nextHandle;

        private Form parent;
        public event cbEventHandler ClipboardHandler;

        public MyClipboardViewer(Form f)
        {
            f.HandleCreated
                        += new EventHandler(this.OnHandleCreated);
            f.HandleDestroyed
                        += new EventHandler(this.OnHandleDestroyed);
            this.parent = f;
        }

        internal void OnHandleCreated(object sender, EventArgs e)
        {
            AssignHandle(((Form)sender).Handle);
            // ビューアを登録
            nextHandle = SetClipboardViewer(this.Handle);
        }

        internal void OnHandleDestroyed(object sender, EventArgs e)
        {
            // ビューアを解除
            bool sts = ChangeClipboardChain(this.Handle, nextHandle);
            ReleaseHandle();
        }

        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
            {

                case WM_DRAWCLIPBOARD:
                    if (Clipboard.ContainsText())
                    {
                        // クリップボードの内容がテキストの場合のみ
                        if (ClipboardHandler != null)
                        {
                            // クリップボードの内容を取得してハンドラを呼び出す
                            ClipboardHandler(this,
                                new ClipboardEventArgs(Clipboard.GetText()));
                        }
                    }
                    if ((int)nextHandle != 0)
                        SendMessage(
                            nextHandle, msg.Msg, msg.WParam, msg.LParam);
                    break;

                // クリップボード・ビューア・チェーンが更新された
                case WM_CHANGECBCHAIN:
                    if (msg.WParam == nextHandle)
                    {
                        nextHandle = (IntPtr)msg.LParam;
                    }
                    else if ((int)nextHandle != 0)
                        SendMessage(
                            nextHandle, msg.Msg, msg.WParam, msg.LParam);
                    break;
            }
            base.WndProc(ref msg);
        }
    }
}

namespace API
{
    internal class APIList
    {

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

        // ウィンドウをアクティブにする
        public static void SetActiveWindow(IntPtr hWnd)
        {
            const int SWP_NOSIZE = 0x0001;
            const int SWP_NOMOVE = 0x0002;
            const int SWP_SHOWWINDOW = 0x0040;

            const int HWND_TOPMOST = -1;
            const int HWND_NOTOPMOST = -2;

            SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            SetWindowPos(hWnd, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_SHOWWINDOW | SWP_NOMOVE | SWP_NOSIZE);
        }

        public static void SetTopMost(IntPtr hWnd)
        {
            const int HWND_TOPMOST = -1;
            const int SWP_NOSIZE = 0x0001;
            const int SWP_NOMOVE = 0x0002;
            const int SWP_NOACTIVATE = 0x0010;
            const int SWP_SHOWWINDOW = 0x0040;
            const int SWP_NOSENDCHANGING = 0x0400;

            SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSENDCHANGING | SWP_NOSIZE | SWP_SHOWWINDOW);
        }

        [DllImport("user32.dll")]
        extern public static int GetDoubleClickTime();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern bool GetCaretPos(out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern IntPtr AttachThreadInput(IntPtr idAttach, IntPtr idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, IntPtr lpdwProcessId);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentThreadId();

        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hwnd, out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern uint keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    }


}
