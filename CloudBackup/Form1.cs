using CloudBackup.worker;
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

namespace CloudBackup
{
    public partial class Form1 : Form
    {
        private config.Configuration configuration;
        private Worker worker;
        Thread workerThread;

        private delegate void PorgressBarDelegateHandler(int length, int value);
        private PorgressBarDelegateHandler ProgressBarDelegate;


        public Form1()
        {
            InitializeComponent();
            configuration = new config.ConfigurationManager().readConfiguration();
            textBoxFolderCrypt.Text = configuration.folderPathCipher;
            textBoxFolderOrig.Text = configuration.folderPathOrig;
            radioButtonBackup.Checked = configuration.isBackup;
            radioButtonRemote.Checked = !configuration.isBackup;
            textBoxPassword.Text = configuration.password;

            // init delegate
            ProgressBarDelegate = new PorgressBarDelegateHandler(UpdateProgressBar);
        }

        private void buttonFolderCrypt_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                configuration.folderPathCipher = folderBrowserDialog1.SelectedPath;
                textBoxFolderCrypt.Text = configuration.folderPathCipher;
            }
        }

        private void buttonFolderOrig_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                configuration.folderPathOrig = folderBrowserDialog1.SelectedPath;
                textBoxFolderOrig.Text = configuration.folderPathOrig;
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {

            configuration.password = textBoxPassword.Text;
            configuration.isBackup = radioButtonBackup.Checked;
            new config.ConfigurationManager().saveConfiguration(configuration);
            if (radioButtonBackup.Checked == true)
            {
                worker = new WorkerBackup(configuration);
            }
            else
            {
                worker = new WorkerRemote(configuration);
            }
            worker.startWorkHandler += new StartWorkHandler(WorkChanged);
            workerThread = new Thread(worker.DoWork);
            workerThread.Start();
            //worker.DoWork();

        }

        private void WorkChanged(object sender, StartEventArgs e)
        {
            this.Invoke(this.ProgressBarDelegate, new object[] { e.Length, e.Count });
        }

        private void UpdateProgressBar(int length, int value)
        {
            this.progressBar1.Maximum = length;
            this.progressBar1.Value = value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            if (worker != null)
            {
                worker.RequestStop();
            }
            if (workerThread != null)
            {
                workerThread.Join();
            }
        }

    }
}
