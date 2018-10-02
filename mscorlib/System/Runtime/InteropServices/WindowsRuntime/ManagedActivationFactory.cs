// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ManagedActivationFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.None)]
  internal sealed class ManagedActivationFactory : IActivationFactory, IManagedActivationFactory
  {
    private Type m_type;

    [SecurityCritical]
    internal ManagedActivationFactory(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if ((object) (type as RuntimeType) == null || !type.IsExportedToWindowsRuntime)
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeNotActivatableViaWindowsRuntime", (object) type), nameof (type));
      this.m_type = type;
    }

    public object ActivateInstance()
    {
      try
      {
        return Activator.CreateInstance(this.m_type);
      }
      catch (MissingMethodException ex)
      {
        throw new NotImplementedException();
      }
      catch (TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }

    void IManagedActivationFactory.RunClassConstructor()
    {
      RuntimeHelpers.RunClassConstructor(this.m_type.TypeHandle);
    }
  }
}
