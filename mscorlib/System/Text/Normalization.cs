// Decompiled with JetBrains decompiler
// Type: System.Text.Normalization
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Text
{
  internal class Normalization
  {
    private static volatile bool NFC;
    private static volatile bool NFD;
    private static volatile bool NFKC;
    private static volatile bool NFKD;
    private static volatile bool IDNA;
    private static volatile bool NFCDisallowUnassigned;
    private static volatile bool NFDDisallowUnassigned;
    private static volatile bool NFKCDisallowUnassigned;
    private static volatile bool NFKDDisallowUnassigned;
    private static volatile bool IDNADisallowUnassigned;
    private static volatile bool Other;
    private const int ERROR_SUCCESS = 0;
    private const int ERROR_NOT_ENOUGH_MEMORY = 8;
    private const int ERROR_INVALID_PARAMETER = 87;
    private const int ERROR_INSUFFICIENT_BUFFER = 122;
    private const int ERROR_NO_UNICODE_TRANSLATION = 1113;

    [SecurityCritical]
    private static unsafe void InitializeForm(NormalizationForm form, string strDataFile)
    {
      byte* pTableData = (byte*) null;
      if (!Environment.IsWindows8OrAbove)
      {
        if (strDataFile == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNormalizationForm"));
        pTableData = GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof (Normalization).Assembly, strDataFile);
        if ((IntPtr) pTableData == IntPtr.Zero)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNormalizationForm"));
      }
      Normalization.nativeNormalizationInitNormalization(form, pTableData);
    }

    [SecurityCritical]
    private static void EnsureInitialized(NormalizationForm form)
    {
      switch ((ExtendedNormalizationForms) form)
      {
        case ExtendedNormalizationForms.FormC:
          if (Normalization.NFC)
            break;
          Normalization.InitializeForm(form, "normnfc.nlp");
          Normalization.NFC = true;
          break;
        case ExtendedNormalizationForms.FormD:
          if (Normalization.NFD)
            break;
          Normalization.InitializeForm(form, "normnfd.nlp");
          Normalization.NFD = true;
          break;
        case ExtendedNormalizationForms.FormKC:
          if (Normalization.NFKC)
            break;
          Normalization.InitializeForm(form, "normnfkc.nlp");
          Normalization.NFKC = true;
          break;
        case ExtendedNormalizationForms.FormKD:
          if (Normalization.NFKD)
            break;
          Normalization.InitializeForm(form, "normnfkd.nlp");
          Normalization.NFKD = true;
          break;
        case ExtendedNormalizationForms.FormIdna:
          if (Normalization.IDNA)
            break;
          Normalization.InitializeForm(form, "normidna.nlp");
          Normalization.IDNA = true;
          break;
        case ExtendedNormalizationForms.FormCDisallowUnassigned:
          if (Normalization.NFCDisallowUnassigned)
            break;
          Normalization.InitializeForm(form, "normnfc.nlp");
          Normalization.NFCDisallowUnassigned = true;
          break;
        case ExtendedNormalizationForms.FormDDisallowUnassigned:
          if (Normalization.NFDDisallowUnassigned)
            break;
          Normalization.InitializeForm(form, "normnfd.nlp");
          Normalization.NFDDisallowUnassigned = true;
          break;
        case ExtendedNormalizationForms.FormKCDisallowUnassigned:
          if (Normalization.NFKCDisallowUnassigned)
            break;
          Normalization.InitializeForm(form, "normnfkc.nlp");
          Normalization.NFKCDisallowUnassigned = true;
          break;
        case ExtendedNormalizationForms.FormKDDisallowUnassigned:
          if (Normalization.NFKDDisallowUnassigned)
            break;
          Normalization.InitializeForm(form, "normnfkd.nlp");
          Normalization.NFKDDisallowUnassigned = true;
          break;
        case ExtendedNormalizationForms.FormIdnaDisallowUnassigned:
          if (Normalization.IDNADisallowUnassigned)
            break;
          Normalization.InitializeForm(form, "normidna.nlp");
          Normalization.IDNADisallowUnassigned = true;
          break;
        default:
          if (Normalization.Other)
            break;
          Normalization.InitializeForm(form, (string) null);
          Normalization.Other = true;
          break;
      }
    }

    [SecurityCritical]
    internal static bool IsNormalized(string strInput, NormalizationForm normForm)
    {
      Normalization.EnsureInitialized(normForm);
      int iError = 0;
      bool flag = Normalization.nativeNormalizationIsNormalizedString(normForm, ref iError, strInput, strInput.Length);
      if (iError <= 8)
      {
        if (iError == 0)
          return flag;
        if (iError == 8)
          throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
      }
      else if (iError == 87 || iError == 1113)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), nameof (strInput));
      throw new InvalidOperationException(Environment.GetResourceString("UnknownError_Num", (object) iError));
    }

    [SecurityCritical]
    internal static string Normalize(string strInput, NormalizationForm normForm)
    {
      Normalization.EnsureInitialized(normForm);
      int iError = 0;
      int length = Normalization.nativeNormalizationNormalizeString(normForm, ref iError, strInput, strInput.Length, (char[]) null, 0);
      if (iError != 0)
      {
        if (iError == 87)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), nameof (strInput));
        if (iError == 8)
          throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
        throw new InvalidOperationException(Environment.GetResourceString("UnknownError_Num", (object) iError));
      }
      if (length == 0)
        return string.Empty;
      char[] lpDstString;
      do
      {
        lpDstString = new char[length];
        length = Normalization.nativeNormalizationNormalizeString(normForm, ref iError, strInput, strInput.Length, lpDstString, lpDstString.Length);
        if (iError != 0)
        {
          if (iError <= 87)
          {
            if (iError != 8)
            {
              if (iError == 87)
                goto label_15;
              else
                goto label_17;
            }
            else
              goto label_16;
          }
        }
        else
          goto label_18;
      }
      while (iError == 122);
      if (iError != 1113)
        goto label_17;
label_15:
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", (object) length), nameof (strInput));
label_16:
      throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
label_17:
      throw new InvalidOperationException(Environment.GetResourceString("UnknownError_Num", (object) iError));
label_18:
      return new string(lpDstString, 0, length);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int nativeNormalizationNormalizeString(NormalizationForm normForm, ref int iError, string lpSrcString, int cwSrcLength, char[] lpDstString, int cwDstLength);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool nativeNormalizationIsNormalizedString(NormalizationForm normForm, ref int iError, string lpString, int cwLength);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void nativeNormalizationInitNormalization(NormalizationForm normForm, byte* pTableData);
  }
}
