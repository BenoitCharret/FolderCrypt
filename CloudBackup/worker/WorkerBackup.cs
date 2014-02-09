using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using CloudBackup.file;

namespace CloudBackup
{
    class WorkerBackup:Worker
    {
        private String folderSrc;
        private String folderDst;
        private string password;
        private ProgressBar progressBar;

        public WorkerBackup(config.Configuration configuration,ProgressBar aProgressBar)
        {
            this.folderDst = configuration.folderPathCipher;
            this.folderSrc = configuration.folderPathOrig;
            this.password = configuration.password;
            this.progressBar = aProgressBar;
        }

        public void DoWork()
        {
            string[] filesToProcess = Directory.GetFiles(folderSrc, "*", SearchOption.AllDirectories);
            Console.WriteLine("nb Files to process: {0}", filesToProcess.Length);
            int count = 0;
            int length = filesToProcess.Length;
            //this.progressBar.Maximum = length;
            foreach (string fileToProcess in filesToProcess)
            {
                while (!_shouldStop)
                {
                    count++;
                    //progressBar.Value = count;
                    Console.WriteLine("traitement de {0}/{1}", count, length);
                    if (needEncryption(fileToProcess, FileHelper.translateFilemame(folderSrc, folderDst, fileToProcess)))
                    {
                        EncryptionHelper.EncryptFile(password, fileToProcess, FileHelper.encryptPath(fileToProcess,folderSrc,folderDst ,password));
                    }
                    break;
                }
            }
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;

        private bool needEncryption(String plainFile, String cipherFile)
        {
            return true;
        }

   
    }


}
