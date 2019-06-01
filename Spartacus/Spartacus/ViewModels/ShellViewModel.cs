using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell, IHandle<MessageQueue>
    {
        public const string DialogHostName = "dialogHost";
        private ProgressBarStatus _barStatus;
        private SnackbarMessageQueue _myMessageQueue;

        //This will allow us to share AiCharacter in the SDI Ai Character Models
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
                    NavigationItemSelectedCallback = item => new CharacterViewModel()
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Milestone Rewards",
                    NavigationItemSelectedCallback = item => new MilestoneRewardsViewModel()
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Proto Units",
                    NavigationItemSelectedCallback = item => new ProtoUnitViewModel()
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Trait",
                    NavigationItemSelectedCallback = item => new TraitInformationViewModel()
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Developer Tool",
                    NavigationItemSelectedCallback = item => new DeveloperToolViewModel()
                },
                new DividerNavigationItem(),

                new SubheaderNavigationItem {Subheader = "Music"},
                new DefaultLevelNavigationItem
                {
                    Label = "Music",
                    NavigationItemSelectedCallback = item => new MusicViewModel()
                },
                new DividerNavigationItem(),

                new SubheaderNavigationItem {Subheader = "Quest"},
                new DefaultLevelNavigationItem
                {
                    Label = "Quest Information",
                    NavigationItemSelectedCallback =
                        item => new QuestInformationViewModel()
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Objectives",
                    NavigationItemSelectedCallback = item => new ObjectivesViewModel()
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Player Settings",
                    NavigationItemSelectedCallback =
                        item => new PlayerSettingsViewModel()
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Targets",
                    NavigationItemSelectedCallback = item => new QuestTargetsViewModel()
                },
                new DefaultLevelNavigationItem
                {
                    Label = "Map Information",
                    NavigationItemSelectedCallback =
                        item => new MapInformationViewModel()
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
                NotifyOfPropertyChange(nameof(BarStatus));
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
                NotifyOfPropertyChange(nameof(MyMessageQueue));
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
                if (navigationItem.NavigationItemSelectedCallback(navigationItem) is IScreen modelItem)
                    ActivateItem(modelItem);
            }
        }

        #endregion
    }
}