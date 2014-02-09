using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudBackup.file;

namespace CloudBackup
{
    class WorkerRemote:Worker
    {
         private String folderSrc;
        private String folderDst;
        private string password;
        public event StartWorkHandler startWorkHandler;

        public WorkerRemote(config.Configuration configuration)
        {
            this.folderDst = configuration.folderPathOrig;
            this.folderSrc = configuration.folderPathCipher;
            this.password = configuration.password;
        }



        public void DoWork()
        {
            string[] filesToProcess = Directory.GetFiles(folderSrc, "*", SearchOption.AllDirectories);
            Console.WriteLine("nb Files to process: {0}", filesToProcess.Length);
            int count = 0;
            int length = filesToProcess.Length;
            startWorkHandler(this, new worker.StartEventArgs(length, 0));
            foreach (string fileToProcess in filesToProcess)
            {
                while (!_shouldStop)
                {
                    count++;
                    startWorkHandler(this, new worker.StartEventArgs(length, count));
                    Console.WriteLine("traitement de {0}/{1}", count, length);
                    if (needDecryption(fileToProcess, FileHelper.translateFilemame(folderSrc, folderDst, fileToProcess)))
                    {
                        EncryptionHelper.DecryptFile(password, fileToProcess, FileHelper.decryptPath(fileToProcess,folderSrc,folderDst,password));                        
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

        private bool needDecryption(String plainFile, String cipherFile)
        {
            return true;
        }

       
    }
}
