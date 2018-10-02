// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.DynamicILInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Обеспечивает поддержку альтернативных методов создания промежуточного языка Microsoft (MSIL) и метаданных для динамического метода, включая методы создания маркеров и вставки кода, обработки исключений и больших двоичных объектов подписи локальных переменных.
  /// </summary>
  [ComVisible(true)]
  public class DynamicILInfo
  {
    private DynamicMethod m_method;
    private DynamicScope m_scope;
    private byte[] m_exceptions;
    private byte[] m_code;
    private byte[] m_localSignature;
    private int m_maxStackSize;
    private int m_methodSignature;

    internal DynamicILInfo(DynamicScope scope, DynamicMethod method, byte[] methodSignature)
    {
      this.m_method = method;
      this.m_scope = scope;
      this.m_methodSignature = this.m_scope.GetTokenFor(methodSignature);
      this.m_exceptions = EmptyArray<byte>.Value;
      this.m_code = EmptyArray<byte>.Value;
      this.m_localSignature = EmptyArray<byte>.Value;
    }

    [SecurityCritical]
    internal void GetCallableMethod(RuntimeModule module, DynamicMethod dm)
    {
      dm.m_methodHandle = ModuleHandle.GetDynamicMethod(dm, module, this.m_method.Name, (byte[]) this.m_scope[this.m_methodSignature], (Resolver) new DynamicResolver(this));
    }

    internal byte[] LocalSignature
    {
      get
      {
        if (this.m_localSignature == null)
          this.m_localSignature = SignatureHelper.GetLocalVarSigHelper().InternalGetSignatureArray();
        return this.m_localSignature;
      }
    }

    internal byte[] Exceptions
    {
      get
      {
        return this.m_exceptions;
      }
    }

    internal byte[] Code
    {
      get
      {
        return this.m_code;
      }
    }

    internal int MaxStackSize
    {
      get
      {
        return this.m_maxStackSize;
      }
    }

    /// <summary>
    ///   Возвращает динамический метод, тело которого создается в текущем экземпляре.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.DynamicMethod" /> объект, представляющий динамический метод, для которого текущий <see cref="T:System.Reflection.Emit.DynamicILInfo" /> объекта создание кода.
    /// </returns>
    public DynamicMethod DynamicMethod
    {
      get
      {
        return this.m_method;
      }
    }

    internal DynamicScope DynamicScope
    {
      get
      {
        return this.m_scope;
      }
    }

    /// <summary>Задает текст кода связанного динамического метода.</summary>
    /// <param name="code">Массив, содержащий поток MSIL.</param>
    /// <param name="maxStackSize">
    ///   Максимальное количество элементов в стеке операторов при выполнении метода.
    /// </param>
    public void SetCode(byte[] code, int maxStackSize)
    {
      this.m_code = code != null ? (byte[]) code.Clone() : EmptyArray<byte>.Value;
      this.m_maxStackSize = maxStackSize;
    }

    /// <summary>Задает текст кода связанного динамического метода.</summary>
    /// <param name="code">
    ///   Указатель на массив байтов, содержащий поток MSIL.
    /// </param>
    /// <param name="codeSize">Число байтов в потоке MSIL.</param>
    /// <param name="maxStackSize">
    ///   Максимальное количество элементов в стеке операторов при выполнении метода.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="code" /> — <see langword="null" /> и <paramref name="codeSize" /> больше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="codeSize" /> меньше 0.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe void SetCode(byte* code, int codeSize, int maxStackSize)
    {
      if (codeSize < 0)
        throw new ArgumentOutOfRangeException(nameof (codeSize), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (codeSize > 0 && (IntPtr) code == IntPtr.Zero)
        throw new ArgumentNullException(nameof (code));
      this.m_code = new byte[codeSize];
      for (int index = 0; index < codeSize; ++index)
      {
        this.m_code[index] = *code;
        ++code;
      }
      this.m_maxStackSize = maxStackSize;
    }

    /// <summary>
    ///   Задает метаданные исключения для связанного динамического метода.
    /// </summary>
    /// <param name="exceptions">
    ///   Массив, содержащий метаданные исключения.
    /// </param>
    public void SetExceptions(byte[] exceptions)
    {
      this.m_exceptions = exceptions != null ? (byte[]) exceptions.Clone() : EmptyArray<byte>.Value;
    }

    /// <summary>
    ///   Задает метаданные исключения для связанного динамического метода.
    /// </summary>
    /// <param name="exceptions">
    ///   Указатель на массив байтов, содержащий метаданные исключения.
    /// </param>
    /// <param name="exceptionsSize">
    ///   Количество байтов в метаданных исключения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="exceptions" /> — <see langword="null" /> и <paramref name="exceptionSize" /> больше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="exceptionSize" /> меньше 0.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe void SetExceptions(byte* exceptions, int exceptionsSize)
    {
      if (exceptionsSize < 0)
        throw new ArgumentOutOfRangeException(nameof (exceptionsSize), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (exceptionsSize > 0 && (IntPtr) exceptions == IntPtr.Zero)
        throw new ArgumentNullException(nameof (exceptions));
      this.m_exceptions = new byte[exceptionsSize];
      for (int index = 0; index < exceptionsSize; ++index)
      {
        this.m_exceptions[index] = *exceptions;
        ++exceptions;
      }
    }

    /// <summary>
    ///   Задает подпись локальной переменной, которая описывает структуру локальных переменных для связанного динамического метода.
    /// </summary>
    /// <param name="localSignature">
    ///   Массив, содержащий структуру локальных переменных для связанного <see cref="T:System.Reflection.Emit.DynamicMethod" />.
    /// </param>
    public void SetLocalSignature(byte[] localSignature)
    {
      this.m_localSignature = localSignature != null ? (byte[]) localSignature.Clone() : EmptyArray<byte>.Value;
    }

    /// <summary>
    ///   Задает подпись локальной переменной, которая описывает структуру локальных переменных для связанного динамического метода.
    /// </summary>
    /// <param name="localSignature">
    ///   Массив, содержащий структуру локальных переменных для связанного <see cref="T:System.Reflection.Emit.DynamicMethod" />.
    /// </param>
    /// <param name="signatureSize">Число байтов в сигнатуре.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="localSignature" /> — <see langword="null" /> и <paramref name="signatureSize" /> больше 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="signatureSize" /> меньше 0.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe void SetLocalSignature(byte* localSignature, int signatureSize)
    {
      if (signatureSize < 0)
        throw new ArgumentOutOfRangeException(nameof (signatureSize), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (signatureSize > 0 && (IntPtr) localSignature == IntPtr.Zero)
        throw new ArgumentNullException(nameof (localSignature));
      this.m_localSignature = new byte[signatureSize];
      for (int index = 0; index < signatureSize; ++index)
      {
        this.m_localSignature[index] = *localSignature;
        ++localSignature;
      }
    }

    /// <summary>
    ///   Возвращает токен, допустимый в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" />, представляющего метод, который должен быть получен из связанного динамического метода.
    /// </summary>
    /// <param name="method">Метод для доступа.</param>
    /// <returns>
    ///   Маркер, который можно использовать в качестве операнда инструкции MSIL, который обращается к методы, такие как <see cref="F:System.Reflection.Emit.OpCodes.Call" /> или <see cref="F:System.Reflection.Emit.OpCodes.Ldtoken" />, в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" /> объекта.
    /// </returns>
    [SecuritySafeCritical]
    public int GetTokenFor(RuntimeMethodHandle method)
    {
      return this.DynamicScope.GetTokenFor(method);
    }

    /// <summary>
    ///   Возвращает токен, допустимый в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" />, представляющего динамический метод, вызываемый из связанного метода.
    /// </summary>
    /// <param name="method">Динамический метод для вызова.</param>
    /// <returns>
    ///   Токен, который может быть встроен в поток MSIL для связанного динамического метода в качестве назначения инструкции MSIL.
    /// </returns>
    public int GetTokenFor(DynamicMethod method)
    {
      return this.DynamicScope.GetTokenFor(method);
    }

    /// <summary>
    ///   Возвращает токен, допустимый в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" />, представляющий метод, для универсального типа.
    /// </summary>
    /// <param name="method">Метод.</param>
    /// <param name="contextType">
    ///   Универсальный тип, к которому принадлежит метод.
    /// </param>
    /// <returns>
    ///   Маркер, который можно использовать в качестве операнда инструкции MSIL, который обращается к методы, такие как <see cref="F:System.Reflection.Emit.OpCodes.Call" /> или <see cref="F:System.Reflection.Emit.OpCodes.Ldtoken" />, в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" /> объекта.
    /// </returns>
    public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle contextType)
    {
      return this.DynamicScope.GetTokenFor(method, contextType);
    }

    /// <summary>
    ///   Возвращает токен, допустимый в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" />, представляющий поле должен быть получен из связанного динамического метода.
    /// </summary>
    /// <param name="field">Поле для доступа.</param>
    /// <returns>
    ///   Маркер, который можно использовать в качестве операнда инструкции MSIL, который обращается к поля в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" /> объекта.
    /// </returns>
    public int GetTokenFor(RuntimeFieldHandle field)
    {
      return this.DynamicScope.GetTokenFor(field);
    }

    /// <summary>
    ///   Возвращает токен, допустимый в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" />, представляющий поле должен быть получен из связанного динамического метода; поле относится указанного универсального типа.
    /// </summary>
    /// <param name="field">Поле для доступа.</param>
    /// <param name="contextType">
    ///   Универсальный тип, к которому принадлежит поле.
    /// </param>
    /// <returns>
    ///   Маркер, который можно использовать в качестве операнда инструкции MSIL, который обращается к поля в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" /> объекта.
    /// </returns>
    public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle contextType)
    {
      return this.DynamicScope.GetTokenFor(field, contextType);
    }

    /// <summary>
    ///   Возвращает токен, допустимый в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" />, представляющий тип, используемый в связанного динамического метода.
    /// </summary>
    /// <param name="type">Тип, используемый.</param>
    /// <returns>
    ///   Маркер, который можно использовать в качестве операнда инструкции MSIL, требуется тип в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" /> объекта.
    /// </returns>
    public int GetTokenFor(RuntimeTypeHandle type)
    {
      return this.DynamicScope.GetTokenFor(type);
    }

    /// <summary>
    ///   Возвращает токен, допустимый в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" />, представляющее строковый литерал для использования в связанного динамического метода.
    /// </summary>
    /// <param name="literal">Строка для использования.</param>
    /// <returns>
    ///   Маркер, который можно использовать в качестве операнда инструкции MSIL, которым необходима строка, в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" /> объекта.
    /// </returns>
    public int GetTokenFor(string literal)
    {
      return this.DynamicScope.GetTokenFor(literal);
    }

    /// <summary>
    ///   Возвращает токен, допустимый в области текущего <see cref="T:System.Reflection.Emit.DynamicILInfo" />, представляющий сигнатуру для связанного динамического метода.
    /// </summary>
    /// <param name="signature">Массив, содержащий сигнатуру.</param>
    /// <returns>
    ///   Токен, который может быть внедрен в метаданные и поток MSIL для связанного динамического метода.
    /// </returns>
    public int GetTokenFor(byte[] signature)
    {
      return this.DynamicScope.GetTokenFor(signature);
    }
  }
}
