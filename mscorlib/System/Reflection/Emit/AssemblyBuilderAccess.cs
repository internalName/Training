// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.AssemblyBuilderAccess
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>Определяет режимы доступа для динамической сборки.</summary>
  [ComVisible(true)]
  [Flags]
  [Serializable]
  public enum AssemblyBuilderAccess
  {
    Run = 1,
    Save = 2,
    RunAndSave = Save | Run, // 0x00000003
    ReflectionOnly = 6,
    RunAndCollect = 9,
  }
}
