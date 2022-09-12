using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Diagnostics.Eventing.Reader;

namespace WinFormsTask
{
    public partial class MainForm : Form
    {
        int j = 0;
        private Object thisLock = new Object();
        private CancellationTokenSource ctSource;
        private CancellationToken token;
        // mre is used to block and release threads manually. It is
        // created in the unsignaled state.
        //Вміє лочити виконаня потоку
        private static ManualResetEvent _mre = new ManualResetEvent(false);
        private bool _isPause = false;

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            btnCopy.Enabled = false;
            ctSource = new CancellationTokenSource();
            token = ctSource.Token;
            //Task.Run();
            int count = int.Parse(txtCount.Text);
            Task task = new Task(() => CopyData(count), token);
            task.Start();
            int threadId = Thread.CurrentThread.ManagedThreadId;
            //Потік зараз не лочимо, а пукаємо
            _mre.Set();
            //Task.Run(() => CopyData(count));
            //CopyData(count);
        }
        void CopyData(int count)
        {
            lock (thisLock)
            {
                for (int i = 0; i < count; i++)
                {
                    _mre.WaitOne(Timeout.Infinite); //Якщо був залочений потік то ми чекаємо поки його розлочать
                    if (token.IsCancellationRequested)  // проверяем наличие сигнала отмены задачи
                    {
                        return;     //  выходим из метода и тем самым завершаем задачу
                    }
                    Thread.Sleep(500);
                    if (lbCounter.InvokeRequired)
                    {
                        string text = $"{++j}/{count}";
                        int threadId = Thread.CurrentThread.ManagedThreadId;
                        //lbCounter.Text = text;
                        lbCounter.Invoke(new MethodInvoker(delegate { lbCounter.Text = text; }));
                    }
                    //lbCounter.Text = ;
                }
            }
            if (btnCopy.InvokeRequired)
            {
                btnCopy.Invoke(new MethodInvoker(delegate { btnCopy.Enabled = true; }));
            }
            //
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ctSource.Cancel();
            btnCopy.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if(_isPause) //Якщо потік був залочений
            {
                _mre.Set(); //Пускаємо потік далі, змінємо кнопку
                btnPause.Text = "Пауза";
            }
            else
            {
                _mre.Reset(); //Залочити потік
                btnPause.Text = "Продовжити"; //Кнопку міняємо на протилежну
            }
            _isPause = !_isPause;
        }
    }
}
