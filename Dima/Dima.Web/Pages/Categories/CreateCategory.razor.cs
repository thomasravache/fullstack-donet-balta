using Dima.Core.Requests.Categories;
using Dima.Web.Handlers;

namespace Dima.Web.Pages.Categories;

public partial class CreateCategoryComponentBase : ComponentBase
{
    #region Injections
    [Inject] private CategoryHandler _categoryHandler { get; init; } = null!;
    [Inject] private NavigationManager _navigationManager { get; init; } = null!;
    [Inject] private ISnackbar _snackbar { get; init; } = null!;
    #endregion

    #region Props
    protected bool IsBusy { get; set; } = false;
    protected CreateCategoryRequest InputModel { get; set; } = new();
    #endregion

    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await _categoryHandler.CreateAsync(InputModel);

            if (result.IsSuccess)
            {
                _snackbar.Add(result?.PrincipalMessage ?? "Categoria criada com sucesso!", Severity.Success);
                _navigationManager.NavigateTo(PageRoutes.Categorias);
            }
            else
                _snackbar.Add(result?.PrincipalMessage ?? string.Empty, Severity.Error);
        }
        catch (Exception ex)
        {
            _snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}