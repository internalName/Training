// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.SymDocumentType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   Хранит открытые идентификаторы GUID для типов документов, используемые в хранилище символов.
  /// </summary>
  [ComVisible(true)]
  public class SymDocumentType
  {
    /// <summary>
    ///   Задает идентификатор GUID типа документа, используемый в хранилище символов.
    /// </summary>
    public static readonly Guid Text = new Guid(1518771467, (short) 26129, (short) 4563, (byte) 189, (byte) 42, (byte) 0, (byte) 0, (byte) 248, (byte) 8, (byte) 73, (byte) 189);
  }
}
