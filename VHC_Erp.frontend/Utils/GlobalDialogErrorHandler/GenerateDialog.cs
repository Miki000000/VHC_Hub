using MudBlazor;

namespace VHC_Erp.frontend.Utils.GlobalDialogErrorHandler;

public interface IGenerateDialog
{
    void ShowErrorMessage(string message);
    void ShowSuccessMessage<T>(T value);
}

public class GenerateDialog(IDialogService dialogService) : IGenerateDialog
{
    private bool _isDialogShowing = false;   
    private readonly object _lock = new object();
    public async void ShowErrorMessage(string message)
    {
        lock (_lock)
        {
            if (_isDialogShowing) return;
            _isDialogShowing = true;
        }
        var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true};
        var parameters = new DialogParameters
        {
            ["Value"] = message,
            ["Title"] = "Error"
        };
        var dialogRef = dialogService.Show<Dialog>("Erro", parameters, options);
        await dialogRef.Result;
        _isDialogShowing = false;
        
    }

    public async void ShowSuccessMessage<T>(T value)
    {
        lock (_lock)
        {
            if (_isDialogShowing) return;
            _isDialogShowing = true;
        }
        var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true};
        var parameters = new DialogParameters
        {
            ["Value"] = value,
            ["Title"] = "Success"
        };
        var dialogRef = dialogService.Show<Dialog>("Sucesso", parameters, options);
        await dialogRef.Result;
        _isDialogShowing = false;
    }
}