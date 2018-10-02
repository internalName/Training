// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ICustomPropertyProviderProxy`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal class ICustomPropertyProviderProxy<T1, T2> : IGetProxyTarget, ICustomPropertyProvider, ICustomQueryInterface, IEnumerable, IBindableVector, IBindableIterable, IBindableVectorView
  {
    private object _target;
    private InterfaceForwardingSupport _flags;

    internal ICustomPropertyProviderProxy(object target, InterfaceForwardingSupport flags)
    {
      this._target = target;
      this._flags = flags;
    }

    internal static object CreateInstance(object target)
    {
      InterfaceForwardingSupport flags = InterfaceForwardingSupport.None;
      if (target is IList)
        flags |= InterfaceForwardingSupport.IBindableVector;
      if (target is IList<T1>)
        flags |= InterfaceForwardingSupport.IVector;
      if (target is IBindableVectorView)
        flags |= InterfaceForwardingSupport.IBindableVectorView;
      if (target is IReadOnlyList<T2>)
        flags |= InterfaceForwardingSupport.IVectorView;
      if (target is IEnumerable)
        flags |= InterfaceForwardingSupport.IBindableIterableOrIIterable;
      return (object) new ICustomPropertyProviderProxy<T1, T2>(target, flags);
    }

    ICustomProperty ICustomPropertyProvider.GetCustomProperty(string name)
    {
      return ICustomPropertyProviderImpl.CreateProperty(this._target, name);
    }

    ICustomProperty ICustomPropertyProvider.GetIndexedProperty(string name, Type indexParameterType)
    {
      return ICustomPropertyProviderImpl.CreateIndexedProperty(this._target, name, indexParameterType);
    }

    string ICustomPropertyProvider.GetStringRepresentation()
    {
      return IStringableHelper.ToString(this._target);
    }

    Type ICustomPropertyProvider.Type
    {
      get
      {
        return this._target.GetType();
      }
    }

    public override string ToString()
    {
      return IStringableHelper.ToString(this._target);
    }

    object IGetProxyTarget.GetTarget()
    {
      return this._target;
    }

    [SecurityCritical]
    public CustomQueryInterfaceResult GetInterface([In] ref Guid iid, out IntPtr ppv)
    {
      ppv = IntPtr.Zero;
      return iid == typeof (IBindableIterable).GUID && (this._flags & InterfaceForwardingSupport.IBindableIterableOrIIterable) == InterfaceForwardingSupport.None || iid == typeof (IBindableVector).GUID && (this._flags & (InterfaceForwardingSupport.IBindableVector | InterfaceForwardingSupport.IVector)) == InterfaceForwardingSupport.None || iid == typeof (IBindableVectorView).GUID && (this._flags & (InterfaceForwardingSupport.IBindableVectorView | InterfaceForwardingSupport.IVectorView)) == InterfaceForwardingSupport.None ? CustomQueryInterfaceResult.Failed : CustomQueryInterfaceResult.NotHandled;
    }

    public IEnumerator GetEnumerator()
    {
      return ((IEnumerable) this._target).GetEnumerator();
    }

    object IBindableVector.GetAt(uint index)
    {
      IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
      if (ibindableVectorNoThrow != null)
        return ibindableVectorNoThrow.GetAt(index);
      return (object) this.GetVectorOfT().GetAt(index);
    }

    uint IBindableVector.Size
    {
      get
      {
        IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
        if (ibindableVectorNoThrow != null)
          return ibindableVectorNoThrow.Size;
        return this.GetVectorOfT().Size;
      }
    }

    IBindableVectorView IBindableVector.GetView()
    {
      IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
      if (ibindableVectorNoThrow != null)
        return ibindableVectorNoThrow.GetView();
      return (IBindableVectorView) new ICustomPropertyProviderProxy<T1, T2>.IVectorViewToIBindableVectorViewAdapter<T1>(this.GetVectorOfT().GetView());
    }

    bool IBindableVector.IndexOf(object value, out uint index)
    {
      IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
      if (ibindableVectorNoThrow != null)
        return ibindableVectorNoThrow.IndexOf(value, out index);
      return this.GetVectorOfT().IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value), out index);
    }

    void IBindableVector.SetAt(uint index, object value)
    {
      IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
      if (ibindableVectorNoThrow != null)
        ibindableVectorNoThrow.SetAt(index, value);
      else
        this.GetVectorOfT().SetAt(index, ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
    }

    void IBindableVector.InsertAt(uint index, object value)
    {
      IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
      if (ibindableVectorNoThrow != null)
        ibindableVectorNoThrow.InsertAt(index, value);
      else
        this.GetVectorOfT().InsertAt(index, ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
    }

    void IBindableVector.RemoveAt(uint index)
    {
      IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
      if (ibindableVectorNoThrow != null)
        ibindableVectorNoThrow.RemoveAt(index);
      else
        this.GetVectorOfT().RemoveAt(index);
    }

    void IBindableVector.Append(object value)
    {
      IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
      if (ibindableVectorNoThrow != null)
        ibindableVectorNoThrow.Append(value);
      else
        this.GetVectorOfT().Append(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
    }

    void IBindableVector.RemoveAtEnd()
    {
      IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
      if (ibindableVectorNoThrow != null)
        ibindableVectorNoThrow.RemoveAtEnd();
      else
        this.GetVectorOfT().RemoveAtEnd();
    }

    void IBindableVector.Clear()
    {
      IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
      if (ibindableVectorNoThrow != null)
        ibindableVectorNoThrow.Clear();
      else
        this.GetVectorOfT().Clear();
    }

    [SecuritySafeCritical]
    private IBindableVector GetIBindableVectorNoThrow()
    {
      if ((this._flags & InterfaceForwardingSupport.IBindableVector) != InterfaceForwardingSupport.None)
        return JitHelpers.UnsafeCast<IBindableVector>(this._target);
      return (IBindableVector) null;
    }

    [SecuritySafeCritical]
    private IVector_Raw<T1> GetVectorOfT()
    {
      if ((this._flags & InterfaceForwardingSupport.IVector) != InterfaceForwardingSupport.None)
        return JitHelpers.UnsafeCast<IVector_Raw<T1>>(this._target);
      throw new InvalidOperationException();
    }

    object IBindableVectorView.GetAt(uint index)
    {
      IBindableVectorView vectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
      if (vectorViewNoThrow != null)
        return vectorViewNoThrow.GetAt(index);
      return (object) this.GetVectorViewOfT().GetAt(index);
    }

    uint IBindableVectorView.Size
    {
      get
      {
        IBindableVectorView vectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
        if (vectorViewNoThrow != null)
          return vectorViewNoThrow.Size;
        return this.GetVectorViewOfT().Size;
      }
    }

    bool IBindableVectorView.IndexOf(object value, out uint index)
    {
      IBindableVectorView vectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
      if (vectorViewNoThrow != null)
        return vectorViewNoThrow.IndexOf(value, out index);
      return this.GetVectorViewOfT().IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T2>(value), out index);
    }

    IBindableIterator IBindableIterable.First()
    {
      IBindableVectorView vectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
      if (vectorViewNoThrow != null)
        return vectorViewNoThrow.First();
      return (IBindableIterator) new ICustomPropertyProviderProxy<T1, T2>.IteratorOfTToIteratorAdapter<T2>(this.GetVectorViewOfT().First());
    }

    [SecuritySafeCritical]
    private IBindableVectorView GetIBindableVectorViewNoThrow()
    {
      if ((this._flags & InterfaceForwardingSupport.IBindableVectorView) != InterfaceForwardingSupport.None)
        return JitHelpers.UnsafeCast<IBindableVectorView>(this._target);
      return (IBindableVectorView) null;
    }

    [SecuritySafeCritical]
    private IVectorView<T2> GetVectorViewOfT()
    {
      if ((this._flags & InterfaceForwardingSupport.IVectorView) != InterfaceForwardingSupport.None)
        return JitHelpers.UnsafeCast<IVectorView<T2>>(this._target);
      throw new InvalidOperationException();
    }

    private static T ConvertTo<T>(object value)
    {
      ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
      return (T) value;
    }

    private sealed class IVectorViewToIBindableVectorViewAdapter<T> : IBindableVectorView, IBindableIterable
    {
      private IVectorView<T> _vectorView;

      public IVectorViewToIBindableVectorViewAdapter(IVectorView<T> vectorView)
      {
        this._vectorView = vectorView;
      }

      object IBindableVectorView.GetAt(uint index)
      {
        return (object) this._vectorView.GetAt(index);
      }

      uint IBindableVectorView.Size
      {
        get
        {
          return this._vectorView.Size;
        }
      }

      bool IBindableVectorView.IndexOf(object value, out uint index)
      {
        return this._vectorView.IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T>(value), out index);
      }

      IBindableIterator IBindableIterable.First()
      {
        return (IBindableIterator) new ICustomPropertyProviderProxy<T1, T2>.IteratorOfTToIteratorAdapter<T>(this._vectorView.First());
      }
    }

    private sealed class IteratorOfTToIteratorAdapter<T> : IBindableIterator
    {
      private IIterator<T> _iterator;

      public IteratorOfTToIteratorAdapter(IIterator<T> iterator)
      {
        this._iterator = iterator;
      }

      public bool HasCurrent
      {
        get
        {
          return this._iterator.HasCurrent;
        }
      }

      public object Current
      {
        get
        {
          return (object) this._iterator.Current;
        }
      }

      public bool MoveNext()
      {
        return this._iterator.MoveNext();
      }
    }
  }
}
