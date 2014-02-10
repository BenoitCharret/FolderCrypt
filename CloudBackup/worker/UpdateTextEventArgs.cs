using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackup.worker
{
    class UpdateTextEventArgs:EventArgs
    {
        public UpdateTextEventArgs(string text)
        {
            this.Text = text;
        }


        public string Text { get; set; }
    }
}
