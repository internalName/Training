// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.EventToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Представляет <see langword="Token" /> возвращенный метаданными для представления события.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public struct EventToken
  {
    /// <summary>
    ///   Значение по умолчанию <see langword="EventToken" /> с <see cref="P:System.Reflection.Emit.EventToken.Token" /> значение 0.
    /// </summary>
    public static readonly EventToken Empty;
    internal int m_event;

    internal EventToken(int str)
    {
      this.m_event = str;
    }

    /// <summary>Извлекает маркер метаданных для данного события.</summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает маркер метаданных для данного события.
    /// </returns>
    public int Token
    {
      get
      {
        return this.m_event;
      }
    }

    /// <summary>Создает хэш-код для данного события.</summary>
    /// <returns>Возвращает хэш-код данного экземпляра.</returns>
    public override int GetHashCode()
    {
      return this.m_event;
    }

    /// <summary>
    ///   Проверяет, является ли указанный объект экземпляром <see langword="EventToken" /> и эквивалентен данному экземпляру.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   Возвращает <see langword="true" /> Если <paramref name="obj" /> является экземпляром класса <see langword="EventToken" /> и совпадает с текущим экземпляром; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is EventToken)
        return this.Equals((EventToken) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.Reflection.Emit.EventToken" />.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Reflection.Emit.EventToken" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(EventToken obj)
    {
      return obj.m_event == this.m_event;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.EventToken" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.EventToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.EventToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(EventToken a, EventToken b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.EventToken" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.EventToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.EventToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(EventToken a, EventToken b)
    {
      return !(a == b);
    }
  }
}
