using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SQLite;
using System.Collections.ObjectModel;
using System.Windows;
using TaskPart1WPF.Models;

namespace TaskPart1WPF.ViewModels
{
    /// <summary>
    /// This is the main view model which contains the actions and logic in that is required.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private SQLiteConnection _connection;
        private ObservableCollection<Package> _packages;

        public ObservableCollection<Package> Packages
        {
            get => _packages;
            set => Set(ref _packages, value);
        }

        public RelayCommand AddPackageCommand { get; }
        public RelayCommand SaveChangesCommand { get; }
        public RelayCommand CancelChangesCommand { get; }

        public MainViewModel()
        {
            _connection = new SQLiteConnection("packagesdb.sqlite");
            _connection.CreateTable<Package>();
            Packages = new ObservableCollection<Package>(_connection.Table<Package>());

            AddPackageCommand = new RelayCommand(AddPackage);
            SaveChangesCommand = new RelayCommand(SaveChanges);
            CancelChangesCommand = new RelayCommand(CancelChanges);
        }

        /// <summary>
        /// Responsible to add package row in Data Grid
        /// </summary>
        private void AddPackage()
        {
            Packages.Add(new Package());
        }

        /// <summary>
        /// Save all packages in database from the UI
        /// </summary>
        private void SaveChanges()
        {
            foreach (var package in Packages)
            {
                if (package.Id == 0)
                {
                    _connection.Insert(package);
                }
                else
                {
                    _connection.Update(package);
                }
            }
            MessageBox.Show("Changes saved successfully.");
            LoadPackagesFromDB();
        }

        /// <summary>
        /// Rest the UI packages as in database
        /// </summary>
        private void CancelChanges()
        {
            LoadPackagesFromDB();
        }

        /// <summary>
        /// Load packages from DB
        /// </summary>
        private void LoadPackagesFromDB()
        {
            Packages.Clear();
            var packages = _connection.Table<Package>().ToList();
            foreach (var package in packages)
            {
                Packages.Add(package);
            }
        }
    }
}