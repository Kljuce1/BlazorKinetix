const instances = new WeakMap();

export function initialize(root, component) {
    const onPointerDown = event => {
        if (root.classList.contains('is-open') && !root.contains(event.target)) component.invokeMethodAsync('CloseAsync');
    };
    const onFocusOut = event => {
        if (root.classList.contains('is-open') && !root.contains(event.relatedTarget)) component.invokeMethodAsync('CloseAsync');
    };
    const onKeyDown = event => {
        if (!root.contains(event.target)) return;
        const key = event.key;
        if (key === 'Escape' || key === 'ArrowDown' || key === 'ArrowUp' || key === 'Enter' || key === 'Home' || key === 'End') {
            if ((key === 'Home' || key === 'End') && event.target.tagName === 'INPUT') return;
            event.preventDefault();
            component.invokeMethodAsync('HandleKeyAsync', key).then(() => {
                root.querySelector('.bk-autocomplete-option.is-active')?.scrollIntoView({ block: 'nearest' });
            });
        }
    };
    document.addEventListener('pointerdown', onPointerDown);
    root.addEventListener('focusout', onFocusOut);
    root.addEventListener('keydown', onKeyDown);
    instances.set(root, { onPointerDown, onFocusOut, onKeyDown });
}

export function dispose(root) {
    const handlers = instances.get(root);
    if (!handlers) return;
    document.removeEventListener('pointerdown', handlers.onPointerDown);
    root.removeEventListener('focusout', handlers.onFocusOut);
    root.removeEventListener('keydown', handlers.onKeyDown);
    instances.delete(root);
}
