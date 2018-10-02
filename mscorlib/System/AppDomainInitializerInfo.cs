// Decompiled with JetBrains decompiler
// Type: System.AppDomainInitializerInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System
{
  internal class AppDomainInitializerInfo
  {
    internal AppDomainInitializerInfo.ItemInfo[] Info;

    internal AppDomainInitializerInfo(AppDomainInitializer init)
    {
      this.Info = (AppDomainInitializerInfo.ItemInfo[]) null;
      if (init == null)
        return;
      List<AppDomainInitializerInfo.ItemInfo> itemInfoList = new List<AppDomainInitializerInfo.ItemInfo>();
      List<AppDomainInitializer> domainInitializerList = new List<AppDomainInitializer>();
      domainInitializerList.Add(init);
      int num = 0;
      while (domainInitializerList.Count > num)
      {
        Delegate[] invocationList = domainInitializerList[num++].GetInvocationList();
        for (int index = 0; index < invocationList.Length; ++index)
        {
          if (!invocationList[index].Method.IsStatic)
          {
            if (invocationList[index].Target != null)
            {
              AppDomainInitializer target = invocationList[index].Target as AppDomainInitializer;
              if (target == null)
                throw new ArgumentException(Environment.GetResourceString("Arg_MustBeStatic"), invocationList[index].Method.ReflectedType.FullName + "::" + invocationList[index].Method.Name);
              domainInitializerList.Add(target);
            }
          }
          else
            itemInfoList.Add(new AppDomainInitializerInfo.ItemInfo()
            {
              TargetTypeAssembly = invocationList[index].Method.ReflectedType.Module.Assembly.FullName,
              TargetTypeName = invocationList[index].Method.ReflectedType.FullName,
              MethodName = invocationList[index].Method.Name
            });
        }
      }
      this.Info = itemInfoList.ToArray();
    }

    [SecuritySafeCritical]
    internal AppDomainInitializer Unwrap()
    {
      if (this.Info == null)
        return (AppDomainInitializer) null;
      AppDomainInitializer domainInitializer1 = (AppDomainInitializer) null;
      new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
      for (int index = 0; index < this.Info.Length; ++index)
      {
        AppDomainInitializer domainInitializer2 = (AppDomainInitializer) Delegate.CreateDelegate(typeof (AppDomainInitializer), Assembly.Load(this.Info[index].TargetTypeAssembly).GetType(this.Info[index].TargetTypeName), this.Info[index].MethodName);
        if (domainInitializer1 == null)
          domainInitializer1 = domainInitializer2;
        else
          domainInitializer1 += domainInitializer2;
      }
      return domainInitializer1;
    }

    internal class ItemInfo
    {
      public string TargetTypeAssembly;
      public string TargetTypeName;
      public string MethodName;
    }
  }
}
