// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.SerTrace
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters
{
  internal static class SerTrace
  {
    [Conditional("_LOGGING")]
    internal static void InfoLog(params object[] messages)
    {
    }

    [Conditional("SER_LOGGING")]
    internal static void Log(params object[] messages)
    {
      if (!(messages[0] is string))
        messages[0] = (object) (messages[0].GetType().Name + " ");
      else
        messages[0] = (object) (messages[0].ToString() + " ");
    }
  }
}
