// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.NativeVersionInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection.Emit
{
  internal class NativeVersionInfo
  {
    internal string m_strDescription;
    internal string m_strCompany;
    internal string m_strTitle;
    internal string m_strCopyright;
    internal string m_strTrademark;
    internal string m_strProduct;
    internal string m_strProductVersion;
    internal string m_strFileVersion;
    internal int m_lcid;

    internal NativeVersionInfo()
    {
      this.m_strDescription = (string) null;
      this.m_strCompany = (string) null;
      this.m_strTitle = (string) null;
      this.m_strCopyright = (string) null;
      this.m_strTrademark = (string) null;
      this.m_strProduct = (string) null;
      this.m_strProductVersion = (string) null;
      this.m_strFileVersion = (string) null;
      this.m_lcid = -1;
    }
  }
}
