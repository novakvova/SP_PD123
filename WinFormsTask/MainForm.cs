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
            Task.Run(() => CopyData(count));
            //CopyData(count);
        }
        void CopyData(int count)
        {
            lock (thisLock)
            {
                for (int i = 0; i < count; i++)
                {
                    //Thread.Sleep(500);
                    lbCounter.Text = $"{++j}/{count}";
                }
            }
            btnCopy.Enabled = true;
        }
        
    }
}
