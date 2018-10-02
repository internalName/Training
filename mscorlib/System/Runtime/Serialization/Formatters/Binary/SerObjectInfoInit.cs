// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.SerObjectInfoInit
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class SerObjectInfoInit
  {
    internal Hashtable seenBeforeTable = new Hashtable();
    internal int objectInfoIdCount = 1;
    internal SerStack oiPool = new SerStack("SerObjectInfo Pool");
  }
}
