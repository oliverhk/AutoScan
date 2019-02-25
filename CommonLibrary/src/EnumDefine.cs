using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    public enum MENU_DE
    {
        menu_main,
        menu_recipe,
        menu_query,
        menu_manual,        
        menu_set,
        menu_user,
        menu_exit,        
    }
    public struct PaperType
    {
        public string type;
        public string spec;
        public string supplier;
        public string logo_ver;
        public float weight;
        public float total_weight;
        public float door_width;
        public int is_partial;
        
    }
    public enum MsgType
    {
        Information = 1,
        Warning,
        Error        
    }
    public struct MessageBody
    {
        public MsgType type;
        public object msg;
    }
    public enum EnumStat
    {
        StartPos,
        HomePos,
        SwathEnd,
        SwathStart,
        ScanFinish,
    }
    public enum SysStatus
    {
        Normal,
        Error,
        Alarm,
    }
    public enum TriggerType
    {
        Software,
        Hardware,
    }
    public enum PiezType
    {
        XinMingTian,
        PI,
    }

    public enum BarcodeType
    {
        OCR,
        Barcode,
    }
    public enum UserRole
    {
        Admin=0,
        Engineer,
        Operator,
    }
}
