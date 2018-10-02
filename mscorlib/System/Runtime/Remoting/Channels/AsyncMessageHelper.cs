// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.AsyncMessageHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.Remoting.Channels
{
  internal static class AsyncMessageHelper
  {
    internal static void GetOutArgs(ParameterInfo[] syncParams, object[] syncArgs, object[] endArgs)
    {
      int num = 0;
      for (int index = 0; index < syncParams.Length; ++index)
      {
        if (syncParams[index].IsOut || syncParams[index].ParameterType.IsByRef)
          endArgs[num++] = syncArgs[index];
      }
    }
  }
}
