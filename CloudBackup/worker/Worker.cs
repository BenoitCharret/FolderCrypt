using CloudBackup.worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackup
{
    delegate void StartWorkHandler(object sender, StartEventArgs e);
    delegate void UpdateTextHandler(object sender, UpdateTextEventArgs e);

    interface Worker
    {
        event StartWorkHandler startWorkHandler;
        event UpdateTextHandler updateTextHandler;
        
        void DoWork(); 
        void RequestStop();
    }
}
