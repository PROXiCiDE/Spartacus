using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Common;
using Spartacus.Common.MessageQueue;
using Spartacus.ViewModels.AiCharacter;
using Spartacus.ViewModels.Music;
using Spartacus.ViewModels.Quest;

namespace Spartacus.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<object>, IShell, IHandle<MessageQueue>
    {
        public const string DialogHostName = "dialogHost";
        private ProgressBarStatus _barStatus;
        private SnackbarMessageQueue _myMessageQueue;

        public AiCharacterXml AiCharacter { get; set; }
        [ImportingConstructor]
        public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            Manager = windowManager;
            EventAggregator = eventAggregator;

            EventAggregator.Subscribe(this);

            BarStatus = new ProgressBarStatus();

            var timespan = ConfigurationManager.AppSettings["SnackBarMessageDurationSeconds"];
            if (double.TryParse(timespan, out var timespanResults))
                MyMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(timespanResults));

            SpartacusNavItems = new List<INavigationItem>
            {
                new FirstLevelNavigationItem
                    {Label = "C01_Ramp_T4_L40.character", NavigationItemSelectedCallback = item => null},
                new FirstLevelNavigationItem
                    {Label = "C01_CyprusRamp_T3_L28.character", NavigationItemSelectedCallback = item => null},
                new FirstLevelNavigationItem   {Label = "C04_Ramp_T2_L09.character", NavigationItemSelectedCallback = item => null},
                new FirstLevelNavigationItem   {Label = "C06_Ramp_T2_L07.character", NavigationItemSelectedCallback = item => null}
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
        }

        public ProgressBarStatus BarStatus
        {
            get => _barStatus;
            set
            {
                _barStatus = value;
                NotifyOfPropertyChange(() => BarStatus);
            }
        }

        public IEventAggregator EventAggregator { get; set; }
        public IWindowManager Manager { get; set; }

        public SnackbarMessageQueue MyMessageQueue
        {
            get => _myMessageQueue;
            set
            {
                _myMessageQueue = value;
                NotifyOfPropertyChange(() => MyMessageQueue);
            }
        }

        public bool IsNavigationDrawerOpen { get; } = false;

        public List<INavigationItem> NavigationItems { get; }

        public List<INavigationItem> SpartacusNavItems { get; }

        #region Event Aggregator Handles

        public void Handle(MessageQueue message)
        {
            Debug.WriteLine($"Snackbar {message.Message}");
            MyMessageQueue.Enqueue(message.Message);
        }

        #endregion

        #region Control Events

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
            var fileName = ((args.OriginalSource as ListBox)?.SelectedItem as FirstLevelNavigationItem)?.Label;
            AiCharacter = AiCharacterXml.FromXmlFile(fileName, "test");
            EventAggregator.PublishOnUIThread(new CharacterMessageQueue(fileName, AiCharacter));
        }

        public void NavigationItemSelectedHandler(NavigationItemSelectedEventArgs args)
        {
            SelectNavigationItem(args.NavigationItem);
        }

        public void SelectNavigationItem(INavigationItem navigationItem)
        {
            if (navigationItem != null)
            {
                var modelItem = navigationItem.NavigationItemSelectedCallback(navigationItem);
                ActivateItem(modelItem);
            }
        }

        #endregion
    }
}