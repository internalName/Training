// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CustomQueryInterfaceResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет значения, возвращаемые для <see cref="M:System.Runtime.InteropServices.ICustomQueryInterface.GetInterface(System.Guid@,System.IntPtr@)" /> метод.
  /// </summary>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum CustomQueryInterfaceResult
  {
    [__DynamicallyInvokable] Handled,
    [__DynamicallyInvokable] NotHandled,
    [__DynamicallyInvokable] Failed,
  }
}
