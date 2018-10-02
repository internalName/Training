// Decompiled with JetBrains decompiler
// Type: System.Threading.LockCookie
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>
  ///   Определяет блокировку, которая реализует семантику однократной записи и нескольких чтения.
  ///    Это тип значения.
  /// </summary>
  [ComVisible(true)]
  public struct LockCookie
  {
    private int _dwFlags;
    private int _dwWriterSeqNum;
    private int _wReaderAndWriterLevel;
    private int _dwThreadID;

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    public override int GetHashCode()
    {
      return this._dwFlags + this._dwWriterSeqNum + this._wReaderAndWriterLevel + this._dwThreadID;
    }

    /// <summary>
    ///   Указывает, является ли указанный объект <see cref="T:System.Threading.LockCookie" /> и равен текущему экземпляру.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is LockCookie)
        return this.Equals((LockCookie) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.Threading.LockCookie" />.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Threading.LockCookie" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(LockCookie obj)
    {
      if (obj._dwFlags == this._dwFlags && obj._dwWriterSeqNum == this._dwWriterSeqNum && obj._wReaderAndWriterLevel == this._wReaderAndWriterLevel)
        return obj._dwThreadID == this._dwThreadID;
      return false;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Threading.LockCookie" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Threading.LockCookie" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Threading.LockCookie" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(LockCookie a, LockCookie b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Threading.LockCookie" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Threading.LockCookie" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Threading.LockCookie" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(LockCookie a, LockCookie b)
    {
      return !(a == b);
    }
  }
}
