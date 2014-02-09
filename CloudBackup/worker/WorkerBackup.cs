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
    class WorkerBackup : Worker
    {
        private String folderSrc;
        private String folderDst;
        private string password;
        public event StartWorkHandler startWorkHandler;

        public WorkerBackup(config.Configuration configuration)
        {
            this.folderDst = configuration.folderPathCipher;
            this.folderSrc = configuration.folderPathOrig;
            this.password = configuration.password;
        }

        public void DoWork()
        {
            string[] filesToProcess = Directory.GetFiles(folderSrc, "*", SearchOption.AllDirectories);
            Console.WriteLine("nb Files to process: {0}", filesToProcess.Length);
            int count = 0;
            int length = filesToProcess.Length;

            startWorkHandler(this, new worker.StartEventArgs(length,0));

            foreach (string fileToProcess in filesToProcess)
            {
                while (!_shouldStop)
                {
                    count++;
                    startWorkHandler(this, new worker.StartEventArgs(length, count));
                    Console.WriteLine("traitement de {0}/{1}", count, length);
                    string encryptPath = FileHelper.encryptPath(fileToProcess, folderSrc, folderDst, password);
                    if (needEncryption(fileToProcess, encryptPath))
                    {
                        // if dest file already exists but name is not crypted we rename it
                        if (File.Exists(FileHelper.translateFilemame(folderSrc, folderDst, fileToProcess)))
                        {
                            if (File.Exists(encryptPath))
                            {
                                File.Delete(FileHelper.translateFilemame(folderSrc, folderDst, fileToProcess));
                            }
                            else
                            {
                                File.Move(FileHelper.translateFilemame(folderSrc, folderDst, fileToProcess), encryptPath);
                            }
                        }
                        else
                        {
                            EncryptionHelper.EncryptFile(password, fileToProcess, encryptPath);
                        }
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
