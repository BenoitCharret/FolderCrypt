using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackup.config
{
    class Configuration
    {
        public string folderPathOrig { get; set; }
        public string folderPathCipher { get; set; }
        public string password { get; set; }
        public bool isBackup { get; set; }

    }
}
