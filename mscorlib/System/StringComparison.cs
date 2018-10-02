// Decompiled with JetBrains decompiler
// Type: System.StringComparison
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает, какой язык, региональные параметры, регистр и правила сортировки должны использовать определенные перегрузки методов <see cref="M:System.String.Compare(System.String,System.String)" /> и <see cref="M:System.String.Equals(System.Object)" />.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum StringComparison
  {
    [__DynamicallyInvokable] CurrentCulture,
    [__DynamicallyInvokable] CurrentCultureIgnoreCase,
    InvariantCulture,
    InvariantCultureIgnoreCase,
    [__DynamicallyInvokable] Ordinal,
    [__DynamicallyInvokable] OrdinalIgnoreCase,
  }
}
