// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.IndexerNameAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает имя, под которым индексатор известен в языках программирования, не поддерживающих индексаторы напрямую.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, Inherited = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class IndexerNameAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.IndexerNameAttribute" />.
    /// </summary>
    /// <param name="indexerName">
    ///   Имя индексатора, как показано в других языках.
    /// </param>
    [__DynamicallyInvokable]
    public IndexerNameAttribute(string indexerName)
    {
    }
  }
}
