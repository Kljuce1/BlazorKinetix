using System.Globalization;
using System.Text.Json;

namespace BlazorKinetix.Core;

/// <summary>Resolves component text by culture and key.</summary>
public sealed class KinetixTexts
{
    private readonly Dictionary<string, Dictionary<string, string>> translations = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<string, string> Defaults = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Input.Required"] = "{field} is required.",
        ["Input.InvalidInteger"] = "{field} must be a valid integer.",
        ["Input.InvalidDecimal"] = "{field} must be a valid decimal number.",
        ["Input.InvalidEmail"] = "{field} must be a valid email address.",
        ["Input.MinLength"] = "{field} must contain at least {min} characters.",
        ["Input.MaxLength"] = "{field} must contain no more than {max} characters.",
        ["Input.Minimum"] = "{field} must be at least {min}.",
        ["Input.Maximum"] = "{field} must be at most {max}.",
        ["Input.Field"] = "This field",
        ["Input.ShowPassword"] = "Show password",
        ["Input.HidePassword"] = "Hide password",
        ["Input.Strength"] = "Estimated strength: {strength}",
        ["Input.Strength.Weak"] = "Weak",
        ["Input.Strength.Fair"] = "Fair",
        ["Input.Strength.Strong"] = "Strong",
        ["Input.PasswordLength"] = "At least {min} characters",
        ["Input.PasswordUppercase"] = "At least {min} uppercase letters",
        ["Input.PasswordDigits"] = "At least {min} digits",
        ["Input.PasswordSpecial"] = "At least {min} special characters",
        ["Input.PasswordRequirement"] = "{field} requires {requirement}.",
        ["Button.Default"] = "Continue",
        ["Button.Working"] = "Working…",
        ["Button.Hold"] = "Hold to confirm",
        ["Button.Slide"] = "Slide to confirm",
        ["Button.Success"] = "Done!",
        ["Modal.Close"] = "Close"
    };

    /// <summary>Loads translations from a JSON object keyed by culture, then text key.</summary>
    public void LoadJson(string json)
    {
        var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json)
            ?? throw new JsonException("A culture dictionary is required.");
        foreach (var (culture, values) in data)
        {
            if (!translations.TryGetValue(culture, out var existing))
                translations[culture] = existing = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var (key, value) in values) existing[key] = value;
        }
    }

    /// <summary>Returns a translated text with optional named replacements.</summary>
    public string Get(string key, IReadOnlyDictionary<string, string>? values = null, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentUICulture;
        var text = Find(culture.Name, key) ?? Find(culture.TwoLetterISOLanguageName, key)
            ?? Find("default", key) ?? (Defaults.TryGetValue(key, out var fallback) ? fallback : key);
        if (values is not null)
            foreach (var (name, value) in values)
                text = text.Replace("{" + name + "}", value, StringComparison.OrdinalIgnoreCase);
        return text;
    }

    private string? Find(string culture, string key) =>
        translations.TryGetValue(culture, out var values) && values.TryGetValue(key, out var text) ? text : null;
}
