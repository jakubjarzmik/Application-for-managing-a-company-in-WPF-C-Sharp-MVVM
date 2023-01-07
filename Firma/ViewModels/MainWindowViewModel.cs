﻿using Firma.Helpers;
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
        public ICommand FakturyRodzajeCommand
        {
            get
            {
                return new BaseCommand(() => showAllFakturyRodzaje());
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
        public ICommand UmowyStanowiskaCommand
        {
            get
            {
                return new BaseCommand(() => showAllUmowyStanowiska());
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
                return new BaseCommand(() => showAllKontakty());
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
                return new BaseCommand(() => showAllKraje());
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
                return new BaseCommand(() => showAllFaktury());
            }
        }
        public ICommand AdresyCommand
        {
            get
            {
                return new BaseCommand(() => showAllAdresy());
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
                return new BaseCommand(() => showAllKontrahenci());
            }
        }
        public ICommand MagazynyCommand
        {
            get
            {
                return new BaseCommand(() => showAllMagazyny());
            }
        }
        public ICommand PozycjePrzyjeciaZewnetrznegoCommand
        {
            get
            {
                return new BaseCommand(() => showAllPozycjePrzyjeciaZewnetrznego());
            }
        }
        public ICommand PozycjeWydaniaZewnetrznegoCommand
        {
            get
            {
                return new BaseCommand(() => showAllPozycjeWydaniaZewnetrznego());
            }
        }
        public ICommand PracownicyCommand
        {
            get
            {
                return new BaseCommand(() => showAllPracownicy());
            }
        }
        public ICommand PrzyjeciaZewnetrzneCommand
        {
            get
            {
                return new BaseCommand(() => showAllPrzyjeciaZewnetrzne());
            }
        }
        public ICommand WydaniaZewnetrzneCommand
        {
            get
            {
                return new BaseCommand(() => showAllWydaniaZewnetrzne());
            }
        }
        public ICommand TowaryCommand
        {
            get
            {
                return new BaseCommand(() => showAllTowary());
            }
        }
        public ICommand TowaryStawkiVatCommand
        {
            get
            {
                return new BaseCommand(() => showAllTowaryStawkiVat());
            }
        }
        public ICommand UmowyCommand
        {
            get
            {
                return new BaseCommand(() => showAllUmowy());
            }
        }
        public ICommand ZmianyCenyCommand
        {
            get
            {
                return new BaseCommand(() => showAllZmianyCeny());
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
                
                new CommandViewModel("Kategorie faktur",new BaseCommand(()=>showAllFakturyKategorie()), "",""),
                new CommandViewModel("Nowa kategoria faktur",new BaseCommand(()=>createView(new NowaKategoriaFakturyViewModel())),"",""),
                new CommandViewModel("Rodzaje faktur",new BaseCommand(()=>showAllFakturyRodzaje()), "",""),
                new CommandViewModel("Nowy rodzaj faktur",new BaseCommand(()=>createView(new NowyRodzajFakturyViewModel())),"",""),
                new CommandViewModel("Jednostki miary", new BaseCommand(()=>showAllTowaryJednMiary()),"",""),
                new CommandViewModel("Nowa jedn. miary",new BaseCommand(()=>createView(new NowaJednostkaMiaryViewModel())),"",""),
                new CommandViewModel("Rodzaje kontrahentów",new BaseCommand(()=>showAllKontrahenciRodzaje()), "",""),
                new CommandViewModel("Nowa rodzaj kontr.",new BaseCommand(()=>createView(new NowyRodzajKontrahentaViewModel())),"",""),
                new CommandViewModel("Kraje",new BaseCommand(()=>showAllKraje()), "",""),
                new CommandViewModel("Nowy kraj",new BaseCommand(()=>createView(new NowyKrajViewModel())),"",""),
                new CommandViewModel("Typy magazynów", new BaseCommand(()=>showAllMagazynyTypy()),"",""),
                new CommandViewModel("Nowy typ magazynu",new BaseCommand(()=>createView(new NowyTypMagazynuViewModel())),"",""),
                new CommandViewModel("Rodzaje platności", new BaseCommand(()=>showAllRodzajePlatnosci()),"",""),
                new CommandViewModel("Nowy rodzaj płatności",new BaseCommand(()=>createView(new NowyRodzajPlatnosciViewModel())),"",""),
                new CommandViewModel("Typy towarów", new BaseCommand(()=>showAllTowaryTypy()),"",""),
                new CommandViewModel("Nowy typ towarów",new BaseCommand(()=>createView(new NowyTypTowarowViewModel())),"",""),
                new CommandViewModel("Rodzaje umów", new BaseCommand(()=>showAllUmowyRodzaje()),"",""),
                new CommandViewModel("Nowy rodzaj umowy",new BaseCommand(()=>createView(new NowyRodzajUmowyViewModel())),"",""),
                new CommandViewModel("Stanowiska", new BaseCommand(()=>showAllUmowyStanowiska()),"",""),
                new CommandViewModel("Nowe stanowisko",new BaseCommand(()=>createView(new NoweStanowiskoViewModel())),"",""),
                new CommandViewModel("Kontakty", new BaseCommand(()=>showAllKontakty()),"",""),
                new CommandViewModel("Nowy kontakt",new BaseCommand(()=>createView(new NowyKontaktViewModel())),"",""),
                new CommandViewModel("Faktury", new BaseCommand(()=>showAllFaktury()),"",""),
                new CommandViewModel("Adresy", new BaseCommand(()=>showAllAdresy()),"",""),
                new CommandViewModel("Nowy adres",new BaseCommand(()=>createView(new NowyAdresViewModel())),"",""),
                new CommandViewModel("Kontrahenci", new BaseCommand(()=>showAllKontrahenci()),"",""),
                new CommandViewModel("Magazyny", new BaseCommand(()=>showAllMagazyny()),"",""),
                new CommandViewModel("Pozycje PZ", new BaseCommand(()=>showAllPozycjePrzyjeciaZewnetrznego()),"",""),
                new CommandViewModel("Pozycje WZ", new BaseCommand(()=>showAllPozycjeWydaniaZewnetrznego()),"",""),
                new CommandViewModel("Pracownicy", new BaseCommand(()=>showAllPracownicy()),"",""),
                new CommandViewModel("Przyjecia Zewnetrzne", new BaseCommand(()=>showAllPrzyjeciaZewnetrzne()),"",""),
                new CommandViewModel("Wydania Zewnetrzne", new BaseCommand(()=>showAllWydaniaZewnetrzne()),"",""),
                new CommandViewModel("Towary", new BaseCommand(()=>showAllTowary()),"",""),
                new CommandViewModel("Grupy towarów", new BaseCommand(()=>showAllTowaryGrupy()),"",""),
                new CommandViewModel("Nowa grupa towarów",new BaseCommand(()=>createView(new NowaGrupaTowarowViewModel())),"",""),
                new CommandViewModel("Stawki VAT", new BaseCommand(()=>showAllTowaryStawkiVat()),"",""),
                new CommandViewModel("Umowy", new BaseCommand(()=>showAllUmowy()),"",""),
                new CommandViewModel("Zmiany ceny", new BaseCommand(()=>showAllZmianyCeny()),"",""),
                new CommandViewModel("Średnie wynagrodz.",new BaseCommand(()=>createView(new SrednieWynagrodzenieViewModel())),"",""),
                new CommandViewModel("Raport sprzedaży",new BaseCommand(()=>createView(new RaportSprzedazyViewModel())),"",""),
                
                
                
                

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
        private void showAllFakturyRodzaje()
        {
            FakturyRodzajeViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is FakturyRodzajeViewModel) as FakturyRodzajeViewModel;
            if (workspace == null)
            {
                workspace = new FakturyRodzajeViewModel();
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
            JednostkiMiaryViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is JednostkiMiaryViewModel) as JednostkiMiaryViewModel;
            if (workspace == null)
            {
                workspace = new JednostkiMiaryViewModel();
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
        private void showAllUmowyStanowiska()
        {
            UmowyStanowiskaViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is UmowyStanowiskaViewModel) as UmowyStanowiskaViewModel;
            if (workspace == null)
            {
                workspace = new UmowyStanowiskaViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllKraje()
        {
            KrajeViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is KrajeViewModel) as KrajeViewModel;
            if (workspace == null)
            {
                workspace = new KrajeViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllKontakty()
        {
            KontaktyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is KontaktyViewModel) as KontaktyViewModel;
            if (workspace == null)
            {
                workspace = new KontaktyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllFaktury()
        {
            FakturyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is FakturyViewModel) as FakturyViewModel;
            if (workspace == null)
            {
                workspace = new FakturyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllAdresy()
        {
            AdresyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AdresyViewModel) as AdresyViewModel;
            if (workspace == null)
            {
                workspace = new AdresyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllKontrahenci()
        {
            KontrahenciViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is KontrahenciViewModel) as KontrahenciViewModel;
            if (workspace == null)
            {
                workspace = new KontrahenciViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllMagazyny()
        {
            MagazynyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is MagazynyViewModel) as MagazynyViewModel;
            if (workspace == null)
            {
                workspace = new MagazynyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllPozycjePrzyjeciaZewnetrznego()
        {
            PozycjePrzyjeciaZewnetrznegoViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is PozycjePrzyjeciaZewnetrznegoViewModel) as PozycjePrzyjeciaZewnetrznegoViewModel;
            if (workspace == null)
            {
                workspace = new PozycjePrzyjeciaZewnetrznegoViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllPozycjeWydaniaZewnetrznego()
        {
            PozycjeWydaniaZewnetrznegoViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is PozycjeWydaniaZewnetrznegoViewModel) as PozycjeWydaniaZewnetrznegoViewModel;
            if (workspace == null)
            {
                workspace = new PozycjeWydaniaZewnetrznegoViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllPracownicy()
        {
            PracownicyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is PracownicyViewModel) as PracownicyViewModel;
            if (workspace == null)
            {
                workspace = new PracownicyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllPrzyjeciaZewnetrzne()
        {
            PrzyjeciaZewnetrzneViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is PrzyjeciaZewnetrzneViewModel) as PrzyjeciaZewnetrzneViewModel;
            if (workspace == null)
            {
                workspace = new PrzyjeciaZewnetrzneViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllWydaniaZewnetrzne()
        {
            WydaniaZewnetrzneViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WydaniaZewnetrzneViewModel) as WydaniaZewnetrzneViewModel;
            if (workspace == null)
            {
                workspace = new WydaniaZewnetrzneViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllTowary()
        {
            TowaryViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is TowaryViewModel) as TowaryViewModel;
            if (workspace == null)
            {
                workspace = new TowaryViewModel();
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
        private void showAllUmowy()
        {
            UmowyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is UmowyViewModel) as UmowyViewModel;
            if (workspace == null)
            {
                workspace = new UmowyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllZmianyCeny()
        {
            ZmianyCenyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is ZmianyCenyViewModel) as ZmianyCenyViewModel;
            if (workspace == null)
            {
                workspace = new ZmianyCenyViewModel();
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
