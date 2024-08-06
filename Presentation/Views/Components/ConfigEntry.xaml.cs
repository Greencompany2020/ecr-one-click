namespace EcrOneClick.Presentation.Views.Components;

public partial class ConfigEntry : ContentView
{
    
    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
        nameof(LabelText), typeof(string), typeof(ConfigEntry), string.Empty);

    public static readonly BindableProperty EntryPlaceholderProperty = BindableProperty.Create(
        nameof(EntryPlaceholder), typeof(string), typeof(ConfigEntry), string.Empty);

    public static readonly BindableProperty IsSecretProperty = BindableProperty.Create(
        nameof(IsSecret), typeof(bool), typeof(ConfigEntry), false);
    
    /// <summary>
    /// Texto de nombre del campo
    /// </summary>
    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    /// <summary>
    /// Placeholder para input/entry de configuracion
    /// </summary>
    public string EntryPlaceholder
    {
        get => (string)GetValue(EntryPlaceholderProperty);
        set => SetValue(EntryPlaceholderProperty, value);
    }

    /// <summary>
    /// Si el campo es de informacion sensible. En true
    /// oculta el valor. Default es false.
    /// </summary>
    public bool IsSecret
    {
        get => (bool)GetValue(IsSecretProperty);
        set => SetValue(IsSecretProperty, value);
    }
    
    public ConfigEntry()
    {
        InitializeComponent();
    }
}