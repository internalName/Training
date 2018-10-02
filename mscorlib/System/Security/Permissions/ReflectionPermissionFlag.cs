// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.ReflectionPermissionFlag
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Указывает разрешенное использование <see cref="N:System.Reflection" /> и <see cref="N:System.Reflection.Emit" /> пространства имен.
  /// </summary>
  [ComVisible(true)]
  [Flags]
  [Serializable]
  public enum ReflectionPermissionFlag
  {
    NoFlags = 0,
    [Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")] TypeInformation = 1,
    MemberAccess = 2,
    [Obsolete("This permission is no longer used by the CLR.")] ReflectionEmit = 4,
    [ComVisible(false)] RestrictedMemberAccess = 8,
    [Obsolete("This permission has been deprecated. Use PermissionState.Unrestricted to get full access.")] AllFlags = ReflectionEmit | MemberAccess | TypeInformation, // 0x00000007
  }
}
