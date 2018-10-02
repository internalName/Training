// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorageFileEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.IO.IsolatedStorage
{
  internal sealed class IsolatedStorageFileEnumerator : IEnumerator
  {
    private const char s_SepExternal = '\\';
    private IsolatedStorageFile m_Current;
    private IsolatedStorageScope m_Scope;
    private FileIOPermission m_fiop;
    private string m_rootDir;
    private TwoLevelFileEnumerator m_fileEnum;
    private bool m_fReset;
    private bool m_fEnd;

    [SecurityCritical]
    internal IsolatedStorageFileEnumerator(IsolatedStorageScope scope)
    {
      this.m_Scope = scope;
      this.m_fiop = IsolatedStorageFile.GetGlobalFileIOPerm(scope);
      this.m_rootDir = IsolatedStorageFile.GetRootDir(scope);
      this.m_fileEnum = new TwoLevelFileEnumerator(this.m_rootDir);
      this.Reset();
    }

    [SecuritySafeCritical]
    public bool MoveNext()
    {
      this.m_fiop.Assert();
      this.m_fReset = false;
      while (this.m_fileEnum.MoveNext())
      {
        IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
        TwoPaths current = (TwoPaths) this.m_fileEnum.Current;
        bool flag = false;
        if (IsolatedStorageFile.NotAssemFilesDir(current.Path2) && IsolatedStorageFile.NotAppFilesDir(current.Path2))
          flag = true;
        Stream s1 = (Stream) null;
        Stream s2 = (Stream) null;
        Stream s3 = (Stream) null;
        IsolatedStorageScope scope;
        string domainName;
        string assemName;
        string appName;
        if (flag)
        {
          if (this.GetIDStream(current.Path1, out s1) && this.GetIDStream(current.Path1 + "\\" + current.Path2, out s2))
          {
            s1.Position = 0L;
            scope = !System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(this.m_Scope) ? (!System.IO.IsolatedStorage.IsolatedStorage.IsMachine(this.m_Scope) ? IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly : IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine) : IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming;
            domainName = current.Path1;
            assemName = current.Path2;
            appName = (string) null;
          }
          else
            continue;
        }
        else if (IsolatedStorageFile.NotAppFilesDir(current.Path2))
        {
          if (this.GetIDStream(current.Path1, out s2))
          {
            scope = !System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(this.m_Scope) ? (!System.IO.IsolatedStorage.IsolatedStorage.IsMachine(this.m_Scope) ? IsolatedStorageScope.User | IsolatedStorageScope.Assembly : IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine) : IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming;
            domainName = (string) null;
            assemName = current.Path1;
            appName = (string) null;
            s2.Position = 0L;
          }
          else
            continue;
        }
        else if (this.GetIDStream(current.Path1, out s3))
        {
          scope = !System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(this.m_Scope) ? (!System.IO.IsolatedStorage.IsolatedStorage.IsMachine(this.m_Scope) ? IsolatedStorageScope.User | IsolatedStorageScope.Application : IsolatedStorageScope.Machine | IsolatedStorageScope.Application) : IsolatedStorageScope.User | IsolatedStorageScope.Roaming | IsolatedStorageScope.Application;
          domainName = (string) null;
          assemName = (string) null;
          appName = current.Path1;
          s3.Position = 0L;
        }
        else
          continue;
        if (isolatedStorageFile.InitStore(scope, s1, s2, s3, domainName, assemName, appName) && isolatedStorageFile.InitExistingStore(scope))
        {
          this.m_Current = isolatedStorageFile;
          return true;
        }
      }
      this.m_fEnd = true;
      return false;
    }

    public object Current
    {
      get
      {
        if (this.m_fReset)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
        if (this.m_fEnd)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
        return (object) this.m_Current;
      }
    }

    public void Reset()
    {
      this.m_Current = (IsolatedStorageFile) null;
      this.m_fReset = true;
      this.m_fEnd = false;
      this.m_fileEnum.Reset();
    }

    private bool GetIDStream(string path, out Stream s)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.m_rootDir);
      stringBuilder.Append(path);
      stringBuilder.Append('\\');
      stringBuilder.Append("identity.dat");
      s = (Stream) null;
      try
      {
        byte[] buffer;
        using (FileStream fileStream = new FileStream(stringBuilder.ToString(), FileMode.Open))
        {
          int length = (int) fileStream.Length;
          buffer = new byte[length];
          int offset = 0;
          while (length > 0)
          {
            int num = fileStream.Read(buffer, offset, length);
            if (num == 0)
              __Error.EndOfFile();
            offset += num;
            length -= num;
          }
        }
        s = (Stream) new MemoryStream(buffer);
      }
      catch
      {
        return false;
      }
      return true;
    }
  }
}
