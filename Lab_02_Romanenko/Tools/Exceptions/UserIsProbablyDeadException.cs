using System;

namespace Lab_02_Romanenko.Tools.Exceptions
{
    public class UserIsProbablyDeadException : Exception
    {
        public UserIsProbablyDeadException(String msg):base(msg)
        {
            
        }
    }
}