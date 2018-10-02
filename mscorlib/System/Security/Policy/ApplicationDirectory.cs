// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationDirectory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Предоставляет каталог приложения в качестве свидетельства для оценки политики.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ApplicationDirectory : EvidenceBase
  {
    private URLString m_appDirectory;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.ApplicationDirectory" />.
    /// </summary>
    /// <param name="name">Путь к каталогу приложения.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public ApplicationDirectory(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      this.m_appDirectory = new URLString(name);
    }

    private ApplicationDirectory(URLString appDirectory)
    {
      this.m_appDirectory = appDirectory;
    }

    /// <summary>Возвращает путь к каталогу приложения.</summary>
    /// <returns>Путь к каталогу приложения.</returns>
    public string Directory
    {
      get
      {
        return this.m_appDirectory.ToString();
      }
    }

    /// <summary>
    ///   Определяет, эквивалентны ли экземпляры того же типа объекта свидетельства.
    /// </summary>
    /// <param name="o">
    ///   Объект типа текущего объекта свидетельства.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если два экземпляра эквивалентны; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      ApplicationDirectory applicationDirectory = o as ApplicationDirectory;
      if (applicationDirectory == null)
        return false;
      return this.m_appDirectory.Equals(applicationDirectory.m_appDirectory);
    }

    /// <summary>Возвращает хэш-код текущего каталога приложения.</summary>
    /// <returns>Хэш-код текущего каталога приложения.</returns>
    public override int GetHashCode()
    {
      return this.m_appDirectory.GetHashCode();
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new ApplicationDirectory(this.m_appDirectory);
    }

    /// <summary>
    ///   Создает новую копию <see cref="T:System.Security.Policy.ApplicationDirectory" />.
    /// </summary>
    /// <returns>
    ///   Новая, идентичная копия <see cref="T:System.Security.Policy.ApplicationDirectory" />.
    /// </returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.ApplicationDirectory");
      securityElement.AddAttribute("version", "1");
      if (this.m_appDirectory != null)
        securityElement.AddChild(new SecurityElement("Directory", this.m_appDirectory.ToString()));
      return securityElement;
    }

    /// <summary>
    ///   Возвращает строковое представление состояния <see cref="T:System.Security.Policy.ApplicationDirectory" /> объект свидетельства.
    /// </summary>
    /// <returns>
    ///   Представление состояния <see cref="T:System.Security.Policy.ApplicationDirectory" /> объект свидетельства.
    /// </returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }
  }
}
