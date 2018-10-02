// Decompiled with JetBrains decompiler
// Type: System.ConsoleKeyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Описывает клавиши консоли, которая была нажата, включая символ, представленный клавиши консоли и состояние клавиш SHIFT, ALT и CTRL.
  /// </summary>
  [Serializable]
  public struct ConsoleKeyInfo
  {
    private char _keyChar;
    private ConsoleKey _key;
    private ConsoleModifiers _mods;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ConsoleKeyInfo" /> структуры с помощью заданного символа, клавиши консоли и клавиши-модификаторы.
    /// </summary>
    /// <param name="keyChar">
    ///   Знак Юникода, который соответствует <paramref name="key" /> параметра.
    /// </param>
    /// <param name="key">
    ///   Клавиша консоли, соответствует <paramref name="keyChar" /> параметра.
    /// </param>
    /// <param name="shift">
    ///   <see langword="true" />Чтобы указать, что была нажата клавиша SHIFT; в противном случае <see langword="false" />.
    /// </param>
    /// <param name="alt">
    ///   <see langword="true" />Чтобы указать, что была нажата клавиша ALT; в противном случае <see langword="false" />.
    /// </param>
    /// <param name="control">
    ///   <see langword="true" />Чтобы указать, что была нажата клавиша CTRL; в противном случае <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Числовое значение <paramref name="key" /> параметр меньше 0 или больше 255.
    /// </exception>
    public ConsoleKeyInfo(char keyChar, ConsoleKey key, bool shift, bool alt, bool control)
    {
      if (key < (ConsoleKey) 0 || key > (ConsoleKey.F16 | ConsoleKey.F17))
        throw new ArgumentOutOfRangeException(nameof (key), Environment.GetResourceString("ArgumentOutOfRange_ConsoleKey"));
      this._keyChar = keyChar;
      this._key = key;
      this._mods = (ConsoleModifiers) 0;
      if (shift)
        this._mods |= ConsoleModifiers.Shift;
      if (alt)
        this._mods |= ConsoleModifiers.Alt;
      if (!control)
        return;
      this._mods |= ConsoleModifiers.Control;
    }

    /// <summary>
    ///   Возвращает знак Юникода, представленный текущим <see cref="T:System.ConsoleKeyInfo" /> объекта.
    /// </summary>
    /// <returns>
    ///   Объект, который соответствует ключу консоли, представленный текущим <see cref="T:System.ConsoleKeyInfo" /> объекта.
    /// </returns>
    public char KeyChar
    {
      get
      {
        return this._keyChar;
      }
    }

    /// <summary>
    ///   Возвращает ключ консоли, представленный текущим <see cref="T:System.ConsoleKeyInfo" /> объекта.
    /// </summary>
    /// <returns>
    ///   Значение, определяющее клавиши консоли, которая была нажата.
    /// </returns>
    public ConsoleKey Key
    {
      get
      {
        return this._key;
      }
    }

    /// <summary>
    ///   Возвращает поразрядное сочетание <see cref="T:System.ConsoleModifiers" /> одновременном нажатии значений, которое задает один или несколько клавиш с клавишей консоли.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание значений перечисления.
    ///    Значение по умолчанию отсутствует.
    /// </returns>
    public ConsoleModifiers Modifiers
    {
      get
      {
        return this._mods;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, равен ли указанный объект текущему объекту <see cref="T:System.ConsoleKeyInfo" />.
    /// </summary>
    /// <param name="value">
    ///   Объект, который требуется сравнить с текущим объектом <see cref="T:System.ConsoleKeyInfo" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="value" /> является объектом <see cref="T:System.ConsoleKeyInfo" /> и равен текущему объекту <see cref="T:System.ConsoleKeyInfo" />. В противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool Equals(object value)
    {
      if (value is ConsoleKeyInfo)
        return this.Equals((ConsoleKeyInfo) value);
      return false;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, равен ли указанный объект <see cref="T:System.ConsoleKeyInfo" /> текущему объекту <see cref="T:System.ConsoleKeyInfo" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект, который требуется сравнить с текущим объектом <see cref="T:System.ConsoleKeyInfo" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если <paramref name="obj" /> равен текущему объекту <see cref="T:System.ConsoleKeyInfo" />. В противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(ConsoleKeyInfo obj)
    {
      if ((int) obj._keyChar == (int) this._keyChar && obj._key == this._key)
        return obj._mods == this._mods;
      return false;
    }

    /// <summary>
    ///   Указывает, равны ли значения заданных объектов <see cref="T:System.ConsoleKeyInfo" />.
    /// </summary>
    /// <param name="a">Первый из сравниваемых объектов.</param>
    /// <param name="b">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(ConsoleKeyInfo a, ConsoleKeyInfo b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, верно ли, что значения указанных объектов <see cref="T:System.ConsoleKeyInfo" /> не равны.
    /// </summary>
    /// <param name="a">Первый из сравниваемых объектов.</param>
    /// <param name="b">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(ConsoleKeyInfo a, ConsoleKeyInfo b)
    {
      return !(a == b);
    }

    /// <summary>
    ///   Возвращает хэш-код для текущего объекта <see cref="T:System.ConsoleKeyInfo" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    public override int GetHashCode()
    {
      return (int) ((ConsoleModifiers) this._keyChar | this._mods);
    }
  }
}
