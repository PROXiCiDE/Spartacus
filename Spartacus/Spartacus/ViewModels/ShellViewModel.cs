using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using Caliburn.Micro;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using Spartacus.Common;
using Spartacus.ViewModels.AiCharacter;
using Spartacus.ViewModels.Music;
using Spartacus.ViewModels.Quest;

namespace Spartacus.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<object>, IShell
    {
        public const string DialogHostName = "dialogHost";
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;

            SpartacusNavItems = new List<INavigationItem>
            {
                new FirstLevelNavigationItem {Label = "TestFile.xml", NavigationItemSelectedCallback = item => null},
                new FirstLevelNavigationItem {Label = "TestFile2.xml", NavigationItemSelectedCallback = item => null}
            };

            NavigationItems = new List<INavigationItem>
            {
                new SubheaderNavigationItem {Subheader = "Ai Character"},
                new DefaultLevelNavigationItem
                {
                    Label = "Character",
                    NavigationItemSelectedCallback = item => new CharacterBasicViewModel(_windowManager, _eventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Proto Unit",
                    NavigationItemSelectedCallback = item => new ProtoUnitViewModel(_windowManager, _eventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Trait's",
                    NavigationItemSelectedCallback =
                        item => new TraitInformationViewModel(_windowManager, _eventAggregator)
                },
                new DividerNavigationItem(),

                new SubheaderNavigationItem {Subheader = "Music"},
                new DefaultLevelNavigationItem
                {
                    Label = "Music",
                    NavigationItemSelectedCallback = item => new MusicViewModel(_windowManager, _eventAggregator)
                },
                new DividerNavigationItem(),

                new SubheaderNavigationItem {Subheader = "Quest"},
                new DefaultLevelNavigationItem
                {
                    Label = "Quest Information",
                    NavigationItemSelectedCallback =
                        item => new QuestInformationViewModel(_windowManager, _eventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Objectives",
                    NavigationItemSelectedCallback = item => new ObjectivesViewModel(_windowManager, _eventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Player Settings",
                    NavigationItemSelectedCallback =
                        item => new PlayerSettingsViewModel(_windowManager, _eventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Targets",
                    NavigationItemSelectedCallback = item => new QuestTargetsViewModel(_windowManager, _eventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Map Information",
                    NavigationItemSelectedCallback =
                        item => new MapInformationViewModel(_windowManager, _eventAggregator)
                },
                new DividerNavigationItem()
            };

            NavigationItems[1].IsSelected = true;
            SelectNavigationItem(NavigationItems[1]);
        }

        public bool IsNavigationDrawerOpen { get; } = false;

        public List<INavigationItem> NavigationItems { get; }

        public List<INavigationItem> SpartacusNavItems { get; }

        public void GoToGitHubButtonClickHandler(RoutedEventArgs e)
        {
            Process.Start("https://github.com/PROXiCiDE/Spartacus");
        }

        public void GoToDocumentation(RoutedEventArgs e)
        {
            if (ActiveItem is IWikiDocumentation wiki)
                Process.Start(wiki.WikiUrl);
        }

        public void RecentFileNavigationItemSelectedHandler(NavigationItemSelectedEventArgs args)
        {
            Debug.WriteLine($"{args.NavigationItem}, {args.OriginalSource}, {args.Source}");
        }

        public void NavigationItemSelectedHandler(NavigationItemSelectedEventArgs args)
        {
            SelectNavigationItem(args.NavigationItem);
        }

        public void SelectNavigationItem(INavigationItem navigationItem)
        {
            if (navigationItem != null) ActivateItem(navigationItem.NavigationItemSelectedCallback(navigationItem));
        }
    }
}