// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyDelaySignAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Указывает, что сборка не подписывается полностью при создании.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyDelaySignAttribute : Attribute
  {
    private bool m_delaySign;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AssemblyDelaySignAttribute" />.
    /// </summary>
    /// <param name="delaySign">
    ///   <see langword="true" /> Если этот атрибут представляет компонент активирован; в противном случае — <see langword="false" />.
    /// </param>
    [__DynamicallyInvokable]
    public AssemblyDelaySignAttribute(bool delaySign)
    {
      this.m_delaySign = delaySign;
    }

    /// <summary>
    ///   Возвращает значение, показывающее состояние атрибута.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если эта сборка была собрана с как отложенной подписью; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool DelaySign
    {
      [__DynamicallyInvokable] get
      {
        return this.m_delaySign;
      }
    }
  }
}
