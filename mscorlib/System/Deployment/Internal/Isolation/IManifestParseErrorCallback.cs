// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IManifestParseErrorCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  [Guid("ace1b703-1aac-4956-ab87-90cac8b93ce6")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IManifestParseErrorCallback
  {
    [SecurityCritical]
    void OnError([In] uint StartLine, [In] uint nStartColumn, [In] uint cCharacterCount, [In] int hr, [MarshalAs(UnmanagedType.LPWStr), In] string ErrorStatusHostFile, [In] uint ParameterCount, [MarshalAs(UnmanagedType.LPArray), In] string[] Parameters);
  }
}
