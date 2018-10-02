// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.ComponentGuaranteesAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Versioning
{
  /// <summary>
  ///   Определяет гарантированную совместимость компонента, типа или члена типа, который может занимать несколько версий.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  public sealed class ComponentGuaranteesAttribute : Attribute
  {
    private ComponentGuaranteesOptions _guarantees;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Versioning.ComponentGuaranteesAttribute" /> значение, указывающее, библиотеки, типа или члена класса гарантированный уровень совместимости нескольких версий.
    /// </summary>
    /// <param name="guarantees">
    ///   Одно из значений перечисления, указывающее уровень совместимости, гарантирующий для нескольких версий.
    /// </param>
    public ComponentGuaranteesAttribute(ComponentGuaranteesOptions guarantees)
    {
      this._guarantees = guarantees;
    }

    /// <summary>
    ///   Возвращает значение, указывающее гарантированный уровень совместимости библиотеки, типа или члена типа с несколькими версиями.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, указывающее уровень совместимости, гарантирующий для нескольких версий.
    /// </returns>
    public ComponentGuaranteesOptions Guarantees
    {
      get
      {
        return this._guarantees;
      }
    }
  }
}
