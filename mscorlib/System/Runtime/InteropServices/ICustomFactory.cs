// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ICustomFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Позволяет пользователям писать код активации для управляемых объектов, расширяющих <see cref="T:System.MarshalByRefObject" />.
  /// </summary>
  [ComVisible(true)]
  public interface ICustomFactory
  {
    /// <summary>Создает новый экземпляр заданного типа.</summary>
    /// <param name="serverType">Тип активации.</param>
    /// <returns>
    ///   A <see cref="T:System.MarshalByRefObject" /> связанный с указанным типом.
    /// </returns>
    MarshalByRefObject CreateInstance(Type serverType);
  }
}
