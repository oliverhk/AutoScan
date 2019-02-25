using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using CommonLibrary;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;

namespace MainApp.Src
{
    public class SocketManager
    {
        #region property        
        public delegate void UpdateMessageHander(object msg);
        public static event UpdateMessageHander UpdateMessage;      
          
        private static byte[] receiveBuffer = new byte[5*1024*1024];
        private static int myProt = 8888;   //端口  
        static Socket serverSocket;
        private Thread listenThread;        //listen thread
        private Socket connectSocket;
                
        #endregion
        #region Instance
        private static SocketManager instance = null;
        private static readonly object plcLock = new object();
        public static SocketManager Instance
        {
            get
            {
                lock (plcLock)
                {
                    if (instance == null)
                    {
                        instance = new SocketManager();
                    }
                    return instance;
                }
            }
        }

        
        #endregion
        public SocketManager()
        {

        }
        ~SocketManager()
        {
            if (serverSocket != null)
            {
                serverSocket.Close();
            }
            if (listenThread != null)
            {
                listenThread.Abort();
            }
        }
        #region interface
        public void Start()
        {
            Connect();
        }
        public void Stop()
        {
            if (serverSocket != null)
            {
                serverSocket.Close();
            }
            if (listenThread !=null)
            {
                listenThread.Abort();
            }     
            
        }
        private void Connect()
        {
            try
            {                
                //服务器IP地址  
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口  
                serverSocket.Listen(10);    //设定最多10个排队连接请求  
                LogHelper.AppLoger.DebugFormat("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

                //通过Clientsoket发送数据  
                listenThread = new Thread(ListenClientConnect);
                listenThread.Start();
                //Console.ReadLine();

               // //接收连接
               // Socket ts = serverSocket.Accept();
               // Console.WriteLine("客户端已连接");

               // //开始异步接收
               // ts.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), ts);
               //// Console.ReadKey();
            }
            catch(Exception ex)
            {
                Dialogs.Show(ex);
            }
        }
        //private void ReceiveCallback(IAsyncResult result)
        //{
        //    Socket ts = (Socket)result.AsyncState;
        //    ts.EndReceive(result);
        //    result.AsyncWaitHandle.Close();
        //    Console.WriteLine("收到消息：{0}", Encoding.ASCII.GetString(receiveBuffer));

        //    //清空数据，重新开始异步接收
        //    receiveBuffer = new byte[receiveBuffer.Length];
        //    ts.BeginReceive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), ts);
        //}
        /// <summary>  
        /// 监听客户端连接  
        /// </summary>
        private void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                connectSocket = clientSocket;
                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }
        public void SendMessage(string msg)
        {
            if (connectSocket != null)
            {
                connectSocket.Send(Encoding.ASCII.GetBytes(msg));
            }
        }
        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="clientSocket"></param>
        private void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {                    
                    //通过clientSocket接收数据  
                    int receiveNumber = myClientSocket.Receive(receiveBuffer);                                        
                    //myClientSocket.Send(Encoding.ASCII.GetBytes("recevie data successed"));
                    var msg = Encoding.Default.GetString(receiveBuffer,0, receiveNumber);
                    ThrowMessage(msg);
                    //break;                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }
        /// <summary>
        /// //将Byte转换为结构体类型
        /// </summary>
        /// <param name="structObj"></param>
        /// <param name="size"></param>
        /// <returns></returns>        
        public static byte[] StructToBytes(object structObj, int size)
        {
            //Entity.DefectBlob sd;
            int num = 2;
            byte[] bytes = new byte[size];
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            //从内存空间拷贝到byte 数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return bytes;

        }
        /// <summary>
        /// //将Byte转换为结构体类型
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        //public static object ByteToStruct(byte[] bytes,ref int indX,ref int indY)
        //{            
            
        //    //List<Entity.DefectBlob> listBlob = new List<Entity.DefectBlob>();
        //    int size = Marshal.SizeOf(new Entity.DefectBlob());
        //    if (size > bytes.Length)
        //    {
        //        return null;
        //    }
        //    //get count
        //    byte[] btCnt = new byte[4];

        //    //die index;
        //    Array.Copy(bytes,btCnt,4);
        //    indX = System.BitConverter.ToInt32(btCnt, 0);

        //    //die indxe y
        //    Array.Copy(bytes, 4, btCnt,0, 4);
        //    indY = System.BitConverter.ToInt32(btCnt, 0);

        //    //defect cout
        //    Array.Copy(bytes,8, btCnt,0,4);
        //    int defectCount = System.BitConverter.ToInt32(btCnt,0);
        //    IntPtr structPtr = Marshal.AllocHGlobal(size);            

        //    int offset = 12;

        //    for (int i=0;i<defectCount;i++)
        //    {
        //        Marshal.Copy(bytes, offset + i * size, structPtr, size);
        //        var blob = Marshal.PtrToStructure(structPtr, typeof(Entity.DefectBlob));
        //        //listBlob.Add((Entity.DefectBlob)blob);
        //    }
        //    Marshal.FreeHGlobal(structPtr);
        //    ////分配结构体内存空间
        //    //IntPtr structPtr = Marshal.AllocHGlobal(size);
        //    ////将byte数组拷贝到分配好的内存空间
        //    //Marshal.Copy(bytes, 0, structPtr, size);
        //    ////将内存空间转换为目标结构体
        //    //object obj = Marshal.PtrToStructure(structPtr, typeof(Entity.DefectBlob));
        //    ////释放内存空间
        //    //Marshal.FreeHGlobal(structPtr);
        //    return listBlob;
        //}
        #endregion
        #region evt
        private void ThrowMessage(object msg)
        {
            if (UpdateMessage != null)
            {
                foreach (UpdateMessageHander t in UpdateMessage.GetInvocationList())
                {
                    t.BeginInvoke(msg, null, null);
                }
            }
        }
        #endregion
    }
}
