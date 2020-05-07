using footballers.Model;
using footballers.ViewModel.BaseClass;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace footballers.ViewModel
{
    internal class PlayerEdition: ViewModelBase
    {
        private string path = @"database.json";

        private Player selectedPlayer = null;
        private BindingList<Player> players = new BindingList<Player>();

        private string firstName = null;
        private string lastName = null;
        private uint age = 30;
        private uint weight = 75;

        #region Properties

        public Player SelectedPlayer
        {
            get => selectedPlayer;
            set
            {
                selectedPlayer = value;
                onPropertyChanged(nameof(SelectedPlayer));
                if (ChangePlayer.CanExecute(null)) ChangePlayer.Execute(null);
            }
        }
        public BindingList<Player> Players
        {
            get => players;
            set
            {
                players = value;
                onPropertyChanged(nameof(Players));
            }
        }
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                onPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                onPropertyChanged(nameof(LastName));
            }
        }
        public uint Age
        {
            get => age;
            set
            {
                age = value;
                onPropertyChanged(nameof(Age));
            }
        }
        public uint Weight
        {
            get => weight;
            set
            {
                weight = value;
                onPropertyChanged(nameof(Weight));
            }
        }

        #endregion

        #region Commands

        private bool FieldsNotNull => !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && Age > 0 && Weight > 0;

        private ICommand addPlayer;
        private ICommand deletePlayer;
        private ICommand editPlayer;
        private ICommand reset;
        private ICommand changePlayer;

        private ICommand load;
        private ICommand save;

        public ICommand AddPlayer
        {
            get
            {
                if (addPlayer is null)
                {
                    addPlayer = new RelayCommand(execute =>
                    {
                        var player = new Player(FirstName, LastName, (uint)Age, (uint)Weight);
                        if (!Players.Contains(player))
                        {
                            Players.Add(player);
                            onPropertyChanged(nameof(Players));
                        }
                    }, canExecute => FieldsNotNull);
                }
                return addPlayer;
            }
        }
        public ICommand DeletePlayer
        {
            get
            {
                if (deletePlayer == null)
                {
                    deletePlayer = new RelayCommand(execute =>
                    {
                        var player = new Player(FirstName, LastName, (uint)Age, (uint)Weight);
                        if (Players.Contains(player))
                        {
                            Players.Remove(player);
                            onPropertyChanged(nameof(Players));
                        }
                    }, canExecute => FieldsNotNull && SelectedPlayer != null
                    );
                }
                return deletePlayer;
            }
        }
        public ICommand EditPlayer
        {
            get
            {
                if (editPlayer is null)
                {
                    editPlayer = new RelayCommand(execute =>
                    {
                        var player = new Player(FirstName, LastName, (uint)Age, (uint)Weight);
                        if (Players.Contains(SelectedPlayer))
                        {
                            var index = players.IndexOf(selectedPlayer);
                            Players[index].Copy(player);
                            Players.ResetItem(index);
                        }
                    }, canExecute => FieldsNotNull && SelectedPlayer != null);
                }
                return editPlayer;
            }
        }
        public ICommand Reset
        {
            get
            {
                if (reset is null)
                {
                    reset = new RelayCommand(
                        execute =>
                        {
                            FirstName = null;
                            LastName = null;
                            Age = 30;
                            Weight = 75;
                        }, canExecute => true);
                }
                return reset;
            }
        }
        public ICommand ChangePlayer
        {
            get
            {
                if (changePlayer is null)
                {
                    changePlayer = new RelayCommand(
                        execute =>
                        {
                            FirstName = SelectedPlayer.FirstName;
                            LastName = SelectedPlayer.LastName;
                            Age = SelectedPlayer.Age;
                            Weight = SelectedPlayer.Weight;
                        }, canExecute => SelectedPlayer != null);
                }
                return changePlayer;
            }
        }

        public ICommand Load
        {
            get
            {
                if (load is null)
                {
                    load = new RelayCommand(execute =>
                    {
                        try
                        {
                            var jsonPlayers = File.ReadAllText(path);
                            Players = JsonConvert.DeserializeObject<BindingList<Player>>(jsonPlayers);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        onPropertyChanged(nameof(Load));
                        Players.ResetBindings();
                    }, canExecute => File.Exists(path) && (new FileInfo(path).Length > 0));
                }
                return load;
            }
        }
        public ICommand Save
        {
            get
            {
                if (save is null)
                {
                    save = new RelayCommand(execute =>
                    {
                        try
                        {
                            var jsonPlayers = JsonConvert.SerializeObject(Players);
                            File.WriteAllText(path, jsonPlayers);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        onPropertyChanged(nameof(Save));
                    }, canExecute => true);
                }
                return save;
            }
        }

        #endregion

        public PlayerEdition() { }
    }
}
