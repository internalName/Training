// Decompiled with JetBrains decompiler
// Type: System.AppDomainInitializer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Представляет метод обратного вызова, который вызывается, когда инициализируется домен приложения.
  /// </summary>
  /// <param name="args">
  ///   Массив строк для передачи аргументов в метод обратного вызова.
  /// </param>
  [ComVisible(true)]
  [Serializable]
  public delegate void AppDomainInitializer(string[] args);
}
