// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.TargetFrameworkAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Versioning
{
  /// <summary>
  ///   Определяет версию платформы .NET Framework, для которой была скомпилирована соответствующая сборка.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class TargetFrameworkAttribute : Attribute
  {
    private string _frameworkName;
    private string _frameworkDisplayName;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Versioning.TargetFrameworkAttribute" /> класса с указанием версии .NET Framework, относительно которой построена сборка.
    /// </summary>
    /// <param name="frameworkName">
    ///   Версия платформы .NET Framework, относительно которой построена сборка.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="frameworkName" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public TargetFrameworkAttribute(string frameworkName)
    {
      if (frameworkName == null)
        throw new ArgumentNullException(nameof (frameworkName));
      this._frameworkName = frameworkName;
    }

    /// <summary>
    ///   Возвращает имя версии платформы .NET Framework, для которой скомпилирована определенная сборка.
    /// </summary>
    /// <returns>
    ///   Имя версии платформы .NET Framework, с которой выполнялась компиляция.
    /// </returns>
    [__DynamicallyInvokable]
    public string FrameworkName
    {
      [__DynamicallyInvokable] get
      {
        return this._frameworkName;
      }
    }

    /// <summary>
    ///   Получает отображаемое имя версии платформы .NET Framework, относительно которой построена сборка.
    /// </summary>
    /// <returns>Отображаемое имя версии платформы .NET Framework.</returns>
    [__DynamicallyInvokable]
    public string FrameworkDisplayName
    {
      [__DynamicallyInvokable] get
      {
        return this._frameworkDisplayName;
      }
      [__DynamicallyInvokable] set
      {
        this._frameworkDisplayName = value;
      }
    }
  }
}
