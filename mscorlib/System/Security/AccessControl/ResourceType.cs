// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.ResourceType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>Задает определенные собственные типы объектов.</summary>
  public enum ResourceType
  {
    Unknown,
    FileObject,
    Service,
    Printer,
    RegistryKey,
    LMShare,
    KernelObject,
    WindowObject,
    DSObject,
    DSObjectAll,
    ProviderDefined,
    WmiGuidObject,
    RegistryWow6432Key,
  }
}
