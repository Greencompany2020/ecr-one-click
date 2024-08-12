using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcrOneClick.DI;
using EcrOneClick.Presentation.ViewModels;

namespace EcrOneClick.Presentation.Views;

public partial class ServicesPage : ContentPage
{
    public ServicesPage()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.GetService<ServicesViewModel>();
    }
}