using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyClipboardHistory
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Mutex名を決める（必ずアプリケーション固有の文字列に変更すること！）
            string mutexName = "@@@EasyClipboardHistory@@@";
            //Mutexオブジェクトを作成する
            bool createdNew;
            System.Threading.Mutex mutex =
                new System.Threading.Mutex(true, mutexName, out createdNew);

            //ミューテックスの初期所有権が付与されたか調べる
            if (createdNew == false)
            {
                //されなかった場合は、すでに起動していると判断して終了
                MessageBox.Show("多重起動はできません。");
                mutex.Close();
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var frm = new Main();
                frm.Opacity = 0;
                frm.Hide();
                Application.Run(frm);
            }
            finally
            {
                //ミューテックスを解放する
                mutex.ReleaseMutex();
                mutex.Close();
            }
           
        }
    }
}
