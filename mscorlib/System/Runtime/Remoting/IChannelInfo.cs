// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.IChannelInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Предоставляет сведения о пользовательском канале, передающиеся по <see cref="T:System.Runtime.Remoting.ObjRef" />.
  /// </summary>
  [ComVisible(true)]
  public interface IChannelInfo
  {
    /// <summary>
    ///   Возвращает и задает канал данных для каждого канала.
    /// </summary>
    /// <returns>Канал данных для каждого канала.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    object[] ChannelData { [SecurityCritical] get; [SecurityCritical] set; }
  }
}
