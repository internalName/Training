// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ExceptionHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Представляет обработчик исключений в массиве байтов IL для передачи методу, такому как <see cref="M:System.Reflection.Emit.MethodBuilder.SetMethodBody(System.Byte[],System.Int32,System.Byte[],System.Collections.Generic.IEnumerable{System.Reflection.Emit.ExceptionHandler},System.Collections.Generic.IEnumerable{System.Int32})" />.
  /// </summary>
  [ComVisible(false)]
  public struct ExceptionHandler : IEquatable<ExceptionHandler>
  {
    internal readonly int m_exceptionClass;
    internal readonly int m_tryStartOffset;
    internal readonly int m_tryEndOffset;
    internal readonly int m_filterOffset;
    internal readonly int m_handlerStartOffset;
    internal readonly int m_handlerEndOffset;
    internal readonly ExceptionHandlingClauseOptions m_kind;

    /// <summary>
    ///   Возвращает токен типа исключения обрабатываются этим обработчиком.
    /// </summary>
    /// <returns>
    ///   Токен типа исключения обрабатываются этот обработчик, или значение 0, если она не существует.
    /// </returns>
    public int ExceptionTypeToken
    {
      get
      {
        return this.m_exceptionClass;
      }
    }

    /// <summary>
    ///   Возвращает смещение байтов, с которого начинается код, который защищен этого обработчика исключений.
    /// </summary>
    /// <returns>
    ///   Смещение в байтах, с которой начинается код, который защищен этого обработчика исключений.
    /// </returns>
    public int TryOffset
    {
      get
      {
        return this.m_tryStartOffset;
      }
    }

    /// <summary>
    ///   Получает длину в байтах кода, защищенного этим обработчиком исключений.
    /// </summary>
    /// <returns>
    ///   Длина кода, защищенного этого обработчика исключений в байтах.
    /// </returns>
    public int TryLength
    {
      get
      {
        return this.m_tryEndOffset - this.m_tryStartOffset;
      }
    }

    /// <summary>
    ///   Возвращает смещение байтов, с которого начинается код фильтра для обработчика исключений.
    /// </summary>
    /// <returns>
    ///   Смещение байтов, с которого начинается код фильтра, или 0, если фильтр не присутствует.
    /// </returns>
    public int FilterOffset
    {
      get
      {
        return this.m_filterOffset;
      }
    }

    /// <summary>
    ///   Возвращает смещение в байтах первой инструкции обработчика исключений.
    /// </summary>
    /// <returns>
    ///   Смещение в байтах первой инструкции обработчика исключений.
    /// </returns>
    public int HandlerOffset
    {
      get
      {
        return this.m_handlerStartOffset;
      }
    }

    /// <summary>Получает длину в байтах обработчика исключений.</summary>
    /// <returns>Длина в байтах, обработчика исключений.</returns>
    public int HandlerLength
    {
      get
      {
        return this.m_handlerEndOffset - this.m_handlerStartOffset;
      }
    }

    /// <summary>
    ///   Возвращает значение, представляющее тип обработчика исключений, который представляет этот объект.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, указывающее тип обработчика исключений.
    /// </returns>
    public ExceptionHandlingClauseOptions Kind
    {
      get
      {
        return this.m_kind;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.Emit.ExceptionHandler" /> с заданными параметрами.
    /// </summary>
    /// <param name="tryOffset">
    ///   Смещение в байтах первой инструкции, защищенные этим обработчиком исключений.
    /// </param>
    /// <param name="tryLength">
    ///   Число байтов, защищенные этим обработчиком исключений.
    /// </param>
    /// <param name="filterOffset">
    ///   Смещение байтов начала кода фильтра.
    ///    Код фильтра заканчивается в первой инструкции блока обработчика.
    ///    Для обработчиков исключений без фильтра укажите 0 (нуль) для этого параметра.
    /// </param>
    /// <param name="handlerOffset">
    ///   Смещение в байтах первой инструкции этого обработчика исключений.
    /// </param>
    /// <param name="handlerLength">
    ///   Число байтов в этом обработчике исключений.
    /// </param>
    /// <param name="kind">
    ///   Одно из значений перечисления, указывающее тип обработчика исключений.
    /// </param>
    /// <param name="exceptionTypeToken">
    ///   Токен тип исключения, обрабатываемый этого обработчика исключений.
    ///    Если не применяется, укажите 0 (ноль).
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="tryOffset" />, <paramref name="filterOffset" />, <paramref name="handlerOffset" />, <paramref name="tryLength" />, или <paramref name="handlerLength" /> являются отрицательными.
    /// </exception>
    public ExceptionHandler(int tryOffset, int tryLength, int filterOffset, int handlerOffset, int handlerLength, ExceptionHandlingClauseOptions kind, int exceptionTypeToken)
    {
      if (tryOffset < 0)
        throw new ArgumentOutOfRangeException(nameof (tryOffset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (tryLength < 0)
        throw new ArgumentOutOfRangeException(nameof (tryLength), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (filterOffset < 0)
        throw new ArgumentOutOfRangeException(nameof (filterOffset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (handlerOffset < 0)
        throw new ArgumentOutOfRangeException(nameof (handlerOffset), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (handlerLength < 0)
        throw new ArgumentOutOfRangeException(nameof (handlerLength), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if ((long) tryOffset + (long) tryLength > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (tryLength), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) (int.MaxValue - tryOffset)));
      if ((long) handlerOffset + (long) handlerLength > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (handlerLength), Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) (int.MaxValue - handlerOffset)));
      if (kind == ExceptionHandlingClauseOptions.Clause && (exceptionTypeToken & 16777215) == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeToken", (object) exceptionTypeToken), nameof (exceptionTypeToken));
      if (!ExceptionHandler.IsValidKind(kind))
        throw new ArgumentOutOfRangeException(nameof (kind), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      this.m_tryStartOffset = tryOffset;
      this.m_tryEndOffset = tryOffset + tryLength;
      this.m_filterOffset = filterOffset;
      this.m_handlerStartOffset = handlerOffset;
      this.m_handlerEndOffset = handlerOffset + handlerLength;
      this.m_kind = kind;
      this.m_exceptionClass = exceptionTypeToken;
    }

    internal ExceptionHandler(int tryStartOffset, int tryEndOffset, int filterOffset, int handlerStartOffset, int handlerEndOffset, int kind, int exceptionTypeToken)
    {
      this.m_tryStartOffset = tryStartOffset;
      this.m_tryEndOffset = tryEndOffset;
      this.m_filterOffset = filterOffset;
      this.m_handlerStartOffset = handlerStartOffset;
      this.m_handlerEndOffset = handlerEndOffset;
      this.m_kind = (ExceptionHandlingClauseOptions) kind;
      this.m_exceptionClass = exceptionTypeToken;
    }

    private static bool IsValidKind(ExceptionHandlingClauseOptions kind)
    {
      switch (kind)
      {
        case ExceptionHandlingClauseOptions.Clause:
        case ExceptionHandlingClauseOptions.Filter:
        case ExceptionHandlingClauseOptions.Finally:
        case ExceptionHandlingClauseOptions.Fault:
          return true;
        default:
          return false;
      }
    }

    /// <summary>Служит хэш-функцией по умолчанию.</summary>
    /// <returns>Хэш-код для текущего объекта.</returns>
    public override int GetHashCode()
    {
      return (int) ((ExceptionHandlingClauseOptions) (this.m_exceptionClass ^ this.m_tryStartOffset ^ this.m_tryEndOffset ^ this.m_filterOffset ^ this.m_handlerStartOffset ^ this.m_handlerEndOffset) ^ this.m_kind);
    }

    /// <summary>
    ///   Указывает, является ли этот экземпляр <see cref="T:System.Reflection.Emit.ExceptionHandler" /> объект равен указанному объекту.
    /// </summary>
    /// <param name="obj">Объект для сравнения данного экземпляра.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> и данный экземпляр равны; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is ExceptionHandler)
        return this.Equals((ExceptionHandler) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, является ли этот экземпляр <see cref="T:System.Reflection.Emit.ExceptionHandler" /> объект равен другому <see cref="T:System.Reflection.Emit.ExceptionHandler" /> объекта.
    /// </summary>
    /// <param name="other">
    ///   Объект обработчика исключения для сравнения данный экземпляр.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="other" /> и данный экземпляр равны; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(ExceptionHandler other)
    {
      if (other.m_exceptionClass == this.m_exceptionClass && other.m_tryStartOffset == this.m_tryStartOffset && (other.m_tryEndOffset == this.m_tryEndOffset && other.m_filterOffset == this.m_filterOffset) && (other.m_handlerStartOffset == this.m_handlerStartOffset && other.m_handlerEndOffset == this.m_handlerEndOffset))
        return other.m_kind == this.m_kind;
      return false;
    }

    /// <summary>
    ///   Определяет, равны ли два заданных экземпляра класса <see cref="T:System.Reflection.Emit.ExceptionHandler" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator ==(ExceptionHandler left, ExceptionHandler right)
    {
      return left.Equals(right);
    }

    /// <summary>
    ///   Определяет, являются ли два заданных экземпляра класса <see cref="T:System.Reflection.Emit.ExceptionHandler" /> неравными.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> не равны друг другу; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator !=(ExceptionHandler left, ExceptionHandler right)
    {
      return !left.Equals(right);
    }
  }
}
