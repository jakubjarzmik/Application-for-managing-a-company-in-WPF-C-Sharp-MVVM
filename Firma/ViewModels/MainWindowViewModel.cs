using Firma.Helpers;
using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public  class MainWindowViewModel : BaseViewModel
    {
        //będzie zawierała kolekcje komend, które pojawiają się w menu lewym oraz kolekcje zakładek 
        #region Komendy menu i paska narzedzi
        public ICommand NowyTowarCommand //ta komenda zostanie podpieta pod menu i pasek narzedzi
        { 
            get
            {
                return new BaseCommand(()=>createView(new NowyTowarViewModel()));//to jest komenda, która wyw. funkcję createTowar
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
        public ICommand PrzyjecieZewnetrzneCommand
        {
            get
            {
                return new BaseCommand(() => createView(new PrzyjecieZewnetrzneViewModel()));
            }
        }
        public ICommand WydanieZewnetrzneCommand
        {
            get
            {
                return new BaseCommand(() => createView(new WydanieZewnetrzneViewModel()));
            }
        }
        public ICommand RejestrVATCommand
        {
            get
            {
                return new BaseCommand(() => createView(new RejestrVATViewModel()));
            }
        }
        public ICommand EwidencjaPlatnosciCommand
        {
            get
            {
                return new BaseCommand(() => createView(new EwidencjaPlatnosciViewModel()));
            }
        }
        public ICommand PojazdyCommand
        {
            get
            {
                return new BaseCommand(() => showAllPojazdy());
            }
        }
        public ICommand NowyPojazdCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyPojazdViewModel()));
            }
        }
        public ICommand FakturyKategorieCommand
        {
            get
            {
                return new BaseCommand(() => showAllFakturyKategorie());
            }
        }
        public ICommand NowaKategoriaFakturyCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaKategoriaFakturyViewModel()));
            }
        }
        public ICommand KontrahenciRodzajeCommand
        {
            get
            {
                return new BaseCommand(() => showAllKontrahenciRodzaje());
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
                return new BaseCommand(() => showAllMagazynyTypy());
            }
        }
        public ICommand NowyTypMagazynuCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyTypMagazynuViewModel()));
            }
        }
        public ICommand RodzajeCenyCommand
        {
            get
            {
                return new BaseCommand(() => showAllRodzajeCeny());
            }
        }
        public ICommand NowyRodzajCenyCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyRodzajCenyViewModel()));
            }
        }
        public ICommand RodzajePlatnosciCommand
        {
            get
            {
                return new BaseCommand(() => showAllRodzajePlatnosci());
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
                return new BaseCommand(() => showAllTowaryGrupy());
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
                return new BaseCommand(() => showAllTowaryJednMiary());
            }
        }
        public ICommand NowaJednMiaryTowarowCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaJednMiaryTowarowViewModel()));
            }
        }
        public ICommand TowaryStawkiVatCommand
        {
            get
            {
                return new BaseCommand(() => showAllTowaryStawkiVat());
            }
        }
        public ICommand NowaStawkaVatTowarowCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaStawkaVatTowarowViewModel()));
            }
        }
        public ICommand TowaryTypyCommand
        {
            get
            {
                return new BaseCommand(() => showAllTowaryTypy());
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
                return new BaseCommand(() => showAllUmowyRodzaje());
            }
        }
        public ICommand NowyRodzajUmowyCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyRodzajUmowyViewModel()));
            }
        }
        #endregion

        #region Przyciski w menu z lewej strony
        private ReadOnlyCollection<CommandViewModel> _Commands;//to jest kolekcja komend w emnu lewym
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get 
            { 
                if(_Commands == null)//sprawdzam czy przyciski z lewej strony menu nie zostały zainicjalizowane
                {
                    List<CommandViewModel> cmds = this.CreateCommands();//tworzę listę przyciskow za pomocą funkcji CreateCommands
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);//tę listę przypisuje do ReadOnlyCollection (bo readOnlyCollection można tylko tworzyć, nie można do niej dodawać)
                }
                return _Commands; 
            }   
        }
        private List<CommandViewModel> CreateCommands()//tu decydujemy jakie przyciski są w lewym menu
        {
            return new List<CommandViewModel>
            {
                //new CommandViewModel("Towar",new BaseCommand(()=>createView(new NowyTowarViewModel())), "pack://application:,,,/Views/Content/Icons/package-variant-closed-plus-white.png",
                //    "pack://application:,,,/Views/Content/Icons/package-variant-closed-plus.png"),
                //new CommandViewModel("Faktura",new BaseCommand(()=>createView(new NowaFakturaViewModel())), "pack://application:,,,/Views/Content/Icons/file-document-plus-outline-white.png",
                //    "pack://application:,,,/Views/Content/Icons/file-document-plus-outline.png"),
                //new CommandViewModel("Przyjęcie zewnętrzne",new BaseCommand(()=>createView(new PrzyjecieZewnetrzneViewModel())), "pack://application:,,,/Views/Content/Icons/pz-white.png",
                //    "pack://application:,,,/Views/Content/Icons/pz.png"),
                //new CommandViewModel("Wydanie zewnętrzne",new BaseCommand(()=>createView(new WydanieZewnetrzneViewModel())), "pack://application:,,,/Views/Content/Icons/wz-white.png",
                //    "pack://application:,,,/Views/Content/Icons/wz.png"),
                //new CommandViewModel("Rejestr VAT zakupu",new BaseCommand(()=>createView(new RejestrVATViewModel())), "pack://application:,,,/Views/Content/Icons/vat-white.png",
                //    "pack://application:,,,/Views/Content/Icons/vat.png"),
                //new CommandViewModel("Kontrahent",new BaseCommand(()=>createView(new NowyKontrahentViewModel())), "pack://application:,,,/Views/Content/Icons/account-plus-white.png",
                //    "pack://application:,,,/Views/Content/Icons/account-plus.png"),
                //new CommandViewModel("Ewidencja płatności",new BaseCommand(()=>createView(new EwidencjaPlatnosciViewModel())), "pack://application:,,,/Views/Content/Icons/receipt_FILL0_wght400_GRAD0_opsz48-white.png",
                //    "pack://application:,,,/Views/Content/Icons/receipt_FILL0_wght400_GRAD0_opsz48.png"),
                new CommandViewModel("Pojazdy",new BaseCommand(()=>showAllPojazdy()), "pack://application:,,,/Views/Content/Icons/car-white.png","pack://application:,,,/Views/Content/Icons/car.png"),
                new CommandViewModel("Nowy pojazd",new BaseCommand(()=>createView(new NowyPojazdViewModel())),"",""),
                new CommandViewModel("Kategorie faktur",new BaseCommand(()=>showAllFakturyKategorie()), "",""),
                new CommandViewModel("Nowa kategoria fakt.",new BaseCommand(()=>createView(new NowaKategoriaFakturyViewModel())),"",""),
                new CommandViewModel("Rodzaje kontrahentów",new BaseCommand(()=>showAllKontrahenciRodzaje()), "",""),
                new CommandViewModel("Nowa rodzaj kontr.",new BaseCommand(()=>createView(new NowyRodzajKontrahentaViewModel())),"",""),
                new CommandViewModel("Typy magazynów", new BaseCommand(()=>showAllMagazynyTypy()),"",""),
                new CommandViewModel("Nowy typ magazynu",new BaseCommand(()=>createView(new NowyTypMagazynuViewModel())),"",""),
                new CommandViewModel("Rodzaje ceny", new BaseCommand(()=>showAllRodzajeCeny()),"",""),
                new CommandViewModel("Nowy rodzaj ceny",new BaseCommand(()=>createView(new NowyRodzajCenyViewModel())),"",""),
                new CommandViewModel("Rodzaje platności", new BaseCommand(()=>showAllRodzajePlatnosci()),"",""),
                new CommandViewModel("Nowy rodzaj płatności",new BaseCommand(()=>createView(new NowyRodzajPlatnosciViewModel())),"",""),
                new CommandViewModel("Grupy towarów", new BaseCommand(()=>showAllTowaryGrupy()),"",""),
                new CommandViewModel("Nowa grupa towarów",new BaseCommand(()=>createView(new NowaGrupaTowarowViewModel())),"",""),
                new CommandViewModel("Jednostki miary", new BaseCommand(()=>showAllTowaryJednMiary()),"",""),
                new CommandViewModel("Nowa jedn. miary",new BaseCommand(()=>createView(new NowaJednMiaryTowarowViewModel())),"",""),
                new CommandViewModel("Stawki VAT", new BaseCommand(()=>showAllTowaryStawkiVat()),"",""),
                new CommandViewModel("Nowa stawka VAT",new BaseCommand(()=>createView(new NowaStawkaVatTowarowViewModel())),"",""),
                new CommandViewModel("Typy towarów", new BaseCommand(()=>showAllTowaryTypy()),"",""),
                new CommandViewModel("Nowy typ towarów",new BaseCommand(()=>createView(new NowyTypTowarowViewModel())),"",""),
                new CommandViewModel("Rodzaje umów", new BaseCommand(()=>showAllUmowyRodzaje()),"",""),
                new CommandViewModel("Nowy rodzaj umowy",new BaseCommand(()=>createView(new NowyRodzajUmowyViewModel())),"",""),

            };
        }
        #endregion

        #region Zakładki
        private ObservableCollection<WorkspaceViewModel> _Workspaces; //to jest kolekcja zakładek
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get 
            {
                if(_Workspaces == null)
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
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }
        #endregion

        #region Funkcje pomocnicze
        private void createView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            this.setActiveWorkspace(workspace);
        }
        private void showAllPojazdy()
        {
            PojazdyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is PojazdyViewModel) as PojazdyViewModel;
            if (workspace == null)
            {
                workspace = new PojazdyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllFakturyKategorie()
        {
            FakturyKategorieViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is FakturyKategorieViewModel) as FakturyKategorieViewModel;
            if (workspace == null)
            {
                workspace = new FakturyKategorieViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllKontrahenciRodzaje()
        {
            KontrahenciRodzajeViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is KontrahenciRodzajeViewModel) as KontrahenciRodzajeViewModel;
            if (workspace == null)
            {
                workspace = new KontrahenciRodzajeViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllMagazynyTypy()
        {
            MagazynyTypyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is MagazynyTypyViewModel) as MagazynyTypyViewModel;
            if (workspace == null)
            {
                workspace = new MagazynyTypyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
       
        private void showAllRodzajeCeny()
        {
            RodzajeCenyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is RodzajeCenyViewModel) as RodzajeCenyViewModel;
            if (workspace == null)
            {
                workspace = new RodzajeCenyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        
       
        private void showAllRodzajePlatnosci()
        {
            RodzajePlatnosciViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is RodzajePlatnosciViewModel) as RodzajePlatnosciViewModel;
            if (workspace == null)
            {
                workspace = new RodzajePlatnosciViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllTowaryGrupy()
        {
            TowaryGrupyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is TowaryGrupyViewModel) as TowaryGrupyViewModel;
            if (workspace == null)
            {
                workspace = new TowaryGrupyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllTowaryJednMiary()
        {
            TowaryJednMiaryViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is TowaryJednMiaryViewModel) as TowaryJednMiaryViewModel;
            if (workspace == null)
            {
                workspace = new TowaryJednMiaryViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllTowaryStawkiVat()
        {
            TowaryStawkiVatViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is TowaryStawkiVatViewModel) as TowaryStawkiVatViewModel;
            if (workspace == null)
            {
                workspace = new TowaryStawkiVatViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllTowaryTypy()
        {
            TowaryTypyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is TowaryTypyViewModel) as TowaryTypyViewModel;
            if (workspace == null)
            {
                workspace = new TowaryTypyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllUmowyRodzaje()
        {
            UmowyRodzajeViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is UmowyRodzajeViewModel) as UmowyRodzajeViewModel;
            if (workspace == null)
            {
                workspace = new UmowyRodzajeViewModel();
                this.Workspaces.Add(workspace);
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


        #endregion



    }
}
