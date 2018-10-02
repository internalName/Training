// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.TypeInformation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class TypeInformation
  {
    private string fullTypeName;
    private string assemblyString;
    private bool hasTypeForwardedFrom;

    internal string FullTypeName
    {
      get
      {
        return this.fullTypeName;
      }
    }

    internal string AssemblyString
    {
      get
      {
        return this.assemblyString;
      }
    }

    internal bool HasTypeForwardedFrom
    {
      get
      {
        return this.hasTypeForwardedFrom;
      }
    }

    internal TypeInformation(string fullTypeName, string assemblyString, bool hasTypeForwardedFrom)
    {
      this.fullTypeName = fullTypeName;
      this.assemblyString = assemblyString;
      this.hasTypeForwardedFrom = hasTypeForwardedFrom;
    }
  }
}
