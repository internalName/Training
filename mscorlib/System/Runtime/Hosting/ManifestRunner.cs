// Decompiled with JetBrains decompiler
// Type: System.Runtime.Hosting.ManifestRunner
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Deployment.Internal.Isolation.Manifest;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Hosting
{
  internal sealed class ManifestRunner
  {
    private AppDomain m_domain;
    private string m_path;
    private string[] m_args;
    private ApartmentState m_apt;
    private RuntimeAssembly m_assembly;
    private int m_runResult;

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
    internal ManifestRunner(AppDomain domain, ActivationContext activationContext)
    {
      this.m_domain = domain;
      string fileName;
      string parameters;
      CmsUtils.GetEntryPoint(activationContext, out fileName, out parameters);
      if (string.IsNullOrEmpty(fileName))
        throw new ArgumentException(Environment.GetResourceString("Argument_NoMain"));
      if (string.IsNullOrEmpty(parameters))
        this.m_args = new string[0];
      else
        this.m_args = parameters.Split(' ');
      this.m_apt = ApartmentState.Unknown;
      this.m_path = Path.Combine(activationContext.ApplicationDirectory, fileName);
    }

    internal RuntimeAssembly EntryAssembly
    {
      [SecurityCritical, FileIOPermission(SecurityAction.Assert, Unrestricted = true), SecurityPermission(SecurityAction.Assert, Unrestricted = true)] get
      {
        if ((Assembly) this.m_assembly == (Assembly) null)
          this.m_assembly = (RuntimeAssembly) Assembly.LoadFrom(this.m_path);
        return this.m_assembly;
      }
    }

    [SecurityCritical]
    private void NewThreadRunner()
    {
      this.m_runResult = this.Run(false);
    }

    [SecurityCritical]
    private int RunInNewThread()
    {
      Thread thread = new Thread(new ThreadStart(this.NewThreadRunner));
      thread.SetApartmentState(this.m_apt);
      thread.Start();
      thread.Join();
      return this.m_runResult;
    }

    [SecurityCritical]
    private int Run(bool checkAptModel)
    {
      if (checkAptModel && this.m_apt != ApartmentState.Unknown)
      {
        if (Thread.CurrentThread.GetApartmentState() != ApartmentState.Unknown && Thread.CurrentThread.GetApartmentState() != this.m_apt)
          return this.RunInNewThread();
        Thread.CurrentThread.SetApartmentState(this.m_apt);
      }
      return this.m_domain.nExecuteAssembly(this.EntryAssembly, this.m_args);
    }

    [SecurityCritical]
    internal int ExecuteAsAssembly()
    {
      if (this.EntryAssembly.EntryPoint.GetCustomAttributes(typeof (STAThreadAttribute), false).Length != 0)
        this.m_apt = ApartmentState.STA;
      if (this.EntryAssembly.EntryPoint.GetCustomAttributes(typeof (MTAThreadAttribute), false).Length != 0)
        this.m_apt = this.m_apt != ApartmentState.Unknown ? ApartmentState.Unknown : ApartmentState.MTA;
      return this.Run(true);
    }
  }
}
