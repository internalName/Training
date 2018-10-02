// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.MethodToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   <see langword="MethodToken" /> Структура является представлением объекта маркера, который представляет метод.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public struct MethodToken
  {
    /// <summary>
    ///   Значение по умолчанию <see langword="MethodToken" /> с <see cref="P:System.Reflection.Emit.MethodToken.Token" /> значение 0.
    /// </summary>
    public static readonly MethodToken Empty;
    internal int m_method;

    internal MethodToken(int str)
    {
      this.m_method = str;
    }

    /// <summary>Возвращает маркер метаданных для данного метода.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Возвращает маркер метаданных для данного метода.
    /// </returns>
    public int Token
    {
      get
      {
        return this.m_method;
      }
    }

    /// <summary>Возвращает созданный хэш-код для этого метода.</summary>
    /// <returns>Возвращает хэш-код данного экземпляра.</returns>
    public override int GetHashCode()
    {
      return this.m_method;
    }

    /// <summary>
    ///   Проверяет, является ли заданный объект равен данном <see langword="MethodToken" /> объекта.
    /// </summary>
    /// <param name="obj">Объект для сравнения на этот объект.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является экземпляром класса <see langword="MethodToken" /> и равен этому объекту, в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is MethodToken)
        return this.Equals((MethodToken) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.Reflection.Emit.MethodToken" />.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Reflection.Emit.MethodToken" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(MethodToken obj)
    {
      return obj.m_method == this.m_method;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.MethodToken" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.MethodToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.MethodToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(MethodToken a, MethodToken b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.MethodToken" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.MethodToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.MethodToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(MethodToken a, MethodToken b)
    {
      return !(a == b);
    }
  }
}
