// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerDisplayAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>
  ///   Определяет, как класс или поле отображается в окнах переменных отладчика.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Delegate, AllowMultiple = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DebuggerDisplayAttribute : Attribute
  {
    private string name;
    private string value;
    private string type;
    private string targetName;
    private System.Type target;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.DebuggerDisplayAttribute" />.
    /// </summary>
    /// <param name="value">
    ///   Строка, отображаемая в столбце значений для экземпляров типа; пустая строка ("») столбец значений будут скрыты.
    /// </param>
    [__DynamicallyInvokable]
    public DebuggerDisplayAttribute(string value)
    {
      this.value = value != null ? value : "";
      this.name = "";
      this.type = "";
    }

    /// <summary>
    ///   Возвращает строку, отображаемую в столбце значений в окнах переменных отладчика.
    /// </summary>
    /// <returns>
    ///   Строка, отображаемая в столбце значений переменных отладчика.
    /// </returns>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this.value;
      }
    }

    /// <summary>
    ///   Возвращает или задает имя, отображаемое в окнах переменных отладчика.
    /// </summary>
    /// <returns>Имя, отображаемое в окнах переменных отладчика.</returns>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        return this.name;
      }
      [__DynamicallyInvokable] set
      {
        this.name = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает строку, отображаемую в столбце типа в окнах переменных отладчика.
    /// </summary>
    /// <returns>
    ///   Строка, отображаемая в столбце типа в окнах переменных отладчика.
    /// </returns>
    [__DynamicallyInvokable]
    public string Type
    {
      [__DynamicallyInvokable] get
      {
        return this.type;
      }
      [__DynamicallyInvokable] set
      {
        this.type = value;
      }
    }

    /// <summary>Возвращает или задает тип целевого атрибута.</summary>
    /// <returns>Тип целевого объекта атрибута.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметру <see cref="P:System.Diagnostics.DebuggerDisplayAttribute.Target" /> задается значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public System.Type Target
    {
      [__DynamicallyInvokable] set
      {
        if (value == (System.Type) null)
          throw new ArgumentNullException(nameof (value));
        this.targetName = value.AssemblyQualifiedName;
        this.target = value;
      }
      [__DynamicallyInvokable] get
      {
        return this.target;
      }
    }

    /// <summary>
    ///   Возвращает или задает имя типа целевого объекта атрибута.
    /// </summary>
    /// <returns>Имя типа целевого объекта атрибута.</returns>
    [__DynamicallyInvokable]
    public string TargetTypeName
    {
      [__DynamicallyInvokable] get
      {
        return this.targetName;
      }
      [__DynamicallyInvokable] set
      {
        this.targetName = value;
      }
    }
  }
}
