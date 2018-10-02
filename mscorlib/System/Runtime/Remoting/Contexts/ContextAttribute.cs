// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.ContextAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Предоставляет реализацию по умолчанию <see cref="T:System.Runtime.Remoting.Contexts.IContextAttribute" /> и <see cref="T:System.Runtime.Remoting.Contexts.IContextProperty" /> интерфейсов.
  /// </summary>
  [SecurityCritical]
  [AttributeUsage(AttributeTargets.Class)]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ContextAttribute : Attribute, IContextAttribute, IContextProperty
  {
    /// <summary>Указывает имя атрибута контекста.</summary>
    protected string AttributeName;

    /// <summary>
    ///   Создает экземпляр <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" /> класса с заданным именем.
    /// </summary>
    /// <param name="name">Имя атрибута контекста.</param>
    public ContextAttribute(string name)
    {
      this.AttributeName = name;
    }

    /// <summary>Возвращает имя атрибута контекста.</summary>
    /// <returns>Имя атрибута контекста.</returns>
    public virtual string Name
    {
      [SecurityCritical] get
      {
        return this.AttributeName;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, совместимо ли контекстное свойство с новым контекстом.
    /// </summary>
    /// <param name="newCtx">
    ///   Новый контекст, в котором было создано свойство.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если контекстное свойство приемлемо в новом контексте; в противном случае — <see langword="false" />.
    /// </returns>
    [SecurityCritical]
    public virtual bool IsNewContextOK(Context newCtx)
    {
      return true;
    }

    /// <summary>Вызывается, когда контекст заморожен.</summary>
    /// <param name="newContext">Контекст для закрепления.</param>
    [SecurityCritical]
    public virtual void Freeze(Context newContext)
    {
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, равен ли данный экземпляр указанному объекту.
    /// </summary>
    /// <param name="o">Объект, сравниваемый с данным экземпляром.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="o" /> не <see langword="null" /> и если имена объектов эквивалентны; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public override bool Equals(object o)
    {
      IContextProperty contextProperty = o as IContextProperty;
      if (contextProperty != null)
        return this.AttributeName.Equals(contextProperty.Name);
      return false;
    }

    /// <summary>
    ///   Возвращает хэш-код для этого экземпляра <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код для этого экземпляра <see cref="T:System.Runtime.Remoting.Contexts.ContextAttribute" />.
    /// </returns>
    [SecuritySafeCritical]
    public override int GetHashCode()
    {
      return this.AttributeName.GetHashCode();
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, является ли параметр контекста требованиям атрибута контекста.
    /// </summary>
    /// <param name="ctx">Контекст, в которой требуется проверить.</param>
    /// <param name="ctorMsg">
    ///   <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> К которому добавляется свойство контекста.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если переданный контекст приемлем; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Либо <paramref name="ctx" />, либо <paramref name="ctorMsg" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public virtual bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
    {
      if (ctx == null)
        throw new ArgumentNullException(nameof (ctx));
      if (ctorMsg == null)
        throw new ArgumentNullException(nameof (ctorMsg));
      if (!ctorMsg.ActivationType.IsContextful)
        return true;
      object property = (object) ctx.GetProperty(this.AttributeName);
      return property != null && this.Equals(property);
    }

    /// <summary>
    ///   Добавляет текущее контекстное свойство в указанное сообщение.
    /// </summary>
    /// <param name="ctorMsg">
    ///   <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> К которому добавляется свойство контекста.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="ctorMsg" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
    {
      if (ctorMsg == null)
        throw new ArgumentNullException(nameof (ctorMsg));
      ctorMsg.ContextProperties.Add((object) this);
    }
  }
}
