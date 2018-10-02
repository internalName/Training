// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.PropertyToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   <see langword="PropertyToken" /> Структура является непрозрачным представлением <see langword="Token" /> возвращенный метаданными для представления свойства.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public struct PropertyToken
  {
    /// <summary>
    ///   Значение по умолчанию <see langword="PropertyToken" /> с <see cref="P:System.Reflection.Emit.PropertyToken.Token" /> значение 0.
    /// </summary>
    public static readonly PropertyToken Empty;
    internal int m_property;

    internal PropertyToken(int str)
    {
      this.m_property = str;
    }

    /// <summary>Извлекает маркер метаданных для данного свойства.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает маркер метаданных для данного экземпляра.
    /// </returns>
    public int Token
    {
      get
      {
        return this.m_property;
      }
    }

    /// <summary>Создает хэш-код для этого свойства.</summary>
    /// <returns>Возвращает хэш-код для этого свойства.</returns>
    public override int GetHashCode()
    {
      return this.m_property;
    }

    /// <summary>
    ///   Проверяет, является ли указанный объект экземпляром <see langword="PropertyToken" /> и эквивалентен данному экземпляру.
    /// </summary>
    /// <param name="obj">Объект, подлежащий сравнению.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является экземпляром класса <see langword="PropertyToken" /> и совпадает с текущим экземпляром; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is PropertyToken)
        return this.Equals((PropertyToken) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.Reflection.Emit.PropertyToken" />.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Reflection.Emit.PropertyToken" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(PropertyToken obj)
    {
      return obj.m_property == this.m_property;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.PropertyToken" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.PropertyToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.PropertyToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(PropertyToken a, PropertyToken b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.PropertyToken" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.PropertyToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.PropertyToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(PropertyToken a, PropertyToken b)
    {
      return !(a == b);
    }
  }
}
