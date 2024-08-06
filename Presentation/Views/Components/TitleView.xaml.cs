namespace EcrOneClick.Presentation.Views.Components;

public partial class TitleView : ContentView
{
    public static readonly BindableProperty TitleViewTitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(TitleView), string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleView.TitleViewTitleProperty);
        set => SetValue(TitleView.TitleViewTitleProperty, value);
    }
    
    public TitleView()
    {
        InitializeComponent();
    }
}