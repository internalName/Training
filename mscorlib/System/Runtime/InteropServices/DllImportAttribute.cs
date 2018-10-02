// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DllImportAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает, что метод с атрибутом передается библиотекой динамической компонентов (DLL) как статическая точка входа.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DllImportAttribute : Attribute
  {
    internal string _val;
    /// <summary>
    ///   Указывает имя или порядковый номер точки входа DLL для вызова.
    /// </summary>
    [__DynamicallyInvokable]
    public string EntryPoint;
    /// <summary>
    ///   Указывает способ маршалинга параметров строки для метода и управляет искажением имени.
    /// </summary>
    [__DynamicallyInvokable]
    public CharSet CharSet;
    /// <summary>
    ///   Указывает, вызывает ли <see langword="SetLastError" /> функции Win32 API перед возвращением из метода, использующего атрибуты.
    /// </summary>
    [__DynamicallyInvokable]
    public bool SetLastError;
    /// <summary>
    ///   Элементы управления ли <see cref="F:System.Runtime.InteropServices.DllImportAttribute.CharSet" /> поле средой CLR для поиска имен точек входа, отличные от адреса, указанного в неуправляемой библиотеке DLL.
    /// </summary>
    [__DynamicallyInvokable]
    public bool ExactSpelling;
    /// <summary>
    ///   Указывает ли неуправляемых методов, <see langword="HRESULT" /> или <see langword="retval" /> возвращают значения непосредственно преобразуются или ли <see langword="HRESULT" /> или <see langword="retval" /> возвращают значения автоматически преобразуются в исключения.
    /// </summary>
    [__DynamicallyInvokable]
    public bool PreserveSig;
    /// <summary>Указывает соглашение о вызовах для точки входа.</summary>
    [__DynamicallyInvokable]
    public CallingConvention CallingConvention;
    /// <summary>
    ///   Включает или отключает поведение наилучшего сопоставления при преобразовании знаков Юникода в символы ANSI.
    /// </summary>
    [__DynamicallyInvokable]
    public bool BestFitMapping;
    /// <summary>
    ///   Включает или отключает возникновение исключения при появлении несопоставимого символа Юникода, который преобразуется в символ ANSI «?» символов.
    /// </summary>
    [__DynamicallyInvokable]
    public bool ThrowOnUnmappableChar;

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
    {
      if ((method.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
        return (Attribute) null;
      MetadataImport metadataImport = ModuleHandle.GetMetadataImport(method.Module.ModuleHandle.GetRuntimeModule());
      string importDll = (string) null;
      int metadataToken = method.MetadataToken;
      PInvokeAttributes attributes = PInvokeAttributes.CharSetNotSpec;
      string importName;
      metadataImport.GetPInvokeMap(metadataToken, out attributes, out importName, out importDll);
      CharSet charSet = CharSet.None;
      switch (attributes & PInvokeAttributes.CharSetMask)
      {
        case PInvokeAttributes.CharSetNotSpec:
          charSet = CharSet.None;
          break;
        case PInvokeAttributes.CharSetAnsi:
          charSet = CharSet.Ansi;
          break;
        case PInvokeAttributes.CharSetUnicode:
          charSet = CharSet.Unicode;
          break;
        case PInvokeAttributes.CharSetMask:
          charSet = CharSet.Auto;
          break;
      }
      CallingConvention callingConvention = CallingConvention.Cdecl;
      switch (attributes & PInvokeAttributes.CallConvMask)
      {
        case PInvokeAttributes.CallConvWinapi:
          callingConvention = CallingConvention.Winapi;
          break;
        case PInvokeAttributes.CallConvCdecl:
          callingConvention = CallingConvention.Cdecl;
          break;
        case PInvokeAttributes.CallConvStdcall:
          callingConvention = CallingConvention.StdCall;
          break;
        case PInvokeAttributes.CallConvThiscall:
          callingConvention = CallingConvention.ThisCall;
          break;
        case PInvokeAttributes.CallConvFastcall:
          callingConvention = CallingConvention.FastCall;
          break;
      }
      bool exactSpelling = (uint) (attributes & PInvokeAttributes.NoMangle) > 0U;
      bool setLastError = (uint) (attributes & PInvokeAttributes.SupportsLastError) > 0U;
      bool bestFitMapping = (attributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
      bool throwOnUnmappableChar = (attributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
      bool preserveSig = (uint) (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > 0U;
      return (Attribute) new DllImportAttribute(importDll, importName, charSet, exactSpelling, setLastError, preserveSig, callingConvention, bestFitMapping, throwOnUnmappableChar);
    }

    internal static bool IsDefined(RuntimeMethodInfo method)
    {
      return (uint) (method.Attributes & MethodAttributes.PinvokeImpl) > 0U;
    }

    internal DllImportAttribute(string dllName, string entryPoint, CharSet charSet, bool exactSpelling, bool setLastError, bool preserveSig, CallingConvention callingConvention, bool bestFitMapping, bool throwOnUnmappableChar)
    {
      this._val = dllName;
      this.EntryPoint = entryPoint;
      this.CharSet = charSet;
      this.ExactSpelling = exactSpelling;
      this.SetLastError = setLastError;
      this.PreserveSig = preserveSig;
      this.CallingConvention = callingConvention;
      this.BestFitMapping = bestFitMapping;
      this.ThrowOnUnmappableChar = throwOnUnmappableChar;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.DllImportAttribute" /> класс с именем библиотеки DLL, содержащей метод для импорта.
    /// </summary>
    /// <param name="dllName">
    ///   Имя библиотеки DLL, содержащей неуправляемый метод.
    ///    Это может включать отображаемое имя сборки, если библиотека DLL будет включен в сборку.
    /// </param>
    [__DynamicallyInvokable]
    public DllImportAttribute(string dllName)
    {
      this._val = dllName;
    }

    /// <summary>
    ///   Возвращает имя DLL-файла, который содержит точку входа.
    /// </summary>
    /// <returns>Имя DLL-файла, который содержит точку входа.</returns>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}
