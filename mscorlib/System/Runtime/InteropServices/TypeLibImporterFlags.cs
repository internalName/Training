// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibImporterFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>Показывает способ сборки.</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum TypeLibImporterFlags
  {
    None = 0,
    PrimaryInteropAssembly = 1,
    UnsafeInterfaces = 2,
    SafeArrayAsSystemArray = 4,
    TransformDispRetVals = 8,
    PreventClassMembers = 16, // 0x00000010
    SerializableValueClasses = 32, // 0x00000020
    ImportAsX86 = 256, // 0x00000100
    ImportAsX64 = 512, // 0x00000200
    ImportAsItanium = 1024, // 0x00000400
    ImportAsAgnostic = 2048, // 0x00000800
    ReflectionOnlyLoading = 4096, // 0x00001000
    NoDefineVersionResource = 8192, // 0x00002000
    ImportAsArm = 16384, // 0x00004000
  }
}
