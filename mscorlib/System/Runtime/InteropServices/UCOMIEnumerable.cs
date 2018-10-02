// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIEnumerable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Runtime.InteropServices
{
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumerable instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
  internal interface UCOMIEnumerable
  {
    [DispId(-4)]
    IEnumerator GetEnumerator();
  }
}
