// Decompiled with JetBrains decompiler
// Type: System.Threading.NativeOverlapped
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>
  ///   Содержит явно заданный макет, видимый из неуправляемого кода и имеющий тот же макет, что и структура OVERLAPPED Win32, с дополнительными зарезервированными полями в конце.
  /// </summary>
  [ComVisible(true)]
  public struct NativeOverlapped
  {
    /// <summary>
    ///   Указывает состояние зависит от системы.
    ///    Зарезервировано для использования операционной системой.
    /// </summary>
    public IntPtr InternalLow;
    /// <summary>
    ///   Задает длину полученных данных.
    ///    Зарезервировано для использования операционной системой.
    /// </summary>
    public IntPtr InternalHigh;
    /// <summary>
    ///   Задает позиции файла, с которого начинается перемещение данных.
    /// </summary>
    public int OffsetLow;
    /// <summary>
    ///   Задает старшее слово смещения в байтах начала передачи.
    /// </summary>
    public int OffsetHigh;
    /// <summary>
    ///   Определяет дескриптор события, которое задается в сигнальное состояние при завершении операции.
    ///    Вызывающий процесс должен установить этот член либо ноль, либо для допустимого события обработки до вызова наложенных функций.
    /// </summary>
    public IntPtr EventHandle;
  }
}
