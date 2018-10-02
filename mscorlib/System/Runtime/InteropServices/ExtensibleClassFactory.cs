// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ExtensibleClassFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Позволяет настраивать управляемых объектов, полученных из неуправляемых объектов во время создания.
  /// </summary>
  [ComVisible(true)]
  public sealed class ExtensibleClassFactory
  {
    private ExtensibleClassFactory()
    {
    }

    /// <summary>
    ///   Регистрирует <see langword="delegate" /> вызывается, когда экземпляр управляемого типа, расширенному из неуправляемого типа, понадобится разместить сводный неуправляемый объект.
    /// </summary>
    /// <param name="callback">
    ///   Объект <see langword="delegate" /> вызываемый вместо <see langword="CoCreateInstance" />.
    /// </param>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void RegisterObjectCreationCallback(ObjectCreationDelegate callback);
  }
}
