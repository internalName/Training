// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.TypeToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Представляет <see langword="Token" /> возвращается метаданными, чтобы представить тип.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public struct TypeToken
  {
    /// <summary>
    ///   Значение по умолчанию <see langword="TypeToken" /> с <see cref="P:System.Reflection.Emit.TypeToken.Token" /> значение 0.
    /// </summary>
    public static readonly TypeToken Empty;
    internal int m_class;

    internal TypeToken(int str)
    {
      this.m_class = str;
    }

    /// <summary>Извлекает маркер метаданных для данного класса.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Получает маркер метаданных для данного типа.
    /// </returns>
    public int Token
    {
      get
      {
        return this.m_class;
      }
    }

    /// <summary>Создает хэш-код для этого типа.</summary>
    /// <returns>Возвращает хэш-код для этого типа.</returns>
    public override int GetHashCode()
    {
      return this.m_class;
    }

    /// <summary>
    ///   Проверяет, является ли указанный объект экземпляром <see langword="TypeToken" /> и эквивалентен данному экземпляру.
    /// </summary>
    /// <param name="obj">Объект, сравниваемый с TypeToken.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является экземпляром класса <see langword="TypeToken" /> и равен этому объекту, в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is TypeToken)
        return this.Equals((TypeToken) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.Reflection.Emit.TypeToken" />.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Reflection.Emit.TypeToken" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(TypeToken obj)
    {
      return obj.m_class == this.m_class;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.TypeToken" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.TypeToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.TypeToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(TypeToken a, TypeToken b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.TypeToken" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.TypeToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.TypeToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(TypeToken a, TypeToken b)
    {
      return !(a == b);
    }
  }
}
