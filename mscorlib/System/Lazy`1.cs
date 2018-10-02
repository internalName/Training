// Decompiled with JetBrains decompiler
// Type: System.Lazy`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System
{
  /// <summary>Обеспечивает поддержку отложенной инициализации.</summary>
  /// <typeparam name="T">
  ///   Тип объекта с отложенной инициализацией.
  /// </typeparam>
  [ComVisible(false)]
  [DebuggerTypeProxy(typeof (System_LazyDebugView<>))]
  [DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class Lazy<T>
  {
    private static readonly Func<T> ALREADY_INVOKED_SENTINEL = (Func<T>) (() => default (T));
    private object m_boxed;
    [NonSerialized]
    private Func<T> m_valueFactory;
    [NonSerialized]
    private object m_threadSafeObj;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Lazy`1" />.
    ///    При отложенной инициализации используется конструктор по умолчанию для целевого типа.
    /// </summary>
    [__DynamicallyInvokable]
    public Lazy()
      : this(LazyThreadSafetyMode.ExecutionAndPublication)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Lazy`1" />.
    ///    Когда происходит отложенная инициализация, используется заданная функция инициализации.
    /// </summary>
    /// <param name="valueFactory">
    ///   Делегат, вызываемый для создания значения с отложенной инициализацией при необходимости.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="valueFactory" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Lazy(Func<T> valueFactory)
      : this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Lazy`1" />.
    ///    При отложенной инициализации используется конструктор по умолчанию целевого типа и указанный режим инициализации.
    /// </summary>
    /// <param name="isThreadSafe">
    ///   Значение <see langword="true" />, чтобы сделать этот экземпляр доступным для одновременного использования несколькими потоками; значение <see langword="false" />, чтобы экземпляр мог использоваться только одним потоком.
    /// </param>
    [__DynamicallyInvokable]
    public Lazy(bool isThreadSafe)
      : this(isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Lazy`1" />, использующий конструктор по умолчанию <paramref name="T" /> и заданный потокобезопасный режим.
    /// </summary>
    /// <param name="mode">
    ///   Одно из значений перечисления, задающее потокобезопасный режим.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="mode" /> содержит недопустимое значение.
    /// </exception>
    [__DynamicallyInvokable]
    public Lazy(LazyThreadSafetyMode mode)
    {
      this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Lazy`1" />.
    ///    Когда происходит отложенная инициализация, используются заданные функция инициализации и режим инициализации.
    /// </summary>
    /// <param name="valueFactory">
    ///   Делегат, вызываемый для создания значения с отложенной инициализацией при необходимости.
    /// </param>
    /// <param name="isThreadSafe">
    ///   Значение <see langword="true" />, чтобы сделать этот экземпляр доступным для одновременного использования несколькими потоками; значение <see langword="false" />, чтобы этот экземпляр мог использоваться только одним потоком в каждый момент времени.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="valueFactory" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Lazy(Func<T> valueFactory, bool isThreadSafe)
      : this(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Lazy`1" />, который использует заданную функцию инициализации и потокобезопасный режим.
    /// </summary>
    /// <param name="valueFactory">
    ///   Делегат, вызываемый для создания значения с отложенной инициализацией при необходимости.
    /// </param>
    /// <param name="mode">
    ///   Одно из значений перечисления, задающее потокобезопасный режим.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="mode" /> содержит недопустимое значение.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="valueFactory" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
    {
      if (valueFactory == null)
        throw new ArgumentNullException(nameof (valueFactory));
      this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
      this.m_valueFactory = valueFactory;
    }

    private static object GetObjectFromMode(LazyThreadSafetyMode mode)
    {
      if (mode == LazyThreadSafetyMode.ExecutionAndPublication)
        return new object();
      if (mode == LazyThreadSafetyMode.PublicationOnly)
        return LazyHelpers.PUBLICATION_ONLY_SENTINEL;
      if (mode != LazyThreadSafetyMode.None)
        throw new ArgumentOutOfRangeException(nameof (mode), Environment.GetResourceString("Lazy_ctor_ModeInvalid"));
      return (object) null;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext context)
    {
      T obj = this.Value;
    }

    /// <summary>
    ///   Создает и возвращает строковое представление свойства <see cref="P:System.Lazy`1.Value" /> для данного экземпляра.
    /// </summary>
    /// <returns>
    ///   Результат вызова метода <see cref="M:System.Object.ToString" /> для свойства <see cref="P:System.Lazy`1.Value" /> данного экземпляра, если значение создано (то есть если свойство <see cref="P:System.Lazy`1.IsValueCreated" /> возвращает <see langword="true" />).
    ///    В противном случае строка, указывающая, что значение не создано.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Значение свойства <see cref="P:System.Lazy`1.Value" /> — <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      if (!this.IsValueCreated)
        return Environment.GetResourceString("Lazy_ToString_ValueNotCreated");
      return this.Value.ToString();
    }

    internal T ValueForDebugDisplay
    {
      get
      {
        if (!this.IsValueCreated)
          return default (T);
        return ((Lazy<T>.Boxed) this.m_boxed).m_value;
      }
    }

    internal LazyThreadSafetyMode Mode
    {
      get
      {
        if (this.m_threadSafeObj == null)
          return LazyThreadSafetyMode.None;
        return this.m_threadSafeObj == LazyHelpers.PUBLICATION_ONLY_SENTINEL ? LazyThreadSafetyMode.PublicationOnly : LazyThreadSafetyMode.ExecutionAndPublication;
      }
    }

    internal bool IsValueFaulted
    {
      get
      {
        return this.m_boxed is Lazy<T>.LazyInternalExceptionHolder;
      }
    }

    /// <summary>
    ///   Получает значение, которое показывает, создано ли значение для этого экземпляра <see cref="T:System.Lazy`1" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если значение создано для этого экземпляра <see cref="T:System.Lazy`1" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsValueCreated
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_boxed != null)
          return this.m_boxed is Lazy<T>.Boxed;
        return false;
      }
    }

    /// <summary>
    ///   Получает значение с отложенной инициализацией текущего экземпляра <see cref="T:System.Lazy`1" />.
    /// </summary>
    /// <returns>
    ///   Значение с отложенной инициализацией текущего экземпляра <see cref="T:System.Lazy`1" />.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Экземпляр <see cref="T:System.Lazy`1" /> инициализируется для использования конструктора по умолчанию, имеющего тип с отложенной инициализацией, а разрешения для доступа к этому конструктору отсутствуют.
    /// </exception>
    /// <exception cref="T:System.MissingMemberException">
    ///   Экземпляр <see cref="T:System.Lazy`1" /> инициализируется для использования конструктора по умолчанию, имеющего тип с отложенной инициализацией, а этот тип не имеет общего конструктора без параметров.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Функция инициализации пытается получить доступ к <see cref="P:System.Lazy`1.Value" /> в данном экземпляре.
    /// </exception>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [__DynamicallyInvokable]
    public T Value
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_boxed != null)
        {
          Lazy<T>.Boxed boxed = this.m_boxed as Lazy<T>.Boxed;
          if (boxed != null)
            return boxed.m_value;
          (this.m_boxed as Lazy<T>.LazyInternalExceptionHolder).m_edi.Throw();
        }
        Debugger.NotifyOfCrossThreadDependency();
        return this.LazyInitValue();
      }
    }

    private T LazyInitValue()
    {
      Lazy<T>.Boxed boxed = (Lazy<T>.Boxed) null;
      switch (this.Mode)
      {
        case LazyThreadSafetyMode.None:
          boxed = this.CreateValue();
          this.m_boxed = (object) boxed;
          break;
        case LazyThreadSafetyMode.PublicationOnly:
          boxed = this.CreateValue();
          if (boxed == null || Interlocked.CompareExchange(ref this.m_boxed, (object) boxed, (object) null) != null)
          {
            boxed = (Lazy<T>.Boxed) this.m_boxed;
            break;
          }
          this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
          break;
        default:
          object obj = Volatile.Read<object>(ref this.m_threadSafeObj);
          bool lockTaken = false;
          try
          {
            if (obj != Lazy<T>.ALREADY_INVOKED_SENTINEL)
              Monitor.Enter(obj, ref lockTaken);
            if (this.m_boxed == null)
            {
              boxed = this.CreateValue();
              this.m_boxed = (object) boxed;
              Volatile.Write<object>(ref this.m_threadSafeObj, (object) Lazy<T>.ALREADY_INVOKED_SENTINEL);
              break;
            }
            boxed = this.m_boxed as Lazy<T>.Boxed;
            if (boxed == null)
            {
              (this.m_boxed as Lazy<T>.LazyInternalExceptionHolder).m_edi.Throw();
              break;
            }
            break;
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit(obj);
          }
      }
      return boxed.m_value;
    }

    private Lazy<T>.Boxed CreateValue()
    {
      LazyThreadSafetyMode mode = this.Mode;
      if (this.m_valueFactory != null)
      {
        try
        {
          if (mode != LazyThreadSafetyMode.PublicationOnly && this.m_valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
            throw new InvalidOperationException(Environment.GetResourceString("Lazy_Value_RecursiveCallsToValue"));
          Func<T> valueFactory = this.m_valueFactory;
          if (mode != LazyThreadSafetyMode.PublicationOnly)
            this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
          else if (valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
            return (Lazy<T>.Boxed) null;
          return new Lazy<T>.Boxed(valueFactory());
        }
        catch (Exception ex)
        {
          if (mode != LazyThreadSafetyMode.PublicationOnly)
            this.m_boxed = (object) new Lazy<T>.LazyInternalExceptionHolder(ex);
          throw;
        }
      }
      else
      {
        try
        {
          return new Lazy<T>.Boxed((T) Activator.CreateInstance(typeof (T)));
        }
        catch (MissingMethodException ex1)
        {
          Exception ex2 = (Exception) new MissingMemberException(Environment.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
          if (mode != LazyThreadSafetyMode.PublicationOnly)
            this.m_boxed = (object) new Lazy<T>.LazyInternalExceptionHolder(ex2);
          throw ex2;
        }
      }
    }

    [Serializable]
    private class Boxed
    {
      internal T m_value;

      internal Boxed(T value)
      {
        this.m_value = value;
      }
    }

    private class LazyInternalExceptionHolder
    {
      internal ExceptionDispatchInfo m_edi;

      internal LazyInternalExceptionHolder(Exception ex)
      {
        this.m_edi = ExceptionDispatchInfo.Capture(ex);
      }
    }
  }
}
