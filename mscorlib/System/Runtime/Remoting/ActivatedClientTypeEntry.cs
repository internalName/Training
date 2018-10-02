// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ActivatedClientTypeEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Содержит значения для типа объектов, зарегистрированных со стороны клиента как тип, который может быть активирован на сервере.
  /// </summary>
  [ComVisible(true)]
  public class ActivatedClientTypeEntry : TypeEntry
  {
    private string _appUrl;
    private IContextAttribute[] _contextAttributes;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> класса с заданным именем, имя сборки и URL-адрес приложения.
    /// </summary>
    /// <param name="typeName">
    ///   Имя типа, активируемого клиентом типа.
    /// </param>
    /// <param name="assemblyName">
    ///   Имя сборки активируемого клиентом типа.
    /// </param>
    /// <param name="appUrl">
    ///   URL-адрес приложения, которое активируется тип.
    /// </param>
    public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
    {
      if (typeName == null)
        throw new ArgumentNullException(nameof (typeName));
      if (assemblyName == null)
        throw new ArgumentNullException(nameof (assemblyName));
      if (appUrl == null)
        throw new ArgumentNullException(nameof (appUrl));
      this.TypeName = typeName;
      this.AssemblyName = assemblyName;
      this._appUrl = appUrl;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> класса с заданной <see cref="T:System.Type" /> и URL-адрес приложения.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Клиента активируемого типа.
    /// </param>
    /// <param name="appUrl">
    ///   URL-адрес приложения, которое активируется тип.
    /// </param>
    public ActivatedClientTypeEntry(Type type, string appUrl)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (appUrl == null)
        throw new ArgumentNullException(nameof (appUrl));
      RuntimeType runtimeType = type as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      this.TypeName = type.FullName;
      this.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
      this._appUrl = appUrl;
    }

    /// <summary>
    ///   Возвращает URL-адрес приложения, которое активируется тип.
    /// </summary>
    /// <returns>URL-адрес приложения, которое активируется тип.</returns>
    public string ApplicationUrl
    {
      get
      {
        return this._appUrl;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Type" /> типа, активируемого клиентом.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Type" /> типа, активируемого клиентом.
    /// </returns>
    public Type ObjectType
    {
      [MethodImpl(MethodImplOptions.NoInlining)] get
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
        return RuntimeTypeHandle.GetTypeByName(this.TypeName + ", " + this.AssemblyName, ref stackMark);
      }
    }

    /// <summary>
    ///   Возвращает или задает атрибуты контекста для активируемого клиентом типа.
    /// </summary>
    /// <returns>Атрибуты контекста для активируемого клиентом типа.</returns>
    public IContextAttribute[] ContextAttributes
    {
      get
      {
        return this._contextAttributes;
      }
      set
      {
        this._contextAttributes = value;
      }
    }

    /// <summary>
    ///   Возвращает имя типа, имя сборки и URL-адрес приложения типа, активируемого клиентом <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Имя типа, имя сборки и URL-адрес приложения типа, активируемого клиентом <see cref="T:System.String" />.
    /// </returns>
    public override string ToString()
    {
      return "type='" + this.TypeName + ", " + this.AssemblyName + "'; appUrl=" + this._appUrl;
    }
  }
}
