// Decompiled with JetBrains decompiler
// Type: System.LocalDataStoreSlot
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Инкапсулирует ячейку памяти для хранения локальных данных.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class LocalDataStoreSlot
  {
    private LocalDataStoreMgr m_mgr;
    private int m_slot;
    private long m_cookie;

    internal LocalDataStoreSlot(LocalDataStoreMgr mgr, int slot, long cookie)
    {
      this.m_mgr = mgr;
      this.m_slot = slot;
      this.m_cookie = cookie;
    }

    internal LocalDataStoreMgr Manager
    {
      get
      {
        return this.m_mgr;
      }
    }

    internal int Slot
    {
      get
      {
        return this.m_slot;
      }
    }

    internal long Cookie
    {
      get
      {
        return this.m_cookie;
      }
    }

    /// <summary>
    ///   Обеспечивает освобождение ресурсов и выполнение других завершающих операций, когда сборщик мусора восстанавливает объект <see cref="T:System.LocalDataStoreSlot" />.
    /// </summary>
    ~LocalDataStoreSlot()
    {
      LocalDataStoreMgr mgr = this.m_mgr;
      if (mgr == null)
        return;
      int slot = this.m_slot;
      this.m_slot = -1;
      mgr.FreeDataSlot(slot, this.m_cookie);
    }
  }
}
