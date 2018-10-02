// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContextAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>Определяет атрибут контекста.</summary>
  [ComVisible(true)]
  public interface IContextAttribute
  {
    /// <summary>
    ///   Возвращает логическое значение, указывающее, удовлетворяет ли указанный контекст требованиям атрибута контекста.
    /// </summary>
    /// <param name="ctx">
    ///   Контекст, в котором проверяется атрибут текущего контекста.
    /// </param>
    /// <param name="msg">
    ///   Вызов конструирования, параметры которого требуется проверить относительно текущего контекста.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если переданный контекст приемлем; в противном случае — <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    bool IsContextOK(Context ctx, IConstructionCallMessage msg);

    /// <summary>
    ///   Возвращает контекстные свойства вызывающей стороне в данном сообщении.
    /// </summary>
    /// <param name="msg">
    ///   <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> К которому необходимо добавить свойства контекста.
    /// </param>
    [SecurityCritical]
    void GetPropertiesForNewContext(IConstructionCallMessage msg);
  }
}
