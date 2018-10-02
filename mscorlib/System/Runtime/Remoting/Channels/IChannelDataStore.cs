// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannelDataStore
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Хранит данные канала для каналов удаленного взаимодействия.
  /// </summary>
  [ComVisible(true)]
  public interface IChannelDataStore
  {
    /// <summary>
    ///   Возвращает массив URI, на который отображается текущий канал каналов.
    /// </summary>
    /// <returns>
    ///   Массив URI, на который отображается текущий канал каналов.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    string[] ChannelUris { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает или задает объект данных, связанный с указанным ключом для используемого канала.
    /// </summary>
    /// <param name="key">С которым связан объект данных ключа.</param>
    /// <returns>Указанный объект данных для используемого канала.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    object this[object key] { [SecurityCritical] get; [SecurityCritical] set; }
  }
}
