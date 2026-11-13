using Microsoft.AspNetCore.Components.Forms;

namespace BlazorKinetix.Components;

/// <summary>Uploads a browser file and reports the number of bytes transferred.</summary>
public delegate Task FileUploadHandler(IBrowserFile file, IProgress<long> progress, CancellationToken cancellationToken);
