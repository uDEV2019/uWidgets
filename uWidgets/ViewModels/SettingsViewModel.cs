﻿using System;
using Avalonia.Controls;
using ReactiveUI;
using uWidgets.Core.Interfaces;
using uWidgets.Locales;
using uWidgets.Views.Pages;

namespace uWidgets.ViewModels;

public class SettingsViewModel(IAppSettingsProvider appSettingsProvider) : ReactiveObject
{
    private UserControl? currentPage;
    public UserControl? CurrentPage
    {
        get => currentPage;
        set => this.RaiseAndSetIfChanged(ref currentPage, value);
    }

    private string? currentPageTitle;
    public string? CurrentPageTitle
    {
        get => currentPageTitle;
        set => this.RaiseAndSetIfChanged(ref currentPageTitle, value);
    }

    // public PageViewModel[] AllItems => MenuItems.Concat(WidgetItems()).ToArray();
    public PageViewModel[] AllItems => MenuItems;

    public PageViewModel[] MenuItems =>
    [
        new PageViewModel(typeof(Appearance), nameof(Appearance), Locale.Settings_Appearance),
        new PageViewModel(typeof(General), nameof(General), Locale.Settings_General),
        new PageViewModel(typeof(About), nameof(About),  Locale.Settings_About)
    ];

    // public PageViewModel[] WidgetItems()
    // {
    //     return assemblyProvider
    //         .GetAssembliesMetadata(Const.WidgetsFolder)
    //         .Select(assemblyInfo => new PageViewModel(typeof(WidgetGallery), assemblyInfo.Key, assemblyInfo.Key, assemblyInfo.MaxBy(assembly => assembly.AssemblyName.Version)))
    //         .ToArray();
    // }

    public void SetCurrentPage(PageViewModel? value)
    {
        CurrentPage = value?.Type != null
            ? value.AssemblyInfo == null
                ? (UserControl?)Activator.CreateInstance(value.Type, appSettingsProvider)
                : null
                // : (UserControl?)new WidgetGallery(appSettingsProvider, layoutProvider, assemblyProvider, value.AssemblyInfo, widgetFactory)
            : null;
        CurrentPageTitle = value?.Text;
    }
}