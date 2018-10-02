// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeHandleMinusOneIsInvalid
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
  /// <summary>
  ///   Предоставляет базовый класс для реализаций безопасного дескриптора Win32, в котором значение -1 обозначает недопустимый дескриптор.
  /// </summary>
  [SecurityCritical]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public abstract class SafeHandleMinusOneIsInvalid : SafeHandle
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:Microsoft.Win32.SafeHandles.SafeHandleMinusOneIsInvalid" /> класса, указывающее, является ли дескриптор освобождается надежно.
    /// </summary>
    /// <param name="ownsHandle">
    ///   Значение <see langword="true" />, чтобы надежно освободить маркер на стадии завершения; значение <see langword="false" />, чтобы предотвратить надежное освобождение (не рекомендуется).
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    protected SafeHandleMinusOneIsInvalid(bool ownsHandle)
      : base(new IntPtr(-1), ownsHandle)
    {
    }

    /// <summary>
    ///   Получает значение, указывающее, является ли дескриптор недействительным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если дескриптор недействителен, в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsInvalid
    {
      [SecurityCritical] get
      {
        return this.handle == new IntPtr(-1);
      }
    }
  }
}
