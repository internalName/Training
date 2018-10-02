// Decompiled with JetBrains decompiler
// Type: System.Security.HostProtectionException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Security
{
  /// <summary>
  ///   Исключение, создаваемое при обнаружении ресурса запрещенного сайта.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class HostProtectionException : SystemException
  {
    private HostProtectionResource m_protected;
    private HostProtectionResource m_demanded;
    private const string ProtectedResourcesName = "ProtectedResources";
    private const string DemandedResourcesName = "DemandedResources";

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.HostProtectionException" /> со значениями по умолчанию.
    /// </summary>
    public HostProtectionException()
    {
      this.m_protected = HostProtectionResource.None;
      this.m_demanded = HostProtectionResource.None;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.HostProtectionException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public HostProtectionException(string message)
      : base(message)
    {
      this.m_protected = HostProtectionResource.None;
      this.m_demanded = HostProtectionResource.None;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.HostProtectionException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="e">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public HostProtectionException(string message, Exception e)
      : base(message, e)
    {
      this.m_protected = HostProtectionResource.None;
      this.m_demanded = HostProtectionResource.None;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.HostProtectionException" /> класса, используя предоставленные сведения о сериализации и контекст потоков.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    protected HostProtectionException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.m_protected = (HostProtectionResource) info.GetValue(nameof (ProtectedResources), typeof (HostProtectionResource));
      this.m_demanded = (HostProtectionResource) info.GetValue(nameof (DemandedResources), typeof (HostProtectionResource));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.HostProtectionException" /> класса с указанным сообщением об ошибке, защищенный узел ресурсы и ресурсы узла, вызвавшие исключения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="protectedResources">
    ///   Побитовое сочетание значений перечисления, указывающих ресурсы узла, недоступные коду с частичным доверием.
    /// </param>
    /// <param name="demandedResources">
    ///   Побитовое сочетание значений перечисления, указывающих ресурсы узла.
    /// </param>
    public HostProtectionException(string message, HostProtectionResource protectedResources, HostProtectionResource demandedResources)
      : base(message)
    {
      this.SetErrorCode(-2146232768);
      this.m_protected = protectedResources;
      this.m_demanded = demandedResources;
    }

    private HostProtectionException(HostProtectionResource protectedResources, HostProtectionResource demandedResources)
      : base(SecurityException.GetResString("HostProtection_HostProtection"))
    {
      this.SetErrorCode(-2146232768);
      this.m_protected = protectedResources;
      this.m_demanded = demandedResources;
    }

    /// <summary>
    ///   Возвращает или задает узел защиты ресурсов, недоступные для частично доверенного кода.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание <see cref="T:System.Security.Permissions.HostProtectionResource" /> значений, определяющих категории защиты узла недоступен.
    ///    Значение по умолчанию — <see cref="F:System.Security.Permissions.HostProtectionResource.None" />.
    /// </returns>
    public HostProtectionResource ProtectedResources
    {
      get
      {
        return this.m_protected;
      }
    }

    /// <summary>
    ///   Возвращает или задает ресурсы защиты, вызвавших исключение узла.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание <see cref="T:System.Security.Permissions.HostProtectionResource" /> значений, определяющих защиты ресурсов, что вызывает исключение.
    ///    Значение по умолчанию — <see cref="F:System.Security.Permissions.HostProtectionResource.None" />.
    /// </returns>
    public HostProtectionResource DemandedResources
    {
      get
      {
        return this.m_demanded;
      }
    }

    private string ToStringHelper(string resourceString, object attr)
    {
      if (attr == null)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append(Environment.GetResourceString(resourceString));
      stringBuilder.Append(Environment.NewLine);
      stringBuilder.Append(attr);
      return stringBuilder.ToString();
    }

    /// <summary>
    ///   Возвращает строковое представление текущего исключения защиты узла.
    /// </summary>
    /// <returns>
    ///   Строковое представление текущего объекта <see cref="T:System.Security.HostProtectionException" />.
    /// </returns>
    public override string ToString()
    {
      string stringHelper = this.ToStringHelper("HostProtection_ProtectedResources", (object) this.ProtectedResources);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(base.ToString());
      stringBuilder.Append(stringHelper);
      stringBuilder.Append(this.ToStringHelper("HostProtection_DemandedResources", (object) this.DemandedResources));
      return stringBuilder.ToString();
    }

    /// <summary>
    ///   Задает указанный <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект со сведениями об исключении защиты узла.
    /// </summary>
    /// <param name="info">
    ///   Данные сериализованного объекта о вызываемом исключении.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      base.GetObjectData(info, context);
      info.AddValue("ProtectedResources", (object) this.ProtectedResources, typeof (HostProtectionResource));
      info.AddValue("DemandedResources", (object) this.DemandedResources, typeof (HostProtectionResource));
    }
  }
}
