using System;

namespace Utility
{
    public class CommonEventArgs<T> : EventArgs
    {
        private T _data;

        public T Data
        {
            get
            {
                return _data;
            }
        }

        public CommonEventArgs(T data)
        {
            _data = data;
        }
    }
}
