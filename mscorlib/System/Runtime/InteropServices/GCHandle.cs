// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.GCHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет способ доступа к управляемому объекту из неуправляемой памяти.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public struct GCHandle
  {
    private static volatile bool s_probeIsActive = Mda.IsInvalidGCHandleCookieProbeEnabled();
    private const GCHandleType MaxHandleType = GCHandleType.Pinned;
    private IntPtr m_handle;
    private static volatile GCHandleCookieTable s_cookieTable;

    [SecuritySafeCritical]
    static GCHandle()
    {
      if (!GCHandle.s_probeIsActive)
        return;
      GCHandle.s_cookieTable = new GCHandleCookieTable();
    }

    [SecurityCritical]
    internal GCHandle(object value, GCHandleType type)
    {
      if ((uint) type > 3U)
        throw new ArgumentOutOfRangeException(nameof (type), Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      this.m_handle = GCHandle.InternalAlloc(value, type);
      if (type != GCHandleType.Pinned)
        return;
      this.SetIsPinned();
    }

    [SecurityCritical]
    internal GCHandle(IntPtr handle)
    {
      GCHandle.InternalCheckDomain(handle);
      this.m_handle = handle;
    }

    /// <summary>
    ///   Выделяет <see cref="F:System.Runtime.InteropServices.GCHandleType.Normal" /> дескриптор для указанного объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект, который использует <see cref="T:System.Runtime.InteropServices.GCHandle" />.
    /// </param>
    /// <returns>
    ///   Новый <see cref="T:System.Runtime.InteropServices.GCHandle" /> защищающий объект от сборки мусора.
    ///    Это <see cref="T:System.Runtime.InteropServices.GCHandle" /> должна быть выпущена с <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> когда он больше не нужен.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Экземпляр с членами nonprimitive (непреобразуемые) не может быть закреплен.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static GCHandle Alloc(object value)
    {
      return new GCHandle(value, GCHandleType.Normal);
    }

    /// <summary>
    ///   Выделяет дескриптор указанного типа для указанного объекта.
    /// </summary>
    /// <param name="value">
    ///   Объект, который использует <see cref="T:System.Runtime.InteropServices.GCHandle" />.
    /// </param>
    /// <param name="type">
    ///   Один из <see cref="T:System.Runtime.InteropServices.GCHandleType" /> значений, указывающий тип <see cref="T:System.Runtime.InteropServices.GCHandle" /> для создания.
    /// </param>
    /// <returns>
    ///   Новый <see cref="T:System.Runtime.InteropServices.GCHandle" /> указанного типа.
    ///    Это <see cref="T:System.Runtime.InteropServices.GCHandle" /> должна быть выпущена с <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> когда он больше не нужен.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Экземпляр с членами nonprimitive (непреобразуемые) не может быть закреплен.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static GCHandle Alloc(object value, GCHandleType type)
    {
      return new GCHandle(value, type);
    }

    /// <summary>
    ///   Выпуски <see cref="T:System.Runtime.InteropServices.GCHandle" />.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Дескриптор освобожден или не инициализирован.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public void Free()
    {
      IntPtr handle = this.m_handle;
      if (!(handle != IntPtr.Zero) || !(Interlocked.CompareExchange(ref this.m_handle, IntPtr.Zero, handle) == handle))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
      if (GCHandle.s_probeIsActive)
        GCHandle.s_cookieTable.RemoveHandleIfPresent(handle);
      GCHandle.InternalFree((IntPtr) ((int) handle & -2));
    }

    /// <summary>
    ///   Возвращает или задает объект, предоставляемый дескриптором.
    /// </summary>
    /// <returns>Объект, представляемый дескриптором.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Дескриптор освобожден или не инициализирован.
    /// </exception>
    [__DynamicallyInvokable]
    public object Target
    {
      [SecurityCritical, __DynamicallyInvokable] get
      {
        if (this.m_handle == IntPtr.Zero)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
        return GCHandle.InternalGet(this.GetHandleValue());
      }
      [SecurityCritical, __DynamicallyInvokable] set
      {
        if (this.m_handle == IntPtr.Zero)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
        GCHandle.InternalSet(this.GetHandleValue(), value, this.IsPinned());
      }
    }

    /// <summary>
    ///   Извлекает адрес объекта в <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" /> обработки.
    /// </summary>
    /// <returns>
    ///   Адрес закрепленного объекта как <see cref="T:System.IntPtr" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Дескриптор имеет любой тип, кроме <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" />.
    /// </exception>
    [SecurityCritical]
    public IntPtr AddrOfPinnedObject()
    {
      if (this.IsPinned())
        return GCHandle.InternalAddrOfPinnedObject(this.GetHandleValue());
      if (this.m_handle == IntPtr.Zero)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotPinned"));
    }

    /// <summary>
    ///   Возвращает значение, показывающее, выделен ли дескриптор.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если дескриптор выделен; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsAllocated
    {
      [__DynamicallyInvokable] get
      {
        return this.m_handle != IntPtr.Zero;
      }
    }

    /// <summary>
    ///   Объект <see cref="T:System.Runtime.InteropServices.GCHandle" /> хранится с использованием внутреннего целочисленного представления.
    /// </summary>
    /// <param name="value">
    ///   <see cref="T:System.IntPtr" /> На дескриптор, для которого требуется преобразование.
    /// </param>
    /// <returns>
    ///   Сохраненный <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекта с помощью внутреннего целочисленного представления.
    /// </returns>
    [SecurityCritical]
    public static explicit operator GCHandle(IntPtr value)
    {
      return GCHandle.FromIntPtr(value);
    }

    /// <summary>
    ///   Возвращает новый <see cref="T:System.Runtime.InteropServices.GCHandle" /> объект, созданный из дескриптора для управляемого объекта.
    /// </summary>
    /// <param name="value">
    ///   <see cref="T:System.IntPtr" /> Дескриптор для управляемого объекта для создания <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекта из.
    /// </param>
    /// <returns>
    ///   Новый <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекта, который соответствует значению параметра.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   параметр <paramref name="value" /> имеет значение <see cref="F:System.IntPtr.Zero" />;
    /// </exception>
    [SecurityCritical]
    public static GCHandle FromIntPtr(IntPtr value)
    {
      if (value == IntPtr.Zero)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_HandleIsNotInitialized"));
      IntPtr handle = value;
      if (GCHandle.s_probeIsActive)
      {
        handle = GCHandle.s_cookieTable.GetHandle(value);
        if (IntPtr.Zero == handle)
        {
          Mda.FireInvalidGCHandleCookieProbe(value);
          return new GCHandle(IntPtr.Zero);
        }
      }
      return new GCHandle(handle);
    }

    /// <summary>
    ///   Объект <see cref="T:System.Runtime.InteropServices.GCHandle" /> хранится с использованием внутреннего целочисленного представления.
    /// </summary>
    /// <param name="value">
    ///   <see cref="T:System.Runtime.InteropServices.GCHandle" /> Для которого требуется целое число.
    /// </param>
    /// <returns>Целочисленное значение.</returns>
    public static explicit operator IntPtr(GCHandle value)
    {
      return GCHandle.ToIntPtr(value);
    }

    /// <summary>
    ///   Возвращает внутреннее целочисленное представление <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   A <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекта для извлечения внутреннего целочисленного представления.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.IntPtr" /> Объект, представляющий <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекта.
    /// </returns>
    public static IntPtr ToIntPtr(GCHandle value)
    {
      if (GCHandle.s_probeIsActive)
        return GCHandle.s_cookieTable.FindOrAddHandle(value.m_handle);
      return value.m_handle;
    }

    /// <summary>
    ///   Возвращает идентификатор для текущего <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекта.
    /// </summary>
    /// <returns>
    ///   Идентификатор для текущего объекта <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекта.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_handle.GetHashCode();
    }

    /// <summary>
    ///   Определяет, является ли указанный <see cref="T:System.Runtime.InteropServices.GCHandle" /> объект равен текущему объекту <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекта.
    /// </summary>
    /// <param name="o">
    ///   <see cref="T:System.Runtime.InteropServices.GCHandle" /> Объект, сравниваемый с текущим <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекта.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанный <see cref="T:System.Runtime.InteropServices.GCHandle" /> объект равен текущему объекту <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекта; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object o)
    {
      if (o == null || !(o is GCHandle))
        return false;
      return this.m_handle == ((GCHandle) o).m_handle;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, будет ли два <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекты равны.
    /// </summary>
    /// <param name="a">
    ///   Объект <see cref="T:System.Runtime.InteropServices.GCHandle" /> объект для сравнения с <paramref name="b" /> параметр.
    /// </param>
    /// <param name="b">
    ///   Объект <see cref="T:System.Runtime.InteropServices.GCHandle" /> объект для сравнения с <paramref name="a" /> параметр.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(GCHandle a, GCHandle b)
    {
      return a.m_handle == b.m_handle;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, будет ли два <see cref="T:System.Runtime.InteropServices.GCHandle" /> объекты не равны.
    /// </summary>
    /// <param name="a">
    ///   Объект <see cref="T:System.Runtime.InteropServices.GCHandle" /> объект для сравнения с <paramref name="b" /> параметр.
    /// </param>
    /// <param name="b">
    ///   Объект <see cref="T:System.Runtime.InteropServices.GCHandle" /> объект для сравнения с <paramref name="a" /> параметр.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="a" /> и <paramref name="b" /> параметров не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(GCHandle a, GCHandle b)
    {
      return a.m_handle != b.m_handle;
    }

    internal IntPtr GetHandleValue()
    {
      return new IntPtr((int) this.m_handle & -2);
    }

    internal bool IsPinned()
    {
      return (uint) ((int) this.m_handle & 1) > 0U;
    }

    internal void SetIsPinned()
    {
      this.m_handle = new IntPtr((int) this.m_handle | 1);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr InternalAlloc(object value, GCHandleType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InternalFree(IntPtr handle);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object InternalGet(IntPtr handle);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InternalSet(IntPtr handle, object value, bool isPinned);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object InternalCompareExchange(IntPtr handle, object value, object oldValue, bool isPinned);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr InternalAddrOfPinnedObject(IntPtr handle);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void InternalCheckDomain(IntPtr handle);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern GCHandleType InternalGetHandleType(IntPtr handle);
  }
}
