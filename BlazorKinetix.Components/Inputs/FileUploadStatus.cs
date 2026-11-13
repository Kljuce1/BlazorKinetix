namespace BlazorKinetix.Components;

/// <summary>Describes a file's current upload state.</summary>
public enum FileUploadStatus
{
    /// <summary>The file is ready to upload.</summary>
    Ready,
    /// <summary>The file is being uploaded.</summary>
    Uploading,
    /// <summary>The upload completed.</summary>
    Completed,
    /// <summary>The upload was canceled.</summary>
    Canceled,
    /// <summary>The upload failed.</summary>
    Failed
}
