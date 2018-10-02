// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  internal static class SoapType
  {
    internal static Type typeofSoapTime = typeof (SoapTime);
    internal static Type typeofSoapDate = typeof (SoapDate);
    internal static Type typeofSoapYearMonth = typeof (SoapYearMonth);
    internal static Type typeofSoapYear = typeof (SoapYear);
    internal static Type typeofSoapMonthDay = typeof (SoapMonthDay);
    internal static Type typeofSoapDay = typeof (SoapDay);
    internal static Type typeofSoapMonth = typeof (SoapMonth);
    internal static Type typeofSoapHexBinary = typeof (SoapHexBinary);
    internal static Type typeofSoapBase64Binary = typeof (SoapBase64Binary);
    internal static Type typeofSoapInteger = typeof (SoapInteger);
    internal static Type typeofSoapPositiveInteger = typeof (SoapPositiveInteger);
    internal static Type typeofSoapNonPositiveInteger = typeof (SoapNonPositiveInteger);
    internal static Type typeofSoapNonNegativeInteger = typeof (SoapNonNegativeInteger);
    internal static Type typeofSoapNegativeInteger = typeof (SoapNegativeInteger);
    internal static Type typeofSoapAnyUri = typeof (SoapAnyUri);
    internal static Type typeofSoapQName = typeof (SoapQName);
    internal static Type typeofSoapNotation = typeof (SoapNotation);
    internal static Type typeofSoapNormalizedString = typeof (SoapNormalizedString);
    internal static Type typeofSoapToken = typeof (SoapToken);
    internal static Type typeofSoapLanguage = typeof (SoapLanguage);
    internal static Type typeofSoapName = typeof (SoapName);
    internal static Type typeofSoapIdrefs = typeof (SoapIdrefs);
    internal static Type typeofSoapEntities = typeof (SoapEntities);
    internal static Type typeofSoapNmtoken = typeof (SoapNmtoken);
    internal static Type typeofSoapNmtokens = typeof (SoapNmtokens);
    internal static Type typeofSoapNcName = typeof (SoapNcName);
    internal static Type typeofSoapId = typeof (SoapId);
    internal static Type typeofSoapIdref = typeof (SoapIdref);
    internal static Type typeofSoapEntity = typeof (SoapEntity);
    internal static Type typeofISoapXsd = typeof (ISoapXsd);

    internal static string FilterBin64(string value)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
      {
        if (value[index] != ' ' && value[index] != '\n' && value[index] != '\r')
          stringBuilder.Append(value[index]);
      }
      return stringBuilder.ToString();
    }

    internal static string LineFeedsBin64(string value)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < value.Length; ++index)
      {
        if (index % 76 == 0)
          stringBuilder.Append('\n');
        stringBuilder.Append(value[index]);
      }
      return stringBuilder.ToString();
    }

    internal static string Escape(string value)
    {
      if (value == null || value.Length == 0)
        return value;
      StringBuilder stringBuilder = new StringBuilder();
      int startIndex1 = value.IndexOf('&');
      if (startIndex1 > -1)
      {
        stringBuilder.Append(value);
        stringBuilder.Replace("&", "&#38;", startIndex1, stringBuilder.Length - startIndex1);
      }
      int startIndex2 = value.IndexOf('"');
      if (startIndex2 > -1)
      {
        if (stringBuilder.Length == 0)
          stringBuilder.Append(value);
        stringBuilder.Replace("\"", "&#34;", startIndex2, stringBuilder.Length - startIndex2);
      }
      int startIndex3 = value.IndexOf('\'');
      if (startIndex3 > -1)
      {
        if (stringBuilder.Length == 0)
          stringBuilder.Append(value);
        stringBuilder.Replace("'", "&#39;", startIndex3, stringBuilder.Length - startIndex3);
      }
      int startIndex4 = value.IndexOf('<');
      if (startIndex4 > -1)
      {
        if (stringBuilder.Length == 0)
          stringBuilder.Append(value);
        stringBuilder.Replace("<", "&#60;", startIndex4, stringBuilder.Length - startIndex4);
      }
      int startIndex5 = value.IndexOf('>');
      if (startIndex5 > -1)
      {
        if (stringBuilder.Length == 0)
          stringBuilder.Append(value);
        stringBuilder.Replace(">", "&#62;", startIndex5, stringBuilder.Length - startIndex5);
      }
      int startIndex6 = value.IndexOf(char.MinValue);
      if (startIndex6 > -1)
      {
        if (stringBuilder.Length == 0)
          stringBuilder.Append(value);
        stringBuilder.Replace(char.MinValue.ToString(), "&#0;", startIndex6, stringBuilder.Length - startIndex6);
      }
      return stringBuilder.Length <= 0 ? value : stringBuilder.ToString();
    }
  }
}
