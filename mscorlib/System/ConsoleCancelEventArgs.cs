// Decompiled with JetBrains decompiler
// Type: System.ConsoleCancelEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>
  ///   Предоставляет данные для события <see cref="E:System.Console.CancelKeyPress" />.
  ///    Этот класс не наследуется.
  /// </summary>
  [Serializable]
  public sealed class ConsoleCancelEventArgs : EventArgs
  {
    private ConsoleSpecialKey _type;
    private bool _cancel;

    internal ConsoleCancelEventArgs(ConsoleSpecialKey type)
    {
      this._type = type;
      this._cancel = false;
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, является ли одновременного нажатия клавиши <see cref="F:System.ConsoleModifiers.Control" /> клавиши-модификатора и <see cref="F:System.ConsoleKey.C" /> клавиши консоли (Ctrl + C) или с помощью клавиш Ctrl + Break прерывает выполнение текущего процесса.
    ///    Значение по умолчанию — <see langword="false" />, который завершает текущий процесс.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />Если текущий процесс следует возобновить при завершении работы обработчика событий; <see langword="false" /> Если завершался текущего процесса.
    ///    Значение по умолчанию — <see langword="false" />; возврат обработчиком события завершении текущего процесса.
    ///    Если <see langword="true" />, по-прежнему текущего процесса.
    /// </returns>
    public bool Cancel
    {
      get
      {
        return this._cancel;
      }
      set
      {
        this._cancel = value;
      }
    }

    /// <summary>
    ///   Получает сочетание клавиш, прерван текущего процесса.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, указывающее сочетание клавиш для текущего процесса прервана.
    ///    Значение по умолчанию отсутствует.
    /// </returns>
    public ConsoleSpecialKey SpecialKey
    {
      get
      {
        return this._type;
      }
    }
  }
}
