using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using CommonLibrary;
using Utility;
using Utility.DEncrypt;
using System.Text.RegularExpressions;
using DBCtrl.Model;
using DBCtrl.DAO;
namespace MainApp.WinUI
{
    public partial class ConfigForm : Form
    {
        #region property
        private SystemConfig sysCfg = SystemConfig.Instance;
        private IList<User> lstUers;     //list user for
        #endregion
        #region instance
        //Singleton instance
        private static ConfigForm _instance;
        public static ConfigForm Instance => _instance ?? (_instance = new ConfigForm());
        #endregion
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            //load config
            dgvUser.AutoGenerateColumns = false;
            InitConfig();
            LoadConfig();
            LoadUser();
        }

        #region Config frm funs
        private void InitConfig()
        {
            try
            {
                //serial port
                cmbFlashControlPort.Items.Clear();
                cmbXinMTPort.Items.Clear();
                cmbIOPort.Items.Clear();
                string[] serialName = System.IO.Ports.SerialPort.GetPortNames();
                foreach(var v in serialName)
                {
                    cmbFlashControlPort.Items.Add(v);
                    cmbXinMTPort.Items.Add(v);
                    cmbIOPort.Items.Add(v);
                }
                // piez type
                foreach(PiezType v in Enum.GetValues(typeof(PiezType)))
                {
                    cmbPiezType.Items.Add(v);
                }

                //Barcode type
                foreach(BarcodeType v in Enum.GetValues(typeof(BarcodeType)))
                {
                    cmbBarcodeType.Items.Add(v);
                }

            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private void LoadConfig()
        {
            try
            {
                string path = System.IO.Directory.GetParent(Application.StartupPath).ToString();
                sysCfg.LoadConfig(path + @"\configure\SystemConfig.xml");

                //OCR config       
                nmbDbRow.Value = sysCfg.OCR_DbRow;
                nmbDbCol.Value = sysCfg.OCR_DbCol;
                nmbDbPhi.Value = sysCfg.OCR_DbPhi;
                nmbDbLen1.Value = sysCfg.OCR_DbLen1;
                nmbDbLen2.Value = sysCfg.OCR_DbLen2;
                nmbFontWidth.Value = sysCfg.OCR_FontWidth;
                nmbFontHeight.Value = sysCfg.OCR_FontHeight;
                nmbStrokeWidth.Value = (decimal)sysCfg.OCR_StrokeWidth;
                cmbBarcodeType.Text = sysCfg.OcrBarcodeType.ToString();

                //assignment to form 
                nmbMaxLog.Value = sysCfg.Max_logs_day;
                nmbXResolution.Value = (decimal)sysCfg.XCountUnit;
                nmbYResolution.Value = (decimal)sysCfg.YCountUnit;
                nmbZSolution.Value = (decimal)sysCfg.ZCountUnit;
                txtEqptID.Text = sysCfg.EquipmentId;
                nmbEqptNum.Value = sysCfg.EquipmentNum;
                txtHospital.Text = sysCfg.Hospital;
                nmbLimitZ.Value = (decimal)sysCfg.Limit_Z;

                //control
                txtControlIp.Text = sysCfg.Control_IP;
                cmbFlashControlPort.Text = sysCfg.Light_Serial_Port;
                cmbPiezType.Text = sysCfg.PizType.ToString();
                cmbXinMTPort.Text = sysCfg.Pize_Serial_Port;
                nmbPIPortNum.Value = sysCfg.PI_ComPort_Num;

                //i/o card
                cmbIOPort.Text = sysCfg.IOCard_Serial_Port;
                nmbIStart.Value = sysCfg.IO_Input_Start;
                nmbIStop.Value = sysCfg.IO_Input_Stop;
                nmbOStartLight.Value = sysCfg.IO_Output_Start_Light;
                nmbOStopLight.Value = sysCfg.IO_Output_Stop_Light;
                nmbONormalLight.Value = sysCfg.IO_Output_Green_Light;
                nmbOAbnormalLight.Value = sysCfg.IO_Output_Red_Light;
                nmbOLightOnAlways.Value = sysCfg.IO_Light_On_Alway;

                //move config
                nmbSpeedX.Value = (decimal)sysCfg.Move_Speed_X;
                nmbSpeedY.Value = (decimal)sysCfg.Move_Speed_Y;
                nmbLoadX.Value = (decimal)sysCfg.Load_Pos_X;
                nmbLoadY.Value = (decimal)sysCfg.Load_Pos_Y;

            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        private bool IsValid()
        {
            bool ret = false;
            //string pattern;
            //Regex rgx;
            try
            {
                if(string.IsNullOrEmpty(txtControlIp.Text))
                {
                    Dialogs.Show("控制器地址不能为空!");
                    txtControlIp.Focus();
                    return ret;
                }
                Regex rgx = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
                if(!rgx.IsMatch(txtControlIp.Text))
                {
                    Dialogs.Show("控制器地址格式不正确!");
                    txtControlIp.Focus();
                    return ret;
                }
                //seril port is 
                if(string.IsNullOrEmpty(cmbPiezType.Text))
                {
                    Dialogs.Show("压电陶瓷类型不能为空!");
                    cmbPiezType.Focus();
                    return ret;
                }
                if (string.IsNullOrEmpty(cmbFlashControlPort.Text))
                {
                    Dialogs.Show("触发控制器端口不能为空!");
                    cmbFlashControlPort.Focus();
                    return ret;
                }
                if (string.IsNullOrEmpty(cmbXinMTPort.Text))
                {
                    Dialogs.Show("芯明天控制端口不能为空!");
                    cmbXinMTPort.Focus();
                    return ret;
                }
                if (string.IsNullOrEmpty(cmbIOPort.Text))
                {
                    Dialogs.Show("I/O控制卡端口不能为空!");
                    cmbIOPort.Focus();
                    return ret;
                }
                if (string.IsNullOrEmpty(cmbBarcodeType.Text))
                {
                    Dialogs.Show("条码类型不能为空!");
                    cmbBarcodeType.Focus();
                    return ret;
                }
                if (cmbFlashControlPort.Text == cmbXinMTPort.Text || 
                    cmbFlashControlPort.Text ==cmbIOPort.Text ||
                    cmbXinMTPort.Text == cmbIOPort.Text)
                {
                    Dialogs.Show("控制端口有重复选择!");                    
                    return ret;
                }

                //io
                if(nmbIStart.Value == nmbIStop.Value)
                {
                    Dialogs.Show("输入I/O有重复定义!");
                    return ret;
                }
                List<decimal> lstO = new List<decimal>();
                if (!lstO.Contains(nmbOStartLight.Value))
                    lstO.Add(nmbOStartLight.Value);
                if (!lstO.Contains(nmbOStopLight.Value))
                    lstO.Add(nmbOStopLight.Value);
                if (!lstO.Contains(nmbONormalLight.Value))
                    lstO.Add(nmbONormalLight.Value);
                if (!lstO.Contains(nmbOAbnormalLight.Value))
                    lstO.Add(nmbOAbnormalLight.Value);
                if (!lstO.Contains(nmbOLightOnAlways.Value))
                    lstO.Add(nmbOLightOnAlways.Value);
                if(lstO.Count<5)
                {
                    Dialogs.Show("输出I/O有重复定义!");
                    return ret;
                }

                ret = true;
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        private bool SaveConfig()
        {
            bool ret = false;
            try
            {
                string path = System.IO.Directory.GetParent(Application.StartupPath).ToString()
                       + @"\configure\";

                SystemConfig cfg = SystemConfig.Instance;

                cfg.Max_logs_day = (int)nmbMaxLog.Value;
                cfg.XCountUnit = (float)nmbXResolution.Value;
                cfg.YCountUnit= (float)nmbYResolution.Value;
                cfg.ZCountUnit= (float)nmbZSolution.Value;
                cfg.EquipmentId= txtEqptID.Text;
                cfg.EquipmentNum= (int)nmbEqptNum.Value;
                cfg.Hospital= txtHospital.Text;
                cfg.Limit_Z= (float)nmbLimitZ.Value;

                //control
                cfg.Control_IP= txtControlIp.Text;
                cfg.Light_Serial_Port= cmbFlashControlPort.Text;
                cfg.PizType= (PiezType)Enum.Parse(typeof(PiezType),cmbPiezType.Text);
                cfg.Pize_Serial_Port= cmbXinMTPort.Text;
                cfg.PI_ComPort_Num= (int)nmbPIPortNum.Value;

                //i/o card
                cfg.IOCard_Serial_Port= cmbIOPort.Text;
                cfg.IO_Input_Start= (int)nmbIStart.Value;
                cfg.IO_Input_Stop= (int)nmbIStop.Value;
                cfg.IO_Output_Start_Light= (int)nmbOStartLight.Value;
                cfg.IO_Output_Stop_Light= (int)nmbOStopLight.Value;
                cfg.IO_Output_Green_Light= (int)nmbONormalLight.Value;
                cfg.IO_Output_Red_Light= (int)nmbOAbnormalLight.Value;
                cfg.IO_Light_On_Alway= (int)nmbOLightOnAlways.Value;

                //move config
                cfg.Move_Speed_X = (float)nmbSpeedX.Value;
                cfg.Move_Speed_Y = (float)nmbSpeedY.Value;
                cfg.Load_Pos_X = (float)nmbLoadX.Value;
                cfg.Load_Pos_Y = (float)nmbLoadY.Value;

                sysCfg.OCR_DbRow = (int)nmbDbRow.Value;
                sysCfg.OCR_DbCol = (int)nmbDbCol.Value;
                sysCfg.OCR_DbPhi = (int)nmbDbPhi.Value;
                sysCfg.OCR_DbLen1 = (int)nmbDbLen1.Value;
                sysCfg.OCR_DbLen2 = (int)nmbDbLen2.Value;
                sysCfg.OCR_FontWidth = (int)nmbFontWidth.Value;
                sysCfg.OCR_FontHeight = (int)nmbFontHeight.Value;
                sysCfg.OCR_StrokeWidth = (float)nmbStrokeWidth.Value;
                cfg.OcrBarcodeType = (BarcodeType)Enum.Parse(typeof(BarcodeType), cmbBarcodeType.Text);

                cfg.Save(path, cfg);
                ret = true;
            }
            catch (Exception ex)
            {
                Dialogs.Show(ex);
            }
            return ret;
        }
        #endregion

        #region user frm funs
        private void LoadUser()
        {
            try
            {
                UserDao dao = new UserDao();
                lstUers = dao.GetList();
                dgvUser.DataSource = lstUers;
                dgvUser.Refresh();
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        #endregion

        #region config frm event
        private void btnCfgLoad_Click(object sender, EventArgs e)
        {
            //load config
            LoadConfig();
            Dialogs.Show("重新加载配置文件完成.");
        }

        private void btnCfgSave_Click(object sender, EventArgs e)
        {
            //save config
            if (!IsValid())
                return;
            if (SaveConfig())
            {
                Dialogs.Show("保存配置成功.");
            }
            else
            {
                Dialogs.Show("保存配置失败.");
            }
        }        
        #endregion

        #region user frm event
        private void btnQueryUser_Click(object sender, EventArgs e)
        {
            LoadUser();
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            Usr.UserAddForm frm = new Usr.UserAddForm();
            frm.IsAdd = true;
            frm.ShowDialog();            
            LoadUser();
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            try
            {


                if (lstUers.Count < 0)
                    return;
                if (dgvUser.SelectedRows.Count <= 0)
                {
                    Dialogs.Show("请选择需要修改的人员!");
                    return;
                }
                string usrId = dgvUser.SelectedRows[0].Cells[0].Value.ToString();
                Usr.UserAddForm frm = new Usr.UserAddForm();
                frm.IsAdd = false;
                frm.UpUserId = usrId;
                frm.ShowDialog();
                LoadUser();
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUers.Count < 0)
                    return;
                if (dgvUser.SelectedRows.Count <= 0)
                {
                    Dialogs.Show("请选择需要修改的人员!");
                    return;
                }
                string usrId = dgvUser.SelectedRows[0].Cells[0].Value.ToString();
                if (Dialogs.Confirm("确认删除用户:{" + usrId + "}?"))
                {
                    UserDao dao = new UserDao();
                    if (dao.Delete(usrId))
                    {
                        Dialogs.Show("删除用户成功!");
                        LoadUser();
                    }
                    else
                    {
                        Dialogs.Show("删除用户失败!");
                    }

                }
            }catch(Exception  ex)
            { LogHelper.AppLoger.Error(ex); }
        }
        #endregion        
    }
}
