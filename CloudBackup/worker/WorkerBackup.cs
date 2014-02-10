using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using CloudBackup.file;
using CloudBackup.worker;
using System.Threading;

namespace CloudBackup
{
    class WorkerBackup : Worker
    {
        private String folderSrc;
        private String folderDst;
        private string password;
        private FileSystemWatcher fsWatcher;
        public event StartWorkHandler startWorkHandler;
        public event UpdateTextHandler updateTextHandler;

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
            if (startWorkHandler != null)
            {
                startWorkHandler(this, new worker.StartEventArgs(length, 0));
            }
            foreach (string fileToProcess in filesToProcess)
            {
                while (!_shouldStop)
                {
                    count++;
                    if (startWorkHandler != null)
                    {
                        startWorkHandler(this, new StartEventArgs(length, count));
                    }
                    string encryptPath = FileHelper.encryptPath(fileToProcess, folderSrc, folderDst, password);
                    if (updateTextHandler != null)
                    {
                        updateTextHandler(this, new UpdateTextEventArgs(String.Format("{0}/{1} : traitement de {2} -> {3}", new object[] { count, length, fileToProcess, encryptPath })));
                    }
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

            // on installe un watcher sur le filesystem
            fsWatcher = initFSWatcher(folderSrc);
            while (!_shouldStop)
            {
                Thread.Sleep(1000);
            }
            fsWatcher.EnableRaisingEvents = false;
        }

        private FileSystemWatcher initFSWatcher(string srcFolder) {
            FileSystemWatcher fsWatcher = new FileSystemWatcher(srcFolder);
            fsWatcher.IncludeSubdirectories = true;
            fsWatcher.Filter = "*";
            fsWatcher.NotifyFilter = (NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.CreationTime | NotifyFilters.LastAccess |NotifyFilters.LastWrite);
            fsWatcher.Changed += new FileSystemEventHandler(OnChanged);
            fsWatcher.Created += new FileSystemEventHandler(OnChanged);
            fsWatcher.Deleted += new FileSystemEventHandler(OnChanged);
            fsWatcher.Renamed += new RenamedEventHandler(OnRenamed);

            fsWatcher.EnableRaisingEvents = true;
            return fsWatcher;
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

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            string encryptPath = null;
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    encryptPath = FileHelper.encryptPath(e.FullPath, folderSrc, folderDst, password);
                    EncryptionHelper.EncryptFile(password, e.FullPath, encryptPath);
                    if (updateTextHandler != null)
                    {
                           updateTextHandler(this,new UpdateTextEventArgs(String.Format("creation : {0}->{1}",e.FullPath,encryptPath)));
                    }
                    break;
                case WatcherChangeTypes.Changed:
                    encryptPath = FileHelper.encryptPath(e.FullPath, folderSrc, folderDst, password);
                    EncryptionHelper.EncryptFile(password, e.FullPath, encryptPath);
                    if (updateTextHandler != null)
                    {
                           updateTextHandler(this,new UpdateTextEventArgs(String.Format("modification : {0}->{1}",e.FullPath,encryptPath)));
                    }
                    break;
                case WatcherChangeTypes.Deleted:
                    encryptPath = FileHelper.encryptPath(e.FullPath, folderSrc, folderDst, password);
                    if (File.Exists(encryptPath))
                    {
                        File.Delete(encryptPath);
                        if (updateTextHandler != null)
                        {
                            updateTextHandler(this, new UpdateTextEventArgs(String.Format("suppression : {0}->{1}", e.FullPath, encryptPath)));
                        }
                    }

                    break;

                default:
                    break;
            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            // on efface l'ancien fichier et on crypte le nouveau
            string oldEncryptPath = FileHelper.encryptPath(e.OldFullPath, folderSrc, folderDst, password);
            if (File.Exists(oldEncryptPath))
            {
                File.Delete(oldEncryptPath);                
            }

            string encryptPath = FileHelper.encryptPath(e.FullPath, folderSrc, folderDst, password);
            EncryptionHelper.EncryptFile(password, e.FullPath, encryptPath);

            if (updateTextHandler != null)
            {
                updateTextHandler(this, new UpdateTextEventArgs(String.Format("renommage : {0}->{1}", e.FullPath, encryptPath)));
            }
        }
    }


}
