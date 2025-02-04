using Newtonsoft.Json;
using SARecuperacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARecuperacion.Services
{
    public class SAService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://dragonball-api.com/api/";

        public SAService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Character>> GetAllCharactersAsync()
        {
            var allCharacters = new List<Character>();
            string nextPageUrl = $"{BaseUrl}characters?limit=10"; // URL de la primera página

            while (!string.IsNullOrEmpty(nextPageUrl))
            {
                var response = await _httpClient.GetStringAsync(nextPageUrl);

                var characterResponse = JsonConvert.DeserializeObject<CharacterResponse>(response);

                // Añadimos los personajes de esta página a la lista
                if (characterResponse?.Items != null)
                {
                    allCharacters.AddRange(characterResponse.Items);
                }

                // Si hay una página siguiente, obtenemos la URL de la siguiente página
                nextPageUrl = characterResponse?.Links?.Next;
            }

            return allCharacters;
        }
        public async Task<List<Character>> GetCharactersByNameAsync(string name)
        {
            var url = $"{BaseUrl}characters?name={name}";
            var response = await _httpClient.GetStringAsync(url);

            var characterResponse = JsonConvert.DeserializeObject<CharacterResponse>(response);

            // Retornamos los personajes filtrados por nombre
            return characterResponse?.Items ?? new List<Character>();
        }
    }
}
