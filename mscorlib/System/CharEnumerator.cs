// Decompiled with JetBrains decompiler
// Type: System.CharEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Поддерживает перебор объекта <see cref="T:System.String" /> и чтение его отдельных символов.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class CharEnumerator : IEnumerator, ICloneable, IEnumerator<char>, IDisposable
  {
    private string str;
    private int index;
    private char currentElement;

    internal CharEnumerator(string str)
    {
      this.str = str;
      this.index = -1;
    }

    /// <summary>
    ///   Создает копию текущего <see cref="T:System.CharEnumerator" /> объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Object" /> , Является копией текущего <see cref="T:System.CharEnumerator" /> объекта.
    /// </returns>
    public object Clone()
    {
      return this.MemberwiseClone();
    }

    /// <summary>
    ///   Увеличивает внутренний индекс текущего <see cref="T:System.CharEnumerator" /> объект на следующий символ перечисляемой строки.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если индекс успешно увеличен и находится в пределах перечисляемой строки; в противном случае — <see langword="false" />.
    /// </returns>
    public bool MoveNext()
    {
      if (this.index < this.str.Length - 1)
      {
        ++this.index;
        this.currentElement = this.str[this.index];
        return true;
      }
      this.index = this.str.Length;
      return false;
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.CharEnumerator" />.
    /// </summary>
    public void Dispose()
    {
      if (this.str != null)
        this.index = this.str.Length;
      this.str = (string) null;
    }

    object IEnumerator.Current
    {
      get
      {
        if (this.index == -1)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
        if (this.index >= this.str.Length)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
        return (object) this.currentElement;
      }
    }

    /// <summary>
    ///   Возвращает текущего указанного символа в строке, перечисленных в данном <see cref="T:System.CharEnumerator" /> объекта.
    /// </summary>
    /// <returns>
    ///   Знак Юникода, который в настоящее время ссылается этот <see cref="T:System.CharEnumerator" /> объекта.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Индекс является недопустимым; он является перед первым или после последнего символа обходимой строки.
    /// </exception>
    public char Current
    {
      get
      {
        if (this.index == -1)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
        if (this.index >= this.str.Length)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
        return this.currentElement;
      }
    }

    /// <summary>
    ///   Инициализирует индекс на позицию логически перед первым знаком перечисленной строки.
    /// </summary>
    public void Reset()
    {
      this.currentElement = char.MinValue;
      this.index = -1;
    }
  }
}
