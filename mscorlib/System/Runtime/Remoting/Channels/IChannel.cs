// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Предоставляет каналы для сообщений, пересекающих границы удаленного взаимодействия.
  /// </summary>
  [ComVisible(true)]
  public interface IChannel
  {
    /// <summary>Возвращает приоритет канала.</summary>
    /// <returns>Целое число, указывающее приоритет канала.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    int ChannelPriority { [SecurityCritical] get; }

    /// <summary>Возвращает имя канала.</summary>
    /// <returns>Имя канала.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    string ChannelName { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает объект URI в виде выходного параметра и URI текущего канала в качестве возвращаемого значения.
    /// </summary>
    /// <param name="url">URL-адрес объекта.</param>
    /// <param name="objectURI">
    ///   При возвращении данного метода содержит <see cref="T:System.String" /> содержащий URI объекта.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   URI текущего канала или <see langword="null" /> Если URI не принадлежит этому каналу.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    string Parse(string url, out string objectURI);
  }
}
