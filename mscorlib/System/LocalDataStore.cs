// Decompiled with JetBrains decompiler
// Type: System.LocalDataStore
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System
{
  internal sealed class LocalDataStore
  {
    private LocalDataStoreElement[] m_DataTable;
    private LocalDataStoreMgr m_Manager;

    public LocalDataStore(LocalDataStoreMgr mgr, int InitialCapacity)
    {
      this.m_Manager = mgr;
      this.m_DataTable = new LocalDataStoreElement[InitialCapacity];
    }

    internal void Dispose()
    {
      this.m_Manager.DeleteLocalDataStore(this);
    }

    public object GetData(LocalDataStoreSlot slot)
    {
      this.m_Manager.ValidateSlot(slot);
      int slot1 = slot.Slot;
      if (slot1 >= 0)
      {
        if (slot1 >= this.m_DataTable.Length)
          return (object) null;
        LocalDataStoreElement dataStoreElement = this.m_DataTable[slot1];
        if (dataStoreElement == null)
          return (object) null;
        if (dataStoreElement.Cookie == slot.Cookie)
          return dataStoreElement.Value;
      }
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_SlotHasBeenFreed"));
    }

    public void SetData(LocalDataStoreSlot slot, object data)
    {
      this.m_Manager.ValidateSlot(slot);
      int slot1 = slot.Slot;
      if (slot1 >= 0)
      {
        LocalDataStoreElement dataStoreElement = (slot1 < this.m_DataTable.Length ? this.m_DataTable[slot1] : (LocalDataStoreElement) null) ?? this.PopulateElement(slot);
        if (dataStoreElement.Cookie == slot.Cookie)
        {
          dataStoreElement.Value = data;
          return;
        }
      }
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_SlotHasBeenFreed"));
    }

    internal void FreeData(int slot, long cookie)
    {
      if (slot >= this.m_DataTable.Length)
        return;
      LocalDataStoreElement dataStoreElement = this.m_DataTable[slot];
      if (dataStoreElement == null || dataStoreElement.Cookie != cookie)
        return;
      this.m_DataTable[slot] = (LocalDataStoreElement) null;
    }

    [SecuritySafeCritical]
    private LocalDataStoreElement PopulateElement(LocalDataStoreSlot slot)
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) this.m_Manager, ref lockTaken);
        int slot1 = slot.Slot;
        if (slot1 < 0)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_SlotHasBeenFreed"));
        if (slot1 >= this.m_DataTable.Length)
        {
          LocalDataStoreElement[] dataStoreElementArray = new LocalDataStoreElement[this.m_Manager.GetSlotTableLength()];
          Array.Copy((Array) this.m_DataTable, (Array) dataStoreElementArray, this.m_DataTable.Length);
          this.m_DataTable = dataStoreElementArray;
        }
        if (this.m_DataTable[slot1] == null)
          this.m_DataTable[slot1] = new LocalDataStoreElement(slot.Cookie);
        return this.m_DataTable[slot1];
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) this.m_Manager);
      }
    }
  }
}
