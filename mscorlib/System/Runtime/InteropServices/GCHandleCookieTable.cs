// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.GCHandleCookieTable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Threading;

namespace System.Runtime.InteropServices
{
  internal class GCHandleCookieTable
  {
    private const int InitialHandleCount = 10;
    private const int MaxListSize = 16777215;
    private const uint CookieMaskIndex = 16777215;
    private const uint CookieMaskSentinal = 4278190080;
    private Dictionary<IntPtr, IntPtr> m_HandleToCookieMap;
    private volatile IntPtr[] m_HandleList;
    private volatile byte[] m_CycleCounts;
    private int m_FreeIndex;
    private readonly object m_syncObject;

    internal GCHandleCookieTable()
    {
      this.m_HandleList = new IntPtr[10];
      this.m_CycleCounts = new byte[10];
      this.m_HandleToCookieMap = new Dictionary<IntPtr, IntPtr>(10);
      this.m_syncObject = new object();
      for (int index = 0; index < 10; ++index)
      {
        this.m_HandleList[index] = IntPtr.Zero;
        this.m_CycleCounts[index] = (byte) 0;
      }
    }

    internal IntPtr FindOrAddHandle(IntPtr handle)
    {
      if (handle == IntPtr.Zero)
        return IntPtr.Zero;
      IntPtr num = IntPtr.Zero;
      lock (this.m_syncObject)
      {
        if (this.m_HandleToCookieMap.ContainsKey(handle))
          return this.m_HandleToCookieMap[handle];
        if (this.m_FreeIndex < this.m_HandleList.Length && Volatile.Read(ref this.m_HandleList[this.m_FreeIndex]) == IntPtr.Zero)
        {
          Volatile.Write(ref this.m_HandleList[this.m_FreeIndex], handle);
          num = this.GetCookieFromData((uint) this.m_FreeIndex, this.m_CycleCounts[this.m_FreeIndex]);
          ++this.m_FreeIndex;
        }
        else
        {
          for (this.m_FreeIndex = 0; this.m_FreeIndex < 16777215; ++this.m_FreeIndex)
          {
            if (this.m_HandleList[this.m_FreeIndex] == IntPtr.Zero)
            {
              Volatile.Write(ref this.m_HandleList[this.m_FreeIndex], handle);
              num = this.GetCookieFromData((uint) this.m_FreeIndex, this.m_CycleCounts[this.m_FreeIndex]);
              ++this.m_FreeIndex;
              break;
            }
            if (this.m_FreeIndex + 1 == this.m_HandleList.Length)
              this.GrowArrays();
          }
        }
        if (num == IntPtr.Zero)
          throw new OutOfMemoryException(Environment.GetResourceString("OutOfMemory_GCHandleMDA"));
        this.m_HandleToCookieMap.Add(handle, num);
      }
      return num;
    }

    internal IntPtr GetHandle(IntPtr cookie)
    {
      IntPtr zero = IntPtr.Zero;
      if (!this.ValidateCookie(cookie))
        return IntPtr.Zero;
      return Volatile.Read(ref this.m_HandleList[this.GetIndexFromCookie(cookie)]);
    }

    internal void RemoveHandleIfPresent(IntPtr handle)
    {
      if (handle == IntPtr.Zero)
        return;
      lock (this.m_syncObject)
      {
        if (!this.m_HandleToCookieMap.ContainsKey(handle))
          return;
        IntPtr handleToCookie = this.m_HandleToCookieMap[handle];
        if (!this.ValidateCookie(handleToCookie))
          return;
        int indexFromCookie = this.GetIndexFromCookie(handleToCookie);
        ++this.m_CycleCounts[indexFromCookie];
        Volatile.Write(ref this.m_HandleList[indexFromCookie], IntPtr.Zero);
        this.m_HandleToCookieMap.Remove(handle);
        this.m_FreeIndex = indexFromCookie;
      }
    }

    private bool ValidateCookie(IntPtr cookie)
    {
      int index;
      byte xorData;
      this.GetDataFromCookie(cookie, out index, out xorData);
      if (index >= 16777215 || index >= this.m_HandleList.Length || Volatile.Read(ref this.m_HandleList[index]) == IntPtr.Zero)
        return false;
      byte num1 = (byte) (AppDomain.CurrentDomain.Id % (int) byte.MaxValue);
      byte num2 = (byte) ((uint) Volatile.Read(ref this.m_CycleCounts[index]) ^ (uint) num1);
      return (int) xorData == (int) num2;
    }

    private void GrowArrays()
    {
      int length = this.m_HandleList.Length;
      IntPtr[] numArray1 = new IntPtr[length * 2];
      byte[] numArray2 = new byte[length * 2];
      Array.Copy((Array) this.m_HandleList, (Array) numArray1, length);
      Array.Copy((Array) this.m_CycleCounts, (Array) numArray2, length);
      this.m_HandleList = numArray1;
      this.m_CycleCounts = numArray2;
    }

    private IntPtr GetCookieFromData(uint index, byte cycleCount)
    {
      byte num = (byte) (AppDomain.CurrentDomain.Id % (int) byte.MaxValue);
      return (IntPtr) ((long) (((int) cycleCount ^ (int) num) << 24) + (long) index + 1L);
    }

    private void GetDataFromCookie(IntPtr cookie, out int index, out byte xorData)
    {
      uint num = (uint) (int) cookie;
      index = ((int) num & 16777215) - 1;
      xorData = (byte) ((num & 4278190080U) >> 24);
    }

    private int GetIndexFromCookie(IntPtr cookie)
    {
      return ((int) cookie & 16777215) - 1;
    }
  }
}
