// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComEventsMethod
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  internal class ComEventsMethod
  {
    private ComEventsMethod.DelegateWrapper[] _delegateWrappers;
    private int _dispid;
    private ComEventsMethod _next;

    internal ComEventsMethod(int dispid)
    {
      this._delegateWrappers = (ComEventsMethod.DelegateWrapper[]) null;
      this._dispid = dispid;
    }

    internal static ComEventsMethod Find(ComEventsMethod methods, int dispid)
    {
      while (methods != null && methods._dispid != dispid)
        methods = methods._next;
      return methods;
    }

    internal static ComEventsMethod Add(ComEventsMethod methods, ComEventsMethod method)
    {
      method._next = methods;
      return method;
    }

    internal static ComEventsMethod Remove(ComEventsMethod methods, ComEventsMethod method)
    {
      if (methods == method)
      {
        methods = methods._next;
      }
      else
      {
        ComEventsMethod comEventsMethod = methods;
        while (comEventsMethod != null && comEventsMethod._next != method)
          comEventsMethod = comEventsMethod._next;
        if (comEventsMethod != null)
          comEventsMethod._next = method._next;
      }
      return methods;
    }

    internal int DispId
    {
      get
      {
        return this._dispid;
      }
    }

    internal bool Empty
    {
      get
      {
        if (this._delegateWrappers != null)
          return this._delegateWrappers.Length == 0;
        return true;
      }
    }

    internal void AddDelegate(Delegate d)
    {
      int index1 = 0;
      if (this._delegateWrappers != null)
        index1 = this._delegateWrappers.Length;
      for (int index2 = 0; index2 < index1; ++index2)
      {
        if (this._delegateWrappers[index2].Delegate.GetType() == d.GetType())
        {
          this._delegateWrappers[index2].Delegate = Delegate.Combine(this._delegateWrappers[index2].Delegate, d);
          return;
        }
      }
      ComEventsMethod.DelegateWrapper[] delegateWrapperArray = new ComEventsMethod.DelegateWrapper[index1 + 1];
      if (index1 > 0)
        this._delegateWrappers.CopyTo((Array) delegateWrapperArray, 0);
      ComEventsMethod.DelegateWrapper delegateWrapper = new ComEventsMethod.DelegateWrapper(d);
      delegateWrapperArray[index1] = delegateWrapper;
      this._delegateWrappers = delegateWrapperArray;
    }

    internal void RemoveDelegate(Delegate d)
    {
      int length = this._delegateWrappers.Length;
      int index1 = -1;
      for (int index2 = 0; index2 < length; ++index2)
      {
        if (this._delegateWrappers[index2].Delegate.GetType() == d.GetType())
        {
          index1 = index2;
          break;
        }
      }
      if (index1 < 0)
        return;
      Delegate @delegate = Delegate.Remove(this._delegateWrappers[index1].Delegate, d);
      if ((object) @delegate != null)
        this._delegateWrappers[index1].Delegate = @delegate;
      else if (length == 1)
      {
        this._delegateWrappers = (ComEventsMethod.DelegateWrapper[]) null;
      }
      else
      {
        ComEventsMethod.DelegateWrapper[] delegateWrapperArray = new ComEventsMethod.DelegateWrapper[length - 1];
        int index2;
        for (index2 = 0; index2 < index1; ++index2)
          delegateWrapperArray[index2] = this._delegateWrappers[index2];
        for (; index2 < length - 1; ++index2)
          delegateWrapperArray[index2] = this._delegateWrappers[index2 + 1];
        this._delegateWrappers = delegateWrapperArray;
      }
    }

    internal object Invoke(object[] args)
    {
      object obj = (object) null;
      foreach (ComEventsMethod.DelegateWrapper delegateWrapper in this._delegateWrappers)
      {
        if (delegateWrapper != null && (object) delegateWrapper.Delegate != null)
          obj = delegateWrapper.Invoke(args);
      }
      return obj;
    }

    internal class DelegateWrapper
    {
      private Delegate _d;
      private bool _once;
      private int _expectedParamsCount;
      private Type[] _cachedTargetTypes;

      public DelegateWrapper(Delegate d)
      {
        this._d = d;
      }

      public Delegate Delegate
      {
        get
        {
          return this._d;
        }
        set
        {
          this._d = value;
        }
      }

      public object Invoke(object[] args)
      {
        if ((object) this._d == null)
          return (object) null;
        if (!this._once)
        {
          this.PreProcessSignature();
          this._once = true;
        }
        if (this._cachedTargetTypes != null && this._expectedParamsCount == args.Length)
        {
          for (int index = 0; index < this._expectedParamsCount; ++index)
          {
            if (this._cachedTargetTypes[index] != (Type) null)
              args[index] = Enum.ToObject(this._cachedTargetTypes[index], args[index]);
          }
        }
        return this._d.DynamicInvoke(args);
      }

      private void PreProcessSignature()
      {
        ParameterInfo[] parameters = this._d.Method.GetParameters();
        this._expectedParamsCount = parameters.Length;
        Type[] typeArray = new Type[this._expectedParamsCount];
        bool flag = false;
        for (int index = 0; index < this._expectedParamsCount; ++index)
        {
          ParameterInfo parameterInfo = parameters[index];
          if (parameterInfo.ParameterType.IsByRef && parameterInfo.ParameterType.HasElementType && parameterInfo.ParameterType.GetElementType().IsEnum)
          {
            flag = true;
            typeArray[index] = parameterInfo.ParameterType.GetElementType();
          }
        }
        if (!flag)
          return;
        this._cachedTargetTypes = typeArray;
      }
    }
  }
}
