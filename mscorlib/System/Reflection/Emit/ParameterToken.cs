// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ParameterToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   <see langword="ParameterToken" /> Структура является непрозрачным представлением маркер, возвращенный метаданными для представления параметра.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public struct ParameterToken
  {
    /// <summary>
    ///   Значение по умолчанию <see langword="ParameterToken" /> с <see cref="P:System.Reflection.Emit.ParameterToken.Token" /> значение 0.
    /// </summary>
    public static readonly ParameterToken Empty;
    internal int m_tkParameter;

    internal ParameterToken(int tkParam)
    {
      this.m_tkParameter = tkParam;
    }

    /// <summary>Извлекает маркер метаданных для данного параметра.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает маркер метаданных для данного параметра.
    /// </returns>
    public int Token
    {
      get
      {
        return this.m_tkParameter;
      }
    }

    /// <summary>Создает хэш-код для этого параметра.</summary>
    /// <returns>Возвращает хэш-код для этого параметра.</returns>
    public override int GetHashCode()
    {
      return this.m_tkParameter;
    }

    /// <summary>
    ///   Проверяет, является ли указанный объект экземпляром <see langword="ParameterToken" /> и эквивалентен данному экземпляру.
    /// </summary>
    /// <param name="obj">Объект для сравнения на этот объект.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является экземпляром класса <see langword="ParameterToken" /> и совпадает с текущим экземпляром; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is ParameterToken)
        return this.Equals((ParameterToken) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.Reflection.Emit.ParameterToken" />.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Reflection.Emit.ParameterToken" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(ParameterToken obj)
    {
      return obj.m_tkParameter == this.m_tkParameter;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.ParameterToken" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.ParameterToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.ParameterToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(ParameterToken a, ParameterToken b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.ParameterToken" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.ParameterToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.ParameterToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(ParameterToken a, ParameterToken b)
    {
      return !(a == b);
    }
  }
}
