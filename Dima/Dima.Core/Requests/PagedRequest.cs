using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests;

public abstract class PagedRequest : Request
{
    [Range(1, int.MaxValue, ErrorMessage = "Não é permitido valores inferiores a 1 para número de página")]
    public int PageNumber { get; set; } = Configuration.DefaultPageNumber;

    [Range(5, 25, ErrorMessage = "Tamanho de página permitido: de 5 a 25 itens por página")]
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
}