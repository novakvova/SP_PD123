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

namespace WinFormsTask
{
    public partial class MainForm : Form
    {
        int j = 0;
        private Object thisLock = new Object();
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            btnCopy.Enabled = false;
            //Task.Run();
            int count = int.Parse(txtCount.Text);
            Task task = new Task(() => CopyData(count));
            task.Start();
            int threadId = Thread.CurrentThread.ManagedThreadId;
            //Task.Run(() => CopyData(count));
            //CopyData(count);
        }
        void CopyData(int count)
        {
            lock (thisLock)
            {
                for (int i = 0; i < count; i++)
                {
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
        
    }
}
