// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.UrlAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  /// <summary>
  ///   Определяет атрибут, который может использоваться во время вызова URL-адреса которых происходит активация.
  ///    Этот класс не наследуется.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  public sealed class UrlAttribute : ContextAttribute
  {
    private static string propertyName = nameof (UrlAttribute);
    private string url;

    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    /// </summary>
    /// <param name="callsiteURL">URL-адрес сайта вызова.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="callsiteURL" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public UrlAttribute(string callsiteURL)
      : base(UrlAttribute.propertyName)
    {
      if (callsiteURL == null)
        throw new ArgumentNullException(nameof (callsiteURL));
      this.url = callsiteURL;
    }

    /// <summary>
    ///   Проверяет, ссылается ли заданный объект же URL-адрес текущего экземпляра.
    /// </summary>
    /// <param name="o">
    ///   Объект, сравниваемый с текущим <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если объект <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> с тем же значением; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecuritySafeCritical]
    public override bool Equals(object o)
    {
      if (o is IContextProperty && o is UrlAttribute)
        return ((UrlAttribute) o).UrlValue.Equals(this.url);
      return false;
    }

    /// <summary>
    ///   Возвращает хэш-значение для текущего <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    /// </summary>
    /// <returns>
    ///   Хэш-значение для текущего <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecuritySafeCritical]
    public override int GetHashCode()
    {
      return this.url.GetHashCode();
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее ли указанный <see cref="T:System.Runtime.Remoting.Contexts.Context" /> соответствует <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />элемента требования.
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
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    [ComVisible(true)]
    public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
    {
      return false;
    }

    /// <summary>
    ///   Принудительно создает контекст и объект сервера в контексте по указанному URL-АДРЕСУ.
    /// </summary>
    /// <param name="ctorMsg">
    ///   <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> Для создания объекта сервера.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    [ComVisible(true)]
    public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
    {
    }

    /// <summary>
    ///   Возвращает значение URL-адреса <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    /// </summary>
    /// <returns>
    ///   Значение URL-адреса <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public string UrlValue
    {
      [SecurityCritical] get
      {
        return this.url;
      }
    }
  }
}
