// Decompiled with JetBrains decompiler
// Type: System.AppDomainManagerInitializationOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает действие, предпринимаемое пользовательского домена приложения при инициализации нового домена.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  public enum AppDomainManagerInitializationOptions
  {
    None = 0,
    RegisterWithHost = 1,
  }
}
