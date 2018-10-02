// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ICustomProperty
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("30DA92C0-23E8-42A0-AE7C-734A0E5D2782")]
  [ComImport]
  internal interface ICustomProperty
  {
    Type Type { get; }

    string Name { get; }

    object GetValue(object target);

    void SetValue(object target, object value);

    object GetValue(object target, object indexValue);

    void SetValue(object target, object value, object indexValue);

    bool CanWrite { get; }

    bool CanRead { get; }
  }
}
