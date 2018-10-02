// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.INVOKEKIND
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Указывает способ вызова функции методом <see langword="IDispatch::Invoke" />.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum INVOKEKIND
  {
    [__DynamicallyInvokable] INVOKE_FUNC = 1,
    [__DynamicallyInvokable] INVOKE_PROPERTYGET = 2,
    [__DynamicallyInvokable] INVOKE_PROPERTYPUT = 4,
    [__DynamicallyInvokable] INVOKE_PROPERTYPUTREF = 8,
  }
}
