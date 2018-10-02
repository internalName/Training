// Decompiled with JetBrains decompiler
// Type: System.Globalization.TextElementEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
  /// <summary>Перечисляет текстовые элементы строки.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class TextElementEnumerator : IEnumerator
  {
    private string str;
    private int index;
    private int startIndex;
    [NonSerialized]
    private int strLen;
    [NonSerialized]
    private int currTextElementLen;
    [OptionalField(VersionAdded = 2)]
    private UnicodeCategory uc;
    [OptionalField(VersionAdded = 2)]
    private int charLen;
    private int endIndex;
    private int nextTextElementLen;

    internal TextElementEnumerator(string str, int startIndex, int strLen)
    {
      this.str = str;
      this.startIndex = startIndex;
      this.strLen = strLen;
      this.Reset();
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.charLen = -1;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this.strLen = this.endIndex + 1;
      this.currTextElementLen = this.nextTextElementLen;
      if (this.charLen != -1)
        return;
      this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.endIndex = this.strLen - 1;
      this.nextTextElementLen = this.currTextElementLen;
    }

    /// <summary>
    ///   Перемещает перечислитель на следующий текстовый элемент строки.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если перечислитель был успешно перемещен на следующий текстовый элемент; <see langword="false" /> если перечислитель достиг конца строки.
    /// </returns>
    [__DynamicallyInvokable]
    public bool MoveNext()
    {
      if (this.index >= this.strLen)
      {
        this.index = this.strLen + 1;
        return false;
      }
      this.currTextElementLen = StringInfo.GetCurrentTextElementLen(this.str, this.index, this.strLen, ref this.uc, ref this.charLen);
      this.index += this.currTextElementLen;
      return true;
    }

    /// <summary>Возвращает текущий текстовый элемент строки.</summary>
    /// <returns>
    ///   Объект, содержащий текущий текстовый элемент строки.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Перечислитель располагается перед первым текстовым элементом строки или после последнего текстового элемента.
    /// </exception>
    [__DynamicallyInvokable]
    public object Current
    {
      [__DynamicallyInvokable] get
      {
        return (object) this.GetTextElement();
      }
    }

    /// <summary>Возвращает текущий текстовый элемент строки.</summary>
    /// <returns>
    ///   Новая строка, содержащая текущий текстовый элемент в читаемой строке.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Перечислитель располагается перед первым текстовым элементом строки или после последнего текстового элемента.
    /// </exception>
    [__DynamicallyInvokable]
    public string GetTextElement()
    {
      if (this.index == this.startIndex)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
      if (this.index > this.strLen)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
      return this.str.Substring(this.index - this.currTextElementLen, this.currTextElementLen);
    }

    /// <summary>
    ///   Возвращает индекс текстового элемента, в котором в настоящий момент находится перечислитель.
    /// </summary>
    /// <returns>
    ///   Индекс текстового элемента, в котором в настоящий момент находится перечислитель.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Перечислитель располагается перед первым текстовым элементом строки или после последнего текстового элемента.
    /// </exception>
    [__DynamicallyInvokable]
    public int ElementIndex
    {
      [__DynamicallyInvokable] get
      {
        if (this.index == this.startIndex)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
        return this.index - this.currTextElementLen;
      }
    }

    /// <summary>
    ///   Перемещает перечислитель в исходное положение, перед первым текстовым элементом в строке.
    /// </summary>
    [__DynamicallyInvokable]
    public void Reset()
    {
      this.index = this.startIndex;
      if (this.index >= this.strLen)
        return;
      this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
    }
  }
}
