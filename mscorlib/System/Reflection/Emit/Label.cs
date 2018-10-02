// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.Label
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Представляет метку в потоке инструкций.
  ///   <see langword="Label" /> используется в сочетании с <see cref="T:System.Reflection.Emit.ILGenerator" /> класса.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public struct Label
  {
    internal int m_label;

    internal Label(int label)
    {
      this.m_label = label;
    }

    internal int GetLabelValue()
    {
      return this.m_label;
    }

    /// <summary>Создает хэш-код для данного экземпляра.</summary>
    /// <returns>Возвращает хэш-код для данного экземпляра.</returns>
    public override int GetHashCode()
    {
      return this.m_label;
    }

    /// <summary>
    ///   Проверяет, является ли указанный объект экземпляром <see langword="Label" /> и эквивалентен данному экземпляру.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим <see langword="Label" /> экземпляра.
    /// </param>
    /// <returns>
    ///   Возвращает <see langword="true" /> Если <paramref name="obj" /> является экземпляром класса <see langword="Label" /> и равен этому объекту, в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is Label)
        return this.Equals((Label) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.Reflection.Emit.Label" />.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Reflection.Emit.Label" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(Label obj)
    {
      return obj.m_label == this.m_label;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.Label" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.Label" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.Label" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(Label a, Label b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.Label" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.Label" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.Label" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(Label a, Label b)
    {
      return !(a == b);
    }
  }
}
