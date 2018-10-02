// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.IDispatchImplType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, какую реализацию <see langword="IDispatch" /> следует использовать для определенного класса.
  /// </summary>
  [Obsolete("The IDispatchImplAttribute is deprecated.", false)]
  [ComVisible(true)]
  [Serializable]
  public enum IDispatchImplType
  {
    SystemDefinedImpl,
    InternalImpl,
    CompatibleImpl,
  }
}
