using System;
using System.Transactions;

namespace Lab_02_Romanenko.Tools.Exceptions
{
    public class InvalidEMailException: Exception
    {
      public  InvalidEMailException(String msg):base(msg)
      {
        }
    }
}