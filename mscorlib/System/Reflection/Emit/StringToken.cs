// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.StringToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>Представляет маркер, представляющий строку.</summary>
  [ComVisible(true)]
  [Serializable]
  public struct StringToken
  {
    internal int m_string;

    internal StringToken(int str)
    {
      this.m_string = str;
    }

    /// <summary>Извлекает маркер метаданных для данной строки.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Получает маркер метаданных для данной строки.
    /// </returns>
    public int Token
    {
      get
      {
        return this.m_string;
      }
    }

    /// <summary>Возвращает хэш-код для этой строки.</summary>
    /// <returns>Возвращает маркер основной строки.</returns>
    public override int GetHashCode()
    {
      return this.m_string;
    }

    /// <summary>
    ///   Проверяет, является ли указанный объект экземпляром <see langword="StringToken" /> и эквивалентен данному экземпляру.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим <see langword="StringToken" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является экземпляром класса <see langword="StringToken" /> и равен этому объекту, в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is StringToken)
        return this.Equals((StringToken) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.Reflection.Emit.StringToken" />.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Reflection.Emit.StringToken" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(StringToken obj)
    {
      return obj.m_string == this.m_string;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.StringToken" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.StringToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.StringToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(StringToken a, StringToken b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.StringToken" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.StringToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.StringToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(StringToken a, StringToken b)
    {
      return !(a == b);
    }
  }
}
