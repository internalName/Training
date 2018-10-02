// Decompiled with JetBrains decompiler
// Type: System.Security.PolicyLevelType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>Указывает тип уровня политики управляемого кода.</summary>
  [ComVisible(true)]
  [Serializable]
  public enum PolicyLevelType
  {
    User,
    Machine,
    Enterprise,
    AppDomain,
  }
}
