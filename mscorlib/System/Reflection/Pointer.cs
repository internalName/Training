// Decompiled with JetBrains decompiler
// Type: System.Reflection.Pointer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  /// <summary>Предоставляет класс-оболочку для указателей.</summary>
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class Pointer : ISerializable
  {
    [SecurityCritical]
    private unsafe void* _ptr;
    private RuntimeType _ptrType;

    private Pointer()
    {
    }

    [SecurityCritical]
    private unsafe Pointer(SerializationInfo info, StreamingContext context)
    {
      this._ptr = ((IntPtr) info.GetValue(nameof (_ptr), typeof (IntPtr))).ToPointer();
      this._ptrType = (RuntimeType) info.GetValue(nameof (_ptrType), typeof (RuntimeType));
    }

    /// <summary>
    ///   Поля предоставленный неуправляемого указателя памяти и тип, связанный с этим указателем в управляемом <see cref="T:System.Reflection.Pointer" /> объект-оболочка.
    ///    Значение и тип сохраняются и поэтому они могут быть доступны из машинного кода при вызове.
    /// </summary>
    /// <param name="ptr">
    ///   Указанный неуправляемый указатель памяти.
    /// </param>
    /// <param name="type">
    ///   Тип, связанный с <paramref name="ptr" /> параметр.
    /// </param>
    /// <returns>Объект-указатель.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является указателем.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static unsafe object Box(void* ptr, Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (!type.IsPointer)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), nameof (ptr));
      RuntimeType runtimeType = type as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), nameof (ptr));
      return (object) new Pointer()
      {
        _ptr = ptr,
        _ptrType = runtimeType
      };
    }

    /// <summary>Возвращает сохраненный указатель.</summary>
    /// <param name="ptr">Сохраненный указатель.</param>
    /// <returns>Этот метод возвращает значение void.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="ptr" /> не является указателем.
    /// </exception>
    [SecurityCritical]
    public static unsafe void* Unbox(object ptr)
    {
      if (!(ptr is Pointer))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBePointer"), nameof (ptr));
      return ((Pointer) ptr)._ptr;
    }

    internal RuntimeType GetPointerType()
    {
      return this._ptrType;
    }

    [SecurityCritical]
    internal unsafe object GetPointerValue()
    {
      return (object) (IntPtr) this._ptr;
    }

    [SecurityCritical]
    unsafe void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("_ptr", (object) new IntPtr(this._ptr));
      info.AddValue("_ptrType", (object) this._ptrType);
    }
  }
}
