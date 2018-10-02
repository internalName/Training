// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.WellKnownClientTypeEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Содержит значения для типа объекта зарегистрированного на клиенте как тип, активируемых сервером (единственного вызова или одноэлементного множества).
  /// </summary>
  [ComVisible(true)]
  public class WellKnownClientTypeEntry : TypeEntry
  {
    private string _objectUrl;
    private string _appUrl;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> с заданного типа, имя сборки и URL-адрес.
    /// </summary>
    /// <param name="typeName">
    ///   Имя типа, активируемого сервером типа.
    /// </param>
    /// <param name="assemblyName">
    ///   Имя сборки типа активированного сервером.
    /// </param>
    /// <param name="objectUrl">
    ///   URL-адрес активируемого сервером типа.
    /// </param>
    public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
    {
      if (typeName == null)
        throw new ArgumentNullException(nameof (typeName));
      if (assemblyName == null)
        throw new ArgumentNullException(nameof (assemblyName));
      if (objectUrl == null)
        throw new ArgumentNullException(nameof (objectUrl));
      this.TypeName = typeName;
      this.AssemblyName = assemblyName;
      this._objectUrl = objectUrl;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> класса с заданным типом и URL-адрес.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Активируемого сервером типа.
    /// </param>
    /// <param name="objectUrl">
    ///   URL-адрес активируемого сервером типа.
    /// </param>
    public WellKnownClientTypeEntry(Type type, string objectUrl)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (objectUrl == null)
        throw new ArgumentNullException(nameof (objectUrl));
      RuntimeType runtimeType = type as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      this.TypeName = type.FullName;
      this.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
      this._objectUrl = objectUrl;
    }

    /// <summary>
    ///   Возвращает URL-адрес активируемого сервером клиентского объекта.
    /// </summary>
    /// <returns>
    ///   URL-адрес активируемого сервером клиентского объекта.
    /// </returns>
    public string ObjectUrl
    {
      get
      {
        return this._objectUrl;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Type" /> типа клиента, активированного сервером.
    /// </summary>
    /// <returns>
    ///   Возвращает <see cref="T:System.Type" /> типа клиента, активированного сервером.
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
    ///   Возвращает или задает URL-адрес приложения, которое активируется тип.
    /// </summary>
    /// <returns>URL-адрес приложения, которое активируется тип.</returns>
    public string ApplicationUrl
    {
      get
      {
        return this._appUrl;
      }
      set
      {
        this._appUrl = value;
      }
    }

    /// <summary>
    ///   Возвращает полное имя типа, имя сборки и URL-адрес объекта, типа клиента, активированного сервером как <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Полное имя типа, имя сборки и URL-адрес объекта клиента активируемого сервером типа, что <see cref="T:System.String" />.
    /// </returns>
    public override string ToString()
    {
      string str = "type='" + this.TypeName + ", " + this.AssemblyName + "'; url=" + this._objectUrl;
      if (this._appUrl != null)
        str = str + "; appUrl=" + this._appUrl;
      return str;
    }
  }
}
