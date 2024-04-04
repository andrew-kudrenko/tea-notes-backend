using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TeaNotes.Common
{
    public static class ModelUtils
    {
        public static Dictionary<string, string> ErrorMessagesToDict(ModelStateDictionary state)
        {
            return state
                .Where(prop => prop.Value?.Errors.Count > 0)
                .ToDictionary(
                    entry => entry.Key, 
                    entry => entry.Value!.Errors.ElementAt(0).ErrorMessage
                );
        }
    }
}
