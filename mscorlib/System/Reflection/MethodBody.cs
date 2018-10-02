// Decompiled with JetBrains decompiler
// Type: System.Reflection.MethodBody
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Предоставляет доступ к метаданным и MSIL для тела метода.
  /// </summary>
  [ComVisible(true)]
  public class MethodBody
  {
    private byte[] m_IL;
    private ExceptionHandlingClause[] m_exceptionHandlingClauses;
    private LocalVariableInfo[] m_localVariables;
    internal MethodBase m_methodBase;
    private int m_localSignatureMetadataToken;
    private int m_maxStackSize;
    private bool m_initLocals;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.MethodBody" />.
    /// </summary>
    protected MethodBody()
    {
    }

    /// <summary>
    ///   Получает маркер метаданных для подписи, которая описывает локальные переменные для этого метода в метаданных.
    /// </summary>
    /// <returns>Целое число, представляющее токен метаданных.</returns>
    public virtual int LocalSignatureMetadataToken
    {
      get
      {
        return this.m_localSignatureMetadataToken;
      }
    }

    /// <summary>
    ///   Возвращает список локальных переменных, объявленных в теле метода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.Generic.IList`1" /> Из <see cref="T:System.Reflection.LocalVariableInfo" /> объекты, описывающие локальные переменные, объявленные в теле метода.
    /// </returns>
    public virtual IList<LocalVariableInfo> LocalVariables
    {
      get
      {
        return (IList<LocalVariableInfo>) Array.AsReadOnly<LocalVariableInfo>(this.m_localVariables);
      }
    }

    /// <summary>
    ///   Возвращает максимальное количество элементов в стеке операторов при выполнении метода.
    /// </summary>
    /// <returns>
    ///   Максимальное количество элементов в стеке операторов при выполнении метода.
    /// </returns>
    public virtual int MaxStackSize
    {
      get
      {
        return this.m_maxStackSize;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, инициализируются ли локальные переменные в теле метода значения по умолчанию для их типов.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если тело метода содержит код для инициализации локальных переменных в <see langword="null" /> для ссылочных типов, или нулевыми значениями для типов значений; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool InitLocals
    {
      get
      {
        return this.m_initLocals;
      }
    }

    /// <summary>
    ///   Возвращает код MSIL для тела метода в виде массива байтов.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Byte" /> содержащий код MSIL для тела метода.
    /// </returns>
    public virtual byte[] GetILAsByteArray()
    {
      return this.m_IL;
    }

    /// <summary>
    ///   Возвращает список, содержащий все предложения обработки исключений в теле метода.
    /// </summary>
    /// <returns>
    ///   Список <see cref="T:System.Collections.Generic.IList`1" /> объектов <see cref="T:System.Reflection.ExceptionHandlingClause" />, представляющий предложения обработки исключений в теле метода.
    /// </returns>
    public virtual IList<ExceptionHandlingClause> ExceptionHandlingClauses
    {
      get
      {
        return (IList<ExceptionHandlingClause>) Array.AsReadOnly<ExceptionHandlingClause>(this.m_exceptionHandlingClauses);
      }
    }
  }
}
