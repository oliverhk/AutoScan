using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    public class SystemStatus
    {
        private static readonly object _lockObj = new object();
        private static SystemStatus _instance = null;
        public static SystemStatus Instance
        {
            get
            {
                lock (_lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new SystemStatus();
                    }
                    return _instance;
                }
            }
        }
        public SystemStatus()
        {

        }
        #region property
        private string _logon_User_Id;
        private string _logon_User_Name;
        private float _z_x;
        public string Logon_User_Name
        {
            get { return _logon_User_Name; }
            set { _logon_User_Name = value; }
        }

        public string Logon_User_ID
        {
            get { return _logon_User_Id; }
            set { _logon_User_Id = value; }
        }
        public float Z_X
        {
            get { return _z_x; }
            set { _z_x = value; }
        }
        #endregion

        public static float ZPos { get; set; }
        public SysStatus SysStat { get; set; }
        public string ScanID { get; set; }
        public string ZStackID { get; set; }
        public bool SaveImgFlag { get; set; }
        public string ImageSavePath { get; set; }
    }
}
