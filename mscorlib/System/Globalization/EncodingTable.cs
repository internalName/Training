// Decompiled with JetBrains decompiler
// Type: System.Globalization.EncodingTable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System.Globalization
{
  internal static class EncodingTable
  {
    private static int lastEncodingItem = EncodingTable.GetNumEncodingItems() - 1;
    [SecurityCritical]
    internal static unsafe InternalEncodingDataItem* encodingDataPtr = EncodingTable.GetEncodingData();
    [SecurityCritical]
    internal static unsafe InternalCodePageDataItem* codePageDataPtr = EncodingTable.GetCodePageData();
    private static Hashtable hashByName = Hashtable.Synchronized(new Hashtable((IEqualityComparer) StringComparer.OrdinalIgnoreCase));
    private static Hashtable hashByCodePage = Hashtable.Synchronized(new Hashtable());
    private static volatile int lastCodePageItem;

    [SecuritySafeCritical]
    static unsafe EncodingTable()
    {
    }

    [SecuritySafeCritical]
    private static unsafe int internalGetCodePageFromName(string name)
    {
      int index1 = 0;
      int num1 = EncodingTable.lastEncodingItem;
      while (num1 - index1 > 3)
      {
        int index2 = (num1 - index1) / 2 + index1;
        int num2 = string.nativeCompareOrdinalIgnoreCaseWC(name, EncodingTable.encodingDataPtr[index2].webName);
        if (num2 == 0)
          return (int) EncodingTable.encodingDataPtr[index2].codePage;
        if (num2 < 0)
          num1 = index2;
        else
          index1 = index2;
      }
      for (; index1 <= num1; ++index1)
      {
        if (string.nativeCompareOrdinalIgnoreCaseWC(name, EncodingTable.encodingDataPtr[index1].webName) == 0)
          return (int) EncodingTable.encodingDataPtr[index1].codePage;
      }
      throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_EncodingNotSupported"), (object) name), nameof (name));
    }

    [SecuritySafeCritical]
    internal static unsafe EncodingInfo[] GetEncodings()
    {
      if (EncodingTable.lastCodePageItem == 0)
      {
        int index = 0;
        while (EncodingTable.codePageDataPtr[index].codePage != (ushort) 0)
          ++index;
        EncodingTable.lastCodePageItem = index;
      }
      EncodingInfo[] encodingInfoArray = new EncodingInfo[EncodingTable.lastCodePageItem];
      for (int index = 0; index < EncodingTable.lastCodePageItem; ++index)
        encodingInfoArray[index] = new EncodingInfo((int) EncodingTable.codePageDataPtr[index].codePage, CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[index].Names, 0U), Environment.GetResourceString("Globalization.cp_" + (object) EncodingTable.codePageDataPtr[index].codePage));
      return encodingInfoArray;
    }

    internal static int GetCodePageFromName(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      object obj = EncodingTable.hashByName[(object) name];
      if (obj != null)
        return (int) obj;
      int codePageFromName = EncodingTable.internalGetCodePageFromName(name);
      EncodingTable.hashByName[(object) name] = (object) codePageFromName;
      return codePageFromName;
    }

    [SecuritySafeCritical]
    internal static unsafe CodePageDataItem GetCodePageDataItem(int codepage)
    {
      CodePageDataItem codePageDataItem1 = (CodePageDataItem) EncodingTable.hashByCodePage[(object) codepage];
      if (codePageDataItem1 != null)
        return codePageDataItem1;
      int codePage;
      for (int dataIndex = 0; (codePage = (int) EncodingTable.codePageDataPtr[dataIndex].codePage) != 0; ++dataIndex)
      {
        if (codePage == codepage)
        {
          CodePageDataItem codePageDataItem2 = new CodePageDataItem(dataIndex);
          EncodingTable.hashByCodePage[(object) codepage] = (object) codePageDataItem2;
          return codePageDataItem2;
        }
      }
      return (CodePageDataItem) null;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe InternalEncodingDataItem* GetEncodingData();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetNumEncodingItems();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe InternalCodePageDataItem* GetCodePageData();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe byte* nativeCreateOpenFileMapping(string inSectionName, int inBytesToAllocate, out IntPtr mappedFileHandle);
  }
}
