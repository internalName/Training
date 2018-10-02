// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadStartException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>
  ///   Исключение выдается, когда происходит сбой в управляемом потоке после запуска базового потока операционной системы, но до готовности потока к выполнению кода пользователя.
  /// </summary>
  [Serializable]
  public sealed class ThreadStartException : SystemException
  {
    private ThreadStartException()
      : base(Environment.GetResourceString("Arg_ThreadStartException"))
    {
      this.SetErrorCode(-2146233051);
    }

    private ThreadStartException(Exception reason)
      : base(Environment.GetResourceString("Arg_ThreadStartException"), reason)
    {
      this.SetErrorCode(-2146233051);
    }

    internal ThreadStartException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
