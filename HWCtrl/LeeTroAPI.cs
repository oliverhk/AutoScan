using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace HWCtrl
{
    public class NativeMethods
    {
        public const string dllLeetroMotor = "MPC2810.dll";

        //初始化
        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int auto_set();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int init_board();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_unit_flag(int flag);

        //参数设置函数
        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_maxspeed(int ch, double speed);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_conspeed(int ch, double conspeed);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_vector_conspeed(double conspeed);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_profile(int ch, double vl, double vh, double ad);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_vector_profile(double vec_vl, double vec_vh, double vec_ad);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int c_set_vector_profile(double vec_vl, double vec_vh, double vec_ad);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int c_set_max_accel(double vec_ad);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int c_set_multiple(double mul);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int c_set_curve_vertex(int nmode);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_ellipse_ratio(double ratio);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_s_curve(int ch, int mode);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_s_section(int ch, double accel_sec, double decel_sec);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_abs_pos(int ch, double pos);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int reset_pos(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int reset_encoder(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_unit(int ch, double dl);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_steps_pr(int ch, int rd);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_enc_thread(int ch, int rd);

        //运动指令函数
        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_pmove(int ch, double step);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_pmove2(int ch1, double step1, int ch2, double step2);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_pmove3(int ch1, double step1, int ch2, double step2, int ch3, double step3);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_pmove4(int ch1, double step1, int ch2, double step2, int ch3, double step3, int ch4, double step4);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_pmove_to(int ch, double step);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_pmove(int ch, double step);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_pmove2(int ch1, double step1, int ch2, double step2);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_pmove3(int ch1, double step1, int ch2, double step2, int ch3, double step3);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_pmove4(int ch1, double step1, int ch2, double step2, int ch3, double step3, int ch4, double step4);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_pmove_to(int ch, double step);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_vmove(int ch, int dir);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_vmove2(int ch1, int dir1, int ch2, int dir2);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_vmove3(int ch1, int dir1, int ch2, int dir2, int ch3, int dir3);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_vmove4(int ch1, int dir1, int ch2, int dir2, int ch3, int dir3, int ch4, int dir4);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_vmove(int ch, int dir);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_vmove2(int ch1, int dir1, int ch2, int dir2);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_vmove3(int ch1, int dir1, int ch2, int dir2, int ch3, int dir3);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_vmove4(int ch1, int dir1, int ch2, int dir2, int ch3, int dir3, int ch4, int dir4);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_hmove(int ch, int dir1);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_hmove2(int ch1, int dir1, int ch2, int dir2);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_hmove3(int ch1, int dir1, int ch2, int dir2, int ch3, int dir3);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_hmove4(int ch1, int dir1, int ch2, int dir2, int ch3, int dir3, int ch4, int dir4);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_hmove(int ch, int dir);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_hmove2(int ch1, int dir1, int ch2, int dir2);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_hmove3(int ch1, int dir1, int ch2, int dir2, int ch3, int dir3);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_hmove4(int ch1, int dir1, int ch2, int dir2, int ch3, int dir3, int ch4, int dir4);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_line2(int ch1, double step1, int ch2, double step2);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_line3(int ch1, double step1, int ch2, double step2, int ch3, double step3);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_line4(int ch1, double step1, int ch2, double step2, int ch3, double step3, int ch4, double step4);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int con_linen_to(int chnum, ref int ch, ref double pos);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_line2(int ch1, double step1, int ch2, double step2);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_line3(int ch1, double step1, int ch2, double step2, int ch3, double step3);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_line4(int ch1, double step1, int ch2, double step2, int ch3, double step3, int ch4, double step4);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_linen_to(int chnum, ref int ch, ref double pos);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int arc_center(int ch1, int ch2, double cen1, double cen2, double angle);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int arc_final(int ch1, int ch2, int dir, double fx, double fy, double r);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int fast_arc_center(int ch1, int ch2, double cen1, double cen2, double angle);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int helical_move(int ch1, int ch2, int ch3, double cen1, double cen2, double angle, double pitch);

        //制动函数
        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int sudden_stop(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int sudden_stop2(int ch1, int ch2);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int sudden_stop3(int ch1, int ch2, int ch3);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int sudden_stop4(int ch1, int ch2, int ch3, int ch4);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int sudden_stop_list();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int decel_stop(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int decel_stop2(int ch1, int ch2);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int decel_stop3(int ch1, int ch2, int ch3);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int decel_stop4(int ch1, int ch2, int ch3, int ch4);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int decel_stop_list();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int move_pause(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int move_pause_list();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int move_resume(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int move_resume_list();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int delay_time(int time);

        //位置和状态查询函数
        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_max_axe();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_board_num();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_axe(int cardno);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_unit(int ch, ref double dl);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int check_IC(int cardno);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_abs_pos(int ch, ref double pos);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_rel_pos(int ch, ref double pos);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_encoder(int ch, ref long count);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_done_source(int ch, ref long src);

        //PWM输出控制
        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int enable_pwm(int cardno, int en);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_pwm_freq(int cardno, double freq);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_pwm_ratio(int cardno, double ratio);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern double get_conspeed(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern double get_vector_conspeed();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_profile(int ch, ref double vl, ref double vh, ref double ad);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_vector_profile(ref double vec_vl, ref double vec_vh, ref double vec_ad);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern double get_rate(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_cur_dir(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int check_status(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int check_done(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int check_limit(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int check_home(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int check_axis_alm(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int check_alarm(int ch);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int check_delay_status();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int get_cmd_counter();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int reset_cmd_counter();

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int set_cmd_counter(int counter);

        [DllImport(dllLeetroMotor, CallingConvention = CallingConvention.StdCall)]
        public static extern int check_timer_num(ref ulong num);
    }
}