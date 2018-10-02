// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.IsolationInterop
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Deployment.Internal.Isolation.Manifest;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  internal static class IsolationInterop
  {
    private static object _synchObject = new object();
    private static volatile IIdentityAuthority _idAuth = (IIdentityAuthority) null;
    private static volatile IAppIdAuthority _appIdAuth = (IAppIdAuthority) null;
    public static Guid IID_ICMS = IsolationInterop.GetGuidOfType(typeof (ICMS));
    public static Guid IID_IDefinitionIdentity = IsolationInterop.GetGuidOfType(typeof (IDefinitionIdentity));
    public static Guid IID_IManifestInformation = IsolationInterop.GetGuidOfType(typeof (IManifestInformation));
    public static Guid IID_IEnumSTORE_ASSEMBLY = IsolationInterop.GetGuidOfType(typeof (IEnumSTORE_ASSEMBLY));
    public static Guid IID_IEnumSTORE_ASSEMBLY_FILE = IsolationInterop.GetGuidOfType(typeof (IEnumSTORE_ASSEMBLY_FILE));
    public static Guid IID_IEnumSTORE_CATEGORY = IsolationInterop.GetGuidOfType(typeof (IEnumSTORE_CATEGORY));
    public static Guid IID_IEnumSTORE_CATEGORY_INSTANCE = IsolationInterop.GetGuidOfType(typeof (IEnumSTORE_CATEGORY_INSTANCE));
    public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA = IsolationInterop.GetGuidOfType(typeof (IEnumSTORE_DEPLOYMENT_METADATA));
    public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY = IsolationInterop.GetGuidOfType(typeof (IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY));
    public static Guid IID_IStore = IsolationInterop.GetGuidOfType(typeof (IStore));
    public static Guid GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING = new Guid("2ec93463-b0c3-45e1-8364-327e96aea856");
    public static Guid SXS_INSTALL_REFERENCE_SCHEME_SXS_STRONGNAME_SIGNED_PRIVATE_ASSEMBLY = new Guid("3ab20ac0-67e8-4512-8385-a487e35df3da");
    public const string IsolationDllName = "clr.dll";

    [SecuritySafeCritical]
    public static Store GetUserStore()
    {
      return new Store(IsolationInterop.GetUserStore(0U, IntPtr.Zero, ref IsolationInterop.IID_IStore) as IStore);
    }

    public static IIdentityAuthority IdentityAuthority
    {
      [SecuritySafeCritical] get
      {
        if (IsolationInterop._idAuth == null)
        {
          lock (IsolationInterop._synchObject)
          {
            if (IsolationInterop._idAuth == null)
              IsolationInterop._idAuth = IsolationInterop.GetIdentityAuthority();
          }
        }
        return IsolationInterop._idAuth;
      }
    }

    public static IAppIdAuthority AppIdAuthority
    {
      [SecuritySafeCritical] get
      {
        if (IsolationInterop._appIdAuth == null)
        {
          lock (IsolationInterop._synchObject)
          {
            if (IsolationInterop._appIdAuth == null)
              IsolationInterop._appIdAuth = IsolationInterop.GetAppIdAuthority();
          }
        }
        return IsolationInterop._appIdAuth;
      }
    }

    [SecuritySafeCritical]
    internal static IActContext CreateActContext(IDefinitionAppId AppId)
    {
      IsolationInterop.CreateActContextParameters Params;
      Params.Size = (uint) Marshal.SizeOf(typeof (IsolationInterop.CreateActContextParameters));
      Params.Flags = 16U;
      Params.CustomStoreList = IntPtr.Zero;
      Params.CultureFallbackList = IntPtr.Zero;
      Params.ProcessorArchitectureList = IntPtr.Zero;
      Params.Source = IntPtr.Zero;
      Params.ProcArch = (ushort) 0;
      IsolationInterop.CreateActContextParametersSource parametersSource;
      parametersSource.Size = (uint) Marshal.SizeOf(typeof (IsolationInterop.CreateActContextParametersSource));
      parametersSource.Flags = 0U;
      parametersSource.SourceType = 1U;
      parametersSource.Data = IntPtr.Zero;
      IsolationInterop.CreateActContextParametersSourceDefinitionAppid sourceDefinitionAppid;
      sourceDefinitionAppid.Size = (uint) Marshal.SizeOf(typeof (IsolationInterop.CreateActContextParametersSourceDefinitionAppid));
      sourceDefinitionAppid.Flags = 0U;
      sourceDefinitionAppid.AppId = AppId;
      try
      {
        parametersSource.Data = sourceDefinitionAppid.ToIntPtr();
        Params.Source = parametersSource.ToIntPtr();
        return IsolationInterop.CreateActContext(ref Params) as IActContext;
      }
      finally
      {
        if (parametersSource.Data != IntPtr.Zero)
        {
          IsolationInterop.CreateActContextParametersSourceDefinitionAppid.Destroy(parametersSource.Data);
          parametersSource.Data = IntPtr.Zero;
        }
        if (Params.Source != IntPtr.Zero)
        {
          IsolationInterop.CreateActContextParametersSource.Destroy(Params.Source);
          Params.Source = IntPtr.Zero;
        }
      }
    }

    [DllImport("clr.dll", PreserveSig = false)]
    [return: MarshalAs(UnmanagedType.IUnknown)]
    internal static extern object CreateActContext(ref IsolationInterop.CreateActContextParameters Params);

    [SecurityCritical]
    [DllImport("clr.dll", PreserveSig = false)]
    [return: MarshalAs(UnmanagedType.IUnknown)]
    internal static extern object CreateCMSFromXml([In] byte[] buffer, [In] uint bufferSize, [In] IManifestParseErrorCallback Callback, [In] ref Guid riid);

    [SecurityCritical]
    [DllImport("clr.dll", PreserveSig = false)]
    [return: MarshalAs(UnmanagedType.IUnknown)]
    internal static extern object ParseManifest([MarshalAs(UnmanagedType.LPWStr), In] string pszManifestPath, [In] IManifestParseErrorCallback pIManifestParseErrorCallback, [In] ref Guid riid);

    [SecurityCritical]
    [DllImport("clr.dll", PreserveSig = false)]
    [return: MarshalAs(UnmanagedType.IUnknown)]
    private static extern object GetUserStore([In] uint Flags, [In] IntPtr hToken, [In] ref Guid riid);

    [SecurityCritical]
    [DllImport("clr.dll", PreserveSig = false)]
    [return: MarshalAs(UnmanagedType.Interface)]
    private static extern IIdentityAuthority GetIdentityAuthority();

    [SecurityCritical]
    [DllImport("clr.dll", PreserveSig = false)]
    [return: MarshalAs(UnmanagedType.Interface)]
    private static extern IAppIdAuthority GetAppIdAuthority();

    internal static Guid GetGuidOfType(Type type)
    {
      return new Guid(((GuidAttribute) Attribute.GetCustomAttribute((MemberInfo) type, typeof (GuidAttribute), false)).Value);
    }

    internal struct CreateActContextParameters
    {
      [MarshalAs(UnmanagedType.U4)]
      public uint Size;
      [MarshalAs(UnmanagedType.U4)]
      public uint Flags;
      [MarshalAs(UnmanagedType.SysInt)]
      public IntPtr CustomStoreList;
      [MarshalAs(UnmanagedType.SysInt)]
      public IntPtr CultureFallbackList;
      [MarshalAs(UnmanagedType.SysInt)]
      public IntPtr ProcessorArchitectureList;
      [MarshalAs(UnmanagedType.SysInt)]
      public IntPtr Source;
      [MarshalAs(UnmanagedType.U2)]
      public ushort ProcArch;

      [Flags]
      public enum CreateFlags
      {
        Nothing = 0,
        StoreListValid = 1,
        CultureListValid = 2,
        ProcessorFallbackListValid = 4,
        ProcessorValid = 8,
        SourceValid = 16, // 0x00000010
        IgnoreVisibility = 32, // 0x00000020
      }
    }

    internal struct CreateActContextParametersSource
    {
      [MarshalAs(UnmanagedType.U4)]
      public uint Size;
      [MarshalAs(UnmanagedType.U4)]
      public uint Flags;
      [MarshalAs(UnmanagedType.U4)]
      public uint SourceType;
      [MarshalAs(UnmanagedType.SysInt)]
      public IntPtr Data;

      [SecurityCritical]
      public IntPtr ToIntPtr()
      {
        IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf<IsolationInterop.CreateActContextParametersSource>(this));
        Marshal.StructureToPtr<IsolationInterop.CreateActContextParametersSource>(this, ptr, false);
        return ptr;
      }

      [SecurityCritical]
      public static void Destroy(IntPtr p)
      {
        Marshal.DestroyStructure(p, typeof (IsolationInterop.CreateActContextParametersSource));
        Marshal.FreeCoTaskMem(p);
      }

      [Flags]
      public enum SourceFlags
      {
        Definition = 1,
        Reference = 2,
      }
    }

    internal struct CreateActContextParametersSourceDefinitionAppid
    {
      [MarshalAs(UnmanagedType.U4)]
      public uint Size;
      [MarshalAs(UnmanagedType.U4)]
      public uint Flags;
      public IDefinitionAppId AppId;

      [SecurityCritical]
      public IntPtr ToIntPtr()
      {
        IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf<IsolationInterop.CreateActContextParametersSourceDefinitionAppid>(this));
        Marshal.StructureToPtr<IsolationInterop.CreateActContextParametersSourceDefinitionAppid>(this, ptr, false);
        return ptr;
      }

      [SecurityCritical]
      public static void Destroy(IntPtr p)
      {
        Marshal.DestroyStructure(p, typeof (IsolationInterop.CreateActContextParametersSourceDefinitionAppid));
        Marshal.FreeCoTaskMem(p);
      }
    }
  }
}
