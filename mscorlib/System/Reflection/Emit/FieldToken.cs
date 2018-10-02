// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.FieldToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   <see langword="FieldToken" /> Структура является объектное представление маркера, представляющего поле.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public struct FieldToken
  {
    /// <summary>
    ///   Значение по умолчанию FieldToken <see cref="P:System.Reflection.Emit.FieldToken.Token" /> значение 0.
    /// </summary>
    public static readonly FieldToken Empty;
    internal int m_fieldTok;
    internal object m_class;

    internal FieldToken(int field, Type fieldClass)
    {
      this.m_fieldTok = field;
      this.m_class = (object) fieldClass;
    }

    /// <summary>Извлекает маркер метаданных для данного поля.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает маркер метаданных данного поля.
    /// </returns>
    public int Token
    {
      get
      {
        return this.m_fieldTok;
      }
    }

    /// <summary>Создает хэш-код для этого поля.</summary>
    /// <returns>Возвращает хэш-код данного экземпляра.</returns>
    public override int GetHashCode()
    {
      return this.m_fieldTok;
    }

    /// <summary>
    ///   Определяет, является ли объект экземпляром <see langword="FieldToken" /> и эквивалентен данному экземпляру.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с <see langword="FieldToken" />.
    /// </param>
    /// <returns>
    ///   Возвращает <see langword="true" /> Если <paramref name="obj" /> является экземпляром класса <see langword="FieldToken" /> и равен этому объекту, в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is FieldToken)
        return this.Equals((FieldToken) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.Reflection.Emit.FieldToken" />.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Reflection.Emit.FieldToken" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(FieldToken obj)
    {
      if (obj.m_fieldTok == this.m_fieldTok)
        return obj.m_class == this.m_class;
      return false;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.FieldToken" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.FieldToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.FieldToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(FieldToken a, FieldToken b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.FieldToken" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.FieldToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.FieldToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(FieldToken a, FieldToken b)
    {
      return !(a == b);
    }
  }
}
