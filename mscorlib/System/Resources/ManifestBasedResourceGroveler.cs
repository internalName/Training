// Decompiled with JetBrains decompiler
// Type: System.Resources.ManifestBasedResourceGroveler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Resources
{
  internal class ManifestBasedResourceGroveler : IResourceGroveler
  {
    private ResourceManager.ResourceManagerMediator _mediator;

    public ManifestBasedResourceGroveler(ResourceManager.ResourceManagerMediator mediator)
    {
      this._mediator = mediator;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark)
    {
      ResourceSet resourceSet = (ResourceSet) null;
      Stream store = (Stream) null;
      CultureInfo cultureInfo = this.UltimateFallbackFixup(culture);
      RuntimeAssembly satellite;
      if (cultureInfo.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.MainAssembly)
        satellite = this._mediator.MainAssembly;
      else if (!cultureInfo.HasInvariantCultureName && !this._mediator.TryLookingForSatellite(cultureInfo))
      {
        satellite = (RuntimeAssembly) null;
      }
      else
      {
        satellite = this.GetSatelliteAssembly(cultureInfo, ref stackMark);
        if ((Assembly) satellite == (Assembly) null && (culture.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.Satellite))
          this.HandleSatelliteMissing();
      }
      string resourceFileName = this._mediator.GetResourceFileName(cultureInfo);
      if ((Assembly) satellite != (Assembly) null)
      {
        lock (localResourceSets)
        {
          if (localResourceSets.TryGetValue(culture.Name, out resourceSet))
          {
            if (FrameworkEventSource.IsInitialized)
              FrameworkEventSource.Log.ResourceManagerFoundResourceSetInCacheUnexpected(this._mediator.BaseName, (Assembly) this._mediator.MainAssembly, culture.Name);
          }
        }
        store = this.GetManifestResourceStream(satellite, resourceFileName, ref stackMark);
      }
      if (FrameworkEventSource.IsInitialized)
      {
        if (store != null)
          FrameworkEventSource.Log.ResourceManagerStreamFound(this._mediator.BaseName, (Assembly) this._mediator.MainAssembly, culture.Name, (Assembly) satellite, resourceFileName);
        else
          FrameworkEventSource.Log.ResourceManagerStreamNotFound(this._mediator.BaseName, (Assembly) this._mediator.MainAssembly, culture.Name, (Assembly) satellite, resourceFileName);
      }
      if (createIfNotExists && store != null && resourceSet == null)
      {
        if (FrameworkEventSource.IsInitialized)
          FrameworkEventSource.Log.ResourceManagerCreatingResourceSet(this._mediator.BaseName, (Assembly) this._mediator.MainAssembly, culture.Name, resourceFileName);
        resourceSet = this.CreateResourceSet(store, (Assembly) satellite);
      }
      else if (store == null & tryParents && culture.HasInvariantCultureName)
        this.HandleResourceStreamMissing(resourceFileName);
      if (!createIfNotExists && store != null && (resourceSet == null && FrameworkEventSource.IsInitialized))
        FrameworkEventSource.Log.ResourceManagerNotCreatingResourceSet(this._mediator.BaseName, (Assembly) this._mediator.MainAssembly, culture.Name);
      return resourceSet;
    }

    public bool HasNeutralResources(CultureInfo culture, string defaultResName)
    {
      string str = defaultResName;
      if (this._mediator.LocationInfo != (Type) null && this._mediator.LocationInfo.Namespace != null)
        str = this._mediator.LocationInfo.Namespace + Type.Delimiter.ToString() + defaultResName;
      foreach (string manifestResourceName in this._mediator.MainAssembly.GetManifestResourceNames())
      {
        if (manifestResourceName.Equals(str))
          return true;
      }
      return false;
    }

    private CultureInfo UltimateFallbackFixup(CultureInfo lookForCulture)
    {
      CultureInfo cultureInfo = lookForCulture;
      if (lookForCulture.Name == this._mediator.NeutralResourcesCulture.Name && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.MainAssembly)
      {
        if (FrameworkEventSource.IsInitialized)
          FrameworkEventSource.Log.ResourceManagerNeutralResourcesSufficient(this._mediator.BaseName, (Assembly) this._mediator.MainAssembly, lookForCulture.Name);
        cultureInfo = CultureInfo.InvariantCulture;
      }
      else if (lookForCulture.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.Satellite)
        cultureInfo = this._mediator.NeutralResourcesCulture;
      return cultureInfo;
    }

    [SecurityCritical]
    internal static CultureInfo GetNeutralResourcesLanguage(Assembly a, ref UltimateResourceFallbackLocation fallbackLocation)
    {
      string s = (string) null;
      short fallbackLocation1 = 0;
      if (ManifestBasedResourceGroveler.GetNeutralResourcesLanguageAttribute(((RuntimeAssembly) a).GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref s), out fallbackLocation1))
      {
        if (fallbackLocation1 < (short) 0 || fallbackLocation1 > (short) 1)
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidNeutralResourcesLanguage_FallbackLoc", (object) fallbackLocation1));
        fallbackLocation = (UltimateResourceFallbackLocation) fallbackLocation1;
        try
        {
          return CultureInfo.GetCultureInfo(s);
        }
        catch (ArgumentException ex)
        {
          if (a == typeof (object).Assembly)
            return CultureInfo.InvariantCulture;
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidNeutralResourcesLanguage_Asm_Culture", (object) a.ToString(), (object) s), (Exception) ex);
        }
      }
      else
      {
        if (FrameworkEventSource.IsInitialized)
          FrameworkEventSource.Log.ResourceManagerNeutralResourceAttributeMissing(a);
        fallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
        return CultureInfo.InvariantCulture;
      }
    }

    [SecurityCritical]
    internal ResourceSet CreateResourceSet(Stream store, Assembly assembly)
    {
      if (store.CanSeek && store.Length > 4L)
      {
        long position = store.Position;
        BinaryReader binaryReader = new BinaryReader(store);
        if (binaryReader.ReadInt32() == ResourceManager.MagicNumber)
        {
          int num1 = binaryReader.ReadInt32();
          string str1;
          string str2;
          if (num1 == ResourceManager.HeaderVersionNumber)
          {
            binaryReader.ReadInt32();
            str1 = binaryReader.ReadString();
            str2 = binaryReader.ReadString();
          }
          else if (num1 > ResourceManager.HeaderVersionNumber)
          {
            int num2 = binaryReader.ReadInt32();
            long offset = binaryReader.BaseStream.Position + (long) num2;
            str1 = binaryReader.ReadString();
            str2 = binaryReader.ReadString();
            binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
          }
          else
            throw new NotSupportedException(Environment.GetResourceString("NotSupported_ObsoleteResourcesFile", (object) this._mediator.MainAssembly.GetSimpleName()));
          store.Position = position;
          if (this.CanUseDefaultResourceClasses(str1, str2))
            return (ResourceSet) new RuntimeResourceSet(store);
          object[] args = new object[1]
          {
            (object) (IResourceReader) Activator.CreateInstance(Type.GetType(str1, true), new object[1]
            {
              (object) store
            })
          };
          return (ResourceSet) Activator.CreateInstance(!(this._mediator.UserResourceSet == (Type) null) ? this._mediator.UserResourceSet : Type.GetType(str2, true, false), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, args, (CultureInfo) null, (object[]) null);
        }
        store.Position = position;
      }
      if (this._mediator.UserResourceSet == (Type) null)
        return (ResourceSet) new RuntimeResourceSet(store);
      object[] objArray = new object[2]
      {
        (object) store,
        (object) assembly
      };
      try
      {
        try
        {
          return (ResourceSet) Activator.CreateInstance(this._mediator.UserResourceSet, objArray);
        }
        catch (MissingMethodException ex)
        {
        }
        return (ResourceSet) Activator.CreateInstance(this._mediator.UserResourceSet, new object[1]
        {
          (object) store
        });
      }
      catch (MissingMethodException ex)
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResMgrBadResSet_Type", (object) this._mediator.UserResourceSet.AssemblyQualifiedName), (Exception) ex);
      }
    }

    [SecurityCritical]
    private Stream GetManifestResourceStream(RuntimeAssembly satellite, string fileName, ref StackCrawlMark stackMark)
    {
      bool skipSecurityCheck = (Assembly) this._mediator.MainAssembly == (Assembly) satellite && (Assembly) this._mediator.CallingAssembly == (Assembly) this._mediator.MainAssembly;
      return satellite.GetManifestResourceStream(this._mediator.LocationInfo, fileName, skipSecurityCheck, ref stackMark) ?? this.CaseInsensitiveManifestResourceStreamLookup(satellite, fileName);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private Stream CaseInsensitiveManifestResourceStreamLookup(RuntimeAssembly satellite, string name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this._mediator.LocationInfo != (Type) null)
      {
        string str = this._mediator.LocationInfo.Namespace;
        if (str != null)
        {
          stringBuilder.Append(str);
          if (name != null)
            stringBuilder.Append(Type.Delimiter);
        }
      }
      stringBuilder.Append(name);
      string str1 = stringBuilder.ToString();
      CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
      string str2 = (string) null;
      foreach (string manifestResourceName in satellite.GetManifestResourceNames())
      {
        if (compareInfo.Compare(manifestResourceName, str1, CompareOptions.IgnoreCase) == 0)
        {
          if (str2 == null)
            str2 = manifestResourceName;
          else
            throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_MultipleBlobs", (object) str1, (object) satellite.ToString()));
        }
      }
      if (FrameworkEventSource.IsInitialized)
      {
        if (str2 != null)
          FrameworkEventSource.Log.ResourceManagerCaseInsensitiveResourceStreamLookupSucceeded(this._mediator.BaseName, (Assembly) this._mediator.MainAssembly, satellite.GetSimpleName(), str1);
        else
          FrameworkEventSource.Log.ResourceManagerCaseInsensitiveResourceStreamLookupFailed(this._mediator.BaseName, (Assembly) this._mediator.MainAssembly, satellite.GetSimpleName(), str1);
      }
      if (str2 == null)
        return (Stream) null;
      bool skipSecurityCheck = (Assembly) this._mediator.MainAssembly == (Assembly) satellite && (Assembly) this._mediator.CallingAssembly == (Assembly) this._mediator.MainAssembly;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Stream manifestResourceStream = satellite.GetManifestResourceStream(str2, ref stackMark, skipSecurityCheck);
      if (manifestResourceStream != null && FrameworkEventSource.IsInitialized)
        FrameworkEventSource.Log.ResourceManagerManifestResourceAccessDenied(this._mediator.BaseName, (Assembly) this._mediator.MainAssembly, satellite.GetSimpleName(), str2);
      return manifestResourceStream;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private RuntimeAssembly GetSatelliteAssembly(CultureInfo lookForCulture, ref StackCrawlMark stackMark)
    {
      if (!this._mediator.LookedForSatelliteContractVersion)
      {
        this._mediator.SatelliteContractVersion = this._mediator.ObtainSatelliteContractVersion((Assembly) this._mediator.MainAssembly);
        this._mediator.LookedForSatelliteContractVersion = true;
      }
      RuntimeAssembly runtimeAssembly = (RuntimeAssembly) null;
      string satelliteAssemblyName = this.GetSatelliteAssemblyName();
      try
      {
        runtimeAssembly = this._mediator.MainAssembly.InternalGetSatelliteAssembly(satelliteAssemblyName, lookForCulture, this._mediator.SatelliteContractVersion, false, ref stackMark);
      }
      catch (FileLoadException ex)
      {
        int hresult = ex._HResult;
        Win32Native.MakeHRFromErrorCode(5);
      }
      catch (BadImageFormatException ex)
      {
      }
      if (FrameworkEventSource.IsInitialized)
      {
        if ((Assembly) runtimeAssembly != (Assembly) null)
          FrameworkEventSource.Log.ResourceManagerGetSatelliteAssemblySucceeded(this._mediator.BaseName, (Assembly) this._mediator.MainAssembly, lookForCulture.Name, satelliteAssemblyName);
        else
          FrameworkEventSource.Log.ResourceManagerGetSatelliteAssemblyFailed(this._mediator.BaseName, (Assembly) this._mediator.MainAssembly, lookForCulture.Name, satelliteAssemblyName);
      }
      return runtimeAssembly;
    }

    private bool CanUseDefaultResourceClasses(string readerTypeName, string resSetTypeName)
    {
      if (this._mediator.UserResourceSet != (Type) null)
        return false;
      AssemblyName asmName2 = new AssemblyName(ResourceManager.MscorlibName);
      return (readerTypeName == null || ResourceManager.CompareNames(readerTypeName, ResourceManager.ResReaderTypeName, asmName2)) && (resSetTypeName == null || ResourceManager.CompareNames(resSetTypeName, ResourceManager.ResSetTypeName, asmName2));
    }

    [SecurityCritical]
    private string GetSatelliteAssemblyName()
    {
      return this._mediator.MainAssembly.GetSimpleName() + ".resources";
    }

    [SecurityCritical]
    private void HandleSatelliteMissing()
    {
      string str1 = this._mediator.MainAssembly.GetSimpleName() + ".resources.dll";
      if (this._mediator.SatelliteContractVersion != (Version) null)
        str1 = str1 + ", Version=" + this._mediator.SatelliteContractVersion.ToString();
      AssemblyName assemblyName = new AssemblyName();
      assemblyName.SetPublicKey(this._mediator.MainAssembly.GetPublicKey());
      byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
      int length = publicKeyToken.Length;
      StringBuilder stringBuilder = new StringBuilder(length * 2);
      for (int index = 0; index < length; ++index)
        stringBuilder.Append(publicKeyToken[index].ToString("x", (IFormatProvider) CultureInfo.InvariantCulture));
      string str2 = str1 + ", PublicKeyToken=" + (object) stringBuilder;
      string cultureName = this._mediator.NeutralResourcesCulture.Name;
      if (cultureName.Length == 0)
        cultureName = "<invariant>";
      throw new MissingSatelliteAssemblyException(Environment.GetResourceString("MissingSatelliteAssembly_Culture_Name", (object) this._mediator.NeutralResourcesCulture, (object) str2), cultureName);
    }

    [SecurityCritical]
    private void HandleResourceStreamMissing(string fileName)
    {
      if ((Assembly) this._mediator.MainAssembly == typeof (object).Assembly && this._mediator.BaseName.Equals("mscorlib"))
        Environment.FailFast("mscorlib.resources couldn't be found!  Large parts of the BCL won't work!");
      string str = string.Empty;
      if (this._mediator.LocationInfo != (Type) null && this._mediator.LocationInfo.Namespace != null)
        str = this._mediator.LocationInfo.Namespace + Type.Delimiter.ToString();
      throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_NoNeutralAsm", (object) (str + fileName), (object) this._mediator.MainAssembly.GetSimpleName()));
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetNeutralResourcesLanguageAttribute(RuntimeAssembly assemblyHandle, StringHandleOnStack cultureName, out short fallbackLocation);
  }
}
