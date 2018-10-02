// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CustomQueryInterfaceMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, является ли <see cref="M:System.Runtime.InteropServices.Marshal.GetComInterfaceForObject(System.Object,System.Type,System.Runtime.InteropServices.CustomQueryInterfaceMode)" /> метода IUnknown::QueryInterface можно использовать вызовы <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" /> интерфейс.
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum CustomQueryInterfaceMode
  {
    [__DynamicallyInvokable] Ignore,
    [__DynamicallyInvokable] Allow,
  }
}
