// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.SoapTypeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
  /// <summary>
  ///   Настраивает генерирование SOAP и обработку для типов целевых объектов.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
  [ComVisible(true)]
  public sealed class SoapTypeAttribute : SoapAttribute
  {
    private SoapTypeAttribute.ExplicitlySet _explicitlySet;
    private SoapOption _SoapOptions;
    private string _XmlElementName;
    private string _XmlTypeName;
    private string _XmlTypeNamespace;
    private XmlFieldOrderOption _XmlFieldOrder;

    internal bool IsInteropXmlElement()
    {
      return (uint) (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlElementName | SoapTypeAttribute.ExplicitlySet.XmlNamespace)) > 0U;
    }

    internal bool IsInteropXmlType()
    {
      return (uint) (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlTypeName | SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace)) > 0U;
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Runtime.Remoting.Metadata.SoapOption" /> значение конфигурации.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Runtime.Remoting.Metadata.SoapOption" /> значение конфигурации.
    /// </returns>
    public SoapOption SoapOptions
    {
      get
      {
        return this._SoapOptions;
      }
      set
      {
        this._SoapOptions = value;
      }
    }

    /// <summary>Возвращает или задает имя элемента XML.</summary>
    /// <returns>Имя элемента XML.</returns>
    public string XmlElementName
    {
      get
      {
        if (this._XmlElementName == null && this.ReflectInfo != null)
          this._XmlElementName = SoapTypeAttribute.GetTypeName((Type) this.ReflectInfo);
        return this._XmlElementName;
      }
      set
      {
        this._XmlElementName = value;
        this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlElementName;
      }
    }

    /// <summary>
    ///   Возвращает или задает пространство имен XML, который используется во время сериализации типа целевого объекта.
    /// </summary>
    /// <returns>
    ///   Пространство имен XML, который используется во время сериализации типа целевого объекта.
    /// </returns>
    public override string XmlNamespace
    {
      get
      {
        if (this.ProtXmlNamespace == null && this.ReflectInfo != null)
          this.ProtXmlNamespace = this.XmlTypeNamespace;
        return this.ProtXmlNamespace;
      }
      set
      {
        this.ProtXmlNamespace = value;
        this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlNamespace;
      }
    }

    /// <summary>
    ///   Возвращает или задает имя типа XML для типа целевого объекта.
    /// </summary>
    /// <returns>Имя типа XML для типа целевого объекта.</returns>
    public string XmlTypeName
    {
      get
      {
        if (this._XmlTypeName == null && this.ReflectInfo != null)
          this._XmlTypeName = SoapTypeAttribute.GetTypeName((Type) this.ReflectInfo);
        return this._XmlTypeName;
      }
      set
      {
        this._XmlTypeName = value;
        this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlTypeName;
      }
    }

    /// <summary>
    ///   Возвращает или задает пространство имен типов XML для текущего типа объекта.
    /// </summary>
    /// <returns>
    ///   Пространство имен типов XML для текущего типа объекта.
    /// </returns>
    public string XmlTypeNamespace
    {
      [SecuritySafeCritical] get
      {
        if (this._XmlTypeNamespace == null && this.ReflectInfo != null)
          this._XmlTypeNamespace = XmlNamespaceEncoder.GetXmlNamespaceForTypeNamespace((RuntimeType) this.ReflectInfo, (string) null);
        return this._XmlTypeNamespace;
      }
      set
      {
        this._XmlTypeNamespace = value;
        this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace;
      }
    }

    /// <summary>
    ///   Возвращает или задает порядок полей XML для типа целевого объекта.
    /// </summary>
    /// <returns>Порядок полей XML для типа целевого объекта.</returns>
    public XmlFieldOrderOption XmlFieldOrder
    {
      get
      {
        return this._XmlFieldOrder;
      }
      set
      {
        this._XmlFieldOrder = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, будет ли целевой объект текущего атрибута сериализован как атрибут XML вместо поля XML.
    /// </summary>
    /// <returns>
    ///   Текущая реализация всегда возвращает <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Предпринята попытка задать текущее свойство.
    /// </exception>
    public override bool UseAttribute
    {
      get
      {
        return false;
      }
      set
      {
        throw new RemotingException(Environment.GetResourceString("Remoting_Attribute_UseAttributeNotsettable"));
      }
    }

    private static string GetTypeName(Type t)
    {
      if (!t.IsNested)
        return t.Name;
      string fullName = t.FullName;
      string str = t.Namespace;
      if (str == null || str.Length == 0)
        return fullName;
      return fullName.Substring(str.Length + 1);
    }

    [Flags]
    [Serializable]
    private enum ExplicitlySet
    {
      None = 0,
      XmlElementName = 1,
      XmlNamespace = 2,
      XmlTypeName = 4,
      XmlTypeNamespace = 8,
    }
  }
}
