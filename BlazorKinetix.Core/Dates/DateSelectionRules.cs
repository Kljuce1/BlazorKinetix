namespace BlazorKinetix.Core;

/// <summary>Shared availability checks for date controls.</summary>
public static class DateSelectionRules
{
    /// <summary>Returns whether a date can be selected.</summary>
    public static bool IsSelectable(DateOnly date, DateOnly? minDate, DateOnly? maxDate, IEnumerable<DisabledDate>? disabledDates)
        => (!minDate.HasValue || date >= minDate.Value)
        && (!maxDate.HasValue || date <= maxDate.Value)
        && (disabledDates is null || !disabledDates.Any(item => item.Date == date));

    /// <summary>Returns the explanation associated with a disabled date.</summary>
    public static string? DisabledMessage(DateOnly date, IEnumerable<DisabledDate>? disabledDates)
        => disabledDates?.FirstOrDefault(item => item.Date == date)?.Message;

    /// <summary>Returns whether every date in an inclusive range can be selected.</summary>
    public static bool IsRangeSelectable(DateOnly start, DateOnly end, DateOnly? minDate, DateOnly? maxDate, IEnumerable<DisabledDate>? disabledDates, bool allowDisabledDatesInsideRange = false)
    {
        if (end < start || !IsSelectable(start, minDate, maxDate, disabledDates) || !IsSelectable(end, minDate, maxDate, disabledDates)) return false;
        if (allowDisabledDatesInsideRange) return true;
        for (var date = start; date <= end; date = date.AddDays(1))
        {
            if (!IsSelectable(date, minDate, maxDate, disabledDates)) return false;
            if (date == DateOnly.MaxValue) break;
        }
        return true;
    }
}
