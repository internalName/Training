// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.IObjectHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Определяет интерфейс для разворачивания маршалирования по значению объектов косвенного обращения.
  /// </summary>
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("C460E2B4-E199-412a-8456-84DC3E4838C3")]
  [ComVisible(true)]
  public interface IObjectHandle
  {
    /// <summary>Развертывает объект.</summary>
    /// <returns>Развернутый объект.</returns>
    object Unwrap();
  }
}
