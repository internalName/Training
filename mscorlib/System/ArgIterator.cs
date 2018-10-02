// Decompiled with JetBrains decompiler
// Type: System.ArgIterator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
  /// <summary>
  ///   Предоставляет список аргументов переменной длины. то есть параметры функции, принимающий переменное число аргументов.
  /// </summary>
  public struct ArgIterator
  {
    private IntPtr ArgCookie;
    private IntPtr sigPtr;
    private IntPtr sigPtrLen;
    private IntPtr ArgPtr;
    private int RemainingArgs;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern ArgIterator(IntPtr arglist);

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ArgIterator" /> структуры с помощью списка аргументов.
    /// </summary>
    /// <param name="arglist">
    ///   Список, состоящий из обязательных и необязательных аргументов.
    /// </param>
    [SecuritySafeCritical]
    public ArgIterator(RuntimeArgumentHandle arglist)
    {
      this = new ArgIterator(arglist.Value);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern unsafe ArgIterator(IntPtr arglist, void* ptr);

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ArgIterator" /> структуры с помощью указанного списка аргументов и указатель на элемент в списке.
    /// </summary>
    /// <param name="arglist">
    ///   Список, состоящий из обязательных и необязательных аргументов.
    /// </param>
    /// <param name="ptr">
    ///   Указатель на аргумент в <paramref name="arglist" /> для доступа к первой или первый обязательный аргумент в <paramref name="arglist" /> Если <paramref name="ptr" /> — <see langword="null" />.
    /// </param>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe ArgIterator(RuntimeArgumentHandle arglist, void* ptr)
    {
      this = new ArgIterator(arglist.Value, ptr);
    }

    /// <summary>
    ///   Возвращает следующий аргумент из списка аргументов переменной длины.
    /// </summary>
    /// <returns>
    ///   Следующий аргумент как <see cref="T:System.TypedReference" /> объект.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Попытка чтения за концом списка.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe TypedReference GetNextArg()
    {
      TypedReference typedReference = new TypedReference();
      this.FCallGetNextArg((void*) &typedReference);
      return typedReference;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern unsafe void FCallGetNextArg(void* result);

    /// <summary>
    ///   Возвращает следующий аргумент в списке аргументов переменной длины, который имеет указанный тип.
    /// </summary>
    /// <param name="rth">
    ///   Дескриптор типа среды выполнения, определяющий тип аргумента для извлечения.
    /// </param>
    /// <returns>
    ///   Следующий аргумент как <see cref="T:System.TypedReference" /> объект.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Попытка чтения за концом списка.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Указатель на остальные аргументы равен нулю.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe TypedReference GetNextArg(RuntimeTypeHandle rth)
    {
      if (this.sigPtr != IntPtr.Zero)
        return this.GetNextArg();
      if (this.ArgPtr == IntPtr.Zero)
        throw new ArgumentNullException();
      TypedReference typedReference = new TypedReference();
      this.InternalGetNextArg((void*) &typedReference, rth.GetRuntimeType());
      return typedReference;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern unsafe void InternalGetNextArg(void* result, RuntimeType rt);

    /// <summary>
    ///   Завершает обработку списка аргументов переменной длины, представленного этим экземпляром.
    /// </summary>
    public void End()
    {
    }

    /// <summary>
    ///   Возвращает число оставшихся аргументов в списке аргументов.
    /// </summary>
    /// <returns>Число оставшихся аргументов.</returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern int GetRemainingCount();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern unsafe void* _GetNextArgType();

    /// <summary>Возвращает тип следующего аргумента.</summary>
    /// <returns>Тип следующего аргумента.</returns>
    [SecuritySafeCritical]
    public unsafe RuntimeTypeHandle GetNextArgType()
    {
      return new RuntimeTypeHandle(Type.GetTypeFromHandleUnsafe((IntPtr) this._GetNextArgType()));
    }

    /// <summary>Возвращает хэш-код для этого объекта.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    public override int GetHashCode()
    {
      return ValueType.GetHashCodeOfPtr(this.ArgCookie);
    }

    /// <summary>
    ///   Этот метод не поддерживается и всегда создает исключение <see cref="T:System.NotSupportedException" />.
    /// </summary>
    /// <param name="o">Объект, сравниваемый с данным экземпляром.</param>
    /// <returns>
    ///   Это сравнение не поддерживается.
    ///    Возвращаемое значение отсутствует.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    public override bool Equals(object o)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NYI"));
    }
  }
}
