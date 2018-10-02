// Decompiled with JetBrains decompiler
// Type: System.DateTimeKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает, является ли <see cref="T:System.DateTime" /> объект представляет локальное время, универсальное глобальное (UTC), или не указано в формате UTC или местного времени.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum DateTimeKind
  {
    [__DynamicallyInvokable] Unspecified,
    [__DynamicallyInvokable] Utc,
    [__DynamicallyInvokable] Local,
  }
}
