namespace Dima.Web.Layout;

public class MainLayoutComponentBase : LayoutComponentBase
{
    #region Properties
    protected bool IsDarkMode { get; set; }
    #endregion

    #region Refs
    protected MudThemeProvider MudThemeProviderRef { get; set; } = null!;
    #endregion

    #region LifeCycle
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            IsDarkMode = await MudThemeProviderRef.GetSystemPreference();
            await MudThemeProviderRef.WatchSystemPreference(OnSystemPreferenceChanged);
            StateHasChanged();
        }
    }
    #endregion

    #region Methods
    protected Task OnSystemPreferenceChanged(bool newValue)
    {
        IsDarkMode = newValue;
        StateHasChanged();

        return Task.CompletedTask;
    }
    #endregion
}