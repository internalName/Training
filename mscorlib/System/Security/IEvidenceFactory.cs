// Decompiled with JetBrains decompiler
// Type: System.Security.IEvidenceFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
  /// <summary>
  ///   Возвращает объект <see cref="T:System.Security.Policy.Evidence" />.
  /// </summary>
  [ComVisible(true)]
  public interface IEvidenceFactory
  {
    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Policy.Evidence" /> проверяет удостоверение текущего объекта.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Policy.Evidence" /> удостоверения текущего объекта.
    /// </returns>
    Evidence Evidence { get; }
  }
}
