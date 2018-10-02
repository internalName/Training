// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationInfoEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Предоставляет удобный для модуля форматирования механизм анализа данных в <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class SerializationInfoEnumerator : IEnumerator
  {
    private string[] m_members;
    private object[] m_data;
    private Type[] m_types;
    private int m_numItems;
    private int m_currItem;
    private bool m_current;

    internal SerializationInfoEnumerator(string[] members, object[] info, Type[] types, int numItems)
    {
      this.m_members = members;
      this.m_data = info;
      this.m_types = types;
      this.m_numItems = numItems - 1;
      this.m_currItem = -1;
      this.m_current = false;
    }

    /// <summary>Обновляет перечислитель к следующему элементу.</summary>
    /// <returns>
    ///   <see langword="true" /> Если новый элемент найден; в противном случае — <see langword="false" />.
    /// </returns>
    public bool MoveNext()
    {
      if (this.m_currItem < this.m_numItems)
      {
        ++this.m_currItem;
        this.m_current = true;
      }
      else
        this.m_current = false;
      return this.m_current;
    }

    object IEnumerator.Current
    {
      get
      {
        if (!this.m_current)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
        return (object) new SerializationEntry(this.m_members[this.m_currItem], this.m_data[this.m_currItem], this.m_types[this.m_currItem]);
      }
    }

    /// <summary>Получает элемент, проверяемый в настоящее время.</summary>
    /// <returns>Элемент, проверяемый в настоящее время.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Перечислитель не началась перечисления элементов или достигнут конец перечисления.
    /// </exception>
    public SerializationEntry Current
    {
      get
      {
        if (!this.m_current)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
        return new SerializationEntry(this.m_members[this.m_currItem], this.m_data[this.m_currItem], this.m_types[this.m_currItem]);
      }
    }

    /// <summary>Сбрасывает перечислитель к первому элементу.</summary>
    public void Reset()
    {
      this.m_currItem = -1;
      this.m_current = false;
    }

    /// <summary>
    ///   Возвращает имя для проверяемого в настоящее время элемента.
    /// </summary>
    /// <returns>Имя элемента.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Перечислитель не началась перечисления элементов или достигнут конец перечисления.
    /// </exception>
    public string Name
    {
      get
      {
        if (!this.m_current)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
        return this.m_members[this.m_currItem];
      }
    }

    /// <summary>
    ///   Возвращает значение проверяемого в настоящее время элемента.
    /// </summary>
    /// <returns>Значение проверяемого в настоящее время элемента.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Перечислитель не началась перечисления элементов или достигнут конец перечисления.
    /// </exception>
    public object Value
    {
      get
      {
        if (!this.m_current)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
        return this.m_data[this.m_currItem];
      }
    }

    /// <summary>
    ///   Получает тип проверяемого в настоящее время элемента.
    /// </summary>
    /// <returns>Тип проверяемого в настоящее время элемента.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Перечислитель не началась перечисления элементов или достигнут конец перечисления.
    /// </exception>
    public Type ObjectType
    {
      get
      {
        if (!this.m_current)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
        return this.m_types[this.m_currItem];
      }
    }
  }
}
