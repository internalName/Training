// Decompiled with JetBrains decompiler
// Type: System.IO.SearchResultHandler`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Security;

namespace System.IO
{
  internal abstract class SearchResultHandler<TSource>
  {
    [SecurityCritical]
    internal abstract bool IsResultIncluded(ref Win32Native.WIN32_FIND_DATA findData);

    [SecurityCritical]
    internal abstract TSource CreateObject(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData);
  }
}
