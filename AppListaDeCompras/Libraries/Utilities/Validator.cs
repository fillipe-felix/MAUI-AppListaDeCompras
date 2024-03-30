using System.Text;

using FluentValidation.Results;

namespace AppListaDeCompras.Libraries.Utilities;

public static class Validator
{
    public static string ShowErrorMessage(ValidationResult result)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var failure in result.Errors)
        {
            sb.AppendLine($"{failure.ErrorMessage}");
        }
        return sb.ToString();
    }
}
