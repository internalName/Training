// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.Internal.Environment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Reflection;
using System.Resources;

namespace System.Diagnostics.Tracing.Internal
{
  internal static class Environment
  {
    public static readonly string NewLine = System.Environment.NewLine;
    private static ResourceManager rm = new ResourceManager("Microsoft.Diagnostics.Tracing.Messages", typeof (Environment).Assembly());

    public static int TickCount
    {
      get
      {
        return System.Environment.TickCount;
      }
    }

    public static string GetResourceString(string key, params object[] args)
    {
      string format = Environment.rm.GetString(key);
      if (format != null)
        return string.Format(format, args);
      string empty = string.Empty;
      foreach (object obj in args)
      {
        if (empty != string.Empty)
          empty += ", ";
        empty += obj.ToString();
      }
      return key + " (" + empty + ")";
    }

    public static string GetRuntimeResourceString(string key, params object[] args)
    {
      return Environment.GetResourceString(key, args);
    }
  }
}
