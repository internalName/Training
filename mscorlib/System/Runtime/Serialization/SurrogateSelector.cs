// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SurrogateSelector
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Содействует форматерам при выборе суррогата сериализации делегировать сериализации или десериализации процесса.
  /// </summary>
  [ComVisible(true)]
  public class SurrogateSelector : ISurrogateSelector
  {
    internal SurrogateHashtable m_surrogates;
    internal ISurrogateSelector m_nextSelector;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.SurrogateSelector" />.
    /// </summary>
    public SurrogateSelector()
    {
      this.m_surrogates = new SurrogateHashtable(32);
    }

    /// <summary>Добавляет суррогат в список проверенных суррогатов.</summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Для которого требуется суррогат.
    /// </param>
    /// <param name="context">Данные, зависящие от контекста.</param>
    /// <param name="surrogate">Суррогат для вызова этого типа.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="type" /> или параметра <paramref name="surrogate" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Для этого типа и контекста суррогат уже существует.
    /// </exception>
    public virtual void AddSurrogate(Type type, StreamingContext context, ISerializationSurrogate surrogate)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (surrogate == null)
        throw new ArgumentNullException(nameof (surrogate));
      this.m_surrogates.Add((object) new SurrogateKey(type, context), (object) surrogate);
    }

    [SecurityCritical]
    private static bool HasCycle(ISurrogateSelector selector)
    {
      ISurrogateSelector surrogateSelector1 = selector;
      ISurrogateSelector surrogateSelector2 = selector;
      while (surrogateSelector1 != null)
      {
        ISurrogateSelector nextSelector = surrogateSelector1.GetNextSelector();
        if (nextSelector == null)
          return true;
        if (nextSelector == surrogateSelector2)
          return false;
        surrogateSelector1 = nextSelector.GetNextSelector();
        surrogateSelector2 = surrogateSelector2.GetNextSelector();
        if (surrogateSelector1 == surrogateSelector2)
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Добавляет указанный <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> может обрабатывать конкретный тип объекта для списка суррогатов.
    /// </summary>
    /// <param name="selector">Суррогатный селектор.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="selector" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Селектор уже находится в списке селекторов.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    public virtual void ChainSelector(ISurrogateSelector selector)
    {
      if (selector == null)
        throw new ArgumentNullException(nameof (selector));
      if (selector == this)
        throw new SerializationException(Environment.GetResourceString("Serialization_DuplicateSelector"));
      if (!SurrogateSelector.HasCycle(selector))
        throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycleInArgument"), nameof (selector));
      ISurrogateSelector nextSelector1 = selector.GetNextSelector();
      ISurrogateSelector surrogateSelector1 = selector;
      for (; nextSelector1 != null && nextSelector1 != this; nextSelector1 = nextSelector1.GetNextSelector())
        surrogateSelector1 = nextSelector1;
      if (nextSelector1 == this)
        throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), nameof (selector));
      ISurrogateSelector surrogateSelector2 = selector;
      ISurrogateSelector surrogateSelector3 = selector;
      while (surrogateSelector2 != null)
      {
        ISurrogateSelector surrogateSelector4 = surrogateSelector2 != surrogateSelector1 ? surrogateSelector2.GetNextSelector() : this.GetNextSelector();
        if (surrogateSelector4 != null)
        {
          if (surrogateSelector4 == surrogateSelector3)
            throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), nameof (selector));
          surrogateSelector2 = surrogateSelector4 != surrogateSelector1 ? surrogateSelector4.GetNextSelector() : this.GetNextSelector();
          surrogateSelector3 = surrogateSelector3 != surrogateSelector1 ? surrogateSelector3.GetNextSelector() : this.GetNextSelector();
          if (surrogateSelector2 == surrogateSelector3)
            throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), nameof (selector));
        }
        else
          break;
      }
      ISurrogateSelector nextSelector2 = this.m_nextSelector;
      this.m_nextSelector = selector;
      if (nextSelector2 == null)
        return;
      surrogateSelector1.ChainSelector(nextSelector2);
    }

    /// <summary>Возвращает следующий селектор в цепочку селекторов.</summary>
    /// <returns>
    ///   Следующий <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> в цепочке селекторов.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    public virtual ISurrogateSelector GetNextSelector()
    {
      return this.m_nextSelector;
    }

    /// <summary>Возвращает суррогат для определенного типа.</summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Для которого запрашивается суррогат.
    /// </param>
    /// <param name="context">Контекст потоковой передачи.</param>
    /// <param name="selector">Суррогат для использования.</param>
    /// <returns>Суррогат для определенного типа.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      selector = (ISurrogateSelector) this;
      ISerializationSurrogate surrogate = (ISerializationSurrogate) this.m_surrogates[(object) new SurrogateKey(type, context)];
      if (surrogate != null)
        return surrogate;
      if (this.m_nextSelector != null)
        return this.m_nextSelector.GetSurrogate(type, context, out selector);
      return (ISerializationSurrogate) null;
    }

    /// <summary>Удаляет суррогат, связанный с заданным типом.</summary>
    /// <param name="type">
    ///   <see cref="T:System.Type" /> Для которого удаляется суррогат.
    /// </param>
    /// <param name="context">
    ///   <see cref="T:System.Runtime.Serialization.StreamingContext" /> Для текущего суррогата.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual void RemoveSurrogate(Type type, StreamingContext context)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      this.m_surrogates.Remove((object) new SurrogateKey(type, context));
    }
  }
}
