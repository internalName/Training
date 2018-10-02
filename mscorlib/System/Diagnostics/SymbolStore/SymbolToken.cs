// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.SymbolToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> Структура является объектным представлением маркера, представляющего символьные данные.
  /// </summary>
  [ComVisible(true)]
  public struct SymbolToken
  {
    internal int m_token;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> структуры для заданного значения.
    /// </summary>
    /// <param name="val">Значение, используемое для маркера.</param>
    public SymbolToken(int val)
    {
      this.m_token = val;
    }

    /// <summary>Возвращает значение текущего маркера.</summary>
    /// <returns>Значение текущего маркера.</returns>
    public int GetToken()
    {
      return this.m_token;
    }

    /// <summary>Создает хэш-код для текущего маркера.</summary>
    /// <returns>Хэш-код для текущего маркера.</returns>
    public override int GetHashCode()
    {
      return this.m_token;
    }

    /// <summary>
    ///   Определяет, является ли <paramref name="obj" /> является экземпляром класса <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> и эквивалентен данному экземпляру.
    /// </summary>
    /// <param name="obj">Объект для проверки.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является экземпляром класса <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> и эквивалентен данному экземпляру; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is SymbolToken)
        return this.Equals((SymbolToken) obj);
      return false;
    }

    /// <summary>
    ///   Определяет, является ли <paramref name="obj" /> равен данному экземпляру.
    /// </summary>
    /// <param name="obj">
    ///   Проверяемый элемент <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если значение параметра <paramref name="obj" /> равно данному экземпляру; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool Equals(SymbolToken obj)
    {
      return obj.m_token == this.m_token;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, будет ли два <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> объекты равны.
    /// </summary>
    /// <param name="a">
    ///   A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> структуры.
    /// </param>
    /// <param name="b">
    ///   A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> структуры.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator ==(SymbolToken a, SymbolToken b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, будет ли два <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> объекты не равны.
    /// </summary>
    /// <param name="a">
    ///   A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> структуры.
    /// </param>
    /// <param name="b">
    ///   A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> структуры.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="a" /> и <paramref name="b" /> не равны друг другу; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator !=(SymbolToken a, SymbolToken b)
    {
      return !(a == b);
    }
  }
}
