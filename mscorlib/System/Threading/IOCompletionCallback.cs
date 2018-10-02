// Decompiled with JetBrains decompiler
// Type: System.Threading.IOCompletionCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Получает код ошибки, количество байтов и тип перекрывающегося значения при завершении операции ввода-вывода в пуле потоков.
  /// </summary>
  /// <param name="errorCode">Код ошибки.</param>
  /// <param name="numBytes">Количество переданных байтов.</param>
  /// <param name="pOVERLAP">
  ///   A <see cref="T:System.Threading.NativeOverlapped" /> представляющий неуправляемый указатель к типу перекрывающегося значения.
  /// </param>
  [SecurityCritical]
  [CLSCompliant(false)]
  [ComVisible(true)]
  public unsafe delegate void IOCompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP);
}
