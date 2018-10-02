// Decompiled with JetBrains decompiler
// Type: System.Runtime.AssemblyTargetedPatchBandAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime
{
  /// <summary>
  ///   Задает сведения диапазона исправления для выполнения целевых исправлений платформы .NET Framework.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  public sealed class AssemblyTargetedPatchBandAttribute : Attribute
  {
    private string m_targetedPatchBand;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.AssemblyTargetedPatchBandAttribute" />.
    /// </summary>
    /// <param name="targetedPatchBand">Диапазон исправления.</param>
    public AssemblyTargetedPatchBandAttribute(string targetedPatchBand)
    {
      this.m_targetedPatchBand = targetedPatchBand;
    }

    /// <summary>Возвращает диапазон исправления.</summary>
    /// <returns>Сведения о диапазоне исправления.</returns>
    public string TargetedPatchBand
    {
      get
      {
        return this.m_targetedPatchBand;
      }
    }
  }
}
