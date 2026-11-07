const menus = new WeakMap();

export function initialize(root, trigger, panel, dotnet) {
    const mobile = window.matchMedia('(max-width: 800px)');
    const state = { trigger, panel, dotnet, mobile, open: false, previousOverflow: '' };
    state.onKeyDown = event => {
        if (!state.open || !mobile.matches) return;
        if (event.key === 'Escape') {
            event.preventDefault();
            dotnet.invokeMethodAsync('CloseFromKeyboard');
            return;
        }
        if (event.key !== 'Tab') return;
        const focusable = [...panel.querySelectorAll('a[href],button:not(:disabled),[tabindex="0"]')].filter(element => element.getClientRects().length);
        if (!focusable.length) return;
        const first = focusable[0], last = focusable[focusable.length - 1];
        if (event.shiftKey && document.activeElement === first) { event.preventDefault(); last.focus(); }
        else if (!event.shiftKey && document.activeElement === last) { event.preventDefault(); first.focus(); }
    };
    state.onChange = () => { if (!mobile.matches && state.open) dotnet.invokeMethodAsync('CloseFromKeyboard'); };
    document.addEventListener('keydown', state.onKeyDown);
    mobile.addEventListener('change', state.onChange);
    menus.set(root, state);
}

export function setOpen(root, open) {
    const state = menus.get(root);
    if (!state) return;
    open = open && state.mobile.matches;
    if (state.open === open) return;
    state.open = open;
    if (open) {
        state.previousOverflow = document.body.style.overflow;
        document.body.style.overflow = 'hidden';
        requestAnimationFrame(() => state.panel.querySelector('.bk-nav-mobile-close')?.focus());
    } else {
        document.body.style.overflow = state.previousOverflow;
        state.trigger.focus();
    }
}

export function dispose(root) {
    const state = menus.get(root);
    if (!state) return;
    document.removeEventListener('keydown', state.onKeyDown);
    state.mobile.removeEventListener('change', state.onChange);
    if (state.open) document.body.style.overflow = state.previousOverflow;
    menus.delete(root);
}
