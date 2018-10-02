// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CONNECTDATA
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.CONNECTDATA" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.CONNECTDATA instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct CONNECTDATA
  {
    /// <summary>
    ///   Представляет указатель на <see langword="IUnknown" /> интерфейса подключенных приемнике уведомлений.
    ///    Вызывающий объект должен вызвать <see langword="IUnknown::Release" /> на этот указатель при <see langword="CONNECTDATA" /> Структура больше не нужен.
    /// </summary>
    [MarshalAs(UnmanagedType.Interface)]
    public object pUnk;
    /// <summary>
    ///   Представляет маркер подключения, который возвращается из вызова <see cref="M:System.Runtime.InteropServices.UCOMIConnectionPoint.Advise(System.Object,System.Int32@)" />.
    /// </summary>
    public int dwCookie;
  }
}
