using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Utility
{
    public sealed class Win32API
    {
        [DllImport("User32.dll", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("User32.dll", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("User32.dll", EntryPoint = "SendInput", CharSet = CharSet.Ansi)]
        public static extern UInt32 SendInput(UInt32 nInputs, Input[] pInputs, Int32 cbSize);

        [DllImport("Kernel32.dll", EntryPoint = "GetTickCount", CharSet = CharSet.Ansi)]
        public static extern int GetTickCount();

        [DllImport("User32.dll", EntryPoint = "GetKeyState", CharSet = CharSet.Ansi)]
        public static extern short GetKeyState(int nVirtKey);

        [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Ansi)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi)]
        public static extern int GetClipBox(IntPtr hDC, ref Rectangle lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("kernel32", CharSet = CharSet.Ansi)]
        public static extern int GetProcessHeap();

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern bool IsWindow(IntPtr hWnd);


        [DllImport("kernel32", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int SetProcessWorkingSetSize(int hProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("user32.dll", EntryPoint = "UpdateWindow")]
        public static extern int UpdateWindow(int hwnd );

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern int FindWindowEx(int hWnd1,int hWnd2,string lpsz1,string lpsz2);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpClassName,string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        public static extern int GetWindowRect(int hWnd, ref WinRECT rec);

        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public static extern int GetCursorPos(ref WinPOINT point);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")] 
        public static extern int SetCursorPos(int x,int y);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetDiskFreeSpaceExW", CharSet = CharSet.Unicode)]
        public static extern bool GetDiskFreeSpace(string drive, ref long freeBytes, ref long totalBytes, ref long totalFreeBytes);


        public static readonly IntPtr InvalidHandleValue = new IntPtr(-1);
        public const UInt32 FILE_MAP_WRITE = 2;
        public const UInt32 PAGE_READWRITE = 0x04;

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileMapping(IntPtr hFile,
            IntPtr pAttributes, UInt32 flProtect,
            UInt32 dwMaximumSizeHigh, UInt32 dwMaximumSizeLow, String pName);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenFileMapping(UInt32 dwDesiredAccess,
            Boolean bInheritHandle, String name);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        public static extern Boolean CloseHandle(IntPtr handle);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject,
            UInt32 dwDesiredAccess,
            UInt32 dwFileOffsetHigh, UInt32 dwFileOffsetLow,
            UInt32 dwNumberOfBytesToMap);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        public static extern Boolean UnmapViewOfFile(IntPtr address);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        public static extern Boolean DuplicateHandle(IntPtr hSourceProcessHandle,
            IntPtr hSourceHandle,
            IntPtr hTargetProcessHandle, ref IntPtr lpTargetHandle,
            UInt32 dwDesiredAccess, Boolean bInheritHandle, UInt32 dwOptions);

        public const UInt32 DUPLICATE_CLOSE_SOURCE = 0x00000001;
        public const UInt32 DUPLICATE_SAME_ACCESS = 0x00000002;

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        public static extern IntPtr GetCurrentProcess();

    
    }

    [StructLayout(LayoutKind.Sequential)] 
    public struct WinPOINT
    {
        int X;
        int Y;
    } 

    [StructLayout(LayoutKind.Sequential)]
    public struct WinRECT 
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom; 
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public int mouseData;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public short wVk;
        public short wScan;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Input
    {
        [FieldOffset(0)]
        public int type;
        [FieldOffset(4)]
        public MOUSEINPUT mi;
        [FieldOffset(4)]
        public KEYBDINPUT ki;
        [FieldOffset(4)]
        public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }

    public class INPUT
    {
        public const int MOUSE = 0;
        public const int KEYBOARD = 1;
        public const int HARDWARE = 2;
    }

    public static class KeyboardConstaint
    {
        public static readonly short VK_F1 = 0x70;
        public static readonly short VK_F2 = 0x71;
        public static readonly short VK_F3 = 0x72;
        public static readonly short VK_F4 = 0x73;
        public static readonly short VK_F5 = 0x74;
        public static readonly short VK_F6 = 0x75;
        public static readonly short VK_F7 = 0x76;
        public static readonly short VK_F8 = 0x77;
        public static readonly short VK_F9 = 0x78;
        public static readonly short VK_F10 = 0x79;
        public static readonly short VK_F11 = 0x7A;
        public static readonly short VK_F12 = 0x7B;

        public static readonly short VK_LEFT = 0x25;
        public static readonly short VK_UP = 0x26;
        public static readonly short VK_RIGHT = 0x27;
        public static readonly short VK_DOWN = 0x28;

        public static readonly short VK_NONE = 0x00;
        public static readonly short VK_ESCAPE = 0x1B;
        public static readonly short VK_EXECUTE = 0x2B;
        public static readonly short VK_CANCEL = 0x03;
        public static readonly short VK_RETURN = 0x0D;
        public static readonly short VK_ACCEPT = 0x1E;
        public static readonly short VK_BACK = 0x08;
        public static readonly short VK_TAB = 0x09;
        public static readonly short VK_DELETE = 0x2E;
        public static readonly short VK_CAPITAL = 0x14;
        public static readonly short VK_NUMLOCK = 0x90;
        public static readonly short VK_SPACE = 0x20;
        public static readonly short VK_DECIMAL = 0x6E;
        public static readonly short VK_SUBTRACT = 0x6D;

        public static readonly short VK_ADD = 0x6B;
        public static readonly short VK_DIVIDE = 0x6F;
        public static readonly short VK_MULTIPLY = 0x6A;
        public static readonly short VK_INSERT = 0x2D;

        public static readonly short VK_OEM_1 = 0xBA;  // ';:' for US
        public static readonly short VK_OEM_PLUS = 0xBB;  // '+' any country

        public static readonly short VK_OEM_MINUS = 0xBD;  // '-' any country

        public static readonly short VK_OEM_2 = 0xBF;  // '/?' for US
        public static readonly short VK_OEM_3 = 0xC0;  // '`~' for US
        public static readonly short VK_OEM_4 = 0xDB;  //  '[{' for US
        public static readonly short VK_OEM_5 = 0xDC;  //  '\|' for US
        public static readonly short VK_OEM_6 = 0xDD;  //  ']}' for US
        public static readonly short VK_OEM_7 = 0xDE;  //  ''"' for US
        public static readonly short VK_OEM_PERIOD = 0xBE;  // '.>' any country
        public static readonly short VK_OEM_COMMA = 0xBC;  // ',<' any country
        public static readonly short VK_SHIFT = 0x10;
        public static readonly short VK_CONTROL = 0x11;
        public static readonly short VK_MENU = 0x12;
        public static readonly short VK_LWIN = 0x5B;
        public static readonly short VK_RWIN = 0x5C;
        public static readonly short VK_APPS = 0x5D;

        public static readonly short VK_LSHIFT = 0xA0;
        public static readonly short VK_RSHIFT = 0xA1;
        public static readonly short VK_LCONTROL = 0xA2;
        public static readonly short VK_RCONTROL = 0xA3;
        public static readonly short VK_LMENU = 0xA4;
        public static readonly short VK_RMENU = 0xA5;

        public static readonly short VK_SNAPSHOT = 0x2C;
        public static readonly short VK_SCROLL = 0x91;
        public static readonly short VK_PAUSE = 0x13;
        public static readonly short VK_HOME = 0x24;

        public static readonly short VK_NEXT = 0x22;
        public static readonly short VK_PRIOR = 0x21;
        public static readonly short VK_END = 0x23;

        public static readonly short VK_NUMPAD0 = 0x60;
        public static readonly short VK_NUMPAD1 = 0x61;
        public static readonly short VK_NUMPAD2 = 0x62;
        public static readonly short VK_NUMPAD3 = 0x63;
        public static readonly short VK_NUMPAD4 = 0x64;
        public static readonly short VK_NUMPAD5 = 0x65;
        public static readonly short VK_NUMPAD5NOTHING = 0x0C;
        public static readonly short VK_NUMPAD6 = 0x66;
        public static readonly short VK_NUMPAD7 = 0x67;
        public static readonly short VK_NUMPAD8 = 0x68;
        public static readonly short VK_NUMPAD9 = 0x69;

        public static readonly short KEYEVENTF_EXTENDEDKEY = 0x0001;
        public static readonly short KEYEVENTF_KEYUP = 0x0002;

        public static readonly int GWL_EXSTYLE = -20;
        public static readonly int WS_DISABLED = 0X8000000;
        public static readonly int WM_SETFOCUS = 0X0007;
    }

}
