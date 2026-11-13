using Microsoft.AspNetCore.Components.Forms;

namespace BlazorKinetix.Components;

/// <summary>Represents a file selected in FileUpload.</summary>
public sealed class FileUploadItem(IBrowserFile file)
{
    /// <summary>Gets the browser file.</summary>
    public IBrowserFile File { get; } = file;
    /// <summary>Gets the upload state.</summary>
    public FileUploadStatus Status { get; internal set; } = FileUploadStatus.Ready;
    /// <summary>Gets the uploaded percentage, from zero to one hundred.</summary>
    public int Progress { get; internal set; }
    /// <summary>Gets an error message when the upload fails.</summary>
    public string? Error { get; internal set; }
    /// <summary>Gets a small image preview when available.</summary>
    public string? PreviewUrl { get; internal set; }
    internal CancellationTokenSource? Cancellation { get; set; }
}
