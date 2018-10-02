// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContextProperty
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Собирает информацию о именах из контекстного свойства и определяет, является ли новый контекст ОК для свойства контекста.
  /// </summary>
  [ComVisible(true)]
  public interface IContextProperty
  {
    /// <summary>
    ///   Возвращает имя свойства, под которой он будет добавлен в контекст.
    /// </summary>
    /// <returns>Имя свойства.</returns>
    string Name { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, совместимо ли контекстное свойство с новым контекстом.
    /// </summary>
    /// <param name="newCtx">
    ///   Новый контекст, в котором <see cref="T:System.Runtime.Remoting.Contexts.ContextProperty" /> будет создан.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если свойство контекста может сосуществовать с другими свойствами контекста в данном контексте; в противном случае — <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    bool IsNewContextOK(Context newCtx);

    /// <summary>Вызывается, когда контекст заморожен.</summary>
    /// <param name="newContext">Контекст для закрепления.</param>
    [SecurityCritical]
    void Freeze(Context newContext);
  }
}
