using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace HWCtrl
{
    class LeetroMotorCtrl : IMotor
    {
        private int totalAxes = 0;

        public bool Connect(string ipAddr)
        {
            bool ret = false;
            try
            {
                totalAxes = NativeMethods.auto_set();
                if(totalAxes<=0)
                {
                    Trace.WriteLine("LeetroMotorCtrl auto_set fail!");
                    return false;
                }

                int totalCards = NativeMethods.init_board();        //初始化
                if (totalCards <= 0)
                {
                    Trace.WriteLine("LeetroMotorCtrl init_board fail!");
                    return false;
                }

                ret = true;
            }
            catch(Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl Connect error! " + ex.Message);
            }
            return ret;
        }

        public void Disconnect()
        {
            //do nothing
        }

        public bool ExecHome(int axis)
        {
            bool ret = false;
            try
            {
                double pos = 0.0;
                if (NativeMethods.get_abs_pos(axis, ref pos) != 0)
                {
                    Trace.WriteLine("LeetroMotorCtrl get_abs_pos fail!");
                }

                int dir = 1;
                dir = pos > 0 ? -1 : 1;
                if (NativeMethods.con_hmove(axis, dir) == 0)
                {
                    NativeMethods.reset_pos(axis);
                    ret = true;
                }
                else
                {
                    Trace.WriteLine("LeetroMotorCtrl con_hmove fail!");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl ExecHome error! " + ex.Message);
            }
            return ret;
        }

        public void ExecHomeALL()
        {
            try
            {
                double pos1 = 0.0;
                double pos2 = 0.0;
                double pos3 = 0.0;
                NativeMethods.get_abs_pos(1, ref pos1);
                NativeMethods.get_abs_pos(2, ref pos2);
                NativeMethods.get_abs_pos(3, ref pos3);

                int dir1 = pos1 > 0 ? -1 : 1;
                int dir2 = pos1 > 0 ? -1 : 1;
                int dir3 = pos1 > 0 ? -1 : 1;
                if (NativeMethods.con_hmove3(1, dir1, 2, dir2, 3, dir3) == 0)
                {
                    NativeMethods.reset_pos(1);
                    NativeMethods.reset_pos(2);
                    NativeMethods.reset_pos(3);
                }
                else
                {
                    Trace.WriteLine("LeetroMotorCtrl con_hmove3 fail!");
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl ExecHomeALL error! " + ex.Message);
            }
        }

        public float GetAxisXPos()
        {
            double pos = 0.0;
            try
            {
                if (NativeMethods.get_abs_pos(1, ref pos) != 0)
                {
                    Trace.WriteLine("LeetroMotorCtrl get_abs_pos fail!");
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl GetAxisXPos error! " + ex.Message);
            }
            return (float)pos;
        }

        public float GetAxisXPosUm()
        {
            throw new NotImplementedException();
        }

        public float GetAxisYPos()
        {
            double pos = 0.0;
            try
            {
                if (NativeMethods.get_abs_pos(2, ref pos) != 0)
                {
                    Trace.WriteLine("LeetroMotorCtrl get_abs_pos fail!");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl GetAxisYPos error! " + ex.Message);
            }
            return (float)pos;
        }

        public float GetAxisYPosUm()
        {
            throw new NotImplementedException();
        }

        public float GetAxisZPos()
        {
            double pos = 0.0;
            try
            {
                if (NativeMethods.get_abs_pos(3, ref pos) != 0)
                {
                    Trace.WriteLine("LeetroMotorCtrl get_abs_pos fail!");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl GetAxisZPos error! " + ex.Message);
            }
            return (float)pos;
        }

        public float GetAxisZPosUm()
        {
            throw new NotImplementedException();
        }
        public float GetAxisXPosUmEx()
        {
            throw new NotImplementedException();
        }
        public float GetAxisYPosUmEx() 
        {
            throw new NotImplementedException();
        }
        public float GetAxisZPosUmEx()
        {
            throw new NotImplementedException();
        }

        public bool IsAxisRun(int axis)
        {
            bool ret = false;
            try
            {
                int res = NativeMethods.get_cur_dir(axis);
                if (res == 1 || res == -1)
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl get_cur_dir error! " + ex.Message);
            }
            return ret;
        }

        public bool IsHome()
        {
            bool ret = false;
            try
            {
                for (int i = 0; i < totalAxes; i++)
                {
                    if (NativeMethods.check_home(i + 1) != 1)
                    {
                        return false;
                    }
                }
                ret = true;
            }
            catch(Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl IsHome error! " + ex.Message);
            }
            return ret;
        }

        public void LoadGlass()
        {
            throw new NotImplementedException();
        }

        public void Move(int axis, float dist)
        {
            try
            {
                if (NativeMethods.con_pmove(axis, dist) != 0)
                {
                    Trace.WriteLine("LeetroMotorCtrl con_pmove fail!");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl SetSpeed error! " + ex.Message);
            }
        }

        public void MoveWait(int axis, float dist)
        {
            try
            {
                Move(axis, dist);
                //
                while (true)
                {
                    if (IsAxisRun(axis))
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl SetSpeed error! " + ex.Message);
            }
        }

        public void SetOca(string cmd)
        {
            throw new NotImplementedException();
        }

        public void SetOcaOff()
        {
            throw new NotImplementedException();
        }

        public void SetSpeed(int axis, float speed)
        {
            try
            {
                if (NativeMethods.set_conspeed(axis, (double)speed) != 0)
                {
                    Trace.WriteLine("LeetroMotorCtrl set_conspeed fail!");
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl SetSpeed error! " + ex.Message);
            }
        }

        public void StopAxis()
        {
            try
            {
                int ret = 0;
                switch(totalAxes)
                {
                    case 1:
                        ret = NativeMethods.sudden_stop(1);
                        break;
                    case 2:
                        ret = NativeMethods.sudden_stop2(1, 2);
                        break;
                    case 3:
                        ret = NativeMethods.sudden_stop3(1, 2, 3);
                        break;
                    default:
                        break;
                }
                if (ret != 0)
                {
                    Trace.WriteLine("LeetroMotorCtrl StopAxis fail!");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl StopAxis error! " + ex.Message);
            }
        }

        public void UnLoadGlass(float pos)
        {
            throw new NotImplementedException();
        }

        public void XYToZero()
        {
            try
            {
                double pos1 = 0.0;
                double pos2 = 0.0;
                NativeMethods.get_abs_pos(1, ref pos1);
                NativeMethods.get_abs_pos(2, ref pos2);

                int dir1 = pos1 > 0 ? -1 : 1;
                int dir2 = pos1 > 0 ? -1 : 1;
                if (NativeMethods.con_hmove2(1, dir1,2, dir2) == 0)
                {
                    NativeMethods.reset_pos(1);
                    NativeMethods.reset_pos(2);
                }
                else
                {
                    Trace.WriteLine("LeetroMotorCtrl con_hmove2 fail!");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("LeetroMotorCtrl XYToZero error! " + ex.Message);
            }
        }

        public void MoveXYAxis(float distX, float distY, bool needWait)
        {
            throw new NotImplementedException();
        }
    }
}
