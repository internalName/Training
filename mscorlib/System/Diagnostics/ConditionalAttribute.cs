// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.ConditionalAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>
  ///   Указывает компиляторам, что вызов метода или атрибут следует игнорировать, если не определен заданный символ условной компиляции.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class ConditionalAttribute : Attribute
  {
    private string m_conditionString;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.ConditionalAttribute" />.
    /// </summary>
    /// <param name="conditionString">
    ///   Строка, указывающая, связанный с атрибутом символ условной компиляции с учетом регистра.
    /// </param>
    [__DynamicallyInvokable]
    public ConditionalAttribute(string conditionString)
    {
      this.m_conditionString = conditionString;
    }

    /// <summary>
    ///   Возвращает символ условной компиляции, с которым связан <see cref="T:System.Diagnostics.ConditionalAttribute" /> атрибута.
    /// </summary>
    /// <returns>
    ///   Строка, указывающая символ условной компиляции с учетом регистра, связанные с <see cref="T:System.Diagnostics.ConditionalAttribute" /> атрибута.
    /// </returns>
    [__DynamicallyInvokable]
    public string ConditionString
    {
      [__DynamicallyInvokable] get
      {
        return this.m_conditionString;
      }
    }
  }
}
