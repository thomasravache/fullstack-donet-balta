using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core.Responses.Categories;
using Dima.Web.Security;

namespace Dima.Web.Pages.Categories;

public partial class ListCategoryComponentBase : ComponentBase
{
    #region Injections
    [Inject] private ISnackbar _snackBar { get; init; } = null!;
    [Inject] private ICategoryHandler _categoryHandler { get; init; } = null!;
    [Inject] private ICookieAuthenticationStateProvider _authStateProvider { get; init; } = null!;
    #endregion

    #region Props
    protected bool IsBusy { get; set; } = false;
    protected PagedResult<CategoryResponse> PagedCategories { get; set; } = PagedResult<CategoryResponse>.EmptyResult();
    #endregion

    #region LifeCycle
    protected override async Task OnInitializedAsync()
    {
        await FillCategories();
    }

    private async Task FillCategories()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllCategoriesRequest();
            var result = await _categoryHandler.GetAllAsync(request);

            if (result.IsSuccess)
                PagedCategories = result.Data ?? PagedResult<CategoryResponse>.EmptyResult();
            else
                _snackBar.Add(result?.PrincipalMessage ?? "Erro ao obter categorias", Severity.Error);
        }
        catch (Exception ex)
        {
            _snackBar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}