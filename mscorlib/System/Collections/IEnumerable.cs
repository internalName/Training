// Decompiled with JetBrains decompiler
// Type: System.Collections.IEnumerable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Collections
{
  /// <summary>
  ///   Предоставляет перечислитель, который поддерживает простой перебор элементов неуниверсальной коллекции.
  /// 
  ///   Для просмотра исходного кода .NET Framework для этого типа, в разделе Reference Source.
  /// </summary>
  [Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IEnumerable
  {
    /// <summary>
    ///   Возвращает перечислитель, который осуществляет итерацию по коллекции.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Collections.IEnumerator" />, который используется для прохода по коллекции.
    /// </returns>
    [DispId(-4)]
    [__DynamicallyInvokable]
    IEnumerator GetEnumerator();
  }
}
