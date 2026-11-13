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
        ["ListBox.Placeholder"] = "Select an option",
        ["ListBox.Search"] = "Search options",
        ["ListBox.Empty"] = "No matching options.",
        ["ListBox.Clear"] = "Clear selection",
        ["Autocomplete.Placeholder"] = "Search for an option",
        ["Autocomplete.Loading"] = "Searching…",
        ["Autocomplete.Start"] = "Start typing to search.",
        ["FileUpload.Drop"] = "Drop files here",
        ["FileUpload.Browse"] = "or choose files from your device",
        ["FileUpload.Files"] = "Selected files",
        ["FileUpload.Cancel"] = "Cancel",
        ["FileUpload.Remove"] = "Remove",
        ["FileUpload.Upload"] = "Upload files",
        ["FileUpload.Ready"] = "Ready",
        ["FileUpload.Completed"] = "Completed",
        ["FileUpload.Canceled"] = "Canceled",
        ["FileUpload.Failed"] = "Upload failed.",
        ["FileUpload.TooMany"] = "Select no more than {max} files.",
        ["FileUpload.TooLarge"] = "{name} exceeds the {max} limit.",
        ["FileUpload.InvalidType"] = "{name} is not an accepted file type.",
        ["Form.Errors"] = "Please review these fields.",
        ["Form.Valid"] = "All fields are valid.",
        ["Button.Default"] = "Continue",
        ["Button.Working"] = "Working…",
        ["Button.Hold"] = "Hold to confirm",
        ["Button.Slide"] = "Slide to confirm",
        ["Button.Success"] = "Done!",
        ["Modal.Close"] = "Close",
        ["Dialog.Cancel"] = "Cancel",
        ["Dialog.Confirm"] = "Confirm",
        ["Dialog.Processing"] = "Working…",
        ["Dialog.Error"] = "The action could not be completed.",
        ["NavMenu.Title"] = "Navigation",
        ["NavMenu.Label"] = "Navigation menu",
        ["NavMenu.MobileTrigger"] = "Menu",
        ["NavMenu.Open"] = "Open menu",
        ["NavMenu.Close"] = "Close menu",
        ["NavMenu.Expand"] = "Expand menu",
        ["NavMenu.Collapse"] = "Collapse menu",
        ["Table.SortBy"] = "Sort by",
        ["Table.DefaultOrder"] = "Default order",
        ["Table.ReverseSort"] = "Reverse sort order",
        ["Table.Loading"] = "Loading rows…",
        ["Table.Error"] = "The rows could not be loaded.",
        ["Table.Retry"] = "Try again",
        ["Table.Empty"] = "No items yet.",
        ["Table.Details"] = "Details",
        ["Table.Actions"] = "Actions",
        ["Table.More"] = "Show more",
        ["Table.Less"] = "Show less",
        ["Table.ShowDetail"] = "Show detail",
        ["Table.HideDetail"] = "Hide detail",
        ["Table.LoadMore"] = "Load more",
        ["Table.Showing"] = "Showing",
        ["Table.Of"] = "of",
        ["Table.Items"] = "items",
        ["Table.RowsPerPage"] = "Rows per page",
        ["Table.RowsShort"] = "Rows",
        ["Table.Previous"] = "Previous page",
        ["Table.Next"] = "Next page",
        ["Pagination.Navigation"] = "Pagination",
        ["Pagination.First"] = "First page",
        ["Pagination.Last"] = "Last page",
        ["Pagination.Page"] = "Page {page}",
        ["Date.Calendar"] = "Calendar",
        ["Date.Choose"] = "Choose a date",
        ["Date.ChooseRange"] = "Choose a date range",
        ["Date.ChooseStart"] = "Choose start date",
        ["Date.ChooseEnd"] = "Choose end date",
        ["Date.RangeBlocked"] = "The range includes an unavailable date.",
        ["Date.PreviousMonth"] = "Previous month",
        ["Date.NextMonth"] = "Next month",
        ["Date.PreviousDays"] = "Previous days",
        ["Date.NextDays"] = "Next days",
        ["Date.Month"] = "Month",
        ["Date.Year"] = "Year",
        ["Date.Close"] = "Close calendar",
        ["Time.Choose"] = "Choose a time",
        ["Time.NoSlots"] = "No times available.",
        ["DateTime.Choose"] = "Choose date and time",
        ["DateTime.ChangeDate"] = "Change date"
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
