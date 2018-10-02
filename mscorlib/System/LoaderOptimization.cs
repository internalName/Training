// Decompiled with JetBrains decompiler
// Type: System.LoaderOptimization
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Перечисление, которое используется с классом <see cref="T:System.LoaderOptimizationAttribute" /> для указания оптимизаций загрузчика для исполняемого файла.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public enum LoaderOptimization
  {
    NotSpecified = 0,
    SingleDomain = 1,
    MultiDomain = 2,
    [Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")] DomainMask = 3,
    MultiDomainHost = 3,
    [Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")] DisallowBindings = 4,
  }
}
