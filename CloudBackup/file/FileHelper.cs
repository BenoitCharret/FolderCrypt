using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackup.file
{
    abstract class FileHelper
    {
        public static string translateFilemame(string folderSrc, string folderDst, string file)
        {
            if (file.Contains(folderSrc) == true)
            {
                return file.Replace(folderSrc, folderDst);
            }
            else
            {
                return folderDst + "\\" + file;
            }
        }


        public static String encryptPath(string path, string srcPath,string dstPath,string key)
        {
            if (path == null || srcPath==null || dstPath==null || key ==null)
            {
                return null;
            }
            StringBuilder sBuilder = new StringBuilder(dstPath);
            String pathToTreat = path.Replace(srcPath, "");
            string[] folders=pathToTreat.Split(new char[]{'\\'});
            foreach (string folder in folders)
            {
                if (folder != null && folder != "")
                {
                    sBuilder.Append("\\");
                    sBuilder.Append(EncryptionHelper.EncryptString(folder, key));
                }
            }

            return sBuilder.ToString();
        }

        public static String decryptPath(string path, string srcPath, string dstPath, string key)
        {
            if (path == null || srcPath == null || dstPath == null || key == null)
            {
                return null;
            }
            StringBuilder sBuilder = new StringBuilder(dstPath);
            String pathToTreat = path.Replace(srcPath, "");
            string[] folders = pathToTreat.Split(new char[] { '\\' });
            foreach (string folder in folders)
            {
                if (folder != null && folder != "")
                {
                    sBuilder.Append("\\");
                    sBuilder.Append(EncryptionHelper.DecryptString(folder, key));
                }
            }

            return sBuilder.ToString();
        }

    }
}
