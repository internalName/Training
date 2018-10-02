// Decompiled with JetBrains decompiler
// Type: System.Resources.RuntimeResourceSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;

namespace System.Resources
{
  internal sealed class RuntimeResourceSet : ResourceSet, IEnumerable
  {
    internal const int Version = 2;
    private Dictionary<string, ResourceLocator> _resCache;
    private ResourceReader _defaultReader;
    private Dictionary<string, ResourceLocator> _caseInsensitiveTable;
    private bool _haveReadFromReader;

    [SecurityCritical]
    internal RuntimeResourceSet(string fileName)
      : base(false)
    {
      this._resCache = new Dictionary<string, ResourceLocator>((IEqualityComparer<string>) FastResourceComparer.Default);
      this._defaultReader = new ResourceReader((Stream) new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), this._resCache);
      this.Reader = (IResourceReader) this._defaultReader;
    }

    [SecurityCritical]
    internal RuntimeResourceSet(Stream stream)
      : base(false)
    {
      this._resCache = new Dictionary<string, ResourceLocator>((IEqualityComparer<string>) FastResourceComparer.Default);
      this._defaultReader = new ResourceReader(stream, this._resCache);
      this.Reader = (IResourceReader) this._defaultReader;
    }

    protected override void Dispose(bool disposing)
    {
      if (this.Reader == null)
        return;
      if (disposing)
      {
        lock (this.Reader)
        {
          this._resCache = (Dictionary<string, ResourceLocator>) null;
          if (this._defaultReader != null)
          {
            this._defaultReader.Close();
            this._defaultReader = (ResourceReader) null;
          }
          this._caseInsensitiveTable = (Dictionary<string, ResourceLocator>) null;
          base.Dispose(disposing);
        }
      }
      else
      {
        this._resCache = (Dictionary<string, ResourceLocator>) null;
        this._caseInsensitiveTable = (Dictionary<string, ResourceLocator>) null;
        this._defaultReader = (ResourceReader) null;
        base.Dispose(disposing);
      }
    }

    public override IDictionaryEnumerator GetEnumerator()
    {
      return this.GetEnumeratorHelper();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumeratorHelper();
    }

    private IDictionaryEnumerator GetEnumeratorHelper()
    {
      IResourceReader reader = this.Reader;
      if (reader == null || this._resCache == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
      return reader.GetEnumerator();
    }

    public override string GetString(string key)
    {
      return (string) this.GetObject(key, false, true);
    }

    public override string GetString(string key, bool ignoreCase)
    {
      return (string) this.GetObject(key, ignoreCase, true);
    }

    public override object GetObject(string key)
    {
      return this.GetObject(key, false, false);
    }

    public override object GetObject(string key, bool ignoreCase)
    {
      return this.GetObject(key, ignoreCase, false);
    }

    private object GetObject(string key, bool ignoreCase, bool isString)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (this.Reader == null || this._resCache == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
      object obj1 = (object) null;
      lock (this.Reader)
      {
        if (this.Reader == null)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
        ResourceLocator resLocation;
        if (this._defaultReader != null)
        {
          int num = -1;
          if (this._resCache.TryGetValue(key, out resLocation))
          {
            obj1 = resLocation.Value;
            num = resLocation.DataPosition;
          }
          if (num == -1 && obj1 == null)
            num = this._defaultReader.FindPosForResource(key);
          if (num != -1 && obj1 == null)
          {
            ResourceTypeCode typeCode;
            if (isString)
            {
              obj1 = (object) this._defaultReader.LoadString(num);
              typeCode = ResourceTypeCode.String;
            }
            else
              obj1 = this._defaultReader.LoadObject(num, out typeCode);
            resLocation = new ResourceLocator(num, ResourceLocator.CanCache(typeCode) ? obj1 : (object) null);
            lock (this._resCache)
              this._resCache[key] = resLocation;
          }
          if (obj1 != null || !ignoreCase)
            return obj1;
        }
        if (!this._haveReadFromReader)
        {
          if (ignoreCase && this._caseInsensitiveTable == null)
            this._caseInsensitiveTable = new Dictionary<string, ResourceLocator>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          if (this._defaultReader == null)
          {
            IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
            while (enumerator.MoveNext())
            {
              DictionaryEntry entry = enumerator.Entry;
              string key1 = (string) entry.Key;
              ResourceLocator resourceLocator = new ResourceLocator(-1, entry.Value);
              this._resCache.Add(key1, resourceLocator);
              if (ignoreCase)
                this._caseInsensitiveTable.Add(key1, resourceLocator);
            }
            if (!ignoreCase)
              this.Reader.Close();
          }
          else
          {
            ResourceReader.ResourceEnumerator enumeratorInternal = this._defaultReader.GetEnumeratorInternal();
            while (enumeratorInternal.MoveNext())
              this._caseInsensitiveTable.Add((string) enumeratorInternal.Key, new ResourceLocator(enumeratorInternal.DataPosition, (object) null));
          }
          this._haveReadFromReader = true;
        }
        object obj2 = (object) null;
        bool flag = false;
        bool keyInWrongCase1 = false;
        if (this._defaultReader != null && this._resCache.TryGetValue(key, out resLocation))
        {
          flag = true;
          obj2 = this.ResolveResourceLocator(resLocation, key, this._resCache, keyInWrongCase1);
        }
        if (!flag & ignoreCase && this._caseInsensitiveTable.TryGetValue(key, out resLocation))
        {
          bool keyInWrongCase2 = true;
          obj2 = this.ResolveResourceLocator(resLocation, key, this._resCache, keyInWrongCase2);
        }
        return obj2;
      }
    }

    private object ResolveResourceLocator(ResourceLocator resLocation, string key, Dictionary<string, ResourceLocator> copyOfCache, bool keyInWrongCase)
    {
      object obj = resLocation.Value;
      if (obj == null)
      {
        ResourceTypeCode typeCode;
        lock (this.Reader)
          obj = this._defaultReader.LoadObject(resLocation.DataPosition, out typeCode);
        if (!keyInWrongCase && ResourceLocator.CanCache(typeCode))
        {
          resLocation.Value = obj;
          copyOfCache[key] = resLocation;
        }
      }
      return obj;
    }
  }
}
