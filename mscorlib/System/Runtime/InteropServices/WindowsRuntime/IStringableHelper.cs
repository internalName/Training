// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IStringableHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal class IStringableHelper
  {
    internal static string ToString(object obj)
    {
      IStringable stringable = obj as IStringable;
      if (stringable != null)
        return stringable.ToString();
      return obj.ToString();
    }
  }
}
