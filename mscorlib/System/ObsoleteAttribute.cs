// Decompiled with JetBrains decompiler
// Type: System.ObsoleteAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Отмечает элементы программы, которые больше не используются.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class ObsoleteAttribute : Attribute
  {
    private string _message;
    private bool _error;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ObsoleteAttribute" /> стандартными свойствами.
    /// </summary>
    [__DynamicallyInvokable]
    public ObsoleteAttribute()
    {
      this._message = (string) null;
      this._error = false;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ObsoleteAttribute" /> класса с указанным сообщением обхода.
    /// </summary>
    /// <param name="message">
    ///   Текстовая строка, описывающая альтернативные обходные пути.
    /// </param>
    [__DynamicallyInvokable]
    public ObsoleteAttribute(string message)
    {
      this._message = message;
      this._error = false;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ObsoleteAttribute" /> класса с сообщением обходной путь и логическое значение, указывающее, является ли использование устаревшего элемента рассматривается как ошибка.
    /// </summary>
    /// <param name="message">
    ///   Текстовая строка, описывающая альтернативные обходные пути.
    /// </param>
    /// <param name="error">
    ///   <see langword="true" /> Если использование устаревшего элемента приводит к ошибке компилятора; <see langword="false" /> Если предупреждение компилятора.
    /// </param>
    [__DynamicallyInvokable]
    public ObsoleteAttribute(string message, bool error)
    {
      this._message = message;
      this._error = error;
    }

    /// <summary>
    ///   Получает сообщение обхода, содержащее описание альтернативных элементов программы.
    /// </summary>
    /// <returns>Текстовая строка обхода.</returns>
    [__DynamicallyInvokable]
    public string Message
    {
      [__DynamicallyInvokable] get
      {
        return this._message;
      }
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, будет ли компилятор считать использование устаревшего элемента программы ошибкой.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если использование устаревшего элемента рассматривается как ошибка; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsError
    {
      [__DynamicallyInvokable] get
      {
        return this._error;
      }
    }
  }
}
