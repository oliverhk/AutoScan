using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Utility;

namespace CommonLibrary
{
    public class CommuConfig
    {
        private static readonly object _lockObj = new object();
        private static CommuConfig _instance = null;
        public static CommuConfig Instance
        {
            get
            {
                lock (_lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new CommuConfig();
                    }
                    return _instance;
                }
            }
        }
        public CommuConfig()
        {

        }
        #region property
        #region PLC 
        private string _ip1_plc="192.168.1.10";
        private string _ip2_plc="192.168.1.11";
        private string _ip3_plc = "192.168.1.12";
        private string _ip4_plc = "192.168.1.13";
        private string _ip5_plc = "192.168.1.14";
        private string _ip6_plc = "192.168.1.15";
        private string _ip7_plc = "192.168.1.16";
        private string _ip8_plc = "192.168.1.17";
        private string _ip9_plc = "192.168.1.18";
        private string _ip10_plc = "192.168.1.19";
        //private string _ip11_plc = "192.168.1.20";
        //private string _ip12_plc = "192.168.1.21";
        //private string _ip13_plc = "192.168.1.22";
        private int _port1_plc = 102;
        private int _port2_plc = 102;
        private int _port3_plc = 102;
        private int _port4_plc = 102;
        private int _port5_plc = 102;
        private int _port6_plc = 102;
        private int _port7_plc = 102;
        private int _port8_plc = 102;
        private int _port9_plc = 102;
        private int _port10_plc = 102;
        //private int _port11_plc = 102;
        //private int _port12_plc = 102;
        //private int _port13_plc = 102;
        private int _block_no = 8;
        private int _block_no_2 = 9;

        

        public string Ip1_plc
        {
            get { return _ip1_plc; }
            set { _ip1_plc = value; }
        }
        public string Ip2_plc
        {
            get { return _ip2_plc; }
            set { _ip2_plc = value; }
        }
        public string Ip3_plc
        {
            get { return _ip3_plc; }
            set { _ip3_plc = value; }
        }
        public string Ip4_plc
        {
            get { return _ip4_plc; }
            set { _ip4_plc = value; }
        }
        public string Ip5_plc
        {
            get { return _ip5_plc; }
            set { _ip5_plc = value; }
        } 
        public string Ip6_plc
        {
            get { return _ip6_plc; }
            set { _ip6_plc = value; }
        }        
        public string Ip7_plc
        {
            get { return _ip7_plc; }
            set { _ip7_plc = value; }
        }        
        public string Ip8_plc
        {
            get { return _ip8_plc; }
            set { _ip8_plc = value; }
        }        
        public string Ip9_plc
        {
            get { return _ip9_plc; }
            set { _ip9_plc = value; }
        }        
        public string Ip10_plc
        {
            get { return _ip10_plc; }
            set { _ip10_plc = value; }
        }        
        //public string Ip11_plc
        //{
        //    get { return _ip11_plc; }
        //    set { _ip11_plc = value; }
        //}        
        //public string Ip12_plc
        //{
        //    get { return _ip12_plc; }
        //    set { _ip12_plc = value; }
        //}
        //public string Ip13_plc
        //{
        //    get { return _ip13_plc; }
        //    set { _ip13_plc = value; }
        //}         
        public int Port1_plc
        {
            get { return _port1_plc; }
            set { _port1_plc = value; }
        }
        public int Port2_plc
        {
            get { return _port2_plc; }
            set { _port2_plc = value; }
        }
        public int Port3_plc
        {
            get { return _port3_plc; }
            set { _port3_plc = value; }
        }
        public int Port4_plc
        {
            get { return _port4_plc; }
            set { _port4_plc = value; }
        }
        public int Port5_plc
        {
            get { return _port5_plc; }
            set { _port5_plc = value; }
        }     
        public int Port6_plc
        {
            get { return _port6_plc; }
            set { _port6_plc = value; }
        }       
        public int Port7_plc
        {
            get { return _port7_plc; }
            set { _port7_plc = value; }
        }        
        public int Port8_plc
        {
            get { return _port8_plc; }
            set { _port8_plc = value; }
        }        
        public int Port9_plc
        {
            get { return _port9_plc; }
            set { _port9_plc = value; }
        }       
        public int Port10_plc
        {
            get { return _port10_plc; }
            set { _port10_plc = value; }
        }        
        //public int Port11_plc
        //{
        //    get { return _port11_plc; }
        //    set { _port11_plc = value; }
        //}        
        //public int Port12_plc
        //{
        //    get { return _port12_plc; }
        //    set { _port12_plc = value; }
        //}        
        //public int Port13_plc
        //{
        //    get { return _port13_plc; }
        //    set { _port13_plc = value; }
        //}       
        public int Block_no
        {
            get { return _block_no; }
            set { _block_no = value; }
        }
        public int Block_no_2
        {
            get { return _block_no_2; }
            set { _block_no_2 = value; }
        }
        #endregion 

        #region BarCode #1
        
        #endregion 



        #endregion

        #region method
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool LoadConfig(string path)
        {
            try
            {
                CommuConfig sysConfig;
                string file = path + @"\CommuConfig.xml";
                XmlSerializer serial = new XmlSerializer(typeof(CommuConfig));
                if (File.Exists(file))
                {
                    FileStream reader = new FileStream(file, FileMode.Open);
                    // Call the Deserialize method and cast to the object type.
                    sysConfig = (CommuConfig)serial.Deserialize(reader);
                    reader.Close();
                }
                else
                {
                    sysConfig = CommuConfig.Instance;
                }

                CommuConfig systemConfig = CommuConfig.Instance;
                systemConfig._block_no = sysConfig._block_no;
                systemConfig._ip1_plc = sysConfig._ip1_plc;
                systemConfig._ip2_plc = sysConfig._ip2_plc;
                systemConfig._ip3_plc = sysConfig._ip3_plc;
                systemConfig._ip4_plc = sysConfig._ip4_plc;
                systemConfig._ip5_plc = sysConfig._ip5_plc;
                systemConfig._ip6_plc = sysConfig._ip6_plc;
                systemConfig._ip7_plc = sysConfig._ip7_plc;
                systemConfig._ip8_plc = sysConfig._ip8_plc;
                systemConfig._ip9_plc = sysConfig._ip9_plc;
                systemConfig._ip10_plc = sysConfig._ip10_plc;
                //systemConfig._ip11_plc = sysConfig._ip11_plc;
                //systemConfig._ip12_plc = sysConfig._ip12_plc;
                //systemConfig._ip13_plc = sysConfig._ip13_plc;

                systemConfig._port1_plc = sysConfig._port1_plc;
                systemConfig._port2_plc = sysConfig._port2_plc;
                systemConfig._port3_plc = sysConfig._port3_plc;
                systemConfig._port4_plc = sysConfig._port4_plc;
                systemConfig._port5_plc = sysConfig._port5_plc;
                systemConfig._port6_plc = sysConfig._port6_plc;
                systemConfig._port7_plc = sysConfig._port7_plc;
                systemConfig._port8_plc = sysConfig._port8_plc;
                systemConfig._port9_plc = sysConfig._port9_plc;
                systemConfig._port10_plc = sysConfig._port10_plc;
                //systemConfig._port11_plc = sysConfig._port11_plc;
                //systemConfig._port12_plc = sysConfig._port12_plc;
                //systemConfig._port13_plc = sysConfig._port13_plc;

                return true;
            }
            catch (Exception ex)
            {
                //SystemLogger.Write(ex);
                return false;
            }
        }
        #endregion
    }
}
