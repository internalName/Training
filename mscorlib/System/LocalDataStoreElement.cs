// Decompiled with JetBrains decompiler
// Type: System.LocalDataStoreElement
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  internal sealed class LocalDataStoreElement
  {
    private object m_value;
    private long m_cookie;

    public LocalDataStoreElement(long cookie)
    {
      this.m_cookie = cookie;
    }

    public object Value
    {
      get
      {
        return this.m_value;
      }
      set
      {
        this.m_value = value;
      }
    }

    public long Cookie
    {
      get
      {
        return this.m_cookie;
      }
    }
  }
}
