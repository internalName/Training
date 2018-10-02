// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, что сборка с данным атрибутом является основной сборки взаимодействия.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  public sealed class PrimaryInteropAssemblyAttribute : Attribute
  {
    internal int _major;
    internal int _minor;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.PrimaryInteropAssemblyAttribute" /> класса с номерами основной и дополнительный номер версии библиотеки типов, для которой эта сборка является основной сборкой взаимодействия.
    /// </summary>
    /// <param name="major">
    ///   Основной номер версии библиотеки типов, для которой эта сборка является основной сборкой взаимодействия.
    /// </param>
    /// <param name="minor">
    ///   Дополнительный номер версии библиотеки типов, для которой эта сборка является основной сборкой взаимодействия.
    /// </param>
    public PrimaryInteropAssemblyAttribute(int major, int minor)
    {
      this._major = major;
      this._minor = minor;
    }

    /// <summary>
    ///   Возвращает основной номер версии библиотеки типов, для которой эта сборка является основной сборкой взаимодействия.
    /// </summary>
    /// <returns>
    ///   Основной номер версии библиотеки типов, для которой эта сборка является основной сборкой взаимодействия.
    /// </returns>
    public int MajorVersion
    {
      get
      {
        return this._major;
      }
    }

    /// <summary>
    ///   Возвращает дополнительный номер версии библиотеки типов, для которой эта сборка является основной сборкой взаимодействия.
    /// </summary>
    /// <returns>
    ///   Дополнительный номер версии библиотеки типов, для которой эта сборка является основной сборкой взаимодействия.
    /// </returns>
    public int MinorVersion
    {
      get
      {
        return this._minor;
      }
    }
  }
}
