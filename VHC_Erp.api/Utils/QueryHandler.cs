using Microsoft.EntityFrameworkCore;

namespace VHC_Erp.api.Utils;

public static class QueryHandler
{
    
    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> objToFilter, string columnName, string propValue)
    {
        if (int.TryParse(propValue, out var intProp))
            objToFilter = objToFilter.Where(item => EF.Property<int>(item!, columnName) == intProp);
        else
        {
            objToFilter = objToFilter.Where(item => EF.Property<string>(item!, columnName) == propValue);
        }
        return objToFilter;
    }

    public static IQueryable<T> ApplySort<T>(this IQueryable<T> objToSort, bool isDecending, string? column)
    {
        if (!string.IsNullOrEmpty(column))
            return isDecending
                ? objToSort.OrderByDescending(i => EF.Property<string>(i!, column))
                : objToSort.OrderBy(i => EF.Property<string>(i!, column));
        return isDecending ? objToSort.OrderBy(i => EF.Property<string>(i!, "Id")) : objToSort.OrderByDescending(i => EF.Property<string>(i!, "Id"));
    }

    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> objToPage, int pageNumber, int pageSize)
        => objToPage.Skip((pageNumber - 1) * pageSize).Take(pageSize);
}