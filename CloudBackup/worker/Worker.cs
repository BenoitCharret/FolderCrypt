using CloudBackup.worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackup
{
    delegate void StartWorkHandler(object sender, StartEventArgs e);
    

    interface Worker
    {
        event StartWorkHandler startWorkHandler;
        
        void DoWork(); 
        void RequestStop();
    }
}
