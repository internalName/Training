// Decompiled with JetBrains decompiler
// Type: System.AppContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System
{
  /// <summary>
  ///   Предоставляет члены для задания и получения данных о контексте приложения.
  /// </summary>
  public static class AppContext
  {
    private static readonly Dictionary<string, AppContext.SwitchValueState> s_switchMap = new Dictionary<string, AppContext.SwitchValueState>();
    private static volatile bool s_defaultsInitialized = false;

    /// <summary>
    ///   Возвращает путь к базовому каталогу, в котором сопоставитель сборок производит поиск.
    /// </summary>
    /// <returns>
    ///   Путь к базовому каталогу, в котором сопоставитель сборок производит поиск.
    /// </returns>
    public static string BaseDirectory
    {
      get
      {
        return (string) AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY") ?? AppDomain.CurrentDomain.BaseDirectory;
      }
    }

    /// <summary>
    ///   Получает имя целевой версии платформы текущего приложения.
    /// </summary>
    /// <returns>Имя целевой версии платформы текущего приложения.</returns>
    public static string TargetFrameworkName
    {
      get
      {
        return AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName;
      }
    }

    /// <summary>
    ///   Возвращает значение именованного элемента данных, назначенное текущему домену приложения.
    /// </summary>
    /// <param name="name">Имя элемента данных.</param>
    /// <returns>
    ///   Значение <paramref name="name" />, если <paramref name="name" /> определяет именованное значение; в противном случае — <see langword="null" />.
    /// </returns>
    public static object GetData(string name)
    {
      return AppDomain.CurrentDomain.GetData(name);
    }

    private static void InitializeDefaultSwitchValues()
    {
      lock (AppContext.s_switchMap)
      {
        if (AppContext.s_defaultsInitialized)
          return;
        AppContextDefaultValues.PopulateDefaultValues();
        AppContext.s_defaultsInitialized = true;
      }
    }

    /// <summary>
    ///   Предпринимает попытку получения значения переключателя.
    /// </summary>
    /// <param name="switchName">Имя переключателя.</param>
    /// <param name="isEnabled">
    ///   При возвращении этого метода содержит значение для <paramref name="switchName" />, если <paramref name="switchName" /> найден, или <see langword="false" />, если <paramref name="switchName" /> не найден.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если для <paramref name="switchName" /> задано значение и аргумент <paramref name="isEnabled" /> содержит значение переключателя; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="switchName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="switchName" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </exception>
    public static bool TryGetSwitch(string switchName, out bool isEnabled)
    {
      if (switchName == null)
        throw new ArgumentNullException(nameof (switchName));
      if (switchName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (switchName));
      if (!AppContext.s_defaultsInitialized)
        AppContext.InitializeDefaultSwitchValues();
      isEnabled = false;
      lock (AppContext.s_switchMap)
      {
        AppContext.SwitchValueState switchValueState;
        if (AppContext.s_switchMap.TryGetValue(switchName, out switchValueState))
        {
          if (switchValueState == AppContext.SwitchValueState.UnknownValue)
          {
            isEnabled = false;
            return false;
          }
          isEnabled = (switchValueState & AppContext.SwitchValueState.HasTrueValue) == AppContext.SwitchValueState.HasTrueValue;
          if ((switchValueState & AppContext.SwitchValueState.HasLookedForOverride) == AppContext.SwitchValueState.HasLookedForOverride)
            return true;
          bool overrideValue;
          if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out overrideValue))
            isEnabled = overrideValue;
          AppContext.s_switchMap[switchName] = (AppContext.SwitchValueState) ((isEnabled ? 2 : 1) | 4);
          return true;
        }
        bool overrideValue1;
        if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out overrideValue1))
        {
          isEnabled = overrideValue1;
          AppContext.s_switchMap[switchName] = (AppContext.SwitchValueState) ((isEnabled ? 2 : 1) | 4);
          return true;
        }
        AppContext.s_switchMap[switchName] = AppContext.SwitchValueState.UnknownValue;
      }
      return false;
    }

    /// <summary>Задает значение переключателя.</summary>
    /// <param name="switchName">Имя переключателя.</param>
    /// <param name="isEnabled">Значение переключателя.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="switchName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="switchName" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </exception>
    public static void SetSwitch(string switchName, bool isEnabled)
    {
      if (switchName == null)
        throw new ArgumentNullException(nameof (switchName));
      if (switchName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (switchName));
      if (!AppContext.s_defaultsInitialized)
        AppContext.InitializeDefaultSwitchValues();
      AppContext.SwitchValueState switchValueState = (AppContext.SwitchValueState) ((isEnabled ? 2 : 1) | 4);
      lock (AppContext.s_switchMap)
        AppContext.s_switchMap[switchName] = switchValueState;
    }

    internal static void DefineSwitchDefault(string switchName, bool isEnabled)
    {
      AppContext.s_switchMap[switchName] = isEnabled ? AppContext.SwitchValueState.HasTrueValue : AppContext.SwitchValueState.HasFalseValue;
    }

    internal static void DefineSwitchOverride(string switchName, bool isEnabled)
    {
      AppContext.s_switchMap[switchName] = (AppContext.SwitchValueState) ((isEnabled ? 2 : 1) | 4);
    }

    [Flags]
    private enum SwitchValueState
    {
      HasFalseValue = 1,
      HasTrueValue = 2,
      HasLookedForOverride = 4,
      UnknownValue = 8,
    }
  }
}
