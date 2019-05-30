using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
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
        public IEventAggregator EventAggregator { get; set; }
        public IWindowManager Manager { get; set; }

        [ImportingConstructor]
        public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            Manager = windowManager;
            EventAggregator = eventAggregator;

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
                    NavigationItemSelectedCallback = item => new CharacterViewModel(Manager, EventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Proto Unit",
                    NavigationItemSelectedCallback = item => new ProtoUnitViewModel(Manager, EventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Trait's",
                    NavigationItemSelectedCallback =
                        item => new TraitInformationViewModel(Manager, EventAggregator)
                },
                new DividerNavigationItem(),

                new SubheaderNavigationItem {Subheader = "Music"},
                new DefaultLevelNavigationItem
                {
                    Label = "Music",
                    NavigationItemSelectedCallback = item => new MusicViewModel(Manager, EventAggregator)
                },
                new DividerNavigationItem(),

                new SubheaderNavigationItem {Subheader = "Quest"},
                new DefaultLevelNavigationItem
                {
                    Label = "Quest Information",
                    NavigationItemSelectedCallback =
                        item => new QuestInformationViewModel(Manager, EventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Objectives",
                    NavigationItemSelectedCallback = item => new ObjectivesViewModel(Manager, EventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Player Settings",
                    NavigationItemSelectedCallback =
                        item => new PlayerSettingsViewModel(Manager, EventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Targets",
                    NavigationItemSelectedCallback = item => new QuestTargetsViewModel(Manager, EventAggregator)
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Map Information",
                    NavigationItemSelectedCallback =
                        item => new MapInformationViewModel(Manager, EventAggregator)
                },
                new DividerNavigationItem()
            };

            //NavigationItems[1].IsSelected = true;
            //SelectNavigationItem(NavigationItems[1]);
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

        public void RecentFileNavigationItemSelectedHandler(RoutedEventArgs args)
        {
            EventAggregator.PublishOnUIThread(new CharacterMessageQueue(((args.OriginalSource as ListBox)?.SelectedItem as FirstLevelNavigationItem)?.Label));
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