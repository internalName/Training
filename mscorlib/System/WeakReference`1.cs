// Decompiled with JetBrains decompiler
// Type: System.WeakReference`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет типизированную слабую ссылку, которая указывает на объект, но позволяет удалять его сборщику мусора.
  /// </summary>
  /// <typeparam name="T">Тип упоминаемого объекта.</typeparam>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class WeakReference<T> : ISerializable where T : class
  {
    internal IntPtr m_handle;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.WeakReference`1" /> класс, который ссылается на указанный объект.
    /// </summary>
    /// <param name="target">
    ///   Объект для ссылки, или <see langword="null" />.
    /// </param>
    [__DynamicallyInvokable]
    public WeakReference(T target)
      : this(target, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.WeakReference`1" /> класс, который ссылается на указанный объект и использующий заданное отслеживание восстановления.
    /// </summary>
    /// <param name="target">
    ///   Объект для ссылки, или <see langword="null" />.
    /// </param>
    /// <param name="trackResurrection">
    ///   <see langword="true" />для отслеживания объекта после завершения; <see langword="false" /> для отслеживания объекта только до завершения.
    /// </param>
    [__DynamicallyInvokable]
    public WeakReference(T target, bool trackResurrection)
    {
      this.Create(target, trackResurrection);
    }

    internal WeakReference(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.Create((T) info.GetValue("TrackedObject", typeof (T)), info.GetBoolean("TrackResurrection"));
    }

    /// <summary>
    ///   Пытается получить целевой объект, который ссылается текущий <see cref="T:System.WeakReference`1" /> объекта.
    /// </summary>
    /// <param name="target">
    ///   По возвращении из этого метода содержит целевой объект, если он доступен.
    ///    Этот параметр обрабатывается как неинициализированный.
    /// </param>
    /// <returns>
    ///   <see langword="true" />Если целевой объект был извлечен; в противном случае <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetTarget(out T target)
    {
      T target1 = this.Target;
      target = target1;
      return (object) target1 != null;
    }

    /// <summary>
    ///   Задает целевой объект, на который ссылается это <see cref="T:System.WeakReference`1" /> объекта.
    /// </summary>
    /// <param name="target">Новый целевой объект.</param>
    [__DynamicallyInvokable]
    public void SetTarget(T target)
    {
      this.Target = target;
    }

    private extern T Target { [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] get; [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] set; }

    /// <summary>
    ///   Удаляет ссылку на целевой объект, представленный текущим <see cref="T:System.WeakReference`1" /> объекта.
    /// </summary>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    extern ~WeakReference();

    /// <summary>
    ///   Заполняет <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект со всеми данными, необходимыми для сериализации текущего <see cref="T:System.WeakReference`1" /> объекта.
    /// </summary>
    /// <param name="info">
    ///   Объект, который содержит все данные, необходимые для сериализации или десериализации текущего <see cref="T:System.WeakReference`1" /> объекта.
    /// </param>
    /// <param name="context">
    ///   Расположение, где хранятся и откуда извлекаются сериализованные данные.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("TrackedObject", (object) this.Target, typeof (T));
      info.AddValue("TrackResurrection", this.IsTrackResurrection());
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void Create(T target, bool trackResurrection);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern bool IsTrackResurrection();
  }
}
