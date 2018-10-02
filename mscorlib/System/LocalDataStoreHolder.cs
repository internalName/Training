// Decompiled with JetBrains decompiler
// Type: System.LocalDataStoreHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  internal sealed class LocalDataStoreHolder
  {
    private LocalDataStore m_Store;

    public LocalDataStoreHolder(LocalDataStore store)
    {
      this.m_Store = store;
    }

    ~LocalDataStoreHolder()
    {
      this.m_Store?.Dispose();
    }

    public LocalDataStore Store
    {
      get
      {
        return this.m_Store;
      }
    }
  }
}
