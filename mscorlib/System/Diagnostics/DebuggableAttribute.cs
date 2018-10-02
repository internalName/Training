// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggableAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>
  ///   Изменяет генерацию кода для JIT-отладки во время выполнения.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DebuggableAttribute : Attribute
  {
    private DebuggableAttribute.DebuggingModes m_debuggingModes;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.DebuggableAttribute" />, используя заданные для JIT-компилятора параметры отслеживания и оптимизации.
    /// </summary>
    /// <param name="isJITTrackingEnabled">
    ///   Значение <see langword="true" /> для включения отладки; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <param name="isJITOptimizerDisabled">
    ///   Значение <see langword="true" /> для отключения оптимизатора выполнения; в противном случае — значение <see langword="false" />.
    /// </param>
    public DebuggableAttribute(bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
    {
      this.m_debuggingModes = DebuggableAttribute.DebuggingModes.None;
      if (isJITTrackingEnabled)
        this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.Default;
      if (!isJITOptimizerDisabled)
        return;
      this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.DisableOptimizations;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.DebuggableAttribute" />, используя заданные для JIT-компилятора режимы отладки.
    /// </summary>
    /// <param name="modes">
    ///   Побитовая комбинация значений <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" />, определяющих режим отладки для JIT-компилятора.
    /// </param>
    [__DynamicallyInvokable]
    public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
    {
      this.m_debuggingModes = modes;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, должна ли среда выполнения отслеживать для отладчика сведения во время создания кода.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если среда выполнения будет отслеживать для отладчика сведения во время создания кода; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsJITTrackingEnabled
    {
      get
      {
        return (uint) (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.Default) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, отключен ли оптимизатор среды выполнения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если оптимизатор во время выполнения отключен; в противном случае — <see langword="false" />.
    /// </returns>
    public bool IsJITOptimizerDisabled
    {
      get
      {
        return (uint) (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.DisableOptimizations) > 0U;
      }
    }

    /// <summary>Возвращает для атрибута режим отладки.</summary>
    /// <returns>
    ///   Поразрядное сочетание значений <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" />, описывающих режим отладки для JIT-компилятора.
    ///    Значение по умолчанию — <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.Default" />.
    /// </returns>
    public DebuggableAttribute.DebuggingModes DebuggingFlags
    {
      get
      {
        return this.m_debuggingModes;
      }
    }

    /// <summary>Задает режим отладки для JIT-компилятора.</summary>
    [Flags]
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public enum DebuggingModes
    {
      [__DynamicallyInvokable] None = 0,
      [__DynamicallyInvokable] Default = 1,
      [__DynamicallyInvokable] DisableOptimizations = 256, // 0x00000100
      [__DynamicallyInvokable] IgnoreSymbolStoreSequencePoints = 2,
      [__DynamicallyInvokable] EnableEditAndContinue = 4,
    }
  }
}
