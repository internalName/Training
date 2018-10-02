// Decompiled with JetBrains decompiler
// Type: System.Threading.LazyInitializer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Обеспечивает процедуры инициализации адаптирующегося типа.
  /// </summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public static class LazyInitializer
  {
    /// <summary>
    ///   Инициализирует целевой ссылочный тип конструктором типа по умолчанию, если он еще не инициализирован.
    /// </summary>
    /// <param name="target">
    ///   Ссылка типа <paramref name="T" /> для инициализации, если он еще не был инициализирован.
    /// </param>
    /// <typeparam name="T">Тип инициализируемой ссылки.</typeparam>
    /// <returns>
    ///   Инициализируемая ссылка типа <paramref name="T" />.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Разрешения для доступа к конструктору типа <paramref name="T" /> отсутствовали.
    /// </exception>
    /// <exception cref="T:System.MissingMemberException">
    ///   Тип <paramref name="T" /> не имеет конструктора по умолчанию.
    /// </exception>
    [__DynamicallyInvokable]
    public static T EnsureInitialized<T>(ref T target) where T : class
    {
      if ((object) Volatile.Read<T>(ref target) != null)
        return target;
      return LazyInitializer.EnsureInitializedCore<T>(ref target, LazyHelpers<T>.s_activatorFactorySelector);
    }

    /// <summary>
    ///   Инициализирует целевой ссылочный тип с помощью указанной функции, если он еще не инициализирован.
    /// </summary>
    /// <param name="target">
    ///   Ссылка типа <paramref name="T" /> для инициализации, если он еще не инициализирован.
    /// </param>
    /// <param name="valueFactory">
    ///   Функция, которая вызывается для инициализации ссылки.
    /// </param>
    /// <typeparam name="T">
    ///   Ссылочный тип инициализируемой ссылки.
    /// </typeparam>
    /// <returns>
    ///   Инициализированное значение типа <paramref name="T" />.
    /// </returns>
    /// <exception cref="T:System.MissingMemberException">
    ///   Тип <paramref name="T" /> не имеет конструктора по умолчанию.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="valueFactory" /> возвращается, значение null (Nothing в Visual Basic).
    /// </exception>
    [__DynamicallyInvokable]
    public static T EnsureInitialized<T>(ref T target, Func<T> valueFactory) where T : class
    {
      if ((object) Volatile.Read<T>(ref target) != null)
        return target;
      return LazyInitializer.EnsureInitializedCore<T>(ref target, valueFactory);
    }

    private static T EnsureInitializedCore<T>(ref T target, Func<T> valueFactory) where T : class
    {
      T obj = valueFactory();
      if ((object) obj == null)
        throw new InvalidOperationException(Environment.GetResourceString("Lazy_StaticInit_InvalidOperation"));
      Interlocked.CompareExchange<T>(ref target, obj, default (T));
      return target;
    }

    /// <summary>
    ///   Инициализирует ссылкой или значением типа целевого объекта его конструктором по умолчанию, если он еще не инициализирован.
    /// </summary>
    /// <param name="target">
    ///   Ссылка или значение типа <paramref name="T" /> для инициализации, если он еще не инициализирован.
    /// </param>
    /// <param name="initialized">
    ///   Ссылка на логическое значение, определяющее, является ли целевой объект уже инициализирован.
    /// </param>
    /// <param name="syncLock">
    ///   Ссылку на объект, используемый как взаимоисключающая блокировка для инициализации <paramref name="target" />.
    ///    Если <paramref name="syncLock" /> является <see langword="null" />, будет создан новый объект.
    /// </param>
    /// <typeparam name="T">Тип инициализируемой ссылки.</typeparam>
    /// <returns>
    ///   Инициализированное значение типа <paramref name="T" />.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Разрешения для доступа к конструктору типа <paramref name="T" /> отсутствовали.
    /// </exception>
    /// <exception cref="T:System.MissingMemberException">
    ///   Тип <paramref name="T" /> не имеет конструктора по умолчанию.
    /// </exception>
    [__DynamicallyInvokable]
    public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock)
    {
      if (Volatile.Read(ref initialized))
        return target;
      return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, LazyHelpers<T>.s_activatorFactorySelector);
    }

    /// <summary>
    ///   Инициализирует целевой тип ссылкой или значением с помощью указанной функции, если он еще не инициализирован.
    /// </summary>
    /// <param name="target">
    ///   Ссылка или значение типа <paramref name="T" /> для инициализации, если он еще не инициализирован.
    /// </param>
    /// <param name="initialized">
    ///   Ссылка на логическое значение, определяющее, является ли целевой объект уже инициализирован.
    /// </param>
    /// <param name="syncLock">
    ///   Ссылку на объект, используемый как взаимоисключающая блокировка для инициализации <paramref name="target" />.
    ///    Если <paramref name="syncLock" /> является <see langword="null" />, будет создан новый объект.
    /// </param>
    /// <param name="valueFactory">
    ///   Функция, которая вызывается для инициализации ссылки или значения.
    /// </param>
    /// <typeparam name="T">Тип инициализируемой ссылки.</typeparam>
    /// <returns>
    ///   Инициализированное значение типа <paramref name="T" />.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Разрешения для доступа к конструктору типа <paramref name="T" /> отсутствовали.
    /// </exception>
    /// <exception cref="T:System.MissingMemberException">
    ///   Тип <paramref name="T" /> не имеет конструктора по умолчанию.
    /// </exception>
    [__DynamicallyInvokable]
    public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
    {
      if (Volatile.Read(ref initialized))
        return target;
      return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, valueFactory);
    }

    private static T EnsureInitializedCore<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
    {
      object obj1 = syncLock;
      if (obj1 == null)
      {
        object obj2 = new object();
        obj1 = Interlocked.CompareExchange(ref syncLock, obj2, (object) null) ?? obj2;
      }
      lock (obj1)
      {
        if (!Volatile.Read(ref initialized))
        {
          target = valueFactory();
          Volatile.Write(ref initialized, true);
        }
      }
      return target;
    }
  }
}
