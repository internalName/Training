// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TCEAdapterGen.EventItfInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
  internal class EventItfInfo
  {
    private string m_strEventItfName;
    private string m_strSrcItfName;
    private string m_strEventProviderName;
    private RuntimeAssembly m_asmImport;
    private RuntimeAssembly m_asmSrcItf;

    public EventItfInfo(string strEventItfName, string strSrcItfName, string strEventProviderName, RuntimeAssembly asmImport, RuntimeAssembly asmSrcItf)
    {
      this.m_strEventItfName = strEventItfName;
      this.m_strSrcItfName = strSrcItfName;
      this.m_strEventProviderName = strEventProviderName;
      this.m_asmImport = asmImport;
      this.m_asmSrcItf = asmSrcItf;
    }

    public Type GetEventItfType()
    {
      Type type = this.m_asmImport.GetType(this.m_strEventItfName, true, false);
      if (type != (Type) null && !type.IsVisible)
        type = (Type) null;
      return type;
    }

    public Type GetSrcItfType()
    {
      Type type = this.m_asmSrcItf.GetType(this.m_strSrcItfName, true, false);
      if (type != (Type) null && !type.IsVisible)
        type = (Type) null;
      return type;
    }

    public string GetEventProviderName()
    {
      return this.m_strEventProviderName;
    }
  }
}
