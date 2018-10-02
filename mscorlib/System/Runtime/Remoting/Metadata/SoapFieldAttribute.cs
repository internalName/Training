// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.SoapFieldAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
  /// <summary>
  ///   Настраивает генерирование SOAP и обработку для поля.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field)]
  [ComVisible(true)]
  public sealed class SoapFieldAttribute : SoapAttribute
  {
    private SoapFieldAttribute.ExplicitlySet _explicitlySet;
    private string _xmlElementName;
    private int _order;

    /// <summary>
    ///   Возвращает значение, указывающее, содержит ли текущий атрибут взаимодействующие значения элементов XML.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если текущий атрибут содержит взаимодействующие значения элементов XML; в противном случае — <see langword="false" />.
    /// </returns>
    public bool IsInteropXmlElement()
    {
      return (uint) (this._explicitlySet & SoapFieldAttribute.ExplicitlySet.XmlElementName) > 0U;
    }

    /// <summary>
    ///   Возвращает или задает имя элемента XML поля, содержащегося в <see cref="T:System.Runtime.Remoting.Metadata.SoapFieldAttribute" /> атрибута.
    /// </summary>
    /// <returns>
    ///   Имя элемента XML поля, содержащегося в этом атрибуте.
    /// </returns>
    public string XmlElementName
    {
      get
      {
        if (this._xmlElementName == null && this.ReflectInfo != null)
          this._xmlElementName = ((MemberInfo) this.ReflectInfo).Name;
        return this._xmlElementName;
      }
      set
      {
        this._xmlElementName = value;
        this._explicitlySet |= SoapFieldAttribute.ExplicitlySet.XmlElementName;
      }
    }

    /// <summary>Возвращает или задает порядок текущий атрибут поля.</summary>
    /// <returns>Порядковый номер текущего поля атрибута.</returns>
    public int Order
    {
      get
      {
        return this._order;
      }
      set
      {
        this._order = value;
      }
    }

    [Flags]
    [Serializable]
    private enum ExplicitlySet
    {
      None = 0,
      XmlElementName = 1,
    }
  }
}
