// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.CodeAnalysis.SuppressMessageAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Diagnostics.CodeAnalysis
{
  /// <summary>
  ///   Подавляет создание отчетов о нарушении правил определенного средства статического анализа, допуская применение нескольких операций подавления к одному артефакту кода.
  /// </summary>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
  [Conditional("CODE_ANALYSIS")]
  [__DynamicallyInvokable]
  public sealed class SuppressMessageAttribute : Attribute
  {
    private string category;
    private string justification;
    private string checkId;
    private string scope;
    private string target;
    private string messageId;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.CodeAnalysis.SuppressMessageAttribute" /> класс, указав категорию средство статического анализа и идентификатор для правила анализа.
    /// </summary>
    /// <param name="category">Категория для атрибута.</param>
    /// <param name="checkId">
    ///   Идентификатор правила инструмента анализа атрибут применяется к.
    /// </param>
    [__DynamicallyInvokable]
    public SuppressMessageAttribute(string category, string checkId)
    {
      this.category = category;
      this.checkId = checkId;
    }

    /// <summary>
    ///   Возвращает категорию, определяющую классификацию атрибута.
    /// </summary>
    /// <returns>Категория, определяющая атрибут.</returns>
    [__DynamicallyInvokable]
    public string Category
    {
      [__DynamicallyInvokable] get
      {
        return this.category;
      }
    }

    /// <summary>
    ///   Возвращает идентификатор правила средство статического анализа для подавления.
    /// </summary>
    /// <returns>
    ///   Идентификатор правила инструмента анализа должны быть отключены.
    /// </returns>
    [__DynamicallyInvokable]
    public string CheckId
    {
      [__DynamicallyInvokable] get
      {
        return this.checkId;
      }
    }

    /// <summary>
    ///   Возвращает или задает область кода, который имеет значение для атрибута.
    /// </summary>
    /// <returns>Область кода, который имеет значение для атрибута.</returns>
    [__DynamicallyInvokable]
    public string Scope
    {
      [__DynamicallyInvokable] get
      {
        return this.scope;
      }
      [__DynamicallyInvokable] set
      {
        this.scope = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает полный путь, представляющий целевой объект атрибута.
    /// </summary>
    /// <returns>
    ///   Полный путь, представляющий целевой объект атрибута.
    /// </returns>
    [__DynamicallyInvokable]
    public string Target
    {
      [__DynamicallyInvokable] get
      {
        return this.target;
      }
      [__DynamicallyInvokable] set
      {
        this.target = value;
      }
    }

    /// <summary>
    ///   Получает или задает необязательное расширение аргумента для критерия исключения.
    /// </summary>
    /// <returns>Строка, содержащая расширенный критерий исключения.</returns>
    [__DynamicallyInvokable]
    public string MessageId
    {
      [__DynamicallyInvokable] get
      {
        return this.messageId;
      }
      [__DynamicallyInvokable] set
      {
        this.messageId = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает причину подавления сообщения анализа кода.
    /// </summary>
    /// <returns>Причина подавления сообщения.</returns>
    [__DynamicallyInvokable]
    public string Justification
    {
      [__DynamicallyInvokable] get
      {
        return this.justification;
      }
      [__DynamicallyInvokable] set
      {
        this.justification = value;
      }
    }
  }
}
