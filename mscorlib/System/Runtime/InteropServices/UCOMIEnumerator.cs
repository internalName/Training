// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumerator instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
  internal interface UCOMIEnumerator
  {
    bool MoveNext();

    object Current { get; }

    void Reset();
  }
}
