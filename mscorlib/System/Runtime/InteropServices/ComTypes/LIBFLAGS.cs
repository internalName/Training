// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.LIBFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Определяет флаги, применяемые к библиотекам и типам.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum LIBFLAGS : short
  {
    [__DynamicallyInvokable] LIBFLAG_FRESTRICTED = 1,
    [__DynamicallyInvokable] LIBFLAG_FCONTROL = 2,
    [__DynamicallyInvokable] LIBFLAG_FHIDDEN = 4,
    [__DynamicallyInvokable] LIBFLAG_FHASDISKIMAGE = 8,
  }
}
