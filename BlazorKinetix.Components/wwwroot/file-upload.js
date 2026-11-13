const instances = new WeakMap();

export function initialize(root, component) {
    const zone = root.querySelector('.bk-file-upload-dropzone');
    const input = zone?.querySelector('input[type=file]');
    if (!zone || !input) return;
    let depth = 0;
    const enter = event => {
        if (input.disabled || !event.dataTransfer?.types?.includes('Files')) return;
        event.preventDefault();
        depth++;
        component.invokeMethodAsync('SetDragging', true);
    };
    const over = event => {
        if (input.disabled || !event.dataTransfer?.types?.includes('Files')) return;
        event.preventDefault();
        event.dataTransfer.dropEffect = 'copy';
    };
    const leave = event => {
        if (!event.dataTransfer?.types?.includes('Files')) return;
        event.preventDefault();
        depth = Math.max(0, depth - 1);
        if (depth === 0) component.invokeMethodAsync('SetDragging', false);
    };
    const drop = event => {
        event.preventDefault();
        depth = 0;
        component.invokeMethodAsync('SetDragging', false);
        if (input.disabled || !event.dataTransfer?.files?.length) return;
        try {
            input.files = event.dataTransfer.files;
            input.dispatchEvent(new Event('change', { bubbles: true }));
        } catch {
            input.click();
        }
    };
    zone.addEventListener('dragenter', enter);
    zone.addEventListener('dragover', over);
    zone.addEventListener('dragleave', leave);
    zone.addEventListener('drop', drop);
    instances.set(root, { zone, enter, over, leave, drop });
}

export function dispose(root) {
    const handlers = instances.get(root);
    if (!handlers) return;
    const { zone, enter, over, leave, drop } = handlers;
    zone.removeEventListener('dragenter', enter);
    zone.removeEventListener('dragover', over);
    zone.removeEventListener('dragleave', leave);
    zone.removeEventListener('drop', drop);
    instances.delete(root);
}
