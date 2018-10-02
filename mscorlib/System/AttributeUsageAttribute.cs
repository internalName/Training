// Decompiled with JetBrains decompiler
// Type: System.AttributeUsageAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает на применение другого класса атрибутов.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, Inherited = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class AttributeUsageAttribute : Attribute
  {
    internal static AttributeUsageAttribute Default = new AttributeUsageAttribute(AttributeTargets.All);
    internal AttributeTargets m_attributeTarget = AttributeTargets.All;
    internal bool m_inherited = true;
    internal bool m_allowMultiple;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.AttributeUsageAttribute" /> класса с указанным списком <see cref="T:System.AttributeTargets" />, <see cref="P:System.AttributeUsageAttribute.AllowMultiple" /> значение и <see cref="P:System.AttributeUsageAttribute.Inherited" /> значение.
    /// </summary>
    /// <param name="validOn">
    ///   Набор значений, определенный с помощью битовой операции OR, чтобы указать, какие элементы программы являются допустимыми.
    /// </param>
    [__DynamicallyInvokable]
    public AttributeUsageAttribute(AttributeTargets validOn)
    {
      this.m_attributeTarget = validOn;
    }

    internal AttributeUsageAttribute(AttributeTargets validOn, bool allowMultiple, bool inherited)
    {
      this.m_attributeTarget = validOn;
      this.m_allowMultiple = allowMultiple;
      this.m_inherited = inherited;
    }

    /// <summary>
    ///   Возвращает набор значений, определяющих какой указанный атрибут может применяться к элементам программы.
    /// </summary>
    /// <returns>
    ///   Один или несколько <see cref="T:System.AttributeTargets" /> значения.
    ///    Значение по умолчанию — <see langword="All" />.
    /// </returns>
    [__DynamicallyInvokable]
    public AttributeTargets ValidOn
    {
      [__DynamicallyInvokable] get
      {
        return this.m_attributeTarget;
      }
    }

    /// <summary>
    ///   Возвращает или задает логическое значение, указывающее, возможно ли для одного программного элемента несколько экземпляров указанного атрибута.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />Если более одного экземпляра может быть задан; в противном случае <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool AllowMultiple
    {
      [__DynamicallyInvokable] get
      {
        return this.m_allowMultiple;
      }
      [__DynamicallyInvokable] set
      {
        this.m_allowMultiple = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение <see cref="T:System.Boolean" />, определяющее, наследуется ли указанный атрибут производными классами и переопределяющими элементами.
    /// </summary>
    /// <returns>
    ///   Возвращает значение <see langword="true" />, если атрибут может наследоваться производными классами и переопределяющими элементами. В противном случае — значение <see langword="false" />.
    ///    Значение по умолчанию — <see langword="true" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Inherited
    {
      [__DynamicallyInvokable] get
      {
        return this.m_inherited;
      }
      [__DynamicallyInvokable] set
      {
        this.m_inherited = value;
      }
    }
  }
}
