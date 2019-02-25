using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp
{
    public delegate void UpdateMessageHander(MessageBody message);
    public delegate void UpdateMoveStatHandler(EnumStat stat);
    public delegate void UpdateJobHandler(DBCtrl.Model.Jobs job);
    public interface IAuto
    {
        bool Start();
        bool Stop();
        void AbortScan();
        string LotId { get; set; }
        string BatchId { get; set; }
        event UpdateMessageHander UpdateMessage;
        event UpdateMoveStatHandler UpdateStat;
        event UpdateJobHandler UpdateJob;
    }
}
