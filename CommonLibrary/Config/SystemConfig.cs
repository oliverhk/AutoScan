using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Utility;
namespace CommonLibrary
{
    public class SystemConfig
    {
        private static readonly object _lockObj = new object();
        private static SystemConfig _instance = null;
        public static SystemConfig Instance
        {
            get
            {
                lock (_lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new SystemConfig();
                    }
                    return _instance;
                }
            }
        }
        public SystemConfig()
        {

        }
        #region property
        private bool _change_flag = true;
        public int Max_logs_day { get; set; } = 7;

        //controler host
        public string Control_IP { get; set; } = "10.0.0.100";
        public string Light_Serial_Port { get; set; } = "COM3";  
        //motor       
        public float XCountUnit { get; set; } = 156.25f;    //nm
        public float YCountUnit { get; set; } = 156.25f;    //nm
        public float ZCountUnit { get; set; } = 100f;       //nm

        public float Limit_Z { get; set; } = 0.5f;  //mm
        public string EquipmentId { get; set; }  //equipment id
        public int EquipmentNum { get; set; }  //equipment num
        public string Hospital { get; set; }  //hospital

        //pi ze
        public PiezType PizType { get; set; }  // piez type
        public string Pize_Serial_Port { get; set; } = "COM4";
        public int PI_ComPort_Num { get; set; } = 1;
        public int PI_ComBaudRate { get; set; } = 115200;

        //I/O card 
        public string IOCard_Serial_Port { get; set; } = "COM5";
        public int IO_Input_Start { get; set; } = 0;
        public int IO_Input_Stop { get; set; } = 1;
        public int IO_Output_Start_Light { get; set; } = 0;
        public int IO_Output_Stop_Light { get; set; } = 1;
        public int IO_Output_Green_Light { get; set; } = 2;
        public int IO_Output_Red_Light { get; set; } = 3;
        public int IO_Light_On_Alway { get; set; } = 4;
        public float Move_Speed_X { get; set; } = 100f;
        public float Move_Speed_Y { get; set; } = 100f;
        public float Load_Pos_X { get; set; } = -28f;
        public float Load_Pos_Y { get; set; } = -22f;
        
        //OCR
        public int OCR_DbRow { get; set; } = 1; //用来读取OCR的旋转矩形中心行坐标
        public int OCR_DbCol { get; set; } = 1; //用来读取OCR的旋转矩形中心列坐标
        public int OCR_DbPhi { get; set; } = 0; //用来读取OCR的旋转矩形角度
        public int OCR_DbLen1 { get; set; } = 1; //用来读取OCR的旋转矩形的长边/2
        public int OCR_DbLen2 { get; set; } = 1; //用来读取OCR的旋转矩形的短边/2
        public int OCR_FontWidth { get; set; } = 20;  //要读取的字符字体宽度
        public int OCR_FontHeight { get; set; } = 30; //要读取的字符字体高度
        public float OCR_StrokeWidth { get; set; } = 5.0f;  //要读取的字符字体笔画宽度
        public BarcodeType OcrBarcodeType { get; set; }

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
                SystemConfig sysConfig;
                string file = path;// + @"\SystemConfig.xml";
                XmlSerializer serial = new XmlSerializer(typeof(SystemConfig));
                if (File.Exists(file))
                {
                    FileStream reader = new FileStream(file, FileMode.Open);
                    // Call the Deserialize method and cast to the object type.
                    sysConfig = (SystemConfig)serial.Deserialize(reader);
                    reader.Close();
                }
                else
                {
                    sysConfig = SystemConfig.Instance;
                }

                SystemConfig systemConfig = SystemConfig.Instance;
                
                systemConfig.Control_IP = sysConfig.Control_IP;
                systemConfig.Light_Serial_Port = sysConfig.Light_Serial_Port;

                systemConfig.XCountUnit = sysConfig.XCountUnit;
                systemConfig.YCountUnit = sysConfig.YCountUnit;
                systemConfig.ZCountUnit = sysConfig.ZCountUnit;
                systemConfig.Limit_Z = sysConfig.Limit_Z;
                systemConfig.EquipmentId = sysConfig.EquipmentId;
                systemConfig.EquipmentNum = sysConfig.EquipmentNum;
                systemConfig.Hospital = sysConfig.Hospital;
                systemConfig.PizType = sysConfig.PizType;
                systemConfig.Pize_Serial_Port = sysConfig.Pize_Serial_Port;
                systemConfig.PI_ComPort_Num = sysConfig.PI_ComPort_Num;
                systemConfig.PI_ComBaudRate = sysConfig.PI_ComBaudRate;
                systemConfig.IOCard_Serial_Port = sysConfig.IOCard_Serial_Port;
                systemConfig.IO_Input_Start = sysConfig.IO_Input_Start;
                systemConfig.IO_Input_Stop = sysConfig.IO_Input_Stop;
                systemConfig.IO_Output_Start_Light = sysConfig.IO_Output_Start_Light;
                systemConfig.IO_Output_Stop_Light = sysConfig.IO_Output_Stop_Light;
                systemConfig.IO_Output_Green_Light = sysConfig.IO_Output_Green_Light;
                systemConfig.IO_Output_Red_Light = sysConfig.IO_Output_Red_Light;
                systemConfig.IO_Light_On_Alway = sysConfig.IO_Light_On_Alway;
                systemConfig.Move_Speed_X = sysConfig.Move_Speed_X;
                systemConfig.Move_Speed_Y = sysConfig.Move_Speed_Y;
                systemConfig.Load_Pos_X = sysConfig.Load_Pos_X;
                systemConfig.Load_Pos_Y = sysConfig.Load_Pos_Y;
				
				systemConfig.OCR_DbRow = sysConfig.OCR_DbRow;
                systemConfig.OCR_DbCol = sysConfig.OCR_DbCol;
                systemConfig.OCR_DbPhi = sysConfig.OCR_DbPhi;
                systemConfig.OCR_DbLen1 = sysConfig.OCR_DbLen1;
                systemConfig.OCR_DbLen2 = sysConfig.OCR_DbLen2;
                systemConfig.OCR_FontWidth = sysConfig.OCR_FontWidth;
                systemConfig.OCR_FontHeight = sysConfig.OCR_FontHeight;
                systemConfig.OCR_StrokeWidth = sysConfig.OCR_StrokeWidth;
                systemConfig.OcrBarcodeType = sysConfig.OcrBarcodeType;

                return true;
            }
            catch
            {
                //SystemLogger.Write(ex);
                return false;
            }
        }

        /// <summary>
        /// Save config to file
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path, SystemConfig config)
        {            
            //dir is or not
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //save config to file
            XmlSerializer serial = new XmlSerializer(typeof(SystemConfig));
            System.IO.TextWriter writer = new System.IO.StreamWriter(path + "SystemConfig.xml");
            serial.Serialize(writer, config);
            writer.Close();
            writer.Dispose();                            
        }
        /// <summary>
        /// 获得数据库连接串
        /// </summary>
        /// <returns></returns>
        //public string GetConnString()
        //{
        //    StringBuilder connString = new StringBuilder();
        //    connString.Append("server=" + _dB_Host);
        //    connString.Append(";database=" + _dB_Schenma);
        //    connString.Append(";uid=" + _dB_User);
        //    connString.Append(";pwd=" + Utility.DEncrypt.DESEncrypt.Decrypt(_dB_PWD));

        //    return connString.ToString();
        //}         
        #endregion
    }
}
