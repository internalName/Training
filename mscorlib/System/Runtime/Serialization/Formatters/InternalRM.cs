// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.InternalRM
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
  /// <summary>
  ///   Сообщения трассировки журналов при компиляции инфраструктуры сериализации платформы .NET Framework.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  public sealed class InternalRM
  {
    /// <summary>Печатает сообщения трассировки SOAP.</summary>
    /// <param name="messages">
    ///   Массив сообщений трассировки для печати.
    /// </param>
    [Conditional("_LOGGING")]
    public static void InfoSoap(params object[] messages)
    {
    }

    /// <summary>Проверяет, включена ли трассировка SOAP.</summary>
    /// <returns>
    ///   <see langword="true" />, если трассировка включена; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool SoapCheckEnabled()
    {
      return BCLDebug.CheckEnabled("SOAP");
    }
  }
}
