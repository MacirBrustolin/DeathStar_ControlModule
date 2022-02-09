using DeathStar_ControlModule.Controllers;
using DeathStar_ControlModule.Data;
using DeathStar_ControlModule.Models;

namespace DeathStar_ControlModule.Services
{
    public class SyncAPIService
    {
        private const string URL_PLANETAS = "http://swapi.dev/api/planets/";
        private const string URL_NAVES = "http://swapi.dev/api/starships/";
        private const string URL_PILOTOS = "http://swapi.dev/api/people/";

        public Task Sincronizar()
        {
            var tasks = new List<Task>();

            tasks.Add(SincronizarPlanetas());
            tasks.Add(SincronizarNaves());

            Task.WhenAll(tasks);

            return SincronizarPilotos();
        }

        private async Task SincronizarPlanetas()
        {
            var httpClient = new HttpClient();
            var lista = new List<PlanetaViewModel>();
            APIResultsModel<PlanetaViewModel> resultadoApi = null;

            do
            {
                resultadoApi = await httpClient.GetFromJsonAsync<APIResultsModel<PlanetaViewModel>>(resultadoApi?.Next ?? URL_PLANETAS);
                lista.AddRange(resultadoApi.Results);
            } while (resultadoApi.Next != null);

            var planetas = lista.Select(item => new Planeta
            {
                PlanetaId = item.IdPlaneta,
                Nome = item.Name,
                Clima = item.Climate,
                Diametro = item.Diametro,
                Orbita = item.Orbita,
                Rotacao = item.Rotacao,
                Populacao = item.Populacao
            }).ToList();

            //using (var dao = new PlanetaDao())
            //    await dao.InserirPlanetas(planetas);
        }

        public async Task SincronizarNaves()
        {
            var httpClient = new HttpClient();
            var lista = new List<NaveViewModel>();
            APIResultsModel<NaveViewModel> resultadoApi = null;

            do
            {
                resultadoApi = await httpClient.GetFromJsonAsync<APIResultsModel<NaveViewModel>>(resultadoApi?.Next ?? URL_NAVES);
                lista.AddRange(resultadoApi.Results);
            } while (resultadoApi.Next != null);

            var naves = lista.Select(item => new Nave
            {
                NaveId = item.IdNave,
                Nome = item.Name,
                Carga = item.Carga,
                Classe = item.Starship_Class,
                Modelo = item.Model,
                Passageiros = item.Passageiros
            }).ToList();

            //using (var controller = new NavesController())
            //    await controller.Create(naves);
        }

        private async Task SincronizarPilotos()
        {
            var httpClient = new HttpClient();
            var lista = new List<PilotoViewModel>();
            APIResultsModel<PilotoViewModel> resultadoApi = null;

            do
            {
                resultadoApi = await httpClient.GetFromJsonAsync<APIResultsModel<PilotoViewModel>>(resultadoApi?.Next ?? URL_PILOTOS);
                lista.AddRange(resultadoApi.Results.Where(p => p.Starships.Any()).ToList());
            } while (resultadoApi.Next != null);

            var pilotos = lista.Select(item => new Piloto
            {
                PilotoId = item.IdPiloto,
                Nome = item.Name,
                AnoNascimento = item.Birth_Year,
                PlanetasId = item.IdPlaneta,
                Naves = item.IdNaves.Select(idNave => new Nave
                {
                    NaveId = int.Parse(idNave)
                }).ToList()
            }).ToList();

            //using (var pilotoDao = new PilotoDao())
            //{
            //    await pilotoDao.InserirPilotos(pilotos);
            //    await pilotoDao.InserirPilotosNaves(pilotos);
            //}
        }
    }
}
