// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ReadOnlyArrayAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>
  ///   При применении к параметру-массиву в компоненте Среда выполнения Windows указывает, что содержимое массива, передаваемое в этом параметре, используется только для входных данных.
  ///    Вызывающий объект ожидает, что массив не изменится вызовом.
  ///    Важные сведения о вызывающих объектах, написанных с помощью управляемого кода, см. в разделе "Примечания".
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class ReadOnlyArrayAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.WindowsRuntime.ReadOnlyArrayAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ReadOnlyArrayAttribute()
    {
    }
  }
}
