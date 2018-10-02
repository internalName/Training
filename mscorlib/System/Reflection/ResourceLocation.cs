// Decompiled with JetBrains decompiler
// Type: System.Reflection.ResourceLocation
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>Указывает расположение ресурса.</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum ResourceLocation
  {
    [__DynamicallyInvokable] Embedded = 1,
    [__DynamicallyInvokable] ContainedInAnotherAssembly = 2,
    [__DynamicallyInvokable] ContainedInManifestFile = 4,
  }
}
