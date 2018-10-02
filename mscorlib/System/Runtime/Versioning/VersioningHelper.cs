// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.VersioningHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System.Runtime.Versioning
{
  /// <summary>
  ///   Предоставляет методы для помощи разработчикам в написании кода безопасности версий.
  ///    Этот класс не наследуется.
  /// </summary>
  public static class VersioningHelper
  {
    private const ResourceScope ResTypeMask = ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library;
    private const ResourceScope VisibilityMask = ResourceScope.Private | ResourceScope.Assembly;

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetRuntimeId();

    /// <summary>
    ///   Возвращает имя безопасности версий на основе заданного имени ресурса и предполагаемой потребления ресурса.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <param name="from">Область видимости ресурса.</param>
    /// <param name="to">Желаемая область потребления ресурса.</param>
    /// <returns>Версия безопасное имя.</returns>
    public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to)
    {
      return VersioningHelper.MakeVersionSafeName(name, from, to, (Type) null);
    }

    /// <summary>
    ///   Возвращает имя безопасности версий на основе заданного имени ресурса, предполагаемой области потребления ресурса и типа, использующего ресурс.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <param name="from">Начало области видимости.</param>
    /// <param name="to">Конец области видимости.</param>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Ресурса.
    /// </param>
    /// <returns>Версия безопасное имя.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значения для <paramref name="from " />и <paramref name="to " />являются недопустимыми.
    ///    Тип ресурса в <see cref="T:System.Runtime.Versioning.ResourceScope" />  перечисления переходом от более строгий тип ресурса к более общему типу ресурса.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="type " /> имеет значение <see langword=" null" />.
    /// </exception>
    [SecuritySafeCritical]
    public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to, Type type)
    {
      ResourceScope resourceScope1 = from & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
      ResourceScope resourceScope2 = to & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
      if (resourceScope1 > resourceScope2)
        throw new ArgumentException(Environment.GetResourceString("Argument_ResourceScopeWrongDirection", (object) resourceScope1, (object) resourceScope2), nameof (from));
      SxSRequirements requirements = VersioningHelper.GetRequirements(to, from);
      if ((requirements & (SxSRequirements.AssemblyName | SxSRequirements.TypeName)) != SxSRequirements.None && type == (Type) null)
        throw new ArgumentNullException(nameof (type), Environment.GetResourceString("ArgumentNull_TypeRequiredByResourceScope"));
      StringBuilder stringBuilder = new StringBuilder(name);
      char ch = '_';
      if ((requirements & SxSRequirements.ProcessID) != SxSRequirements.None)
      {
        stringBuilder.Append(ch);
        stringBuilder.Append('p');
        stringBuilder.Append(Win32Native.GetCurrentProcessId());
      }
      if ((requirements & SxSRequirements.CLRInstanceID) != SxSRequirements.None)
      {
        string clrInstanceString = VersioningHelper.GetCLRInstanceString();
        stringBuilder.Append(ch);
        stringBuilder.Append('r');
        stringBuilder.Append(clrInstanceString);
      }
      if ((requirements & SxSRequirements.AppDomainID) != SxSRequirements.None)
      {
        stringBuilder.Append(ch);
        stringBuilder.Append("ad");
        stringBuilder.Append(AppDomain.CurrentDomain.Id);
      }
      if ((requirements & SxSRequirements.TypeName) != SxSRequirements.None)
      {
        stringBuilder.Append(ch);
        stringBuilder.Append(type.Name);
      }
      if ((requirements & SxSRequirements.AssemblyName) != SxSRequirements.None)
      {
        stringBuilder.Append(ch);
        stringBuilder.Append(type.Assembly.FullName);
      }
      return stringBuilder.ToString();
    }

    private static string GetCLRInstanceString()
    {
      return VersioningHelper.GetRuntimeId().ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    private static SxSRequirements GetRequirements(ResourceScope consumeAsScope, ResourceScope calleeScope)
    {
      SxSRequirements sxSrequirements = SxSRequirements.None;
      switch (calleeScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
      {
        case ResourceScope.Machine:
          switch (consumeAsScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
          {
            case ResourceScope.Machine:
              goto label_8;
            case ResourceScope.Process:
              sxSrequirements |= SxSRequirements.ProcessID;
              goto label_8;
            case ResourceScope.AppDomain:
              sxSrequirements |= SxSRequirements.AppDomainID | SxSRequirements.ProcessID | SxSRequirements.CLRInstanceID;
              goto label_8;
            default:
              throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeTypeBits", (object) consumeAsScope), nameof (consumeAsScope));
          }
        case ResourceScope.Process:
          if ((consumeAsScope & ResourceScope.AppDomain) != ResourceScope.None)
          {
            sxSrequirements |= SxSRequirements.AppDomainID | SxSRequirements.CLRInstanceID;
            goto case ResourceScope.AppDomain;
          }
          else
            goto case ResourceScope.AppDomain;
        case ResourceScope.AppDomain:
label_8:
          switch (calleeScope & (ResourceScope.Private | ResourceScope.Assembly))
          {
            case ResourceScope.None:
              switch (consumeAsScope & (ResourceScope.Private | ResourceScope.Assembly))
              {
                case ResourceScope.None:
                  goto label_16;
                case ResourceScope.Private:
                  sxSrequirements |= SxSRequirements.AssemblyName | SxSRequirements.TypeName;
                  goto label_16;
                case ResourceScope.Assembly:
                  sxSrequirements |= SxSRequirements.AssemblyName;
                  goto label_16;
                default:
                  throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeVisibilityBits", (object) consumeAsScope), nameof (consumeAsScope));
              }
            case ResourceScope.Private:
label_16:
              return sxSrequirements;
            case ResourceScope.Assembly:
              if ((consumeAsScope & ResourceScope.Private) != ResourceScope.None)
              {
                sxSrequirements |= SxSRequirements.TypeName;
                goto case ResourceScope.Private;
              }
              else
                goto case ResourceScope.Private;
            default:
              throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeVisibilityBits", (object) calleeScope), nameof (calleeScope));
          }
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeTypeBits", (object) calleeScope), nameof (calleeScope));
      }
    }
  }
}
