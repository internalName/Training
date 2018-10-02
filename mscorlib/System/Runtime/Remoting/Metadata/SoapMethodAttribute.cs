// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.SoapMethodAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
  /// <summary>
  ///   Настраивает генерирование SOAP и обработку для метода.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  [ComVisible(true)]
  public sealed class SoapMethodAttribute : SoapAttribute
  {
    private string _SoapAction;
    private string _responseXmlElementName;
    private string _responseXmlNamespace;
    private string _returnXmlElementName;
    private bool _bSoapActionExplicitySet;

    internal bool SoapActionExplicitySet
    {
      get
      {
        return this._bSoapActionExplicitySet;
      }
    }

    /// <summary>
    ///   Возвращает или задает поле заголовка SOAPAction, используемое с запросами HTTP, отправляемыми с помощью этого метода.
    ///    Это свойство в настоящее время не реализовано.
    /// </summary>
    /// <returns>
    ///   Поле заголовка SOAPAction, используемое с запросами HTTP, отправляемыми с помощью этого метода.
    /// </returns>
    public string SoapAction
    {
      [SecuritySafeCritical] get
      {
        if (this._SoapAction == null)
          this._SoapAction = this.XmlTypeNamespaceOfDeclaringType + "#" + ((MemberInfo) this.ReflectInfo).Name;
        return this._SoapAction;
      }
      set
      {
        this._SoapAction = value;
        this._bSoapActionExplicitySet = true;
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

    /// <summary>
    ///   Возвращает или задает пространство имен XML, используемое при сериализации удаленных вызовов целевого метода.
    /// </summary>
    /// <returns>
    ///   Пространство имен XML, используемое при сериализации удаленных вызовов целевого метода.
    /// </returns>
    public override string XmlNamespace
    {
      [SecuritySafeCritical] get
      {
        if (this.ProtXmlNamespace == null)
          this.ProtXmlNamespace = this.XmlTypeNamespaceOfDeclaringType;
        return this.ProtXmlNamespace;
      }
      set
      {
        this.ProtXmlNamespace = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает имя элемента XML, используемого для ответа метода целевому методу.
    /// </summary>
    /// <returns>
    ///   Имя элемента XML, используемого для ответа метода целевому методу.
    /// </returns>
    public string ResponseXmlElementName
    {
      get
      {
        if (this._responseXmlElementName == null && this.ReflectInfo != null)
          this._responseXmlElementName = ((MemberInfo) this.ReflectInfo).Name + "Response";
        return this._responseXmlElementName;
      }
      set
      {
        this._responseXmlElementName = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает пространство имен элемента XML, используемого для ответа метода целевому методу.
    /// </summary>
    /// <returns>
    ///   Пространство имен элемента XML, используемого для ответа метода целевому методу.
    /// </returns>
    public string ResponseXmlNamespace
    {
      get
      {
        if (this._responseXmlNamespace == null)
          this._responseXmlNamespace = this.XmlNamespace;
        return this._responseXmlNamespace;
      }
      set
      {
        this._responseXmlNamespace = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает имя элемента XML, используемое для возвращаемого значения из целевого метода.
    /// </summary>
    /// <returns>
    ///   Имя элемента XML, используемое для возвращаемого значения из целевого метода.
    /// </returns>
    public string ReturnXmlElementName
    {
      get
      {
        if (this._returnXmlElementName == null)
          this._returnXmlElementName = "return";
        return this._returnXmlElementName;
      }
      set
      {
        this._returnXmlElementName = value;
      }
    }

    private string XmlTypeNamespaceOfDeclaringType
    {
      [SecurityCritical] get
      {
        if (this.ReflectInfo != null)
          return XmlNamespaceEncoder.GetXmlNamespaceForType((RuntimeType) ((MemberInfo) this.ReflectInfo).DeclaringType, (string) null);
        return (string) null;
      }
    }
  }
}
