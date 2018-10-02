// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DeriveBytes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Представляет абстрактный базовый класс, от которого наследуются все классы, получающие последовательности байтов заданной длины.
  /// </summary>
  [ComVisible(true)]
  public abstract class DeriveBytes : IDisposable
  {
    /// <summary>
    ///   Если переопределено в производном классе, возвращает псевдослучайные байты ключа.
    /// </summary>
    /// <param name="cb">
    ///   Число генерируемых псевдослучайных байтов ключа.
    /// </param>
    /// <returns>
    ///   Массив байтов, заполненный псевдослучайными байтами ключа.
    /// </returns>
    public abstract byte[] GetBytes(int cb);

    /// <summary>
    ///   Если переопределено в производном классе, восстанавливает состояние данной операции.
    /// </summary>
    public abstract void Reset();

    /// <summary>
    ///   Если переопределено в производном классе, освобождает все ресурсы, используемые текущим объектом <see cref="T:System.Security.Cryptography.DeriveBytes" />.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Если переопределено в производном классе, освобождает неуправляемые ресурсы, используемые классом <see cref="T:System.Security.Cryptography.DeriveBytes" />, и опционально освобождает управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
    }
  }
}
