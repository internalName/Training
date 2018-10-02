// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.SoapParameterAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
  /// <summary>
  ///   Настраивает генерирование SOAP и обработку для параметра.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter)]
  [ComVisible(true)]
  public sealed class SoapParameterAttribute : SoapAttribute
  {
  }
}
