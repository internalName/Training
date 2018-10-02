// Decompiled with JetBrains decompiler
// Type: System.WeakReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System
{
  /// <summary>
  ///   Представляет слабую ссылку, которая указывает на объект, но позволяет удалять его сборщику мусора.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
  public class WeakReference : ISerializable
  {
    internal IntPtr m_handle;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.WeakReference" /> класса, ссылающийся на заданный объект.
    /// </summary>
    /// <param name="target">
    ///   Объект для отслеживания или <see langword="null" />.
    /// </param>
    [__DynamicallyInvokable]
    public WeakReference(object target)
      : this(target, false)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.WeakReference" />, ссылающийся на заданный объект и использующий заданное отслеживание восстановления.
    /// </summary>
    /// <param name="target">Отслеживаемый объект.</param>
    /// <param name="trackResurrection">
    ///   Указывает, когда необходимо прекратить отслеживание объекта.
    ///    Если задано значение <see langword="true" />, объект отслеживается после завершения; если значение <see langword="false" />, объект отслеживается только до завершения.
    /// </param>
    [__DynamicallyInvokable]
    public WeakReference(object target, bool trackResurrection)
    {
      this.Create(target, trackResurrection);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.WeakReference" /> класса, используя десериализованные данные из заданных объектов сериализации и потока.
    /// </summary>
    /// <param name="info">
    ///   Объект, который содержит все данные, необходимые для сериализации или десериализации текущего <see cref="T:System.WeakReference" /> объекта.
    /// </param>
    /// <param name="context">
    ///   (Зарезервировано) Описывает источник и назначение сериализованного потока, заданного параметром <paramref name="info" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    protected WeakReference(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.Create(info.GetValue("TrackedObject", typeof (object)), info.GetBoolean(nameof (TrackResurrection)));
    }

    /// <summary>
    ///   Возвращает сведения о том, был ли удален сборщиком мусора объект, на который ссылается текущий объект <see cref="T:System.WeakReference" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект, на который ссылается текущий объект <see cref="T:System.WeakReference" />, не был удален сборщиком мусора и остается доступным; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual extern bool IsAlive { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>
    ///   Возвращает сведения о том, отслеживается ли после завершения объект, на который ссылается текущий объект <see cref="T:System.WeakReference" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект, на который ссылается текущий объект <see cref="T:System.WeakReference" />, отслеживается после завершения; значение <see langword="false" />, если этот объект отслеживается только до завершения.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool TrackResurrection
    {
      [__DynamicallyInvokable] get
      {
        return this.IsTrackResurrection();
      }
    }

    /// <summary>
    ///   Возвращает или задает (целевой) объект, на который ссылается текущий объект <see cref="T:System.WeakReference" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="null" />, если объект, на который ссылается текущий объект <see cref="T:System.WeakReference" />, не был удален сборщиком мусора; в противном случае ссылка на объект, на который ссылается текущий объект <see cref="T:System.WeakReference" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Ссылка на целевой объект является недопустимой.
    ///    Это исключение может быть создано при задании свойства, если значением является пустая ссылка или объект финализирован во время операции задания.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual extern object Target { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] set; }

    /// <summary>
    ///   Удаляет ссылку на целевой объект, представленный текущим <see cref="T:System.WeakReference" /> объекта.
    /// </summary>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    extern ~WeakReference();

    /// <summary>
    ///   Заполняет <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект со всеми данными, необходимыми для сериализации текущего <see cref="T:System.WeakReference" /> объекта.
    /// </summary>
    /// <param name="info">
    ///   Объект, который содержит все данные, необходимые для сериализации или десериализации текущего <see cref="T:System.WeakReference" /> объекта.
    /// </param>
    /// <param name="context">
    ///   (Зарезервировано) Расположение, где хранятся и откуда извлекаются сериализованные данные.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.AddValue("TrackedObject", this.Target, typeof (object));
      info.AddValue("TrackResurrection", this.IsTrackResurrection());
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void Create(object target, bool trackResurrection);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern bool IsTrackResurrection();
  }
}
