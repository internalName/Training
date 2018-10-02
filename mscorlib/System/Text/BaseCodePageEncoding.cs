// Decompiled with JetBrains decompiler
// Type: System.Text.BaseCodePageEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  [Serializable]
  internal abstract class BaseCodePageEncoding : EncodingNLS, ISerializable
  {
    [NonSerialized]
    protected bool bFlagDataTable = true;
    [SecurityCritical]
    [NonSerialized]
    protected unsafe BaseCodePageEncoding.CodePageHeader* pCodePage = (BaseCodePageEncoding.CodePageHeader*) null;
    [SecurityCritical]
    private static unsafe BaseCodePageEncoding.CodePageDataFileHeader* m_pCodePageFileHeader = (BaseCodePageEncoding.CodePageDataFileHeader*) GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof (CharUnicodeInfo).Assembly, "codepages.nlp");
    internal const string CODE_PAGE_DATA_FILE_NAME = "codepages.nlp";
    [NonSerialized]
    protected int dataTableCodePage;
    [NonSerialized]
    protected int iExtraBytes;
    [NonSerialized]
    protected char[] arrayUnicodeBestFit;
    [NonSerialized]
    protected char[] arrayBytesBestFit;
    [NonSerialized]
    protected bool m_bUseMlangTypeForSerialization;
    [SecurityCritical]
    [NonSerialized]
    protected SafeViewOfFileHandle safeMemorySectionHandle;
    [SecurityCritical]
    [NonSerialized]
    protected SafeFileMappingHandle safeFileMappingHandle;

    [SecuritySafeCritical]
    static unsafe BaseCodePageEncoding()
    {
    }

    [SecurityCritical]
    internal BaseCodePageEncoding(int codepage)
      : this(codepage, codepage)
    {
    }

    [SecurityCritical]
    internal unsafe BaseCodePageEncoding(int codepage, int dataCodePage)
      : base(codepage == 0 ? Win32Native.GetACP() : codepage)
    {
      this.dataTableCodePage = dataCodePage;
      this.LoadCodePageTables();
    }

    [SecurityCritical]
    internal unsafe BaseCodePageEncoding(SerializationInfo info, StreamingContext context)
      : base(0)
    {
      throw new ArgumentNullException("this");
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      this.SerializeEncoding(info, context);
      info.AddValue(this.m_bUseMlangTypeForSerialization ? "m_maxByteSize" : "maxCharSize", this.IsSingleByte ? 1 : 2);
      info.SetType(this.m_bUseMlangTypeForSerialization ? typeof (MLangCodePageEncoding) : typeof (CodePageEncoding));
    }

    [SecurityCritical]
    private unsafe void LoadCodePageTables()
    {
      BaseCodePageEncoding.CodePageHeader* codePage = BaseCodePageEncoding.FindCodePage(this.dataTableCodePage);
      if ((IntPtr) codePage == IntPtr.Zero)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", (object) this.CodePage));
      this.pCodePage = codePage;
      this.LoadManagedCodePage();
    }

    [SecurityCritical]
    private static unsafe BaseCodePageEncoding.CodePageHeader* FindCodePage(int codePage)
    {
      for (int index = 0; index < (int) BaseCodePageEncoding.m_pCodePageFileHeader->CodePageCount; ++index)
      {
        BaseCodePageEncoding.CodePageIndex* codePageIndexPtr = &BaseCodePageEncoding.m_pCodePageFileHeader->CodePages + index;
        if ((int) codePageIndexPtr->CodePage == codePage)
          return (BaseCodePageEncoding.CodePageHeader*) ((IntPtr) BaseCodePageEncoding.m_pCodePageFileHeader + codePageIndexPtr->Offset);
      }
      return (BaseCodePageEncoding.CodePageHeader*) null;
    }

    [SecurityCritical]
    internal static unsafe int GetCodePageByteSize(int codePage)
    {
      BaseCodePageEncoding.CodePageHeader* codePage1 = BaseCodePageEncoding.FindCodePage(codePage);
      if ((IntPtr) codePage1 == IntPtr.Zero)
        return 0;
      return (int) codePage1->ByteCount;
    }

    [SecurityCritical]
    protected abstract void LoadManagedCodePage();

    [SecurityCritical]
    protected unsafe byte* GetSharedMemory(int iSize)
    {
      IntPtr mappedFileHandle;
      byte* openFileMapping = EncodingTable.nativeCreateOpenFileMapping(this.GetMemorySectionName(), iSize, out mappedFileHandle);
      if ((IntPtr) openFileMapping == IntPtr.Zero)
        throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
      if (mappedFileHandle != IntPtr.Zero)
      {
        this.safeMemorySectionHandle = new SafeViewOfFileHandle((IntPtr) ((void*) openFileMapping), true);
        this.safeFileMappingHandle = new SafeFileMappingHandle(mappedFileHandle, true);
      }
      return openFileMapping;
    }

    [SecurityCritical]
    protected virtual unsafe string GetMemorySectionName()
    {
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "NLS_CodePage_{0}_{1}_{2}_{3}_{4}", (object) (this.bFlagDataTable ? this.dataTableCodePage : this.CodePage), (object) this.pCodePage->VersionMajor, (object) this.pCodePage->VersionMinor, (object) this.pCodePage->VersionRevision, (object) this.pCodePage->VersionBuild);
    }

    [SecurityCritical]
    protected abstract void ReadBestFitTable();

    [SecuritySafeCritical]
    internal override char[] GetBestFitUnicodeToBytesData()
    {
      if (this.arrayUnicodeBestFit == null)
        this.ReadBestFitTable();
      return this.arrayUnicodeBestFit;
    }

    [SecuritySafeCritical]
    internal override char[] GetBestFitBytesToUnicodeData()
    {
      if (this.arrayBytesBestFit == null)
        this.ReadBestFitTable();
      return this.arrayBytesBestFit;
    }

    [SecurityCritical]
    internal void CheckMemorySection()
    {
      if (this.safeMemorySectionHandle == null || !(this.safeMemorySectionHandle.DangerousGetHandle() == IntPtr.Zero))
        return;
      this.LoadManagedCodePage();
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct CodePageDataFileHeader
    {
      [FieldOffset(0)]
      internal char TableName;
      [FieldOffset(32)]
      internal ushort Version;
      [FieldOffset(40)]
      internal short CodePageCount;
      [FieldOffset(42)]
      internal short unused1;
      [FieldOffset(44)]
      internal BaseCodePageEncoding.CodePageIndex CodePages;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 2)]
    internal struct CodePageIndex
    {
      [FieldOffset(0)]
      internal char CodePageName;
      [FieldOffset(32)]
      internal short CodePage;
      [FieldOffset(34)]
      internal short ByteCount;
      [FieldOffset(36)]
      internal int Offset;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct CodePageHeader
    {
      [FieldOffset(0)]
      internal char CodePageName;
      [FieldOffset(32)]
      internal ushort VersionMajor;
      [FieldOffset(34)]
      internal ushort VersionMinor;
      [FieldOffset(36)]
      internal ushort VersionRevision;
      [FieldOffset(38)]
      internal ushort VersionBuild;
      [FieldOffset(40)]
      internal short CodePage;
      [FieldOffset(42)]
      internal short ByteCount;
      [FieldOffset(44)]
      internal char UnicodeReplace;
      [FieldOffset(46)]
      internal ushort ByteReplace;
      [FieldOffset(48)]
      internal short FirstDataWord;
    }
  }
}
