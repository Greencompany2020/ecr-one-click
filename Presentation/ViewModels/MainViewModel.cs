using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using EcrOneClick.Infrastructure.Abstract;
using EcrOneClick.Presentation.Abstract;

namespace EcrOneClick.Presentation.ViewModels;

public partial class MainViewModel : ObservableObject, IBaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<DockerImage> _images;

    private readonly IDockerService _dockerService;
    
    public MainViewModel(IDockerService dockerService)
    {
        _dockerService = dockerService;
    }

    public async void LoadImages()
    {
        var images = await _dockerService.GetImages();
        
        Images = new ObservableCollection<DockerImage>(images);
    }
}