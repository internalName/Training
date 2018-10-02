// Decompiled with JetBrains decompiler
// Type: System.LocalDataStoreMgr
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
  internal sealed class LocalDataStoreMgr
  {
    private bool[] m_SlotInfoTable = new bool[64];
    private List<LocalDataStore> m_ManagedLocalDataStores = new List<LocalDataStore>();
    private Dictionary<string, LocalDataStoreSlot> m_KeyToSlotMap = new Dictionary<string, LocalDataStoreSlot>();
    private const int InitialSlotTableSize = 64;
    private const int SlotTableDoubleThreshold = 512;
    private const int LargeSlotTableSizeIncrease = 128;
    private int m_FirstAvailableSlot;
    private long m_CookieGenerator;

    [SecuritySafeCritical]
    public LocalDataStoreHolder CreateLocalDataStore()
    {
      LocalDataStore store = new LocalDataStore(this, this.m_SlotInfoTable.Length);
      LocalDataStoreHolder localDataStoreHolder = new LocalDataStoreHolder(store);
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) this, ref lockTaken);
        this.m_ManagedLocalDataStores.Add(store);
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) this);
      }
      return localDataStoreHolder;
    }

    [SecuritySafeCritical]
    public void DeleteLocalDataStore(LocalDataStore store)
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) this, ref lockTaken);
        this.m_ManagedLocalDataStores.Remove(store);
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) this);
      }
    }

    [SecuritySafeCritical]
    public LocalDataStoreSlot AllocateDataSlot()
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) this, ref lockTaken);
        int length = this.m_SlotInfoTable.Length;
        int firstAvailableSlot = this.m_FirstAvailableSlot;
        while (firstAvailableSlot < length && this.m_SlotInfoTable[firstAvailableSlot])
          ++firstAvailableSlot;
        if (firstAvailableSlot >= length)
        {
          bool[] flagArray = new bool[length >= 512 ? length + 128 : length * 2];
          Array.Copy((Array) this.m_SlotInfoTable, (Array) flagArray, length);
          this.m_SlotInfoTable = flagArray;
        }
        this.m_SlotInfoTable[firstAvailableSlot] = true;
        LocalDataStoreSlot localDataStoreSlot = new LocalDataStoreSlot(this, firstAvailableSlot, checked (this.m_CookieGenerator++));
        this.m_FirstAvailableSlot = firstAvailableSlot + 1;
        return localDataStoreSlot;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) this);
      }
    }

    [SecuritySafeCritical]
    public LocalDataStoreSlot AllocateNamedDataSlot(string name)
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) this, ref lockTaken);
        LocalDataStoreSlot localDataStoreSlot = this.AllocateDataSlot();
        this.m_KeyToSlotMap.Add(name, localDataStoreSlot);
        return localDataStoreSlot;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) this);
      }
    }

    [SecuritySafeCritical]
    public LocalDataStoreSlot GetNamedDataSlot(string name)
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) this, ref lockTaken);
        return this.m_KeyToSlotMap.GetValueOrDefault(name) ?? this.AllocateNamedDataSlot(name);
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) this);
      }
    }

    [SecuritySafeCritical]
    public void FreeNamedDataSlot(string name)
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) this, ref lockTaken);
        this.m_KeyToSlotMap.Remove(name);
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) this);
      }
    }

    [SecuritySafeCritical]
    internal void FreeDataSlot(int slot, long cookie)
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) this, ref lockTaken);
        for (int index = 0; index < this.m_ManagedLocalDataStores.Count; ++index)
          this.m_ManagedLocalDataStores[index].FreeData(slot, cookie);
        this.m_SlotInfoTable[slot] = false;
        if (slot >= this.m_FirstAvailableSlot)
          return;
        this.m_FirstAvailableSlot = slot;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) this);
      }
    }

    public void ValidateSlot(LocalDataStoreSlot slot)
    {
      if (slot == null || slot.Manager != this)
        throw new ArgumentException(Environment.GetResourceString("Argument_ALSInvalidSlot"));
    }

    internal int GetSlotTableLength()
    {
      return this.m_SlotInfoTable.Length;
    }
  }
}
