// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ImporterCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.Runtime.InteropServices
{
  internal class ImporterCallback : ITypeLibImporterNotifySink
  {
    public void ReportEvent(ImporterEventKind EventKind, int EventCode, string EventMsg)
    {
    }

    [SecuritySafeCritical]
    public Assembly ResolveRef(object TypeLib)
    {
      try
      {
        return (Assembly) new TypeLibConverter().ConvertTypeLibToAssembly(TypeLib, Marshal.GetTypeLibName((ITypeLib) TypeLib) + ".dll", TypeLibImporterFlags.None, (ITypeLibImporterNotifySink) new ImporterCallback(), (byte[]) null, (StrongNameKeyPair) null, (string) null, (Version) null);
      }
      catch (Exception ex)
      {
        return (Assembly) null;
      }
    }
  }
}
