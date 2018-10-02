// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ManagedToNativeComInteropStubAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Обеспечивает поддержку пользовательской настройки заглушек взаимодействия в сценариях управляемых для COM-взаимодействия.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  [ComVisible(false)]
  public sealed class ManagedToNativeComInteropStubAttribute : Attribute
  {
    internal Type _classType;
    internal string _methodName;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ManagedToNativeComInteropStubAttribute" /> с именем типа и метод указанного класса.
    /// </summary>
    /// <param name="classType">
    ///   Класс, содержащий требуемый метод-заглушку.
    /// </param>
    /// <param name="methodName">Имя метода-заглушки.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Метода-заглушки не в той же сборке, как интерфейс, содержащий управляемый метод взаимодействия.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="classType" /> является универсальным типом.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="classType" /> является интерфейсом.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="methodName" /> не удается найти.
    /// 
    ///   -или-
    /// 
    ///   Метод не является статическим или универсальным.
    /// 
    ///   -или-
    /// 
    ///   Список параметров метода соответствует списку ожидаемый параметр для заглушки.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Интерфейс, содержащий управляемый метод взаимодействия не имеет доступа к методу-заглушке, поскольку метод-заглушка имеет закрытый или защищенный доступ, или из-за проблемы безопасности.
    /// </exception>
    public ManagedToNativeComInteropStubAttribute(Type classType, string methodName)
    {
      this._classType = classType;
      this._methodName = methodName;
    }

    /// <summary>
    ///   Возвращает класс, содержащий требуемый метод-заглушку.
    /// </summary>
    /// <returns>
    ///   Класс, содержащий настроенную заглушку взаимодействия.
    /// </returns>
    public Type ClassType
    {
      get
      {
        return this._classType;
      }
    }

    /// <summary>Возвращает имя метода-заглушки.</summary>
    /// <returns>Имя настроенной заглушки взаимодействия.</returns>
    public string MethodName
    {
      get
      {
        return this._methodName;
      }
    }
  }
}
