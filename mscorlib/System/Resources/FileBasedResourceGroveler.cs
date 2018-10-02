// Decompiled with JetBrains decompiler
// Type: System.Resources.FileBasedResourceGroveler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Resources
{
  internal class FileBasedResourceGroveler : IResourceGroveler
  {
    private ResourceManager.ResourceManagerMediator _mediator;

    public FileBasedResourceGroveler(ResourceManager.ResourceManagerMediator mediator)
    {
      this._mediator = mediator;
    }

    [SecuritySafeCritical]
    public ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark)
    {
      ResourceSet resourceSet = (ResourceSet) null;
      try
      {
        new FileIOPermission(PermissionState.Unrestricted).Assert();
        string resourceFileName = this._mediator.GetResourceFileName(culture);
        string resourceFile = this.FindResourceFile(culture, resourceFileName);
        if (resourceFile == null)
        {
          if (tryParents && culture.HasInvariantCultureName)
            throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_NoNeutralDisk") + Environment.NewLine + "baseName: " + this._mediator.BaseNameField + "  locationInfo: " + (this._mediator.LocationInfo == (Type) null ? "<null>" : this._mediator.LocationInfo.FullName) + "  fileName: " + this._mediator.GetResourceFileName(culture));
        }
        else
          resourceSet = this.CreateResourceSet(resourceFile);
        return resourceSet;
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
    }

    public bool HasNeutralResources(CultureInfo culture, string defaultResName)
    {
      string resourceFile = this.FindResourceFile(culture, defaultResName);
      if (resourceFile != null && File.Exists(resourceFile))
        return true;
      string str = this._mediator.ModuleDir;
      if (resourceFile != null)
        str = Path.GetDirectoryName(resourceFile);
      return false;
    }

    private string FindResourceFile(CultureInfo culture, string fileName)
    {
      if (this._mediator.ModuleDir != null)
      {
        string path = Path.Combine(this._mediator.ModuleDir, fileName);
        if (File.Exists(path))
          return path;
      }
      if (File.Exists(fileName))
        return fileName;
      return (string) null;
    }

    [SecurityCritical]
    private ResourceSet CreateResourceSet(string file)
    {
      if (this._mediator.UserResourceSet == (Type) null)
        return (ResourceSet) new RuntimeResourceSet(file);
      object[] objArray = new object[1]{ (object) file };
      try
      {
        return (ResourceSet) Activator.CreateInstance(this._mediator.UserResourceSet, objArray);
      }
      catch (MissingMethodException ex)
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResMgrBadResSet_Type", (object) this._mediator.UserResourceSet.AssemblyQualifiedName), (Exception) ex);
      }
    }
  }
}
