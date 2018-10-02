// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.XmlNamespaceEncoder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting
{
  internal static class XmlNamespaceEncoder
  {
    [SecurityCritical]
    internal static string GetXmlNamespaceForType(RuntimeType type, string dynamicUrl)
    {
      string fullName = type.FullName;
      RuntimeAssembly runtimeAssembly = type.GetRuntimeAssembly();
      StringBuilder stringBuilder = new StringBuilder(256);
      Assembly assembly = typeof (string).Module.Assembly;
      if ((Assembly) runtimeAssembly == assembly)
      {
        stringBuilder.Append(SoapServices.namespaceNS);
        stringBuilder.Append(fullName);
      }
      else
      {
        stringBuilder.Append(SoapServices.fullNS);
        stringBuilder.Append(fullName);
        stringBuilder.Append('/');
        stringBuilder.Append(runtimeAssembly.GetSimpleName());
      }
      return stringBuilder.ToString();
    }

    [SecurityCritical]
    internal static string GetXmlNamespaceForTypeNamespace(RuntimeType type, string dynamicUrl)
    {
      string str = type.Namespace;
      RuntimeAssembly runtimeAssembly = type.GetRuntimeAssembly();
      StringBuilder sb = StringBuilderCache.Acquire(256);
      Assembly assembly = typeof (string).Module.Assembly;
      if ((Assembly) runtimeAssembly == assembly)
      {
        sb.Append(SoapServices.namespaceNS);
        sb.Append(str);
      }
      else
      {
        sb.Append(SoapServices.fullNS);
        sb.Append(str);
        sb.Append('/');
        sb.Append(runtimeAssembly.GetSimpleName());
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    [SecurityCritical]
    internal static string GetTypeNameForSoapActionNamespace(string uri, out bool assemblyIncluded)
    {
      assemblyIncluded = false;
      string fullNs = SoapServices.fullNS;
      string namespaceNs = SoapServices.namespaceNS;
      if (uri.StartsWith(fullNs, StringComparison.Ordinal))
      {
        uri = uri.Substring(fullNs.Length);
        char[] chArray = new char[1]{ '/' };
        string[] strArray = uri.Split(chArray);
        if (strArray.Length != 2)
          return (string) null;
        assemblyIncluded = true;
        return strArray[0] + ", " + strArray[1];
      }
      if (!uri.StartsWith(namespaceNs, StringComparison.Ordinal))
        return (string) null;
      string simpleName = ((RuntimeAssembly) typeof (string).Module.Assembly).GetSimpleName();
      assemblyIncluded = true;
      return uri.Substring(namespaceNs.Length) + ", " + simpleName;
    }
  }
}
