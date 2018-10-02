// Decompiled with JetBrains decompiler
// Type: System.Reflection.ObfuscationAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Инструктирует инструменты запутывания предпринять заданные действия для сборки, типа или члена.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  public sealed class ObfuscationAttribute : Attribute
  {
    private bool m_strip = true;
    private bool m_exclude = true;
    private bool m_applyToMembers = true;
    private string m_feature = "all";

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Boolean" /> значение, указывающее, должно ли средство запутывания удалить этот атрибут после обработки.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если средство запутывания должно удалять атрибут после обработки; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="true" />.
    /// </returns>
    public bool StripAfterObfuscation
    {
      get
      {
        return this.m_strip;
      }
      set
      {
        this.m_strip = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Boolean" /> значение, указывающее, следует ли средство запутывания исключить тип или член из операции запутывания.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если тип или член, к которому применяется этот атрибут должен быть исключен из операции запутывания; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="true" />.
    /// </returns>
    public bool Exclude
    {
      get
      {
        return this.m_exclude;
      }
      set
      {
        this.m_exclude = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Boolean" /> значение, указывающее, является ли атрибут типа для применения к членам типа.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если атрибут предназначен для применения к членам типа; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="true" />.
    /// </returns>
    public bool ApplyToMembers
    {
      get
      {
        return this.m_applyToMembers;
      }
      set
      {
        this.m_applyToMembers = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строковое значение, которое распознается средством запутывания и определяет параметры обработки.
    /// </summary>
    /// <returns>
    ///   Строковое значение, которое распознается средством запутывания и определяет параметры обработки.
    ///    Значение по умолчанию — «все».
    /// </returns>
    public string Feature
    {
      get
      {
        return this.m_feature;
      }
      set
      {
        this.m_feature = value;
      }
    }
  }
}
