// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.TypeLoadExceptionHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization
{
  internal class TypeLoadExceptionHolder
  {
    private string m_typeName;

    internal TypeLoadExceptionHolder(string typeName)
    {
      this.m_typeName = typeName;
    }

    internal string TypeName
    {
      get
      {
        return this.m_typeName;
      }
    }
  }
}
