const tables = new WeakMap();

export function initialize(root, sentinel, dotnet, breakpoint) {
    const viewport = root.querySelector('.bk-table-scroll');
    const state = { dotnet, breakpoint, mobile: null };
    state.resize = new ResizeObserver(entries => {
        const mobile = entries[0].contentRect.width < state.breakpoint;
        if (state.mobile !== mobile) {
            state.mobile = mobile;
            dotnet.invokeMethodAsync('OnViewportChange', mobile);
        }
    });
    state.resize.observe(root);
    if (sentinel && sentinel.isConnected) {
        state.intersection = new IntersectionObserver(entries => {
            if (entries.some(entry => entry.isIntersecting)) dotnet.invokeMethodAsync('OnReachEnd');
        }, { root: viewport, rootMargin: '0px 0px 120px 0px' });
        state.intersection.observe(sentinel);
    }
    tables.set(root, state);
}

export function dispose(root) {
    const state = tables.get(root);
    if (!state) return;
    state.resize.disconnect();
    state.intersection?.disconnect();
    tables.delete(root);
}
