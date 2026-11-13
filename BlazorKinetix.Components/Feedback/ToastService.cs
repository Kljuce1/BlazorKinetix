namespace BlazorKinetix.Components;

/// <summary>Shows notifications in a ToastHost.</summary>
public sealed class ToastService
{
    internal event Action<ToastRequest>? Shown;

    /// <summary>Adds a toast to the active host.</summary>
    public Guid Show(ToastOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        var id = Guid.NewGuid();
        Shown?.Invoke(new ToastRequest(id, options));
        return id;
    }
}

internal sealed record ToastRequest(Guid Id, ToastOptions Options);
