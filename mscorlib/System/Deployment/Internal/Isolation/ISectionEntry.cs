// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.ISectionEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("285a8861-c84a-11d7-850f-005cd062464f")]
  [ComImport]
  internal interface ISectionEntry
  {
    object GetField(uint fieldId);

    string GetFieldName(uint fieldId);
  }
}
