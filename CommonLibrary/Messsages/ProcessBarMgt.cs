using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CommonLibrary
{
    public class ProcessBarMgt : IDisposable
    {
        #region property
        private bool _isStop;
        private Thread doProcessThread;
        private string _title;
        private bool _isShowButton;
        private WaitForm waitForm;
        public bool IsStop
        {
            get { return _isStop; }
            set { _isStop = value; }
        }
        #endregion
        public ProcessBarMgt()
        {
        }
        public ProcessBarMgt(string title)
        {
            _title = title;
            _isShowButton = false;
            StartThread();
            Thread.Sleep(100);
        }
        ~ProcessBarMgt()
        {
            //if (waitForm != null)
            //{
            //    waitForm.Close();
            //    doProcessThread.Abort();
            //}
        }
        #region method
        private void StartThread()
        {
            if (doProcessThread == null)
            {
                doProcessThread = new Thread(new ThreadStart(Display));
                doProcessThread.Name = "Do Process Bar Thread";
                doProcessThread.IsBackground = true;
                doProcessThread.Start();
            }
        }
        private void Display()
        {
            waitForm = new WaitForm();
            waitForm.Title = _title;
            waitForm.IsShowButton = _isShowButton;
            waitForm.ShowDialog();
            _isStop = true;
        }
        public void ShowBar(string title, bool isShowButton)
        {
            _title = title;
            _isShowButton = isShowButton;
            StartThread();

        }
        public void ShowBar(string title)
        {
            _title = title;
            _isShowButton = true;
            StartThread();
        }
        public void ShowBarNoClose(string title)
        {
            _title = title;
            _isShowButton = false;
            StartThread();
        }
        public void CloseBar()
        {
            if (!_isStop)
            {
                if (waitForm != null)
                {
                    waitForm.IsClose = true;
                }
            }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                Thread.Sleep(1000);
                if (waitForm != null)
                {
                    waitForm.IsClose = true;
                    //doProcessThread.Abort();
                }
            }
            catch (Exception ex)
            {                
            }
        }

        #endregion
    }
}
