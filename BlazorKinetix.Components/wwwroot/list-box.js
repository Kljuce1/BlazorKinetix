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
        const search = event.target.closest('.bk-listbox-search');
        const option = event.target.closest('.bk-listbox-option');
        const key = event.key;
        if (key === 'Escape' && root.classList.contains('is-open')) {
            event.preventDefault();
            component.invokeMethodAsync('HandleKeyAsync', key);
            root.querySelector('.bk-listbox-trigger')?.focus();
            return;
        }
        if (option && key === 'Enter') return;
        if (key === 'Enter' && (search || (root.classList.contains('is-open') && event.target.closest('.bk-listbox-trigger')))) {
            event.preventDefault();
            component.invokeMethodAsync('HandleKeyAsync', key);
            return;
        }
        if (key === 'ArrowDown' || key === 'ArrowUp' || (!search && (key === 'Home' || key === 'End'))) {
            event.preventDefault();
            component.invokeMethodAsync('HandleKeyAsync', key).then(() => {
                root.querySelector('.bk-listbox-option.is-active')?.scrollIntoView({ block: 'nearest' });
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
