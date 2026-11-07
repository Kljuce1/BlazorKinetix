export function connect(dialog) {
  dialog.addEventListener("click", event => {
    if (event.target === dialog) dialog.close();
  });
}

export function show(dialog) {
  if (dialog && !dialog.open) dialog.showModal();
}

export function close(dialog) {
  if (dialog?.open) dialog.close();
}
