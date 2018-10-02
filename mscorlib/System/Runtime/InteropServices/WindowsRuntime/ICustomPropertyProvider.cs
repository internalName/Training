// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ICustomPropertyProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("7C925755-3E48-42B4-8677-76372267033F")]
  [ComImport]
  internal interface ICustomPropertyProvider
  {
    ICustomProperty GetCustomProperty(string name);

    ICustomProperty GetIndexedProperty(string name, Type indexParameterType);

    string GetStringRepresentation();

    Type Type { get; }
  }
}
