export function connect(dialog, dotnet, allowBackdropClose, allowEscape) {
  const onClick = event => {
    if (allowBackdropClose && event.target === dialog) dotnet.invokeMethodAsync("RequestDismiss");
  };
  const onCancel = event => {
    event.preventDefault();
    if (allowEscape) dotnet.invokeMethodAsync("RequestDismiss");
  };
  dialog.addEventListener("click", onClick);
  dialog.addEventListener("cancel", onCancel);
  return {
    show() { if (!dialog.open) dialog.showModal(); },
    close() { if (dialog.open) dialog.close(); },
    dispose() {
      dialog.removeEventListener("click", onClick);
      dialog.removeEventListener("cancel", onCancel);
    }
  };
}
