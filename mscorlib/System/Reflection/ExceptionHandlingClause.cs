// Decompiled with JetBrains decompiler
// Type: System.Reflection.ExceptionHandlingClause
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Представляет предложение в структурированный блок обработки исключений.
  /// </summary>
  [ComVisible(true)]
  public class ExceptionHandlingClause
  {
    private MethodBody m_methodBody;
    private ExceptionHandlingClauseOptions m_flags;
    private int m_tryOffset;
    private int m_tryLength;
    private int m_handlerOffset;
    private int m_handlerLength;
    private int m_catchMetadataToken;
    private int m_filterOffset;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.ExceptionHandlingClause" />.
    /// </summary>
    protected ExceptionHandlingClause()
    {
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли это предложение обработки исключений и, наконец, предложение с фильтрацией по типу или пользовательской фильтрацией предложением.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.ExceptionHandlingClauseOptions" /> Значение, указывающее, какие действия выполняет это предложение.
    /// </returns>
    public virtual ExceptionHandlingClauseOptions Flags
    {
      get
      {
        return this.m_flags;
      }
    }

    /// <summary>
    ///   Смещение в методе, в байтах, блока try, содержащего это предложение обработки исключений.
    /// </summary>
    /// <returns>
    ///   Целое число, представляющее смещение в методе, в байтах, блока try, содержащего это предложение обработки исключений.
    /// </returns>
    public virtual int TryOffset
    {
      get
      {
        return this.m_tryOffset;
      }
    }

    /// <summary>
    ///   Общая длина в байтах блока try, содержащего это предложение обработки исключений.
    /// </summary>
    /// <returns>
    ///   Общая длина в байтах блока try, содержащего это предложение обработки исключений.
    /// </returns>
    public virtual int TryLength
    {
      get
      {
        return this.m_tryLength;
      }
    }

    /// <summary>
    ///   Возвращает смещение в теле метода в байтах этого предложения обработки исключений.
    /// </summary>
    /// <returns>
    ///   Целое число, представляющее смещение в теле метода в байтах этого предложения обработки исключений.
    /// </returns>
    public virtual int HandlerOffset
    {
      get
      {
        return this.m_handlerOffset;
      }
    }

    /// <summary>
    ///   Получает длину в байтах тела этого предложения обработки исключений.
    /// </summary>
    /// <returns>
    ///   Целое число, представляющее длину в байтах кода MSIL, образующего тело этого предложения обработки исключений.
    /// </returns>
    public virtual int HandlerLength
    {
      get
      {
        return this.m_handlerLength;
      }
    }

    /// <summary>
    ///   Возвращает смещение в теле метода в байтах кода пользовательского фильтра.
    /// </summary>
    /// <returns>
    ///   Смещение в теле метода в байтах кода пользовательского фильтра.
    ///    Значение этого свойства не имеет смысла при <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> свойство имеет любое значение, отличное от <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Не удается получить смещение, так как предложение обработки исключения не является фильтром.
    /// </exception>
    public virtual int FilterOffset
    {
      get
      {
        if (this.m_flags != ExceptionHandlingClauseOptions.Filter)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_EHClauseNotFilter"));
        return this.m_filterOffset;
      }
    }

    /// <summary>
    ///   Возвращает тип исключения, обрабатываемого этим предложением.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" /> объект, представляющий этот тип исключения, обрабатываемого этим предложением или <see langword="null" /> при <see cref="P:System.Reflection.ExceptionHandlingClause.Flags" /> свойство <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Filter" /> или <see cref="F:System.Reflection.ExceptionHandlingClauseOptions.Finally" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Недопустимое использование свойства для текущего состояния объекта.
    /// </exception>
    public virtual Type CatchType
    {
      get
      {
        if (this.m_flags != ExceptionHandlingClauseOptions.Clause)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_EHClauseNotClause"));
        Type type = (Type) null;
        if (!MetadataToken.IsNullToken(this.m_catchMetadataToken))
        {
          Type declaringType = this.m_methodBody.m_methodBase.DeclaringType;
          type = (declaringType == (Type) null ? this.m_methodBody.m_methodBase.Module : declaringType.Module).ResolveType(this.m_catchMetadataToken, declaringType == (Type) null ? (Type[]) null : declaringType.GetGenericArguments(), (object) (this.m_methodBody.m_methodBase as MethodInfo) != null ? this.m_methodBody.m_methodBase.GetGenericArguments() : (Type[]) null);
        }
        return type;
      }
    }

    /// <summary>
    ///   Строковое представление предложения обработки исключений.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая значения соответствующих свойств для типа предложения фильтра.
    /// </returns>
    public override string ToString()
    {
      if (this.Flags == ExceptionHandlingClauseOptions.Clause)
        return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, CatchType={5}", (object) this.Flags, (object) this.TryOffset, (object) this.TryLength, (object) this.HandlerOffset, (object) this.HandlerLength, (object) this.CatchType);
      if (this.Flags == ExceptionHandlingClauseOptions.Filter)
        return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}, FilterOffset={5}", (object) this.Flags, (object) this.TryOffset, (object) this.TryLength, (object) this.HandlerOffset, (object) this.HandlerLength, (object) this.FilterOffset);
      return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, "Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}", (object) this.Flags, (object) this.TryOffset, (object) this.TryLength, (object) this.HandlerOffset, (object) this.HandlerLength);
    }
  }
}
