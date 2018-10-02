// Decompiled with JetBrains decompiler
// Type: System.Globalization.GlobalizationAssembly
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Reflection;
using System.Security;

namespace System.Globalization
{
  internal sealed class GlobalizationAssembly
  {
    [SecurityCritical]
    internal static unsafe byte* GetGlobalizationResourceBytePtr(Assembly assembly, string tableName)
    {
      UnmanagedMemoryStream manifestResourceStream = assembly.GetManifestResourceStream(tableName) as UnmanagedMemoryStream;
      if (manifestResourceStream != null)
      {
        byte* positionPointer = manifestResourceStream.PositionPointer;
        if ((IntPtr) positionPointer != IntPtr.Zero)
          return positionPointer;
      }
      throw new InvalidOperationException();
    }
  }
}
