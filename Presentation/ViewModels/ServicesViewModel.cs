using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using EcrOneClick.Infrastructure.Abstract;
using EcrOneClick.Infrastructure.Data;
using EcrOneClick.Presentation.Abstract;
using EcrOneClick.Presentation.Models;
using EcrOneClick.UseCases.Abstract;
using FluentResults;

namespace EcrOneClick.Presentation.ViewModels;

public partial class ServicesViewModel : ObservableObject, IBaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<DockerServiceUiModel> _services;
    public ICommand ButtonClickedCommand { get; }
    
    // Propiedades internas
    
    private readonly IDockerService _dockerService;
    private readonly IGetSwarmMode _getSwarmMode;
    private readonly IDopplerService _dopplerService;
    private bool _isInSwarmMode;
    
    public ServicesViewModel(
        IDockerService dockerService,
        IDopplerService dopplerService,
        IGetSwarmMode getSwarmMode)
    {
        Services = [];
        ButtonClickedCommand = new Command(DisplaySelectedItem);
        _dockerService = dockerService;
        _getSwarmMode = getSwarmMode;
        _isInSwarmMode = false;
        _dopplerService = dopplerService;
    }

    // TODO: Delegar llamada de DisplayAlert al Page, en lugar del ViewModel.
    public async void LoadServices()
    {
        var swarmMode = _getSwarmMode.Execute();

        if (swarmMode.IsFailed)
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo obtener configuracion de docker", "OK");
            return;
        }
        
        if (swarmMode.Value)
        {
            _isInSwarmMode = true;
            var services = await _dockerService.GetServices();

            if (services.IsFailed)
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo obtener la informacion de servicios", "OK");
            }
            else
            {
                var list = new List<DockerServiceUiModel>();
                
                services.Value.ForEach(srv =>
                {
                    var dockerServiceUiModel = new DockerServiceUiModel()
                    {
                        ServiceName = srv.Name,
                        Status = srv.Status
                    };
                    list.Add(dockerServiceUiModel);
                });
                
                Services = new ObservableCollection<DockerServiceUiModel>(list);
            }
        }
        else
        {
            _isInSwarmMode = false;
            var containers = await _dockerService.GetContainers();

            if (containers.IsFailed)
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo obtener la informacion de contenedores", "OK");
            }
            else
            {
                var list = new List<DockerServiceUiModel>();
                
                containers.Value.ForEach(srv =>
                {
                    var dockerServiceUiModel = new DockerServiceUiModel()
                    {
                        ServiceName = srv.Name,
                        Status = srv.Status
                    };
                    list.Add(dockerServiceUiModel);
                });
                
                Services = new ObservableCollection<DockerServiceUiModel>(list);
            }
        }
    }
    
    public async Task<Result<string>> InitServices()
    {
        if (_isInSwarmMode)
        {
            var result = await _dockerService.CreateTestService();

            return result.IsFailed ? Result.Fail("No se pudo crear el servicio") : Result.Ok("Servicio creado");
        }
        else
        {
            var result = await _dockerService.CreateTestContainer();
            
            return result.IsFailed ? Result.Fail("No se pudo crear el contenedor") : Result.Ok("Contenedor creado");
        }
    }

    public async Task<Result<DopplerSecretsResponse?>> FetchSecrets()
    {
        var secrets = await _dopplerService.GetDopplerSecretsAsync(
            "api-ecr-backoffice",
            "dev",
            ""); // Agregar token durante pruebas

        return secrets;
    }
    
    private async void DisplaySelectedItem(object obj)
    {
        var selectedItem = obj as DockerServiceUiModel;
        await Shell.Current.DisplayAlert("Alert", selectedItem?.ServiceName, "OK");
    }
}