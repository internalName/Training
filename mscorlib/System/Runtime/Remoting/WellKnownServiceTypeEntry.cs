// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.WellKnownServiceTypeEntry
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
  ///   Содержит значения для типа объектов, зарегистрированных со стороны службы как объект активируемого сервером типа (единственного вызова или одноэлементного множества).
  /// </summary>
  [ComVisible(true)]
  public class WellKnownServiceTypeEntry : TypeEntry
  {
    private string _objectUri;
    private WellKnownObjectMode _mode;
    private IContextAttribute[] _contextAttributes;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> класса с заданным именем типа, имя сборки, объект URI, и <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.
    /// </summary>
    /// <param name="typeName">
    ///   Полное имя типа службы, активируемых сервером.
    /// </param>
    /// <param name="assemblyName">
    ///   Имя сборки типа активированного сервером.
    /// </param>
    /// <param name="objectUri">
    ///   URI объекта, активируемого сервером.
    /// </param>
    /// <param name="mode">
    ///   <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> Типа, который определяет способ активации объекта.
    /// </param>
    public WellKnownServiceTypeEntry(string typeName, string assemblyName, string objectUri, WellKnownObjectMode mode)
    {
      if (typeName == null)
        throw new ArgumentNullException(nameof (typeName));
      if (assemblyName == null)
        throw new ArgumentNullException(nameof (assemblyName));
      if (objectUri == null)
        throw new ArgumentNullException(nameof (objectUri));
      this.TypeName = typeName;
      this.AssemblyName = assemblyName;
      this._objectUri = objectUri;
      this._mode = mode;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> класса с заданной <see cref="T:System.Type" />, объект URI, и <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Активируемого сервером типа объекта.
    /// </param>
    /// <param name="objectUri">
    ///   URI-адрес активируемого сервером типа.
    /// </param>
    /// <param name="mode">
    ///   <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> Типа, который определяет способ активации объекта.
    /// </param>
    public WellKnownServiceTypeEntry(Type type, string objectUri, WellKnownObjectMode mode)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (objectUri == null)
        throw new ArgumentNullException(nameof (objectUri));
      if ((object) (type as RuntimeType) == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      this.TypeName = type.FullName;
      this.AssemblyName = type.Module.Assembly.FullName;
      this._objectUri = objectUri;
      this._mode = mode;
    }

    /// <summary>Возвращает URI хорошо известного типа службы.</summary>
    /// <returns>URI активированного сервером типа.</returns>
    public string ObjectUri
    {
      get
      {
        return this._objectUri;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> активированного сервером типа.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> Активированного сервером типа.
    /// </returns>
    public WellKnownObjectMode Mode
    {
      get
      {
        return this._mode;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Type" /> активированного сервером типа.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Активированного сервером типа.
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
    ///   Возвращает или задает атрибуты контекста для типа службы, активируемых сервером.
    /// </summary>
    /// <returns>
    ///   Возвращает или задает атрибуты контекста для типа службы, активируемых сервером.
    /// </returns>
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
    ///   Возвращает имя типа, имя сборки, объект URI и <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> типа, активируемого сервером <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Имя типа, имя сборки, объект URI и <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> типа, активируемого сервером <see cref="T:System.String" />.
    /// </returns>
    public override string ToString()
    {
      return "type='" + this.TypeName + ", " + this.AssemblyName + "'; objectUri=" + this._objectUri + "; mode=" + this._mode.ToString();
    }
  }
}
