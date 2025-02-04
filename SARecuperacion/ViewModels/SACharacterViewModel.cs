using SARecuperacion.Models;
using SARecuperacion.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SARecuperacion.ViewModels
{
    public class SACharacterViewModel : BindableObject
    {
        private readonly SAService _apiService;
        private ObservableCollection<Character> _characters;
        private bool _isLoading;
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
    


        public ICommand LoadCharactersCommand { get; }
        public ICommand SearchCommand { get; }


        public SACharacterViewModel()
        {
            _apiService = new SAService();
            SearchCommand = new Command(async () => await SearchCharactersAsync());

        }
        private async Task SearchCharactersAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchName))
            {
                // Si no se ingresa ningún nombre, cargar todos los personajes
                await LoadCharactersAsync();
            }
            else
            {
                // Si se ingresa un nombre, cargar los personajes filtrados por nombre
                IsLoading = true;
                var characters = await _apiService.GetCharactersByNameAsync(SearchName);
                Characters = new ObservableCollection<Character>(characters);
                IsLoading = false;
            }
        }

        private async Task LoadCharactersAsync()
        {
            var characters = await _apiService.GetAllCharactersAsync();
            Characters = new ObservableCollection<Character>(characters);
        }
    }
}
