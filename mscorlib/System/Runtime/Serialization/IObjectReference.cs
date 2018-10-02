// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.IObjectReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Указывает, что конструктор текущего интерфейса является ссылкой на другой объект.
  /// </summary>
  [ComVisible(true)]
  public interface IObjectReference
  {
    /// <summary>
    ///   Возвращает реальный объект, который необходимо десериализовать, вместо объекта, задаваемого сериализованным потоком.
    /// </summary>
    /// <param name="context">
    ///   <see cref="T:System.Runtime.Serialization.StreamingContext" /> Из которого десериализуется текущий объект.
    /// </param>
    /// <returns>
    ///   Возвращает реальный объект, который помещается в граф.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    ///    Вызов не будет работать на средний доверенных серверов.
    /// </exception>
    [SecurityCritical]
    object GetRealObject(StreamingContext context);
  }
}
