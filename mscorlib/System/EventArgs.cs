// Decompiled with JetBrains decompiler
// Type: System.EventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Представляет базовый класс для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class EventArgs
  {
    /// <summary>
    ///   Предоставляет значение для использования с событиями, которые не имеют данные события.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly EventArgs Empty = new EventArgs();

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.EventArgs" />.
    /// </summary>
    [__DynamicallyInvokable]
    public EventArgs()
    {
    }
  }
}
