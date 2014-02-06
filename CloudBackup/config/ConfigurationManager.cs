using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CloudBackup.config
{
    class ConfigurationManager
    {
        private static string KEY_BACKUP = "backup:";
        private static string KEY_FOLDER_ORIG = "folder_orig:";
        private static string KEY_FOLDER_CIPHER = "folder_cipher:";
        private static string KEY_PASSWORD = "password_hash:";

        private static string CONFIG_FILENAME = @"config.cfg";
        private static string KEY_HASH_PASSWORD = "5EB{GT/r0k~7Zjm";

        public Boolean saveConfiguration(Configuration configuration)
        {
            using (StreamWriter file = new StreamWriter(CONFIG_FILENAME))
            {
                file.WriteLine(KEY_BACKUP + configuration.isBackup);
                file.WriteLine(KEY_FOLDER_CIPHER + configuration.folderPathCipher);
                file.WriteLine(KEY_FOLDER_ORIG + configuration.folderPathOrig);
                file.WriteLine(KEY_PASSWORD + EncryptionHelper.EncryptString(configuration.password, KEY_HASH_PASSWORD));
            }
            return true;
        }

        public Configuration readConfiguration()
        {
            if (!File.Exists(CONFIG_FILENAME))
            {
                return new Configuration();
            }
            Configuration config = new Configuration();

            using (StreamReader file = new StreamReader(CONFIG_FILENAME))
            {
                string line = file.ReadLine();
                while (line != null)
                {
                    parseLine(line, config);
                    line = file.ReadLine();
                }
            }

            return config;
        }

        private Configuration parseLine(string line, Configuration configuration)
        {
            if (line.StartsWith(KEY_BACKUP))
            {
                configuration.isBackup = Boolean.Parse(line.Replace(KEY_BACKUP, ""));
            }
            else if (line.StartsWith(KEY_FOLDER_CIPHER))
            {
                configuration.folderPathCipher = line.Replace(KEY_FOLDER_CIPHER, "");
            }
            else if (line.StartsWith(KEY_FOLDER_ORIG))
            {
                configuration.folderPathOrig = line.Replace(KEY_FOLDER_ORIG, "");
            }
            else if (line.StartsWith(KEY_PASSWORD))
            {
                configuration.password = EncryptionHelper.DecryptString(line.Replace(KEY_PASSWORD, ""), KEY_HASH_PASSWORD);
            }

            return configuration;
        }
    }
}
