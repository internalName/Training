// Decompiled with JetBrains decompiler
// Type: System.Reflection.ObfuscateAssemblyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Указывает, что средства запутывания использовать подходящие правила запутывания для соответствующего типа сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [ComVisible(true)]
  public sealed class ObfuscateAssemblyAttribute : Attribute
  {
    private bool m_strip = true;
    private bool m_assemblyIsPrivate;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.ObfuscateAssemblyAttribute" /> класс, определяющим, является ли сборка быть скрыт открытым или закрытым.
    /// </summary>
    /// <param name="assemblyIsPrivate">
    ///   <see langword="true" /> Если сборка используется в области одного приложения; в противном случае — <see langword="false" />.
    /// </param>
    public ObfuscateAssemblyAttribute(bool assemblyIsPrivate)
    {
      this.m_assemblyIsPrivate = assemblyIsPrivate;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Boolean" /> значение, указывающее, является ли сборка отмечена как закрытая.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если сборка отмечена как закрытая; в противном случае — <see langword="false" />.
    /// </returns>
    public bool AssemblyIsPrivate
    {
      get
      {
        return this.m_assemblyIsPrivate;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Boolean" /> значение, указывающее, является ли средство запутывания должно удалять атрибут после обработки.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если средство запутывания должно удалять атрибут после обработки; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию для этого свойства — <see langword="true" />.
    /// </returns>
    public bool StripAfterObfuscation
    {
      get
      {
        return this.m_strip;
      }
      set
      {
        this.m_strip = value;
      }
    }
  }
}
