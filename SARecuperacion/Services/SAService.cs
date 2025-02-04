using Newtonsoft.Json;
using SARecuperacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            var response2 = await _httpClient.GetStringAsync(url);

            var characterResponse = JsonConvert.DeserializeObject<Character>(response2);

            // Retornamos los personajes filtrados por nombre
            return new List<Character>();
        }
        public async Task<List<Planet>> GetAllPlanetsAsync()
        {
            var allPlanets = new List<Planet>();
            string nextPageUrl = $"{BaseUrl}planets?limit=10"; // URL de la primera página

            while (!string.IsNullOrEmpty(nextPageUrl))
            {
                var response = await _httpClient.GetStringAsync(nextPageUrl);
                var planetResponse = JsonConvert.DeserializeObject<PlanetResponse>(response);

                if (planetResponse?.Items != null)
                {
                    allPlanets.AddRange(planetResponse.Items);
                }

                nextPageUrl = planetResponse?.Links?.Next;
            }

            // Verifica si los planetas se están cargando
            Console.WriteLine($"Total de planetas obtenidos: {allPlanets.Count}");

            return allPlanets;
        }

        public async Task<List<Character>> GetCharactersByPlanetAsync(int planetId)
        {
            var url = $"{BaseUrl}planets/{planetId}";
            var response = await _httpClient.GetStringAsync(url);
            var planetDetailsResponse = JsonConvert.DeserializeObject<PlanetDetailsResponse>(response);

            return planetDetailsResponse?.Characters ?? new List<Character>();
        }
    }
}
