// Decompiled with JetBrains decompiler
// Type: System.Configuration.Assemblies.AssemblyVersionCompatibility
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
  /// <summary>
  ///   Определяет различные типы совместимости версий сборки.
  ///    Этот компонент недоступен в .NET Framework версии 1.0.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum AssemblyVersionCompatibility
  {
    SameMachine = 1,
    SameProcess = 2,
    SameDomain = 3,
  }
}
