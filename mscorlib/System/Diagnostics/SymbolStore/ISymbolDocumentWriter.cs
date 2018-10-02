// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolDocumentWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   Представляет документ, на который ссылается хранилище символов.
  /// </summary>
  [ComVisible(true)]
  public interface ISymbolDocumentWriter
  {
    /// <summary>
    ///   Сохраняет необработанный источник документа в хранилище символов.
    /// </summary>
    /// <param name="source">
    ///   Источник документа, представленный байтами без знака.
    /// </param>
    void SetSource(byte[] source);

    /// <summary>Задает сведения о контрольной сумме.</summary>
    /// <param name="algorithmId">
    ///   Идентификатор GUID, представляющий идентификатор алгоритма.
    /// </param>
    /// <param name="checkSum">Контрольная сумма.</param>
    void SetCheckSum(Guid algorithmId, byte[] checkSum);
  }
}
