using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Utility
{
    public class DLLWrapper
    {
        private static DLLWrapper _instance;
        private static readonly object dllLoadlock = new object();

        public static DLLWrapper Instance
        {
            get
            {
                lock (dllLoadlock)
                {
                    if (_instance == null)
                    {
                        _instance = new DLLWrapper();
                    }
                    return _instance;
                }
            }
        }
        private DLLWrapper()
        {
           
        }

        ///<summary>
        /// API LoadLibrary
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int LoadLibrary(String funcname);

        ///<summary>
        /// API GetProcAddress
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int GetProcAddress(int handle, String funcname);

        ///<summary>
        /// API FreeLibrary
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int FreeLibrary(int handle);

        ///<summary>
        ///ͨ�����йܺ�����ת��Ϊ��Ӧ��ί��
        ///</summary>
        ///<param name="dllModule">ͨ��LoadLibrary��õ�DLL���</param>
        ///<param name="functionName">���йܺ�����</param>
        ///<param name="t">��Ӧ��ί������</param>
        ///<returns>ί��ʵ������ǿ��ת��Ϊ�ʵ���ί������</returns>

        public static Delegate GetFunctionAddress(int dllModule, string functionName, Type t)
        {
            int address = GetProcAddress(dllModule, functionName);
            if (address == 0)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);
        }

        ///<summary>
        ///����ʾ������ַ��IntPtrʵ��ת���ɶ�Ӧ��ί��
        ///</summary>
        public static Delegate GetDelegateFromIntPtr(IntPtr address, Type t)
        {
            if (address == IntPtr.Zero)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(address, t);
        }

        ///<summary>
        ///����ʾ������ַ��intת���ɶ�Ӧ��ί��
        ///</summary>
        public static Delegate GetDelegateFromIntPtr(int address, Type t)
        {
            if (address == 0)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);
        }
    }
}
