// Decompiled with JetBrains decompiler
// Type: System.MarshalByRefObject
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Разрешает доступ к объектам через границы доменов приложения в приложениях, поддерживающих удаленное взаимодействие.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public abstract class MarshalByRefObject
  {
    private object __identity;

    private object Identity
    {
      get
      {
        return this.__identity;
      }
      set
      {
        this.__identity = value;
      }
    }

    [SecuritySafeCritical]
    internal IntPtr GetComIUnknown(bool fIsBeingMarshalled)
    {
      return !RemotingServices.IsTransparentProxy((object) this) ? Marshal.GetIUnknownForObject((object) this) : RemotingServices.GetRealProxy((object) this).GetCOMIUnknown(fIsBeingMarshalled);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetComIUnknown(MarshalByRefObject o);

    internal bool IsInstanceOfType(Type T)
    {
      return T.IsInstanceOfType((object) this);
    }

    internal object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      Type type = this.GetType();
      if (!type.IsCOMObject)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_InvokeMember"));
      return type.InvokeMember(name, invokeAttr, binder, (object) this, args, modifiers, culture, namedParameters);
    }

    /// <summary>
    ///   Создает неполную копию текущего <see cref="T:System.MarshalByRefObject" /> объекта.
    /// </summary>
    /// <param name="cloneIdentity">
    ///   <see langword="false" />Чтобы удалить текущий <see cref="T:System.MarshalByRefObject" /> идентификатора объекта, что вызовет объекта для назначения нового удостоверения при маршалинге через границы удаленного взаимодействия.
    ///    Значение <see langword="false" /> является наиболее подходящим.
    ///   <see langword="true" />Чтобы скопировать текущую <see cref="T:System.MarshalByRefObject" /> идентификатора объекта его клон, что вызовет вызовы удаленного клиента для маршрутизации объект удаленного сервера.
    /// </param>
    /// <returns>
    ///   Неполная копия текущего <see cref="T:System.MarshalByRefObject" /> объекта.
    /// </returns>
    protected MarshalByRefObject MemberwiseClone(bool cloneIdentity)
    {
      MarshalByRefObject marshalByRefObject = (MarshalByRefObject) this.MemberwiseClone();
      if (!cloneIdentity)
        marshalByRefObject.Identity = (object) null;
      return marshalByRefObject;
    }

    [SecuritySafeCritical]
    internal static System.Runtime.Remoting.Identity GetIdentity(MarshalByRefObject obj, out bool fServer)
    {
      fServer = true;
      System.Runtime.Remoting.Identity identity = (System.Runtime.Remoting.Identity) null;
      if (obj != null)
      {
        if (!RemotingServices.IsTransparentProxy((object) obj))
        {
          identity = (System.Runtime.Remoting.Identity) obj.Identity;
        }
        else
        {
          fServer = false;
          identity = RemotingServices.GetRealProxy((object) obj).IdentityObject;
        }
      }
      return identity;
    }

    internal static System.Runtime.Remoting.Identity GetIdentity(MarshalByRefObject obj)
    {
      bool fServer;
      return MarshalByRefObject.GetIdentity(obj, out fServer);
    }

    internal ServerIdentity __RaceSetServerIdentity(ServerIdentity id)
    {
      if (this.__identity == null)
      {
        if (!id.IsContextBound)
          id.RaceSetTransparentProxy((object) this);
        Interlocked.CompareExchange(ref this.__identity, (object) id, (object) null);
      }
      return (ServerIdentity) this.__identity;
    }

    internal void __ResetServerIdentity()
    {
      this.__identity = (object) null;
    }

    /// <summary>
    ///   Извлекает объект текущее время существования службы, который управляет политикой времени существования данного экземпляра.
    /// </summary>
    /// <returns>
    ///   Объект типа <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> используется для управления политикой времени существования данного экземпляра.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public object GetLifetimeService()
    {
      return (object) LifetimeServices.GetLease(this);
    }

    /// <summary>
    ///   Получает объект службы времени существования для управления политикой времени существования для этого экземпляра.
    /// </summary>
    /// <returns>
    ///   Объект типа <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> используется для управления политикой времени существования данного экземпляра.
    ///    Это текущее время жизни объекта службы для данного экземпляра, если он существует; в противном случае — значение инициализируется объект новое время существования службы <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime" /> свойство.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public virtual object InitializeLifetimeService()
    {
      return (object) LifetimeServices.GetLeaseInitial(this);
    }

    /// <summary>
    ///   Создает объект, который содержит все необходимые сведения, необходимые для создания прокси-сервер, используемый для обмена данными с удаленным объектом.
    /// </summary>
    /// <param name="requestedType">
    ///   <see cref="T:System.Type" /> Объекта, новый <see cref="T:System.Runtime.Remoting.ObjRef" /> будет ссылаться.
    /// </param>
    /// <returns>
    ///   Сведения, необходимые для создания учетной записи-посредника.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Этот экземпляр не является допустимым удаленным объектом.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public virtual ObjRef CreateObjRef(Type requestedType)
    {
      if (this.__identity == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
      return new ObjRef(this, requestedType);
    }

    [SecuritySafeCritical]
    internal bool CanCastToXmlType(string xmlTypeName, string xmlTypeNamespace)
    {
      Type type = SoapServices.GetInteropTypeFromXmlType(xmlTypeName, xmlTypeNamespace);
      if (type == (Type) null)
      {
        string typeNamespace;
        string assemblyName;
        if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlTypeNamespace, out typeNamespace, out assemblyName))
          return false;
        string name = typeNamespace == null || typeNamespace.Length <= 0 ? xmlTypeName : typeNamespace + "." + xmlTypeName;
        try
        {
          type = Assembly.Load(assemblyName).GetType(name, false, false);
        }
        catch
        {
          return false;
        }
      }
      if (type != (Type) null)
        return type.IsAssignableFrom(this.GetType());
      return false;
    }

    [SecuritySafeCritical]
    internal static bool CanCastToXmlTypeHelper(RuntimeType castType, MarshalByRefObject o)
    {
      if (castType == (RuntimeType) null)
        throw new ArgumentNullException(nameof (castType));
      if (!castType.IsInterface && !castType.IsMarshalByRef)
        return false;
      string xmlType = (string) null;
      string xmlTypeNamespace = (string) null;
      if (!SoapServices.GetXmlTypeForInteropType((Type) castType, out xmlType, out xmlTypeNamespace))
      {
        xmlType = castType.Name;
        xmlTypeNamespace = SoapServices.CodeXmlNamespaceForClrTypeNamespace(castType.Namespace, castType.GetRuntimeAssembly().GetSimpleName());
      }
      return o.CanCastToXmlType(xmlType, xmlTypeNamespace);
    }
  }
}
