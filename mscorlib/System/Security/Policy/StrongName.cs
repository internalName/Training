// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.StrongName
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Предоставляет строгое имя сборки кода в качестве свидетельства для оценки политики.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class StrongName : EvidenceBase, IIdentityPermissionFactory, IDelayEvaluatedEvidence
  {
    private StrongNamePublicKeyBlob m_publicKeyBlob;
    private string m_name;
    private Version m_version;
    [NonSerialized]
    private RuntimeAssembly m_assembly;
    [NonSerialized]
    private bool m_wasUsed;

    internal StrongName()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.StrongName" /> класса объекта blob открытого ключа строгого имени, имени и версии.
    /// </summary>
    /// <param name="blob">
    ///   <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> Издателя программного обеспечения.
    /// </param>
    /// <param name="name">Секция простого имени строгое имя.</param>
    /// <param name="version">
    ///   <see cref="T:System.Version" /> Строгого имени.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="blob" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="version" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> представляет собой пустую строку ("").
    /// </exception>
    public StrongName(StrongNamePublicKeyBlob blob, string name, Version version)
      : this(blob, name, version, (Assembly) null)
    {
    }

    internal StrongName(StrongNamePublicKeyBlob blob, string name, Version version, Assembly assembly)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (string.IsNullOrEmpty(name))
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
      if (blob == null)
        throw new ArgumentNullException(nameof (blob));
      if (version == (Version) null)
        throw new ArgumentNullException(nameof (version));
      RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
      if (assembly != (Assembly) null && (Assembly) runtimeAssembly == (Assembly) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), nameof (assembly));
      this.m_publicKeyBlob = blob;
      this.m_name = name;
      this.m_version = version;
      this.m_assembly = runtimeAssembly;
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> для текущего экземпляра <see cref="T:System.Security.Policy.StrongName" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> Текущего <see cref="T:System.Security.Policy.StrongName" />.
    /// </returns>
    public StrongNamePublicKeyBlob PublicKey
    {
      get
      {
        return this.m_publicKeyBlob;
      }
    }

    /// <summary>
    ///   Получает простое имя текущего <see cref="T:System.Security.Policy.StrongName" />.
    /// </summary>
    /// <returns>
    ///   Часть простого имени <see cref="T:System.Security.Policy.StrongName" />.
    /// </returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Version" /> для текущего экземпляра <see cref="T:System.Security.Policy.StrongName" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Version" /> Текущего <see cref="T:System.Security.Policy.StrongName" />.
    /// </returns>
    public Version Version
    {
      get
      {
        return this.m_version;
      }
    }

    bool IDelayEvaluatedEvidence.IsVerified
    {
      [SecurityCritical] get
      {
        if (!((Assembly) this.m_assembly != (Assembly) null))
          return true;
        return this.m_assembly.IsStrongNameVerified;
      }
    }

    bool IDelayEvaluatedEvidence.WasUsed
    {
      get
      {
        return this.m_wasUsed;
      }
    }

    void IDelayEvaluatedEvidence.MarkUsed()
    {
      this.m_wasUsed = true;
    }

    internal static bool CompareNames(string asmName, string mcName)
    {
      if (mcName.Length > 0 && mcName[mcName.Length - 1] == '*' && mcName.Length - 1 <= asmName.Length)
        return string.Compare(mcName, 0, asmName, 0, mcName.Length - 1, StringComparison.OrdinalIgnoreCase) == 0;
      return string.Compare(mcName, asmName, StringComparison.OrdinalIgnoreCase) == 0;
    }

    /// <summary>
    ///   Создает <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> связанный с текущим <see cref="T:System.Security.Policy.StrongName" />.
    /// </summary>
    /// <param name="evidence">
    ///   <see cref="T:System.Security.Policy.Evidence" /> Для формирования <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> для указанного <see cref="T:System.Security.Policy.StrongName" />.
    /// </returns>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new StrongNameIdentityPermission(this.m_publicKeyBlob, this.m_name, this.m_version);
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new StrongName(this.m_publicKeyBlob, this.m_name, this.m_version);
    }

    /// <summary>
    ///   Создает эквивалентную копию текущего объекта <see cref="T:System.Security.Policy.StrongName" />.
    /// </summary>
    /// <returns>
    ///   Новая, идентичная копия текущего объекта <see cref="T:System.Security.Policy.StrongName" />.
    /// </returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement(nameof (StrongName));
      securityElement.AddAttribute("version", "1");
      if (this.m_publicKeyBlob != null)
        securityElement.AddAttribute("Key", Hex.EncodeHexString(this.m_publicKeyBlob.PublicKey));
      if (this.m_name != null)
        securityElement.AddAttribute("Name", this.m_name);
      if (this.m_version != (Version) null)
        securityElement.AddAttribute("Version", this.m_version.ToString());
      return securityElement;
    }

    internal void FromXml(SecurityElement element)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (string.Compare(element.Tag, nameof (StrongName), StringComparison.Ordinal) != 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
      this.m_publicKeyBlob = (StrongNamePublicKeyBlob) null;
      this.m_version = (Version) null;
      string hexString = element.Attribute("Key");
      if (hexString != null)
        this.m_publicKeyBlob = new StrongNamePublicKeyBlob(Hex.DecodeHexString(hexString));
      this.m_name = element.Attribute("Name");
      string version = element.Attribute("Version");
      if (version == null)
        return;
      this.m_version = new Version(version);
    }

    /// <summary>
    ///   Создает строковое представление текущего объекта <see cref="T:System.Security.Policy.StrongName" />.
    /// </summary>
    /// <returns>
    ///   Представление текущего <see cref="T:System.Security.Policy.StrongName" />.
    /// </returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    /// <summary>
    ///   Определяет, равен ли указанное строгое имя текущему строгому имени.
    /// </summary>
    /// <param name="o">
    ///   Строгое имя для сравнения с текущей строгого имени.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанное строгое имя равен текущему строгому имени; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      StrongName strongName = o as StrongName;
      if (strongName != null && object.Equals((object) this.m_publicKeyBlob, (object) strongName.m_publicKeyBlob) && object.Equals((object) this.m_name, (object) strongName.m_name))
        return object.Equals((object) this.m_version, (object) strongName.m_version);
      return false;
    }

    /// <summary>
    ///   Возвращает хэш-код текущего <see cref="T:System.Security.Policy.StrongName" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код текущего <see cref="T:System.Security.Policy.StrongName" />.
    /// </returns>
    public override int GetHashCode()
    {
      if (this.m_publicKeyBlob != null)
        return this.m_publicKeyBlob.GetHashCode();
      if (this.m_name != null || this.m_version != (Version) null)
        return (this.m_name == null ? 0 : this.m_name.GetHashCode()) + (this.m_version == (Version) null ? 0 : this.m_version.GetHashCode());
      return typeof (StrongName).GetHashCode();
    }

    internal object Normalize()
    {
      MemoryStream memoryStream = new MemoryStream();
      BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream);
      binaryWriter.Write(this.m_publicKeyBlob.PublicKey);
      binaryWriter.Write(this.m_version.Major);
      binaryWriter.Write(this.m_name);
      memoryStream.Position = 0L;
      return (object) memoryStream;
    }
  }
}
