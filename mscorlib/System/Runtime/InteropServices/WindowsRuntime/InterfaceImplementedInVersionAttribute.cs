// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.InterfaceImplementedInVersionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>
  ///   Указывает версию целевого типа, который первым реализовал указанный интерфейс.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class InterfaceImplementedInVersionAttribute : Attribute
  {
    private Type m_interfaceType;
    private byte m_majorVersion;
    private byte m_minorVersion;
    private byte m_buildVersion;
    private byte m_revisionVersion;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.WindowsRuntime.InterfaceImplementedInVersionAttribute" /> класс, интерфейс, который реализует целевого типа и версии, в которой впервые был реализован этот интерфейс.
    /// </summary>
    /// <param name="interfaceType">
    ///   Интерфейс, который впервые был реализован в указанной версии типа целевого объекта.
    /// </param>
    /// <param name="majorVersion">
    ///   Основной компонент версии типа целевого объекта, который первым реализовал <paramref name="interfaceType" />.
    /// </param>
    /// <param name="minorVersion">
    ///   Компонент дополнительный номер версии целевого типа, который первым реализовал <paramref name="interfaceType" />.
    /// </param>
    /// <param name="buildVersion">
    ///   Компонент построения версии типа целевого объекта, который первым реализовал <paramref name="interfaceType" />.
    /// </param>
    /// <param name="revisionVersion">
    ///   Редакции — компонент версии типа целевого объекта, который первым реализовал <paramref name="interfaceType" />.
    /// </param>
    [__DynamicallyInvokable]
    public InterfaceImplementedInVersionAttribute(Type interfaceType, byte majorVersion, byte minorVersion, byte buildVersion, byte revisionVersion)
    {
      this.m_interfaceType = interfaceType;
      this.m_majorVersion = majorVersion;
      this.m_minorVersion = minorVersion;
      this.m_buildVersion = buildVersion;
      this.m_revisionVersion = revisionVersion;
    }

    /// <summary>
    ///   Возвращает тип интерфейса, который реализует тип целевого объекта.
    /// </summary>
    /// <returns>Тип интерфейса.</returns>
    [__DynamicallyInvokable]
    public Type InterfaceType
    {
      [__DynamicallyInvokable] get
      {
        return this.m_interfaceType;
      }
    }

    /// <summary>
    ///   Возвращает основной компонент версии типа целевого объекта, который первым реализовал интерфейс.
    /// </summary>
    /// <returns>Основной компонент версии.</returns>
    [__DynamicallyInvokable]
    public byte MajorVersion
    {
      [__DynamicallyInvokable] get
      {
        return this.m_majorVersion;
      }
    }

    /// <summary>
    ///   Возвращает компонент дополнительный номер версии целевого типа, который сначала реализации интерфейса.
    /// </summary>
    /// <returns>Компонент дополнительный номер версии.</returns>
    [__DynamicallyInvokable]
    public byte MinorVersion
    {
      [__DynamicallyInvokable] get
      {
        return this.m_minorVersion;
      }
    }

    /// <summary>
    ///   Возвращает компонент сборки версии типа целевого объекта, который первым реализовал интерфейс.
    /// </summary>
    /// <returns>Компонент версии сборки.</returns>
    [__DynamicallyInvokable]
    public byte BuildVersion
    {
      [__DynamicallyInvokable] get
      {
        return this.m_buildVersion;
      }
    }

    /// <summary>
    ///   Возвращает компонент редакции версии типа целевого объекта, который первым реализовал интерфейс.
    /// </summary>
    /// <returns>Компонент редакции версии.</returns>
    [__DynamicallyInvokable]
    public byte RevisionVersion
    {
      [__DynamicallyInvokable] get
      {
        return this.m_revisionVersion;
      }
    }
  }
}
