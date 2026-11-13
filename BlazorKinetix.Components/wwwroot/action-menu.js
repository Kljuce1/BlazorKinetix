const menus = new WeakMap();

export function initialize(root, component) {
    const onPointerDown = event => { if (root.classList.contains('is-open') && !root.contains(event.target)) component.invokeMethodAsync('CloseAsync'); };
    const onKeyDown = event => {
        if (event.key !== 'Escape' || !root.classList.contains('is-open')) return;
        event.preventDefault();
        component.invokeMethodAsync('CloseAsync');
        root.querySelector('.bk-action-trigger')?.focus();
    };
    document.addEventListener('pointerdown', onPointerDown);
    document.addEventListener('keydown', onKeyDown);
    menus.set(root, { onPointerDown, onKeyDown });
}

export function dispose(root) {
    const handlers = menus.get(root);
    if (!handlers) return;
    document.removeEventListener('pointerdown', handlers.onPointerDown);
    document.removeEventListener('keydown', handlers.onKeyDown);
    menus.delete(root);
}
