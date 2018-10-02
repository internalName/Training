// Decompiled with JetBrains decompiler
// Type: System.Runtime.TargetedPatchingOptOutAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime
{
  /// <summary>
  ///   Указывает, что метод библиотеки классов .NET Framework, к которому применяется этот атрибут вряд ли будут затронуты обслуживания выпусков и поэтому может быть встроенным по образов генератор машинных образов (NGen).
  /// </summary>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  public sealed class TargetedPatchingOptOutAttribute : Attribute
  {
    private string m_reason;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.TargetedPatchingOptOutAttribute" />.
    /// </summary>
    /// <param name="reason">
    ///   Причина почему метод, который <see cref="T:System.Runtime.TargetedPatchingOptOutAttribute" /> атрибут считается для встраивания в образы генератор машинных образов (NGen).
    /// </param>
    public TargetedPatchingOptOutAttribute(string reason)
    {
      this.m_reason = reason;
    }

    /// <summary>
    ///   Возвращает причину, почему считается метода, к которому применяется этот атрибут для встраивания в образы генератор машинных образов (NGen).
    /// </summary>
    /// <returns>
    ///   Причина, почему метод считается для встраивания в образы NGen.
    /// </returns>
    public string Reason
    {
      get
      {
        return this.m_reason;
      }
    }

    private TargetedPatchingOptOutAttribute()
    {
    }
  }
}
