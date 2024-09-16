using MudBlazor;

namespace VHC_Erp.frontend.Utils.GlobalDialogErrorHandler;

public interface IGenerateDialog
{
    void ShowErrorMessage(string message);
    void ShowSuccessMessage<T>(T value);
}

public class GenerateDialog(IDialogService dialogService) : IGenerateDialog
{
    
    public void ShowErrorMessage(string message)
    {
        var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true};
        var parameters = new DialogParameters
        {
            ["Value"] = message,
            ["Title"] = "Error"
        };
        dialogService.Show<Dialog>("Erro", parameters, options);
    }

    public void ShowSuccessMessage<T>(T value)
    {
        var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true};
        var parameters = new DialogParameters
        {
            ["Value"] = value,
            ["Title"] = "Success"
        };
        dialogService.Show<Dialog>("Sucesso", parameters, options);
    }
}