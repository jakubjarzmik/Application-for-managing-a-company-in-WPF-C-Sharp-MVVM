using Firma.Helpers;
using Firma.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Konstruktor
        private static MainWindowViewModel instance;
        public static MainWindowViewModel GetInstance()
        {
            if (instance == null)
                instance = new MainWindowViewModel();
            return instance;
        }
        private MainWindowViewModel() : base()
        {
            Loading = "Visible";
            IsConnected = false;
            setMessengers();
            NumberOfTabs = 0;
        }
        #endregion
        #region Commands
        public ICommand NowyTowarCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyTowarViewModel()));
            }
        }
        public ICommand NowaFakturaCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaFakturaViewModel()));
            }
        }
        public ICommand NowyKontrahentCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyKontrahentViewModel()));
            }
        }
        public ICommand FakturyKategorieCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new FakturyKategorieViewModel()));
            }
        }
        public ICommand NowaKategoriaFakturyCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaKategoriaFakturyViewModel()));
            }
        }
        public ICommand FakturyRodzajeCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new FakturyRodzajeViewModel()));
            }
        }
        public ICommand NowyRodzajFakturyCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyRodzajFakturyViewModel()));
            }
        }
        public ICommand KontrahenciRodzajeCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new KontrahenciRodzajeViewModel()));
            }
        }
        public ICommand NowyRodzajKontrahentaCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyRodzajKontrahentaViewModel()));
            }
        }
        public ICommand MagazynyTypyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new MagazynyTypyViewModel()));
            }
        }
        public ICommand NowyTypMagazynuCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyTypMagazynuViewModel()));
            }
        }
        public ICommand RodzajePlatnosciCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new RodzajePlatnosciViewModel()));
            }
        }
        public ICommand NowyRodzajPlatnosciCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyRodzajPlatnosciViewModel()));
            }
        }
        public ICommand TowaryGrupyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new TowaryGrupyViewModel()));
            }
        }
        public ICommand NowaGrupaTowarowCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaGrupaTowarowViewModel()));
            }
        }
        public ICommand TowaryJednMiaryCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new JednostkiMiaryViewModel()));
            }
        }
        public ICommand NowaJednostkaMiaryCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaJednostkaMiaryViewModel()));
            }
        }
        public ICommand TowaryTypyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new TowaryTypyViewModel()));
            }
        }
        public ICommand NowyTypTowarowCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyTypTowarowViewModel()));
            }
        }
        public ICommand UmowyRodzajeCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new UmowyRodzajeViewModel()));
            }
        }
        public ICommand NowyRodzajUmowyCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyRodzajUmowyViewModel()));
            }
        }
        public ICommand UmowyStanowiskaCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new UmowyStanowiskaViewModel()));
            }
        }
        public ICommand NoweStanowiskoCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NoweStanowiskoViewModel()));
            }
        }
        public ICommand KontaktyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new KontaktyViewModel()));
            }
        }
        public ICommand NowyKontaktCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyKontaktViewModel()));
            }
        }
        public ICommand KrajeCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new KrajeViewModel()));
            }
        }
        public ICommand NowyKrajCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyKrajViewModel()));
            }
        }
        public ICommand FakturyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new FakturyViewModel()));
            }
        }
        public ICommand FakturyWydaniaZewnetrzneCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new FakturyWydaniaZewnetrzneViewModel()));
            }
        }
        public ICommand AdresyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new AdresyViewModel()));
            }
        }
        public ICommand NowyAdresCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyAdresViewModel()));
            }
        }
        public ICommand KontrahenciCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new KontrahenciViewModel()));
            }
        }
        public ICommand KontrahenciKontaktyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new KontrahenciKontaktyViewModel()));
            }
        }
        public ICommand MagazynyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new MagazynyViewModel()));
            }
        }
        public ICommand NowyMagazynCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyMagazynViewModel()));
            }
        }
        public ICommand PozycjePrzyjeciaZewnetrznegoCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new PozycjePrzyjeciaZewnetrznegoViewModel()));
            }
        }
        public ICommand PozycjeWydaniaZewnetrznegoCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new PozycjeWydaniaZewnetrznegoViewModel()));
            }
        }
        public ICommand PracownicyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new PracownicyViewModel()));
            }
        }
        public ICommand PracownicyUmowyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new PracownicyUmowyViewModel()));
            }
        }
        public ICommand NowyPracownikCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyPracownikViewModel()));
            }
        }
        public ICommand PrzyjeciaZewnetrzneCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new PrzyjeciaZewnetrzneViewModel()));
            }
        }
        public ICommand NowePrzyjecieZewnetrzneCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowePrzyjecieZewnetrzneViewModel()));
            }
        }
        public ICommand WydaniaZewnetrzneCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new WydaniaZewnetrzneViewModel()));
            }
        }
        public ICommand NoweWydanieZewnetrzneCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NoweWydanieZewnetrzneViewModel()));
            }
        }
        public ICommand TowaryCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new TowaryViewModel()));
            }
        }
        public ICommand TowaryStawkiVatCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new TowaryStawkiVatViewModel()));
            }
        }
        public ICommand NowaStawkaVatTowarowCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaStawkaVatTowarowViewModel()));
            }
        }
        public ICommand UmowyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new UmowyViewModel()));
            }
        }
        public ICommand NowaUmowaCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaUmowaViewModel()));
            }
        }
        public ICommand ZmianyCenyCommand
        {
            get
            {
                return new BaseCommand(() => showAll(new ZmianyCenyViewModel()));
            }
        }
        public ICommand NowaZmianaCenyCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaZmianaCenyViewModel()));
            }
        }
        public ICommand SrednieWynagrodzenieCommand
        {
            get
            {
                return new BaseCommand(() => createView(new SrednieWynagrodzenieViewModel()));
            }
        }
        public ICommand RaportSprzedazyCommand
        {
            get
            {
                return new BaseCommand(() => createView(new RaportSprzedazyViewModel()));
            }
        }
        #endregion
        #region Properties
        private bool _IsConnected;
        public bool IsConnected
        {
            get
            {
                return _IsConnected;
            }
            set
            {
                if (value != _IsConnected)
                {
                    _IsConnected = value;
                    base.OnPropertyChanged(() => IsConnected);
                }
            }
        }
        private string _Loading;
        public string Loading
        {
            get
            {
                return _Loading;
            }
            set
            {
                if (value != _Loading)
                {
                    _Loading = value;
                    base.OnPropertyChanged(() => Loading);
                }
            }
        }
        private int _NumberOfTabs;
        public int NumberOfTabs
        {
            get
            {
                return _NumberOfTabs;
            }
            set
            {
                if (value != _NumberOfTabs)
                {
                    _NumberOfTabs = value;
                    base.OnPropertyChanged(() => NumberOfTabs);
                }
            }
        }
        #endregion
        #region Leftside menu buttons
        private ReadOnlyCollection<CommandViewModel> _Commands;
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }
        private List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel("Faktury", new BaseCommand(()=>showAll(new FakturyViewModel())),"pack://application:,,,/Views/Content/Icons/file-white.png",
                "pack://application:,,,/Views/Content/Icons/file.png"),
                new CommandViewModel("Kontrahenci", new BaseCommand(()=>showAll(new KontrahenciViewModel())),"pack://application:,,,/Views/Content/Icons/person-white.png",
                "pack://application:,,,/Views/Content/Icons/person.png"),
                new CommandViewModel("Pracownicy", new BaseCommand(()=>showAll(new PracownicyViewModel())),"pack://application:,,,/Views/Content/Icons/employee-white.png",
                "pack://application:,,,/Views/Content/Icons/employee.png"),
                new CommandViewModel("Przyjecia Zewnetrzne", new BaseCommand(()=>showAll(new PrzyjeciaZewnetrzneViewModel())),"pack://application:,,,/Views/Content/Icons/pz-white.png",
                "pack://application:,,,/Views/Content/Icons/pz.png"),
                new CommandViewModel("Wydania Zewnetrzne", new BaseCommand(()=>showAll(new WydaniaZewnetrzneViewModel())),"pack://application:,,,/Views/Content/Icons/wz-white.png",
                "pack://application:,,,/Views/Content/Icons/wz.png"),
                new CommandViewModel("Towary", new BaseCommand(()=>showAll(new TowaryViewModel())),"pack://application:,,,/Views/Content/Icons/package-white.png",
                "pack://application:,,,/Views/Content/Icons/package.png"),
                new CommandViewModel("Średnie wynagrodz.",new BaseCommand(()=>createView(new SrednieWynagrodzenieViewModel())),"pack://application:,,,/Views/Content/Icons/payments-white.png",
                "pack://application:,,,/Views/Content/Icons/payments.png"),
                new CommandViewModel("Raport sprzedaży",new BaseCommand(()=>createView(new RaportSprzedazyViewModel())),"pack://application:,,,/Views/Content/Icons/summarize-white.png",
                "pack://application:,,,/Views/Content/Icons/summarize.png"),
                new CommandViewModel("Najpopularniejszy",new BaseCommand(()=>createView(new NajpopularniejszyViewModel())),"pack://application:,,,/Views/Content/Icons/star-white.png",
                "pack://application:,,,/Views/Content/Icons/star.png"),
            };
        }
        #endregion
        #region Tabs
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.onWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.onWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.onWorkspaceRequestClose;
        }
        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            NumberOfTabs--;
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            this.Workspaces.Remove(workspace);
        }
        #endregion
        #region Helpers
        private void createView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            NumberOfTabs++;
            this.setActiveWorkspace(workspace);
        }
        private void showAll(WorkspaceViewModel workspace)
        {
            bool contains = false;
            foreach (var existingWorkspace in Workspaces)
            {
                if (existingWorkspace.GetType() == workspace.GetType())
                {
                    contains = true;
                    workspace = existingWorkspace;
                    break;
                }
            }
            if (!contains)
            {
                Workspaces.Add(workspace);
                NumberOfTabs++;
            }
            this.setActiveWorkspace(workspace);
        }
        private void setActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        private void open(string message)
        {
            string[] messageParts = message.Split(';');
            string name = messageParts[0];
            string token = "";
            if (messageParts.Length > 1)
                token = messageParts[1];
            switch (name)
            {
                #region Add a foreign key through a window
                case "FakturyAll":
                    createView(new FakturyViewModel(token));
                    break;
                case "KontrahenciAll":
                    createView(new KontrahenciViewModel(token));
                    break;
                case "KrajeAll":
                    createView(new KrajeViewModel(token));
                    break;
                case "AdresyAll":
                    createView(new AdresyViewModel(token, false));
                    break;
                case "AdresyKorAll":
                    createView(new AdresyViewModel(token, true));
                    break;
                case "GrupyTowaruAll":
                    createView(new TowaryGrupyViewModel(token));
                    break;
                case "VatSprzAll":
                    createView(new TowaryStawkiVatViewModel(token, false));
                    break;
                case "VatZakAll":
                    createView(new TowaryStawkiVatViewModel(token, true));
                    break;
                case "JednostkiMiaryAll":
                    createView(new JednostkiMiaryViewModel(token));
                    break;
                case "StanowiskaAll":
                    createView(new UmowyStanowiskaViewModel(token));
                    break;
                case "TowaryAll":
                    createView(new TowaryViewModel(token));
                    break;
                case "WydaniaZewnetrzneAll":
                    createView(new WydaniaZewnetrzneViewModel(token));
                    break;
                case "PrzyjeciaZewnetrzneAll":
                    createView(new PrzyjeciaZewnetrzneViewModel(token));
                    break;
                case "KontaktyAll":
                    createView(new KontaktyViewModel(token));
                    break;
                case "PracownicyAll":
                    createView(new PracownicyViewModel(token));
                    break;
                case "UmowyAll":
                    createView(new UmowyViewModel(token));
                    break;
                    #endregion
            }
        }
        private void setMessengers()
        {
            Messenger.Default.Register<string>(this, open);
            Messenger.Default.Register<NowaFakturaViewModel>(this, createView);
            Messenger.Default.Register<NowaFakturaWydanieZewnetrzneViewModel>(this, createView);
            Messenger.Default.Register<NowaGrupaTowarowViewModel>(this, createView);
            Messenger.Default.Register<NowaJednostkaMiaryViewModel>(this, createView);
            Messenger.Default.Register<NowaKategoriaFakturyViewModel>(this, createView);
            Messenger.Default.Register<NowaStawkaVatTowarowViewModel>(this, createView);
            Messenger.Default.Register<NowaUmowaViewModel>(this, createView);
            Messenger.Default.Register<NowaZmianaCenyViewModel>(this, createView);
            Messenger.Default.Register<NowePrzyjecieZewnetrzneViewModel>(this, createView);
            Messenger.Default.Register<NoweStanowiskoViewModel>(this, createView);
            Messenger.Default.Register<NoweWydanieZewnetrzneViewModel>(this, createView);
            Messenger.Default.Register<NowyAdresViewModel>(this, createView);
            Messenger.Default.Register<NowyKontaktViewModel>(this, createView);
            Messenger.Default.Register<NowyKontrahentViewModel>(this, createView);
            Messenger.Default.Register<NowyKontrahentKontaktViewModel>(this, createView);
            Messenger.Default.Register<NowyKrajViewModel>(this, createView);
            Messenger.Default.Register<NowyMagazynViewModel>(this, createView);
            Messenger.Default.Register<NowyPracownikViewModel>(this, createView);
            Messenger.Default.Register<NowyPracownikUmowaViewModel>(this, createView);
            Messenger.Default.Register<NowyRodzajFakturyViewModel>(this, createView);
            Messenger.Default.Register<NowyRodzajKontrahentaViewModel>(this, createView);
            Messenger.Default.Register<NowyRodzajPlatnosciViewModel>(this, createView);
            Messenger.Default.Register<NowyRodzajUmowyViewModel>(this, createView);
            Messenger.Default.Register<NowyTowarViewModel>(this, createView);
            Messenger.Default.Register<NowaPozycjaPrzyjeciaZewnetrznegoViewModel>(this, createView);
            Messenger.Default.Register<NowaPozycjaWydaniaZewnetrznegoViewModel>(this, createView);
            Messenger.Default.Register<NowyTypMagazynuViewModel>(this, createView);
            Messenger.Default.Register<NowyTypTowarowViewModel>(this, createView);
        }
        public void CheckDatabaseConnection()
        {
            using (var db = new JJFirmaEntities())
            {
                try
                {
                    db.Connection.Open();
                    IsConnected = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Połączenie z bazą danych nieudane.");
                    Environment.Exit(1);
                }
                finally
                {
                    Loading = "Hidden";
                }
            }
        }
        #endregion
    }
}
