// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ObjectCreationDelegate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>Создает COM-объект.</summary>
  /// <param name="aggregator">
  ///   Указатель на управляемый объект <see langword="IUnknown" /> интерфейса.
  /// </param>
  /// <returns>
  ///   <see cref="T:System.IntPtr" /> Объект, представляющий <see langword="IUnknown" /> интерфейс COM-объекта.
  /// </returns>
  [ComVisible(true)]
  public delegate IntPtr ObjectCreationDelegate(IntPtr aggregator);
}
