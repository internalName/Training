// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Lifetime.ISponsor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
  /// <summary>
  ///   Указывает, что средству реализации необходимо стать спонсором времени жизни аренды.
  /// </summary>
  [ComVisible(true)]
  public interface ISponsor
  {
    /// <summary>
    ///   Запрашивает клиент-спонсор для обновления аренды для заданного объекта.
    /// </summary>
    /// <param name="lease">
    ///   Аренда времени существования объекта, который требуется продление срока аренды.
    /// </param>
    /// <returns>Дополнительное время аренды для заданного объекта.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    TimeSpan Renewal(ILease lease);
  }
}
