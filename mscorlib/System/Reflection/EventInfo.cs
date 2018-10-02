// Decompiled with JetBrains decompiler
// Type: System.Reflection.EventInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>
  ///   Обнаруживает атрибуты события и обеспечивает доступ к его метаданным.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_EventInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class EventInfo : MemberInfo, _EventInfo
  {
    /// <summary>
    ///   Определение равенства двух объектов <see cref="T:System.Reflection.EventInfo" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(EventInfo left, EventInfo right)
    {
      if ((object) left == (object) right)
        return true;
      if ((object) left == null || (object) right == null || (left is RuntimeEventInfo || right is RuntimeEventInfo))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Определяет неравенство двух объектов <see cref="T:System.Reflection.EventInfo" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(EventInfo left, EventInfo right)
    {
      return !(left == right);
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим экземпляром, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="obj" /> равно типу и значению данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>
    ///   Возвращает значение <see cref="T:System.Reflection.MemberTypes" />, указывающее, что этот элемент является событием.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Reflection.MemberTypes" />, указывающее, что этот элемент является событием.
    /// </returns>
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Event;
      }
    }

    /// <summary>
    ///   Возвращает методы, которые были сопоставлены событию в метаданных с помощью <see langword=".other" /> директивы, указывая, следует ли включать закрытые методы.
    /// </summary>
    /// <param name="nonPublic">
    ///   <see langword="true" /> Чтобы включить закрытые методы; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Массив <see cref="T:System.Reflection.EventInfo" /> объекты, представляющие методы, которые были сопоставлены событию в метаданных с использованием <see langword=".other" /> директивы.
    ///    Если имеются методы, не соответствующих спецификации, возвращается пустой массив.
    /// </returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Этот метод не реализован.
    /// </exception>
    public virtual MethodInfo[] GetOtherMethods(bool nonPublic)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   При переопределении в производном классе получает объект <see langword="MethodInfo" /> для метода <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> события, указывающий, следует ли возвращать закрытые методы.
    /// </summary>
    /// <param name="nonPublic">
    ///   Значение <see langword="true" />, если закрытые методы могут быть возвращены. В противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, представляющий метод, используемый для добавления делегата обработчика событий в источник событий.
    /// </returns>
    /// <exception cref="T:System.MethodAccessException">
    ///   Параметр <paramref name="nonPublic" /> имеет значение <see langword="true" />. Метод, используемый для добавления делегата обработчика событий, закрытый. У вызывающего объекта нет прав на отражение закрытых методов.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetAddMethod(bool nonPublic);

    /// <summary>
    ///   При переопределении в производном классе получает объект <see langword="MethodInfo" /> для удаления метода события, указывающий, следует ли возвращать закрытые методы.
    /// </summary>
    /// <param name="nonPublic">
    ///   Значение <see langword="true" />, если закрытые методы могут быть возвращены. В противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, представляющий метод, используемый для удаления делегата обработчика событий из источника событий.
    /// </returns>
    /// <exception cref="T:System.MethodAccessException">
    ///   Параметр <paramref name="nonPublic" /> имеет значение <see langword="true" />. Метод, используемый для добавления делегата обработчика событий, закрытый. У вызывающего объекта нет прав на отражение закрытых методов.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetRemoveMethod(bool nonPublic);

    /// <summary>
    ///   При переопределении в производном классе возвращает метод, который вызывается при возникновении события; указывает, следует ли возвращать закрытые методы.
    /// </summary>
    /// <param name="nonPublic">
    ///   Значение <see langword="true" />, если закрытые методы могут быть возвращены. В противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see langword="MethodInfo" />, который был вызван при возникновении события.
    /// </returns>
    /// <exception cref="T:System.MethodAccessException">
    ///   Параметр <paramref name="nonPublic" /> имеет значение <see langword="true" />. Метод, используемый для добавления делегата обработчика событий, закрытый. У вызывающего объекта нет прав на отражение закрытых методов.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetRaiseMethod(bool nonPublic);

    /// <summary>Получает атрибуты для этого события.</summary>
    /// <returns>Атрибуты только для чтения для данного события.</returns>
    [__DynamicallyInvokable]
    public abstract EventAttributes Attributes { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.MethodInfo" /> для объекта <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> метод события, включая закрытые методы.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.MethodInfo" /> Для объекта <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> метод.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo AddMethod
    {
      [__DynamicallyInvokable] get
      {
        return this.GetAddMethod(true);
      }
    }

    /// <summary>
    ///   Возвращает <see langword="MethodInfo" /> объект для удаления метода события, включая закрытые методы.
    /// </summary>
    /// <returns>
    ///   <see langword="MethodInfo" /> Объект для удаления метода события.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo RemoveMethod
    {
      [__DynamicallyInvokable] get
      {
        return this.GetRemoveMethod(true);
      }
    }

    /// <summary>
    ///   Возвращает метод, вызываемый при возникновении события, включая закрытые методы.
    /// </summary>
    /// <returns>Метод, вызываемый при возникновении события.</returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo RaiseMethod
    {
      [__DynamicallyInvokable] get
      {
        return this.GetRaiseMethod(true);
      }
    }

    /// <summary>
    ///   Возвращает открытые методы, которые были сопоставлены событию в метаданных с помощью <see langword=".other" /> директивы.
    /// </summary>
    /// <returns>
    ///   Массив <see cref="T:System.Reflection.EventInfo" /> объектов, представляющих открытые методы, которые были сопоставлены событию в метаданных с использованием <see langword=".other" /> директивы.
    ///    Если такие открытые методы отсутствует, возвращается пустой массив.
    /// </returns>
    public MethodInfo[] GetOtherMethods()
    {
      return this.GetOtherMethods(false);
    }

    /// <summary>
    ///   Возвращает метод, используемый для добавления делегата обработчика событий в источник событий.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, представляющий метод, используемый для добавления делегата обработчика событий в источник событий.
    /// </returns>
    [__DynamicallyInvokable]
    public MethodInfo GetAddMethod()
    {
      return this.GetAddMethod(false);
    }

    /// <summary>
    ///   Возвращает метод, используемый для удаления делегата обработчика событий из источника событий.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, представляющий метод, используемый для удаления делегата обработчика событий из источника событий.
    /// </returns>
    [__DynamicallyInvokable]
    public MethodInfo GetRemoveMethod()
    {
      return this.GetRemoveMethod(false);
    }

    /// <summary>
    ///   Возвращает метод, который вызывается при возникновении события.
    /// </summary>
    /// <returns>
    ///   Метод, который вызывается при возникновении события.
    /// </returns>
    [__DynamicallyInvokable]
    public MethodInfo GetRaiseMethod()
    {
      return this.GetRaiseMethod(false);
    }

    /// <summary>Добавляет обработчик событий в источник события.</summary>
    /// <param name="target">Источник события.</param>
    /// <param name="handler">
    ///   Инкапсулирует метод или методы, вызываемые при создании события целевым объектом.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Это событие не поддерживает открытый метод доступа <see langword="add" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Переданный обработчик нельзя использовать.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MemberAccessException" />.
    /// 
    ///   Вызывающий объект не имеет разрешения на доступ к этому элементу.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение <see cref="T:System.Exception" />.
    /// 
    ///   Параметр <paramref name="target" /> имеет значение <see langword="null" />, и событие не является статическим.
    /// 
    ///   -или-
    /// 
    ///   Класс <see cref="T:System.Reflection.EventInfo" /> не объявлен для целевого объекта.
    /// </exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public virtual void AddEventHandler(object target, Delegate handler)
    {
      MethodInfo addMethod = this.GetAddMethod();
      if (addMethod == (MethodInfo) null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicAddMethod"));
      if (addMethod.ReturnType == typeof (EventRegistrationToken))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotSupportedOnWinRTEvent"));
      addMethod.Invoke(target, new object[1]
      {
        (object) handler
      });
    }

    /// <summary>Удаляет обработчик событий из источника события.</summary>
    /// <param name="target">Источник события.</param>
    /// <param name="handler">
    ///   Делегат, связь которого с событиями, вызываемыми целевым объектом, должна быть разорвана.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Это событие не поддерживает открытый метод доступа <see langword="remove" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Переданный обработчик нельзя использовать.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение <see cref="T:System.Exception" />.
    /// 
    ///   Параметр <paramref name="target" /> имеет значение <see langword="null" />, и событие не является статическим.
    /// 
    ///   -или-
    /// 
    ///   Класс <see cref="T:System.Reflection.EventInfo" /> не объявлен для целевого объекта.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MemberAccessException" />.
    /// 
    ///   Вызывающий объект не имеет разрешения на доступ к этому элементу.
    /// </exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public virtual void RemoveEventHandler(object target, Delegate handler)
    {
      MethodInfo removeMethod = this.GetRemoveMethod();
      if (removeMethod == (MethodInfo) null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicRemoveMethod"));
      if (removeMethod.GetParametersNoCopy()[0].ParameterType == typeof (EventRegistrationToken))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotSupportedOnWinRTEvent"));
      removeMethod.Invoke(target, new object[1]
      {
        (object) handler
      });
    }

    /// <summary>
    ///   Возвращает <see langword="Type" /> объекта соответствующего делегата обработчика событий, связанных с этим событием.
    /// </summary>
    /// <returns>
    ///   Только для чтения <see langword="Type" /> объект, представляющий обработчик событий делегата.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type EventHandlerType
    {
      [__DynamicallyInvokable] get
      {
        ParameterInfo[] parametersNoCopy = this.GetAddMethod(true).GetParametersNoCopy();
        Type c = typeof (Delegate);
        for (int index = 0; index < parametersNoCopy.Length; ++index)
        {
          Type parameterType = parametersNoCopy[index].ParameterType;
          if (parameterType.IsSubclassOf(c))
            return parameterType;
        }
        return (Type) null;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see langword="EventInfo" /> имеет имя со специальным значением.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если у события есть специальное имя; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsSpecialName
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & EventAttributes.SpecialName) > 0U;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, является ли событие многоадресным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если делегат является многоадресным. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual bool IsMulticast
    {
      [__DynamicallyInvokable] get
      {
        return typeof (MulticastDelegate).IsAssignableFrom(this.EventHandlerType);
      }
    }

    Type _EventInfo.GetType()
    {
      return this.GetType();
    }

    void _EventInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _EventInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _EventInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _EventInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
