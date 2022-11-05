using Firma.Helpers;
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
                return new BaseCommand(() => createView(new PojazdyViewModel()));
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
                new CommandViewModel("Towar",new BaseCommand(()=>createView(new NowyTowarViewModel())), "pack://application:,,,/Views/Content/Icons/package-variant-closed-plus-white.png",
                    "pack://application:,,,/Views/Content/Icons/package-variant-closed-plus.png"),
                new CommandViewModel("Faktura",new BaseCommand(()=>createView(new NowaFakturaViewModel())), "pack://application:,,,/Views/Content/Icons/file-document-plus-outline-white.png",
                    "pack://application:,,,/Views/Content/Icons/file-document-plus-outline.png"),
                new CommandViewModel("Przyjęcie zewnętrzne",new BaseCommand(()=>createView(new PrzyjecieZewnetrzneViewModel())), "pack://application:,,,/Views/Content/Icons/pz-white.png",
                    "pack://application:,,,/Views/Content/Icons/pz.png"),
                new CommandViewModel("Wydanie zewnętrzne",new BaseCommand(()=>createView(new WydanieZewnetrzneViewModel())), "pack://application:,,,/Views/Content/Icons/wz-white.png",
                    "pack://application:,,,/Views/Content/Icons/wz.png"),
                new CommandViewModel("Rejestr VAT zakupu",new BaseCommand(()=>createView(new RejestrVATViewModel())), "pack://application:,,,/Views/Content/Icons/vat-white.png",
                    "pack://application:,,,/Views/Content/Icons/vat.png"),
                new CommandViewModel("Kontrahent",new BaseCommand(()=>createView(new NowyKontrahentViewModel())), "pack://application:,,,/Views/Content/Icons/account-plus-white.png",
                    "pack://application:,,,/Views/Content/Icons/account-plus.png"),
                new CommandViewModel("Ewidencja płatności",new BaseCommand(()=>createView(new EwidencjaPlatnosciViewModel())), "pack://application:,,,/Views/Content/Icons/receipt_FILL0_wght400_GRAD0_opsz48-white.png",
                    "pack://application:,,,/Views/Content/Icons/receipt_FILL0_wght400_GRAD0_opsz48.png"),
                new CommandViewModel("Pojazdy",new BaseCommand(()=>createView(new PojazdyViewModel())), "pack://application:,,,/Views/Content/Icons/car-white.png",
                    "pack://application:,,,/Views/Content/Icons/car.png"),

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
      
        //to jest funkcja, która otwiera nową zakładke Towar
        //za każdym tworzy nową NOWĄ zakładkę do dodawania towaru
        //private void createTowar()
        //{
        //    //tworzy zakładke NowyTowar (VM)
        //    NowyTowarViewModel workspace=new NowyTowarViewModel();
        //    //dodajmy ją do kolkcji aktywnych zakładek
        //    this.Workspaces.Add(workspace);
        //    this.setActiveWorkspace(workspace);
        //}
        //to jest funkcja, która otwiera zakładke ze wszystkimi towarami
        //za każdym razem sprawdza czy zakladka z towarami jest juz otwarta, jak jest to ja aktywuje, ajk nie ma to tworzy 
       
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
