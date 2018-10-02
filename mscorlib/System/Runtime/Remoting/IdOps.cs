// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.IdOps
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  internal struct IdOps
  {
    internal const int None = 0;
    internal const int GenerateURI = 1;
    internal const int StrongIdentity = 2;
    internal const int IsInitializing = 4;

    internal static bool bStrongIdentity(int flags)
    {
      return (uint) (flags & 2) > 0U;
    }

    internal static bool bIsInitializing(int flags)
    {
      return (uint) (flags & 4) > 0U;
    }
  }
}
