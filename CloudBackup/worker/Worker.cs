using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackup
{
    interface Worker
    {
        void DoWork(); 
        void RequestStop();
    }
}
