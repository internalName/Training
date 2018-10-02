// Decompiled with JetBrains decompiler
// Type: System.MulticastDelegate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Представляет групповой делегат, то есть делегат, имеющий в своем списке вызовов более одного элемента.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class MulticastDelegate : Delegate
  {
    [SecurityCritical]
    private object _invocationList;
    [SecurityCritical]
    private IntPtr _invocationCount;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MulticastDelegate" />.
    /// </summary>
    /// <param name="target">
    ///   Объект, для которого <paramref name="method" /> определен.
    /// </param>
    /// <param name="method">
    ///   Имя метода, для которого создается делегат.
    /// </param>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    protected MulticastDelegate(object target, string method)
      : base(target, method)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MulticastDelegate" />.
    /// </summary>
    /// <param name="target">
    ///   Тип объекта, на котором <paramref name="method" /> определен.
    /// </param>
    /// <param name="method">
    ///   Имя статического метода, для которого создается делегат.
    /// </param>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    protected MulticastDelegate(Type target, string method)
      : base(target, method)
    {
    }

    [SecuritySafeCritical]
    internal bool IsUnmanagedFunctionPtr()
    {
      return this._invocationCount == (IntPtr) -1;
    }

    [SecuritySafeCritical]
    internal bool InvocationListLogicallyNull()
    {
      if (this._invocationList != null && !(this._invocationList is LoaderAllocator))
        return this._invocationList is DynamicResolver;
      return true;
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект со всеми данными, необходимыми для сериализации данного экземпляра.
    /// </summary>
    /// <param name="info">
    ///   Объект, который содержит все данные, необходимые для сериализации или десериализации данного экземпляра.
    /// </param>
    /// <param name="context">
    ///   (Зарезервировано) Расположение, где хранятся и откуда извлекаются сериализованные данные.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Произошла ошибка сериализации.
    /// </exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      int targetIndex = 0;
      object[] invocationList = this._invocationList as object[];
      if (invocationList == null)
      {
        MethodInfo method = this.Method;
        if (!(method is RuntimeMethodInfo) || this.IsUnmanagedFunctionPtr())
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidDelegateType"));
        if (!this.InvocationListLogicallyNull() && !this._invocationCount.IsNull() && !this._methodPtrAux.IsNull())
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidDelegateType"));
        DelegateSerializationHolder.GetDelegateSerializationInfo(info, this.GetType(), this.Target, method, targetIndex);
      }
      else
      {
        DelegateSerializationHolder.DelegateEntry delegateEntry = (DelegateSerializationHolder.DelegateEntry) null;
        int invocationCount = (int) this._invocationCount;
        while (--invocationCount >= 0)
        {
          MulticastDelegate multicastDelegate = (MulticastDelegate) invocationList[invocationCount];
          MethodInfo method = multicastDelegate.Method;
          if (method is RuntimeMethodInfo && !this.IsUnmanagedFunctionPtr() && (multicastDelegate.InvocationListLogicallyNull() || multicastDelegate._invocationCount.IsNull() || multicastDelegate._methodPtrAux.IsNull()))
          {
            DelegateSerializationHolder.DelegateEntry serializationInfo = DelegateSerializationHolder.GetDelegateSerializationInfo(info, multicastDelegate.GetType(), multicastDelegate.Target, method, targetIndex++);
            if (delegateEntry != null)
              delegateEntry.Entry = serializationInfo;
            delegateEntry = serializationInfo;
          }
        }
        if (delegateEntry == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_InvalidDelegateType"));
      }
    }

    /// <summary>
    ///   Определяет, равны ли этот групповой делегат и указанный объект.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> и данный экземпляр имеют одинаковые списки вызовов; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override sealed bool Equals(object obj)
    {
      if (obj == null || !Delegate.InternalEqualTypes((object) this, obj))
        return false;
      MulticastDelegate d = obj as MulticastDelegate;
      if ((object) d == null)
        return false;
      if (this._invocationCount != (IntPtr) 0)
      {
        if (this.InvocationListLogicallyNull())
        {
          if (this.IsUnmanagedFunctionPtr())
          {
            if (!d.IsUnmanagedFunctionPtr())
              return false;
            return Delegate.CompareUnmanagedFunctionPtrs((Delegate) this, (Delegate) d);
          }
          if ((object) (d._invocationList as Delegate) != null)
            return this.Equals(d._invocationList);
          return base.Equals(obj);
        }
        if ((object) (this._invocationList as Delegate) != null)
          return this._invocationList.Equals(obj);
        return this.InvocationListEquals(d);
      }
      if (!this.InvocationListLogicallyNull())
      {
        if (!this._invocationList.Equals(d._invocationList))
          return false;
        return base.Equals((object) d);
      }
      if ((object) (d._invocationList as Delegate) != null)
        return this.Equals(d._invocationList);
      return base.Equals((object) d);
    }

    [SecuritySafeCritical]
    private bool InvocationListEquals(MulticastDelegate d)
    {
      object[] invocationList = this._invocationList as object[];
      if (d._invocationCount != this._invocationCount)
        return false;
      int invocationCount = (int) this._invocationCount;
      for (int index = 0; index < invocationCount; ++index)
      {
        if (!((Delegate) invocationList[index]).Equals((d._invocationList as object[])[index]))
          return false;
      }
      return true;
    }

    [SecurityCritical]
    private bool TrySetSlot(object[] a, int index, object o)
    {
      if (a[index] == null && Interlocked.CompareExchange<object>(ref a[index], o, (object) null) == null)
        return true;
      if (a[index] != null)
      {
        MulticastDelegate multicastDelegate1 = (MulticastDelegate) o;
        MulticastDelegate multicastDelegate2 = (MulticastDelegate) a[index];
        if (multicastDelegate2._methodPtr == multicastDelegate1._methodPtr && multicastDelegate2._target == multicastDelegate1._target && multicastDelegate2._methodPtrAux == multicastDelegate1._methodPtrAux)
          return true;
      }
      return false;
    }

    [SecurityCritical]
    private MulticastDelegate NewMulticastDelegate(object[] invocationList, int invocationCount, bool thisIsMultiCastAlready)
    {
      MulticastDelegate multicastDelegate = Delegate.InternalAllocLike((Delegate) this);
      if (thisIsMultiCastAlready)
      {
        multicastDelegate._methodPtr = this._methodPtr;
        multicastDelegate._methodPtrAux = this._methodPtrAux;
      }
      else
      {
        multicastDelegate._methodPtr = this.GetMulticastInvoke();
        multicastDelegate._methodPtrAux = this.GetInvokeMethod();
      }
      multicastDelegate._target = (object) multicastDelegate;
      multicastDelegate._invocationList = (object) invocationList;
      multicastDelegate._invocationCount = (IntPtr) invocationCount;
      return multicastDelegate;
    }

    [SecurityCritical]
    internal MulticastDelegate NewMulticastDelegate(object[] invocationList, int invocationCount)
    {
      return this.NewMulticastDelegate(invocationList, invocationCount, false);
    }

    [SecurityCritical]
    internal void StoreDynamicMethod(MethodInfo dynamicMethod)
    {
      if (this._invocationCount != (IntPtr) 0)
        ((Delegate) this._invocationList)._methodBase = (object) dynamicMethod;
      else
        this._methodBase = (object) dynamicMethod;
    }

    /// <summary>
    ///   Объединяет <see cref="T:System.Delegate" /> с указанным <see cref="T:System.Delegate" /> для формирования нового делегата.
    /// </summary>
    /// <param name="follow">
    ///   Делегат для объединения с данным делегатом.
    /// </param>
    /// <returns>
    ///   Объект делегат, который вызывается новый корень <see cref="T:System.MulticastDelegate" /> список вызовов.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="follow" />не совпадает с типом данного экземпляра.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    [SecuritySafeCritical]
    protected override sealed Delegate CombineImpl(Delegate follow)
    {
      if ((object) follow == null)
        return (Delegate) this;
      if (!Delegate.InternalEqualTypes((object) this, (object) follow))
        throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTypeMis"));
      MulticastDelegate multicastDelegate = (MulticastDelegate) follow;
      int num = 1;
      object[] invocationList1 = multicastDelegate._invocationList as object[];
      if (invocationList1 != null)
        num = (int) multicastDelegate._invocationCount;
      object[] invocationList2 = this._invocationList as object[];
      if (invocationList2 == null)
      {
        int invocationCount = 1 + num;
        object[] invocationList3 = new object[invocationCount];
        invocationList3[0] = (object) this;
        if (invocationList1 == null)
        {
          invocationList3[1] = (object) multicastDelegate;
        }
        else
        {
          for (int index = 0; index < num; ++index)
            invocationList3[1 + index] = invocationList1[index];
        }
        return (Delegate) this.NewMulticastDelegate(invocationList3, invocationCount);
      }
      int invocationCount1 = (int) this._invocationCount;
      int invocationCount2 = invocationCount1 + num;
      object[] objArray = (object[]) null;
      if (invocationCount2 <= invocationList2.Length)
      {
        objArray = invocationList2;
        if (invocationList1 == null)
        {
          if (!this.TrySetSlot(objArray, invocationCount1, (object) multicastDelegate))
            objArray = (object[]) null;
        }
        else
        {
          for (int index = 0; index < num; ++index)
          {
            if (!this.TrySetSlot(objArray, invocationCount1 + index, invocationList1[index]))
            {
              objArray = (object[]) null;
              break;
            }
          }
        }
      }
      if (objArray == null)
      {
        int length = invocationList2.Length;
        while (length < invocationCount2)
          length *= 2;
        objArray = new object[length];
        for (int index = 0; index < invocationCount1; ++index)
          objArray[index] = invocationList2[index];
        if (invocationList1 == null)
        {
          objArray[invocationCount1] = (object) multicastDelegate;
        }
        else
        {
          for (int index = 0; index < num; ++index)
            objArray[invocationCount1 + index] = invocationList1[index];
        }
      }
      return (Delegate) this.NewMulticastDelegate(objArray, invocationCount2, true);
    }

    [SecurityCritical]
    private object[] DeleteFromInvocationList(object[] invocationList, int invocationCount, int deleteIndex, int deleteCount)
    {
      int length = (this._invocationList as object[]).Length;
      while (length / 2 >= invocationCount - deleteCount)
        length /= 2;
      object[] objArray = new object[length];
      for (int index = 0; index < deleteIndex; ++index)
        objArray[index] = invocationList[index];
      for (int index = deleteIndex + deleteCount; index < invocationCount; ++index)
        objArray[index - deleteCount] = invocationList[index];
      return objArray;
    }

    private bool EqualInvocationLists(object[] a, object[] b, int start, int count)
    {
      for (int index = 0; index < count; ++index)
      {
        if (!a[start + index].Equals(b[index]))
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Удаляет элемент из списка вызовов данного <see cref="T:System.MulticastDelegate" /> равен указанный делегат.
    /// </summary>
    /// <param name="value">Для поиска в списке вызова делегата.</param>
    /// <returns>
    ///   Если <paramref name="value" /> найден в списке вызовов данного экземпляра, то новый <see cref="T:System.Delegate" /> без <paramref name="value" /> в своем списке вызовов; в противном случае — данный экземпляр со своим исходным списком вызовов.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    [SecuritySafeCritical]
    protected override sealed Delegate RemoveImpl(Delegate value)
    {
      MulticastDelegate multicastDelegate = value as MulticastDelegate;
      if ((object) multicastDelegate == null)
        return (Delegate) this;
      if (!(multicastDelegate._invocationList is object[]))
      {
        object[] invocationList = this._invocationList as object[];
        if (invocationList == null)
        {
          if (this.Equals((object) value))
            return (Delegate) null;
        }
        else
        {
          int invocationCount = (int) this._invocationCount;
          int deleteIndex = invocationCount;
          while (--deleteIndex >= 0)
          {
            if (value.Equals(invocationList[deleteIndex]))
            {
              if (invocationCount == 2)
                return (Delegate) invocationList[1 - deleteIndex];
              return (Delegate) this.NewMulticastDelegate(this.DeleteFromInvocationList(invocationList, invocationCount, deleteIndex, 1), invocationCount - 1, true);
            }
          }
        }
      }
      else
      {
        object[] invocationList = this._invocationList as object[];
        if (invocationList != null)
        {
          int invocationCount1 = (int) this._invocationCount;
          int invocationCount2 = (int) multicastDelegate._invocationCount;
          for (int index = invocationCount1 - invocationCount2; index >= 0; --index)
          {
            if (this.EqualInvocationLists(invocationList, multicastDelegate._invocationList as object[], index, invocationCount2))
            {
              if (invocationCount1 - invocationCount2 == 0)
                return (Delegate) null;
              if (invocationCount1 - invocationCount2 == 1)
                return (Delegate) invocationList[index != 0 ? 0 : invocationCount1 - 1];
              return (Delegate) this.NewMulticastDelegate(this.DeleteFromInvocationList(invocationList, invocationCount1, index, invocationCount2), invocationCount1 - invocationCount2, true);
            }
          }
        }
      }
      return (Delegate) this;
    }

    /// <summary>
    ///   Возвращает список вызовов данного группового делегата в порядке вызова.
    /// </summary>
    /// <returns>
    ///   Массив делегатов, списки вызовов которых совпадают со списком вызовов данного экземпляра.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override sealed Delegate[] GetInvocationList()
    {
      object[] invocationList = this._invocationList as object[];
      Delegate[] delegateArray;
      if (invocationList == null)
      {
        delegateArray = new Delegate[1]{ (Delegate) this };
      }
      else
      {
        int invocationCount = (int) this._invocationCount;
        delegateArray = new Delegate[invocationCount];
        for (int index = 0; index < invocationCount; ++index)
          delegateArray[index] = (Delegate) invocationList[index];
      }
      return delegateArray;
    }

    /// <summary>
    ///   Определяет равенство двух объектов <see cref="T:System.MulticastDelegate" />.
    /// </summary>
    /// <param name="d1">Левый операнд.</param>
    /// <param name="d2">Правый операнд.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="d1" /> и <paramref name="d2" /> имеют одинаковые списки вызовов; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool operator ==(MulticastDelegate d1, MulticastDelegate d2)
    {
      if ((object) d1 == null)
        return (object) d2 == null;
      return d1.Equals((object) d2);
    }

    /// <summary>
    ///   Определяет неравенство двух <see cref="T:System.MulticastDelegate" /> объекты не равны.
    /// </summary>
    /// <param name="d1">Левый операнд.</param>
    /// <param name="d2">Правый операнд.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="d1" /> и <paramref name="d2" /> не имеют одинаковые списки вызовов; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool operator !=(MulticastDelegate d1, MulticastDelegate d2)
    {
      if ((object) d1 == null)
        return d2 != null;
      return !d1.Equals((object) d2);
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Невозможно создать экземпляр абстрактного класса, или этот элемент был вызван с помощь механизма позднего связывания.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override sealed int GetHashCode()
    {
      if (this.IsUnmanagedFunctionPtr())
        return ValueType.GetHashCodeOfPtr(this._methodPtr) ^ ValueType.GetHashCodeOfPtr(this._methodPtrAux);
      object[] invocationList = this._invocationList as object[];
      if (invocationList == null)
        return base.GetHashCode();
      int num = 0;
      for (int index = 0; index < (int) this._invocationCount; ++index)
        num = num * 33 + invocationList[index].GetHashCode();
      return num;
    }

    [SecuritySafeCritical]
    internal override object GetTarget()
    {
      if (this._invocationCount != (IntPtr) 0)
      {
        if (this.InvocationListLogicallyNull())
          return (object) null;
        object[] invocationList1 = this._invocationList as object[];
        if (invocationList1 != null)
        {
          int invocationCount = (int) this._invocationCount;
          return ((Delegate) invocationList1[invocationCount - 1]).GetTarget();
        }
        Delegate invocationList2 = this._invocationList as Delegate;
        if ((object) invocationList2 != null)
          return invocationList2.GetTarget();
      }
      return base.GetTarget();
    }

    /// <summary>
    ///   Возвращает статический метод, представленный текущим <see cref="T:System.MulticastDelegate" />.
    /// </summary>
    /// <returns>
    ///   Статический метод, представленный текущим <see cref="T:System.MulticastDelegate" />.
    /// </returns>
    [SecuritySafeCritical]
    protected override MethodInfo GetMethodImpl()
    {
      if (this._invocationCount != (IntPtr) 0 && this._invocationList != null)
      {
        object[] invocationList1 = this._invocationList as object[];
        if (invocationList1 != null)
        {
          int index = (int) this._invocationCount - 1;
          return ((Delegate) invocationList1[index]).Method;
        }
        MulticastDelegate invocationList2 = this._invocationList as MulticastDelegate;
        if ((object) invocationList2 != null)
          return invocationList2.GetMethodImpl();
      }
      else if (this.IsUnmanagedFunctionPtr())
      {
        if (this._methodBase == null || (object) (this._methodBase as MethodInfo) == null)
        {
          IRuntimeMethodInfo methodHandle = this.FindMethodHandle();
          RuntimeType runtimeType = RuntimeMethodHandle.GetDeclaringType(methodHandle);
          if (RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType) || RuntimeTypeHandle.HasInstantiation(runtimeType))
            runtimeType = this.GetType() as RuntimeType;
          this._methodBase = (object) (MethodInfo) RuntimeType.GetMethodBase(runtimeType, methodHandle);
        }
        return (MethodInfo) this._methodBase;
      }
      return base.GetMethodImpl();
    }

    [DebuggerNonUserCode]
    private void ThrowNullThisInDelegateToInstance()
    {
      throw new ArgumentException(Environment.GetResourceString("Arg_DlgtNullInst"));
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorClosed(object target, IntPtr methodPtr)
    {
      if (target == null)
        this.ThrowNullThisInDelegateToInstance();
      this._target = target;
      this._methodPtr = methodPtr;
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorClosedStatic(object target, IntPtr methodPtr)
    {
      this._target = target;
      this._methodPtr = methodPtr;
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorRTClosed(object target, IntPtr methodPtr)
    {
      this._target = target;
      this._methodPtr = this.AdjustTarget(target, methodPtr);
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorOpened(object target, IntPtr methodPtr, IntPtr shuffleThunk)
    {
      this._target = (object) this;
      this._methodPtr = shuffleThunk;
      this._methodPtrAux = methodPtr;
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorSecureClosed(object target, IntPtr methodPtr, IntPtr callThunk, IntPtr creatorMethod)
    {
      MulticastDelegate multicastDelegate = Delegate.InternalAllocLike((Delegate) this);
      multicastDelegate.CtorClosed(target, methodPtr);
      this._invocationList = (object) multicastDelegate;
      this._target = (object) this;
      this._methodPtr = callThunk;
      this._methodPtrAux = creatorMethod;
      this._invocationCount = this.GetInvokeMethod();
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorSecureClosedStatic(object target, IntPtr methodPtr, IntPtr callThunk, IntPtr creatorMethod)
    {
      MulticastDelegate multicastDelegate = Delegate.InternalAllocLike((Delegate) this);
      multicastDelegate.CtorClosedStatic(target, methodPtr);
      this._invocationList = (object) multicastDelegate;
      this._target = (object) this;
      this._methodPtr = callThunk;
      this._methodPtrAux = creatorMethod;
      this._invocationCount = this.GetInvokeMethod();
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorSecureRTClosed(object target, IntPtr methodPtr, IntPtr callThunk, IntPtr creatorMethod)
    {
      MulticastDelegate multicastDelegate = Delegate.InternalAllocLike((Delegate) this);
      multicastDelegate.CtorRTClosed(target, methodPtr);
      this._invocationList = (object) multicastDelegate;
      this._target = (object) this;
      this._methodPtr = callThunk;
      this._methodPtrAux = creatorMethod;
      this._invocationCount = this.GetInvokeMethod();
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorSecureOpened(object target, IntPtr methodPtr, IntPtr shuffleThunk, IntPtr callThunk, IntPtr creatorMethod)
    {
      MulticastDelegate multicastDelegate = Delegate.InternalAllocLike((Delegate) this);
      multicastDelegate.CtorOpened(target, methodPtr, shuffleThunk);
      this._invocationList = (object) multicastDelegate;
      this._target = (object) this;
      this._methodPtr = callThunk;
      this._methodPtrAux = creatorMethod;
      this._invocationCount = this.GetInvokeMethod();
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorVirtualDispatch(object target, IntPtr methodPtr, IntPtr shuffleThunk)
    {
      this._target = (object) this;
      this._methodPtr = shuffleThunk;
      this._methodPtrAux = this.GetCallStub(methodPtr);
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorSecureVirtualDispatch(object target, IntPtr methodPtr, IntPtr shuffleThunk, IntPtr callThunk, IntPtr creatorMethod)
    {
      MulticastDelegate multicastDelegate = Delegate.InternalAllocLike((Delegate) this);
      multicastDelegate.CtorVirtualDispatch(target, methodPtr, shuffleThunk);
      this._invocationList = (object) multicastDelegate;
      this._target = (object) this;
      this._methodPtr = callThunk;
      this._methodPtrAux = creatorMethod;
      this._invocationCount = this.GetInvokeMethod();
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorCollectibleClosedStatic(object target, IntPtr methodPtr, IntPtr gchandle)
    {
      this._target = target;
      this._methodPtr = methodPtr;
      this._methodBase = GCHandle.InternalGet(gchandle);
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorCollectibleOpened(object target, IntPtr methodPtr, IntPtr shuffleThunk, IntPtr gchandle)
    {
      this._target = (object) this;
      this._methodPtr = shuffleThunk;
      this._methodPtrAux = methodPtr;
      this._methodBase = GCHandle.InternalGet(gchandle);
    }

    [SecurityCritical]
    [DebuggerNonUserCode]
    private void CtorCollectibleVirtualDispatch(object target, IntPtr methodPtr, IntPtr shuffleThunk, IntPtr gchandle)
    {
      this._target = (object) this;
      this._methodPtr = shuffleThunk;
      this._methodPtrAux = this.GetCallStub(methodPtr);
      this._methodBase = GCHandle.InternalGet(gchandle);
    }
  }
}
