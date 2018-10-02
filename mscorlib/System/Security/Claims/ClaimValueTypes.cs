// Decompiled with JetBrains decompiler
// Type: System.Security.Claims.ClaimValueTypes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Claims
{
  /// <summary>
  ///   Определяет типы значений утверждения согласно URI-кодам, определенным W3C и OASIS.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(false)]
  public static class ClaimValueTypes
  {
    private const string XmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";
    /// <summary>
    ///   URI, который представляет <see langword="base64Binary" /> типа данных XML.
    /// </summary>
    public const string Base64Binary = "http://www.w3.org/2001/XMLSchema#base64Binary";
    /// <summary>
    ///   URI, представляющий <see langword="base64Octet" /> типа данных XML.
    /// </summary>
    public const string Base64Octet = "http://www.w3.org/2001/XMLSchema#base64Octet";
    /// <summary>
    ///   URI, который представляет <see langword="boolean" /> типа данных XML.
    /// </summary>
    public const string Boolean = "http://www.w3.org/2001/XMLSchema#boolean";
    /// <summary>
    ///   URI, который представляет <see langword="date" /> типа данных XML.
    /// </summary>
    public const string Date = "http://www.w3.org/2001/XMLSchema#date";
    /// <summary>
    ///   URI, который представляет <see langword="dateTime" /> типа данных XML.
    /// </summary>
    public const string DateTime = "http://www.w3.org/2001/XMLSchema#dateTime";
    /// <summary>
    ///   URI, который представляет <see langword="double" /> типа данных XML.
    /// </summary>
    public const string Double = "http://www.w3.org/2001/XMLSchema#double";
    /// <summary>
    ///   URI, который представляет <see langword="fqbn" /> типа данных XML.
    /// </summary>
    public const string Fqbn = "http://www.w3.org/2001/XMLSchema#fqbn";
    /// <summary>
    ///   URI, который представляет <see langword="hexBinary" /> типа данных XML.
    /// </summary>
    public const string HexBinary = "http://www.w3.org/2001/XMLSchema#hexBinary";
    /// <summary>
    ///   URI, который представляет <see langword="integer" /> типа данных XML.
    /// </summary>
    public const string Integer = "http://www.w3.org/2001/XMLSchema#integer";
    /// <summary>
    ///   URI, который представляет <see langword="integer32" /> типа данных XML.
    /// </summary>
    public const string Integer32 = "http://www.w3.org/2001/XMLSchema#integer32";
    /// <summary>
    ///   URI, который представляет <see langword="integer64" /> типа данных XML.
    /// </summary>
    public const string Integer64 = "http://www.w3.org/2001/XMLSchema#integer64";
    /// <summary>
    ///   URI, который представляет <see langword="sid" /> типа данных XML.
    /// </summary>
    public const string Sid = "http://www.w3.org/2001/XMLSchema#sid";
    /// <summary>
    ///   URI, который представляет <see langword="string" /> типа данных XML.
    /// </summary>
    public const string String = "http://www.w3.org/2001/XMLSchema#string";
    /// <summary>
    ///   URI, который представляет <see langword="time" /> типа данных XML.
    /// </summary>
    public const string Time = "http://www.w3.org/2001/XMLSchema#time";
    /// <summary>
    ///   URI, который представляет <see langword="uinteger32" /> типа данных XML.
    /// </summary>
    public const string UInteger32 = "http://www.w3.org/2001/XMLSchema#uinteger32";
    /// <summary>
    ///   URI, который представляет <see langword="uinteger64" /> типа данных XML.
    /// </summary>
    public const string UInteger64 = "http://www.w3.org/2001/XMLSchema#uinteger64";
    private const string SoapSchemaNamespace = "http://schemas.xmlsoap.org/";
    /// <summary>
    ///   URI, который представляет <see langword="dns" /> данные типа SOAP.
    /// </summary>
    public const string DnsName = "http://schemas.xmlsoap.org/claims/dns";
    /// <summary>
    ///   URI, который представляет <see langword="emailaddress" /> данные типа SOAP.
    /// </summary>
    public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    /// <summary>
    ///   URI, который представляет <see langword="rsa" /> данные типа SOAP.
    /// </summary>
    public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";
    /// <summary>
    ///   URI, который представляет <see langword="UPN" /> данные типа SOAP.
    /// </summary>
    public const string UpnName = "http://schemas.xmlsoap.org/claims/UPN";
    private const string XmlSignatureConstantsNamespace = "http://www.w3.org/2000/09/xmldsig#";
    /// <summary>
    ///   URI, который представляет <see langword="DSAKeyValue" /> тип данных XML-подписи.
    /// </summary>
    public const string DsaKeyValue = "http://www.w3.org/2000/09/xmldsig#DSAKeyValue";
    /// <summary>
    ///   URI, который представляет <see langword="KeyInfo" /> тип данных XML-подписи.
    /// </summary>
    public const string KeyInfo = "http://www.w3.org/2000/09/xmldsig#KeyInfo";
    /// <summary>
    ///   URI, который представляет <see langword="RSAKeyValue" /> тип данных XML-подписи.
    /// </summary>
    public const string RsaKeyValue = "http://www.w3.org/2000/09/xmldsig#RSAKeyValue";
    private const string XQueryOperatorsNameSpace = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816";
    /// <summary>
    ///   URI, который представляет <see langword="daytimeDuration" /> тип данных XQuery.
    /// </summary>
    public const string DaytimeDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#dayTimeDuration";
    /// <summary>
    ///   URI, который представляет <see langword="yearMonthDuration" /> тип данных XQuery.
    /// </summary>
    public const string YearMonthDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#yearMonthDuration";
    private const string Xacml10Namespace = "urn:oasis:names:tc:xacml:1.0";
    /// <summary>
    ///   URI, который представляет <see langword="rfc822Name" /> XACML 1.0 тип данных.
    /// </summary>
    public const string Rfc822Name = "urn:oasis:names:tc:xacml:1.0:data-type:rfc822Name";
    /// <summary>
    ///   URI, который представляет <see langword="x500Name" /> XACML 1.0 тип данных.
    /// </summary>
    public const string X500Name = "urn:oasis:names:tc:xacml:1.0:data-type:x500Name";
  }
}
