// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.SoapServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Обеспечивает несколько методов для использования и публикации удаленных объектов в формате SOAP.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class SoapServices
  {
    private static Hashtable _interopXmlElementToType = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _interopTypeToXmlElement = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _interopXmlTypeToType = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _interopTypeToXmlType = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _xmlToFieldTypeMap = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _methodBaseToSoapAction = Hashtable.Synchronized(new Hashtable());
    private static Hashtable _soapActionToMethodBase = Hashtable.Synchronized(new Hashtable());
    internal static string startNS = "http://schemas.microsoft.com/clr/";
    internal static string assemblyNS = "http://schemas.microsoft.com/clr/assem/";
    internal static string namespaceNS = "http://schemas.microsoft.com/clr/ns/";
    internal static string fullNS = "http://schemas.microsoft.com/clr/nsassem/";

    private SoapServices()
    {
    }

    private static string CreateKey(string elementName, string elementNamespace)
    {
      if (elementNamespace == null)
        return elementName;
      return elementName + " " + elementNamespace;
    }

    /// <summary>
    ///   Связывает данное имя элемента XML и пространство имен с типом среды выполнения, который должен использоваться для десериализации.
    /// </summary>
    /// <param name="xmlElement">
    ///   Имя элемента XML, используемое при десериализации.
    /// </param>
    /// <param name="xmlNamespace">
    ///   Пространство имен XML, используемый при десериализации.
    /// </param>
    /// <param name="type">
    ///   Время выполнения <see cref="T:System.Type" /> используемый при десериализации.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static void RegisterInteropXmlElement(string xmlElement, string xmlNamespace, Type type)
    {
      SoapServices._interopXmlElementToType[(object) SoapServices.CreateKey(xmlElement, xmlNamespace)] = (object) type;
      SoapServices._interopTypeToXmlElement[(object) type] = (object) new SoapServices.XmlEntry(xmlElement, xmlNamespace);
    }

    /// <summary>
    ///   Связывает данное имя типа XML и пространство имен с типом во время выполнения, который следует использовать при десериализации.
    /// </summary>
    /// <param name="xmlType">
    ///   Тип XML, используемый при десериализации.
    /// </param>
    /// <param name="xmlTypeNamespace">
    ///   Пространство имен XML, используемый при десериализации.
    /// </param>
    /// <param name="type">
    ///   Время выполнения <see cref="T:System.Type" /> используемый при десериализации.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static void RegisterInteropXmlType(string xmlType, string xmlTypeNamespace, Type type)
    {
      SoapServices._interopXmlTypeToType[(object) SoapServices.CreateKey(xmlType, xmlTypeNamespace)] = (object) type;
      SoapServices._interopTypeToXmlType[(object) type] = (object) new SoapServices.XmlEntry(xmlType, xmlTypeNamespace);
    }

    /// <summary>
    ///   Предварительно загружает данного <see cref="T:System.Type" /> на основе значений в <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> на тип.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Для предварительной загрузки.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static void PreLoad(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if ((object) (type as RuntimeType) == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      foreach (MethodBase method in type.GetMethods())
        SoapServices.RegisterSoapActionForMethodBase(method);
      SoapTypeAttribute cachedSoapAttribute1 = (SoapTypeAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) type);
      if (cachedSoapAttribute1.IsInteropXmlElement())
        SoapServices.RegisterInteropXmlElement(cachedSoapAttribute1.XmlElementName, cachedSoapAttribute1.XmlNamespace, type);
      if (cachedSoapAttribute1.IsInteropXmlType())
        SoapServices.RegisterInteropXmlType(cachedSoapAttribute1.XmlTypeName, cachedSoapAttribute1.XmlTypeNamespace, type);
      int num = 0;
      SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = new SoapServices.XmlToFieldTypeMap();
      foreach (FieldInfo field in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
      {
        SoapFieldAttribute cachedSoapAttribute2 = (SoapFieldAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) field);
        if (cachedSoapAttribute2.IsInteropXmlElement())
        {
          string xmlElementName = cachedSoapAttribute2.XmlElementName;
          string xmlNamespace = cachedSoapAttribute2.XmlNamespace;
          if (cachedSoapAttribute2.UseAttribute)
            xmlToFieldTypeMap.AddXmlAttribute(field.FieldType, field.Name, xmlElementName, xmlNamespace);
          else
            xmlToFieldTypeMap.AddXmlElement(field.FieldType, field.Name, xmlElementName, xmlNamespace);
          ++num;
        }
      }
      if (num <= 0)
        return;
      SoapServices._xmlToFieldTypeMap[(object) type] = (object) xmlToFieldTypeMap;
    }

    /// <summary>
    ///   Предварительно загружает каждый <see cref="T:System.Type" /> в указанном <see cref="T:System.Reflection.Assembly" /> из сведений, указанных в <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> связанные с каждым типом.
    /// </summary>
    /// <param name="assembly">
    ///   <see cref="T:System.Reflection.Assembly" /> Для каждого типа, который вызывается <see cref="M:System.Runtime.Remoting.SoapServices.PreLoad(System.Type)" />.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static void PreLoad(Assembly assembly)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException(nameof (assembly));
      if (!(assembly is RuntimeAssembly))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), nameof (assembly));
      foreach (Type type in assembly.GetTypes())
        SoapServices.PreLoad(type);
    }

    /// <summary>
    ///   Извлекает <see cref="T:System.Type" /> следует использовать во время десериализации нераспознанного типа объекта с данным именем элемента XML и пространством имен.
    /// </summary>
    /// <param name="xmlElement">
    ///   Имя элемента XML неизвестного типа объекта.
    /// </param>
    /// <param name="xmlNamespace">
    ///   Пространство имен XML неизвестного типа объекта.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Type" /> Объекта, связанного с указанным именем элемента XML и пространством имен.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static Type GetInteropTypeFromXmlElement(string xmlElement, string xmlNamespace)
    {
      return (Type) SoapServices._interopXmlElementToType[(object) SoapServices.CreateKey(xmlElement, xmlNamespace)];
    }

    /// <summary>
    ///   Получает объект <see cref="T:System.Type" /> следует использовать во время десериализации нераспознанного типа объекта с данным именем типа XML и пространством имен.
    /// </summary>
    /// <param name="xmlType">Тип XML неизвестного типа объекта.</param>
    /// <param name="xmlTypeNamespace">
    ///   Пространство имен типа XML из неизвестного типа объекта.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Type" /> Объекта, связанного с указанным кодом XML, введите имя и пространство имен.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static Type GetInteropTypeFromXmlType(string xmlType, string xmlTypeNamespace)
    {
      return (Type) SoapServices._interopXmlTypeToType[(object) SoapServices.CreateKey(xmlType, xmlTypeNamespace)];
    }

    /// <summary>
    ///   Получает <see cref="T:System.Type" /> и имя поля из предоставленного имени элемента XML, пространства имен и содержащего типа.
    /// </summary>
    /// <param name="containingType">
    ///   <see cref="T:System.Type" /> Объект, содержащий поле.
    /// </param>
    /// <param name="xmlElement">Имя элемента XML поля.</param>
    /// <param name="xmlNamespace">
    ///   Пространство имен XML типа поля.
    /// </param>
    /// <param name="type">
    ///   При возвращении данного метода содержит <see cref="T:System.Type" /> поля.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="name">
    ///   При возвращении данного метода содержит <see cref="T:System.String" /> содержащий имя поля.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static void GetInteropFieldTypeAndNameFromXmlElement(Type containingType, string xmlElement, string xmlNamespace, out Type type, out string name)
    {
      if (containingType == (Type) null)
      {
        type = (Type) null;
        name = (string) null;
      }
      else
      {
        SoapServices.XmlToFieldTypeMap xmlToFieldType = (SoapServices.XmlToFieldTypeMap) SoapServices._xmlToFieldTypeMap[(object) containingType];
        if (xmlToFieldType != null)
        {
          xmlToFieldType.GetFieldTypeAndNameFromXmlElement(xmlElement, xmlNamespace, out type, out name);
        }
        else
        {
          type = (Type) null;
          name = (string) null;
        }
      }
    }

    /// <summary>
    ///   Извлекает тип поля из XML имени атрибута, пространство имен и <see cref="T:System.Type" /> вмещающего объекта.
    /// </summary>
    /// <param name="containingType">
    ///   <see cref="T:System.Type" /> Объект, содержащий поле.
    /// </param>
    /// <param name="xmlAttribute">Имя XML-атрибута типа поля.</param>
    /// <param name="xmlNamespace">
    ///   Пространство имен XML типа поля.
    /// </param>
    /// <param name="type">
    ///   При возвращении данного метода содержит <see cref="T:System.Type" /> поля.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="name">
    ///   При возвращении данного метода содержит <see cref="T:System.String" /> содержащий имя поля.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static void GetInteropFieldTypeAndNameFromXmlAttribute(Type containingType, string xmlAttribute, string xmlNamespace, out Type type, out string name)
    {
      if (containingType == (Type) null)
      {
        type = (Type) null;
        name = (string) null;
      }
      else
      {
        SoapServices.XmlToFieldTypeMap xmlToFieldType = (SoapServices.XmlToFieldTypeMap) SoapServices._xmlToFieldTypeMap[(object) containingType];
        if (xmlToFieldType != null)
        {
          xmlToFieldType.GetFieldTypeAndNameFromXmlAttribute(xmlAttribute, xmlNamespace, out type, out name);
        }
        else
        {
          type = (Type) null;
          name = (string) null;
        }
      }
    }

    /// <summary>
    ///   Возвращает сведения об элементе XML, который должен использоваться при сериализации заданного типа.
    /// </summary>
    /// <param name="type">
    ///   Объект <see cref="T:System.Type" /> для которого запрашиваются имена XML-элемент и пространство имен.
    /// </param>
    /// <param name="xmlElement">
    ///   При возвращении данного метода содержит <see cref="T:System.String" /> содержащий имя элемента XML для указанного типа объекта.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="xmlNamespace">
    ///   При возвращении данного метода содержит <see cref="T:System.String" /> содержащий имя пространства имен XML для указанного типа объекта.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если запрошенные значения были заданы отмечены <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static bool GetXmlElementForInteropType(Type type, out string xmlElement, out string xmlNamespace)
    {
      SoapServices.XmlEntry xmlEntry = (SoapServices.XmlEntry) SoapServices._interopTypeToXmlElement[(object) type];
      if (xmlEntry != null)
      {
        xmlElement = xmlEntry.Name;
        xmlNamespace = xmlEntry.Namespace;
        return true;
      }
      SoapTypeAttribute cachedSoapAttribute = (SoapTypeAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) type);
      if (cachedSoapAttribute.IsInteropXmlElement())
      {
        xmlElement = cachedSoapAttribute.XmlElementName;
        xmlNamespace = cachedSoapAttribute.XmlNamespace;
        return true;
      }
      xmlElement = (string) null;
      xmlNamespace = (string) null;
      return false;
    }

    /// <summary>
    ///   Возвращает сведения о типе XML, который должен использоваться при сериализации данного <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="type">
    ///   Объект <see cref="T:System.Type" /> для которого запрашиваются имена XML-элемент и пространство имен.
    /// </param>
    /// <param name="xmlType">
    ///   Тип данных XML указанного объекта <see cref="T:System.Type" />.
    /// </param>
    /// <param name="xmlTypeNamespace">
    ///   Пространство имен типа XML указанного объекта <see cref="T:System.Type" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если запрошенные значения были заданы отмечены <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static bool GetXmlTypeForInteropType(Type type, out string xmlType, out string xmlTypeNamespace)
    {
      SoapServices.XmlEntry xmlEntry = (SoapServices.XmlEntry) SoapServices._interopTypeToXmlType[(object) type];
      if (xmlEntry != null)
      {
        xmlType = xmlEntry.Name;
        xmlTypeNamespace = xmlEntry.Namespace;
        return true;
      }
      SoapTypeAttribute cachedSoapAttribute = (SoapTypeAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) type);
      if (cachedSoapAttribute.IsInteropXmlType())
      {
        xmlType = cachedSoapAttribute.XmlTypeName;
        xmlTypeNamespace = cachedSoapAttribute.XmlTypeNamespace;
        return true;
      }
      xmlType = (string) null;
      xmlTypeNamespace = (string) null;
      return false;
    }

    /// <summary>
    ///   Получает пространство имен XML, используемое во время удаленных вызовов метода, указанного в данной <see cref="T:System.Reflection.MethodBase" />.
    /// </summary>
    /// <param name="mb">
    ///   <see cref="T:System.Reflection.MethodBase" /> Метода, для которого был запрошен пространство имен XML.
    /// </param>
    /// <returns>
    ///   Пространство имен XML, используемое во время удаленных вызовов указанного метода.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static string GetXmlNamespaceForMethodCall(MethodBase mb)
    {
      return InternalRemotingServices.GetCachedSoapAttribute((object) mb).XmlNamespace;
    }

    /// <summary>
    ///   Получает пространство имен XML, используемое во время создания ответов на удаленный вызов метода, указанного в данной <see cref="T:System.Reflection.MethodBase" />.
    /// </summary>
    /// <param name="mb">
    ///   <see cref="T:System.Reflection.MethodBase" /> Метода, для которого был запрошен пространство имен XML.
    /// </param>
    /// <returns>
    ///   Пространство имен XML, используемое во время создания ответов на удаленный вызов метода.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static string GetXmlNamespaceForMethodResponse(MethodBase mb)
    {
      return ((SoapMethodAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) mb)).ResponseXmlNamespace;
    }

    /// <summary>
    ///   Связывает указанный <see cref="T:System.Reflection.MethodBase" /> с кэшированным в нем SOAPAction.
    /// </summary>
    /// <param name="mb">
    ///   <see cref="T:System.Reflection.MethodBase" /> Метода, связываемый с кэшированным в нем SOAPAction.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static void RegisterSoapActionForMethodBase(MethodBase mb)
    {
      SoapMethodAttribute cachedSoapAttribute = (SoapMethodAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) mb);
      if (!cachedSoapAttribute.SoapActionExplicitySet)
        return;
      SoapServices.RegisterSoapActionForMethodBase(mb, cachedSoapAttribute.SoapAction);
    }

    /// <summary>
    ///   Связывает предоставленное значение SOAPAction, с помощью заданного <see cref="T:System.Reflection.MethodBase" /> для использования в приемники каналов.
    /// </summary>
    /// <param name="mb">
    ///   <see cref="T:System.Reflection.MethodBase" /> Для связи с предоставленным SOAPAction.
    /// </param>
    /// <param name="soapAction">
    ///   Значение SOAPAction, связанное с данной <see cref="T:System.Reflection.MethodBase" />.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static void RegisterSoapActionForMethodBase(MethodBase mb, string soapAction)
    {
      if (soapAction == null)
        return;
      SoapServices._methodBaseToSoapAction[(object) mb] = (object) soapAction;
      ArrayList arrayList = (ArrayList) SoapServices._soapActionToMethodBase[(object) soapAction];
      if (arrayList == null)
      {
        lock (SoapServices._soapActionToMethodBase)
        {
          arrayList = ArrayList.Synchronized(new ArrayList());
          SoapServices._soapActionToMethodBase[(object) soapAction] = (object) arrayList;
        }
      }
      arrayList.Add((object) mb);
    }

    /// <summary>
    ///   Возвращает значение SOAPAction, связанное с методом, указанным в данной <see cref="T:System.Reflection.MethodBase" />.
    /// </summary>
    /// <param name="mb">
    ///   <see cref="T:System.Reflection.MethodBase" /> Содержащий метод, для которого запрашивается SOAPAction.
    /// </param>
    /// <returns>
    ///   Значение SOAPAction, связанное с методом, указанным в данной <see cref="T:System.Reflection.MethodBase" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static string GetSoapActionFromMethodBase(MethodBase mb)
    {
      return (string) SoapServices._methodBaseToSoapAction[(object) mb] ?? ((SoapMethodAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) mb)).SoapAction;
    }

    /// <summary>
    ///   Определяет, является ли указанный SOAPAction приемлем для данного <see cref="T:System.Reflection.MethodBase" />.
    /// </summary>
    /// <param name="soapAction">
    ///   SOAPAction для проверки данного <see cref="T:System.Reflection.MethodBase" />.
    /// </param>
    /// <param name="mb">
    ///   <see cref="T:System.Reflection.MethodBase" /> Указанный SOAPAction проверяется.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный SOAPAction приемлем для данного <see cref="T:System.Reflection.MethodBase" />; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static bool IsSoapActionValidForMethodBase(string soapAction, MethodBase mb)
    {
      if (mb == (MethodBase) null)
        throw new ArgumentNullException(nameof (mb));
      if (soapAction[0] == '"' && soapAction[soapAction.Length - 1] == '"')
        soapAction = soapAction.Substring(1, soapAction.Length - 2);
      if (string.CompareOrdinal(((SoapMethodAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) mb)).SoapAction, soapAction) == 0)
        return true;
      string strA = (string) SoapServices._methodBaseToSoapAction[(object) mb];
      if (strA != null && string.CompareOrdinal(strA, soapAction) == 0)
        return true;
      string[] strArray = soapAction.Split('#');
      if (strArray.Length != 2)
        return false;
      bool assemblyIncluded;
      string soapActionNamespace = XmlNamespaceEncoder.GetTypeNameForSoapActionNamespace(strArray[0], out assemblyIncluded);
      if (soapActionNamespace == null)
        return false;
      string str1 = strArray[1];
      RuntimeMethodInfo runtimeMethodInfo = mb as RuntimeMethodInfo;
      RuntimeConstructorInfo runtimeConstructorInfo = mb as RuntimeConstructorInfo;
      RuntimeModule runtimeModule;
      if ((MethodInfo) runtimeMethodInfo != (MethodInfo) null)
      {
        runtimeModule = runtimeMethodInfo.GetRuntimeModule();
      }
      else
      {
        if (!((ConstructorInfo) runtimeConstructorInfo != (ConstructorInfo) null))
          throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
        runtimeModule = runtimeConstructorInfo.GetRuntimeModule();
      }
      string str2 = mb.DeclaringType.FullName;
      if (assemblyIncluded)
        str2 = str2 + ", " + runtimeModule.GetRuntimeAssembly().GetSimpleName();
      if (str2.Equals(soapActionNamespace))
        return mb.Name.Equals(str1);
      return false;
    }

    /// <summary>
    ///   Определяет тип и имя метода, связанного с указанным значением SOAPAction.
    /// </summary>
    /// <param name="soapAction">
    ///   SOAPAction метода, для которого запрашиваются имена типов и методов.
    /// </param>
    /// <param name="typeName">
    ///   При возвращении данного метода содержит <see cref="T:System.String" /> содержащий имя типа метода в вопросе.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="methodName">
    ///   При возвращении данного метода содержит <see cref="T:System.String" /> содержащий имя метода в вопросе.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если тип и имя метода восстановлены успешно; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Значение SOAPAction не начинаться и заканчиваться кавычками.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static bool GetTypeAndMethodNameFromSoapAction(string soapAction, out string typeName, out string methodName)
    {
      if (soapAction[0] == '"' && soapAction[soapAction.Length - 1] == '"')
        soapAction = soapAction.Substring(1, soapAction.Length - 2);
      ArrayList arrayList = (ArrayList) SoapServices._soapActionToMethodBase[(object) soapAction];
      if (arrayList != null)
      {
        if (arrayList.Count > 1)
        {
          typeName = (string) null;
          methodName = (string) null;
          return false;
        }
        MethodBase methodBase = (MethodBase) arrayList[0];
        if (methodBase != (MethodBase) null)
        {
          RuntimeMethodInfo runtimeMethodInfo = methodBase as RuntimeMethodInfo;
          RuntimeConstructorInfo runtimeConstructorInfo = methodBase as RuntimeConstructorInfo;
          RuntimeModule runtimeModule;
          if ((MethodInfo) runtimeMethodInfo != (MethodInfo) null)
          {
            runtimeModule = runtimeMethodInfo.GetRuntimeModule();
          }
          else
          {
            if (!((ConstructorInfo) runtimeConstructorInfo != (ConstructorInfo) null))
              throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
            runtimeModule = runtimeConstructorInfo.GetRuntimeModule();
          }
          typeName = methodBase.DeclaringType.FullName + ", " + runtimeModule.GetRuntimeAssembly().GetSimpleName();
          methodName = methodBase.Name;
          return true;
        }
      }
      string[] strArray = soapAction.Split('#');
      if (strArray.Length == 2)
      {
        bool assemblyIncluded;
        typeName = XmlNamespaceEncoder.GetTypeNameForSoapActionNamespace(strArray[0], out assemblyIncluded);
        if (typeName == null)
        {
          methodName = (string) null;
          return false;
        }
        methodName = strArray[1];
        return true;
      }
      typeName = (string) null;
      methodName = (string) null;
      return false;
    }

    /// <summary>
    ///   Возвращает префикс пространства имен XML для типами среды CLR.
    /// </summary>
    /// <returns>Префикс пространства имен XML для типами среды CLR.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static string XmlNsForClrType
    {
      get
      {
        return SoapServices.startNS;
      }
    }

    /// <summary>
    ///   Возвращает префикс пространства имен XML по умолчанию, который должен использоваться для XML-кодировки класса среды выполнения, имеющего сборку, но не в собственном пространстве имен.
    /// </summary>
    /// <returns>
    ///   Префикс пространства имен XML по умолчанию, который должен использоваться для XML-кодировки класса среды выполнения, имеющего сборку, но нет собственного пространства имен.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static string XmlNsForClrTypeWithAssembly
    {
      get
      {
        return SoapServices.assemblyNS;
      }
    }

    /// <summary>
    ///   Возвращает префикс пространства имен XML, который должен использоваться для XML-кодировки класса среды выполнения, являющегося частью файла mscorlib.dll.
    /// </summary>
    /// <returns>
    ///   Префикс пространства имен XML, который должен использоваться для XML-кодировки класса среды выполнения, являющегося частью файла mscorlib.dll.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static string XmlNsForClrTypeWithNs
    {
      get
      {
        return SoapServices.namespaceNS;
      }
    }

    /// <summary>
    ///   Возвращает префикс пространства имен XML по умолчанию, который должен использоваться для XML-кодировки класса среды выполнения, имеющего сборку и пространство имен среды выполнения.
    /// </summary>
    /// <returns>
    ///   Префикс пространства имен XML по умолчанию, который должен использоваться для XML-кодировки класса среды выполнения, имеющего сборку и пространство имен среды выполнения.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static string XmlNsForClrTypeWithNsAndAssembly
    {
      get
      {
        return SoapServices.fullNS;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, можно ли встроенный общеязыковая среда выполнения указанного пространства имен.
    /// </summary>
    /// <param name="namespaceString">
    ///   Пространство имен для возврата среды CLR.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если пространства имен собственного для среды CLR; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static bool IsClrTypeNamespace(string namespaceString)
    {
      return namespaceString.StartsWith(SoapServices.startNS, StringComparison.Ordinal);
    }

    /// <summary>
    ///   Возвращает имя пространства имен типа общий язык среды выполнения из предоставленных имен сборки и пространства имен.
    /// </summary>
    /// <param name="typeNamespace">Пространство имен, Кодируемое.</param>
    /// <param name="assemblyName">Имя сборки, Кодируемое.</param>
    /// <returns>
    ///   Общий язык среды выполнения тип пространство имен из предоставленных имен сборки и пространства имен.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="assemblyName" /> И <paramref name="typeNamespace" /> параметры находятся либо <see langword="null" /> или пустым.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static string CodeXmlNamespaceForClrTypeNamespace(string typeNamespace, string assemblyName)
    {
      StringBuilder sb = new StringBuilder(256);
      if (SoapServices.IsNameNull(typeNamespace))
      {
        if (SoapServices.IsNameNull(assemblyName))
          throw new ArgumentNullException("typeNamespace,assemblyName");
        sb.Append(SoapServices.assemblyNS);
        SoapServices.UriEncode(assemblyName, sb);
      }
      else if (SoapServices.IsNameNull(assemblyName))
      {
        sb.Append(SoapServices.namespaceNS);
        sb.Append(typeNamespace);
      }
      else
      {
        sb.Append(SoapServices.fullNS);
        if (typeNamespace[0] == '.')
          sb.Append(typeNamespace.Substring(1));
        else
          sb.Append(typeNamespace);
        sb.Append('/');
        SoapServices.UriEncode(assemblyName, sb);
      }
      return sb.ToString();
    }

    /// <summary>
    ///   Декодирует имена пространства имен и сборки XML из предоставленных имен среды CLR.
    /// </summary>
    /// <param name="inNamespace">Имен среды CLR.</param>
    /// <param name="typeNamespace">
    ///   При возвращении данного метода содержит <see cref="T:System.String" /> содержащий декодированную пространство имен.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="assemblyName">
    ///   При возвращении данного метода содержит <see cref="T:System.String" /> содержащий указано декодированное имя сборки.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если имена пространства имен и сборки были успешно декодированные; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="inNamespace" /> Параметр <see langword="null" /> или пустым.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static bool DecodeXmlNamespaceForClrTypeNamespace(string inNamespace, out string typeNamespace, out string assemblyName)
    {
      if (SoapServices.IsNameNull(inNamespace))
        throw new ArgumentNullException(nameof (inNamespace));
      assemblyName = (string) null;
      typeNamespace = "";
      if (inNamespace.StartsWith(SoapServices.assemblyNS, StringComparison.Ordinal))
        assemblyName = SoapServices.UriDecode(inNamespace.Substring(SoapServices.assemblyNS.Length));
      else if (inNamespace.StartsWith(SoapServices.namespaceNS, StringComparison.Ordinal))
      {
        typeNamespace = inNamespace.Substring(SoapServices.namespaceNS.Length);
      }
      else
      {
        if (!inNamespace.StartsWith(SoapServices.fullNS, StringComparison.Ordinal))
          return false;
        int num = inNamespace.IndexOf("/", SoapServices.fullNS.Length);
        typeNamespace = inNamespace.Substring(SoapServices.fullNS.Length, num - SoapServices.fullNS.Length);
        assemblyName = SoapServices.UriDecode(inNamespace.Substring(num + 1));
      }
      return true;
    }

    internal static void UriEncode(string value, StringBuilder sb)
    {
      if (value == null || value.Length == 0)
        return;
      for (int index = 0; index < value.Length; ++index)
      {
        if (value[index] == ' ')
          sb.Append("%20");
        else if (value[index] == '=')
          sb.Append("%3D");
        else if (value[index] == ',')
          sb.Append("%2C");
        else
          sb.Append(value[index]);
      }
    }

    internal static string UriDecode(string value)
    {
      if (value == null || value.Length == 0)
        return value;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
      {
        if (value[index] == '%' && value.Length - index >= 3)
        {
          if (value[index + 1] == '2' && value[index + 2] == '0')
          {
            stringBuilder.Append(' ');
            index += 2;
          }
          else if (value[index + 1] == '3' && value[index + 2] == 'D')
          {
            stringBuilder.Append('=');
            index += 2;
          }
          else if (value[index + 1] == '2' && value[index + 2] == 'C')
          {
            stringBuilder.Append(',');
            index += 2;
          }
          else
            stringBuilder.Append(value[index]);
        }
        else
          stringBuilder.Append(value[index]);
      }
      return stringBuilder.ToString();
    }

    private static bool IsNameNull(string name)
    {
      return name == null || name.Length == 0;
    }

    private class XmlEntry
    {
      public string Name;
      public string Namespace;

      public XmlEntry(string name, string xmlNamespace)
      {
        this.Name = name;
        this.Namespace = xmlNamespace;
      }
    }

    private class XmlToFieldTypeMap
    {
      private Hashtable _attributes = new Hashtable();
      private Hashtable _elements = new Hashtable();

      [SecurityCritical]
      public void AddXmlElement(Type fieldType, string fieldName, string xmlElement, string xmlNamespace)
      {
        this._elements[(object) SoapServices.CreateKey(xmlElement, xmlNamespace)] = (object) new SoapServices.XmlToFieldTypeMap.FieldEntry(fieldType, fieldName);
      }

      [SecurityCritical]
      public void AddXmlAttribute(Type fieldType, string fieldName, string xmlAttribute, string xmlNamespace)
      {
        this._attributes[(object) SoapServices.CreateKey(xmlAttribute, xmlNamespace)] = (object) new SoapServices.XmlToFieldTypeMap.FieldEntry(fieldType, fieldName);
      }

      [SecurityCritical]
      public void GetFieldTypeAndNameFromXmlElement(string xmlElement, string xmlNamespace, out Type type, out string name)
      {
        SoapServices.XmlToFieldTypeMap.FieldEntry element = (SoapServices.XmlToFieldTypeMap.FieldEntry) this._elements[(object) SoapServices.CreateKey(xmlElement, xmlNamespace)];
        if (element != null)
        {
          type = element.Type;
          name = element.Name;
        }
        else
        {
          type = (Type) null;
          name = (string) null;
        }
      }

      [SecurityCritical]
      public void GetFieldTypeAndNameFromXmlAttribute(string xmlAttribute, string xmlNamespace, out Type type, out string name)
      {
        SoapServices.XmlToFieldTypeMap.FieldEntry attribute = (SoapServices.XmlToFieldTypeMap.FieldEntry) this._attributes[(object) SoapServices.CreateKey(xmlAttribute, xmlNamespace)];
        if (attribute != null)
        {
          type = attribute.Type;
          name = attribute.Name;
        }
        else
        {
          type = (Type) null;
          name = (string) null;
        }
      }

      private class FieldEntry
      {
        public Type Type;
        public string Name;

        public FieldEntry(Type type, string name)
        {
          this.Type = type;
          this.Name = name;
        }
      }
    }
  }
}
