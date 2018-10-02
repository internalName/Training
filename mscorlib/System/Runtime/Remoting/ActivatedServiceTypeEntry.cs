// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ActivatedServiceTypeEntry
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
  ///   Содержит значения для типа объектов, зарегистрированных со стороны службы, может быть активирован по запросу клиента.
  /// </summary>
  [ComVisible(true)]
  public class ActivatedServiceTypeEntry : TypeEntry
  {
    private IContextAttribute[] _contextAttributes;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> класс с именем заданного типа и имя сборки.
    /// </summary>
    /// <param name="typeName">
    ///   Имя типа службы, активируемого клиентом типа.
    /// </param>
    /// <param name="assemblyName">
    ///   Имя сборки типа службы, активируемых клиентом.
    /// </param>
    public ActivatedServiceTypeEntry(string typeName, string assemblyName)
    {
      if (typeName == null)
        throw new ArgumentNullException(nameof (typeName));
      if (assemblyName == null)
        throw new ArgumentNullException(nameof (assemblyName));
      this.TypeName = typeName;
      this.AssemblyName = assemblyName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> класса с заданной <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Типа активируемых клиентом службы.
    /// </param>
    public ActivatedServiceTypeEntry(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      RuntimeType runtimeType = type as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      this.TypeName = type.FullName;
      this.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Type" /> типа активируемых клиентом службы.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Типа активируемых клиентом службы.
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
    ///   Возвращает или задает атрибуты контекста для типа службы, активируемых клиентом.
    /// </summary>
    /// <returns>
    ///   Атрибуты контекста для типа службы, активируемых клиентом.
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
    ///   Возвращает тип и имя сборки типа, активируемого клиентом службы <see cref="T:System.String" />.
    /// </summary>
    /// <returns>
    ///   Тип и имя сборки типа, активируемого клиентом службы <see cref="T:System.String" />.
    /// </returns>
    public override string ToString()
    {
      return "type='" + this.TypeName + ", " + this.AssemblyName + "'";
    }
  }
}
