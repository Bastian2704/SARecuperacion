using SARecuperacion.Models;
using SARecuperacion.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SARecuperacion.ViewModels
{
    public class SACharacterViewModel : BindableObject
    {
        private readonly SAService _apiService;
        private ObservableCollection<Character> _characters;
        private ObservableCollection<Planet> _planets;
        private bool _isLoading;
        private Planet _selectedPlanet;
        private string _searchName;

        public ObservableCollection<Character> Characters
        {
            get => _characters;
            set
            {
                _characters = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Planet> Planets
        {
            get => _planets;
            set
            {
                _planets = value;
                OnPropertyChanged();
            }
        }

        public Planet SelectedPlanet
        {
            get => _selectedPlanet;
            set
            {
                _selectedPlanet = value;
                OnPropertyChanged();
                if (_selectedPlanet != null)
                {
                    // Al seleccionar un planeta, cargar los personajes de ese planeta
                    LoadCharactersByPlanetAsync();
                }
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string SearchName
        {
            get => _searchName;
            set
            {
                _searchName = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand LoadCharactersCommand { get; }
        public ICommand LoadPlanetsCommand { get; }

        public SACharacterViewModel()
        {
            _apiService = new SAService();
            LoadCharactersCommand = new Command(async () => await LoadCharactersAsync());
            SearchCommand = new Command(async () => await SearchCharactersAsync());
            LoadPlanetsCommand = new Command(async () => await LoadPlanetsAsync());
        }

        private async Task LoadPlanetsAsync()
        {
            IsLoading = true;
            var planets = await _apiService.GetAllPlanetsAsync();
            Planets = new ObservableCollection<Planet>(planets);
            IsLoading = false;
        }

        private async Task LoadCharactersByPlanetAsync()
        {
            if (SelectedPlanet == null)
            {
                return;
            }

            IsLoading = true;

            var characters = await _apiService.GetCharactersByPlanetAsync(SelectedPlanet.Id);
            Characters = new ObservableCollection<Character>(characters);

            IsLoading = false;
        }

        private async Task SearchCharactersAsync()
        {
            IsLoading = true;

            var characters = await _apiService.GetCharactersByNameAsync(SearchName);
            Characters = new ObservableCollection<Character>(characters);

            IsLoading = false;
        }

        private async Task LoadCharactersAsync()
        {
            var characters = await _apiService.GetAllCharactersAsync();
            Characters = new ObservableCollection<Character>(characters);
        }
    }
}