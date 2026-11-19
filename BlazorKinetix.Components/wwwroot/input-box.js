const filters = new WeakMap();

function validDraft(text, settings) {
    let number = text;
    const sign = [settings.negativeSign, settings.positiveSign]
        .filter(Boolean)
        .find(candidate => number.startsWith(candidate));
    if (sign) number = number.slice(sign.length);

    const parts = settings.decimal ? number.split(settings.decimalSeparator) : [number];
    return parts.length <= (settings.decimal ? 2 : 1) &&
        parts.every(part => /^[0-9]*$/.test(part));
}

function insertedValue(input, text) {
    const start = input.selectionStart ?? input.value.length;
    const end = input.selectionEnd ?? start;
    return input.value.slice(0, start) + text + input.value.slice(end);
}

function normalizeSeparator(text, settings) {
    return settings.decimal && settings.decimalSeparator !== "."
        ? text.replaceAll(".", settings.decimalSeparator)
        : text;
}

export function configure(input, decimal, decimalSeparator, negativeSign, positiveSign) {
    dispose(input);

    const settings = { decimal, decimalSeparator, negativeSign, positiveSign };
    let lastAccepted = input.value;
    let lastCaret = input.selectionStart ?? lastAccepted.length;
    let composing = false;

    const insert = text => {
        const next = insertedValue(input, text);
        if (!validDraft(next, settings)) return false;
        input.setRangeText(text, input.selectionStart, input.selectionEnd, "end");
        input.dispatchEvent(new Event("input", { bubbles: true }));
        return true;
    };

    const beforeInput = event => {
        if (composing || event.isComposing || !event.inputType?.startsWith("insert") || event.data == null) return;
        const normalized = normalizeSeparator(event.data, settings);
        if (normalized !== event.data) {
            event.preventDefault();
            insert(normalized);
        } else if (!validDraft(insertedValue(input, event.data), settings)) event.preventDefault();
    };
    const paste = event => {
        const text = event.clipboardData?.getData("text");
        if (text == null) return;
        event.preventDefault();
        insert(normalizeSeparator(text, settings));
    };
    const inputChanged = event => {
        if (composing || event.isComposing) return;
        const normalized = normalizeSeparator(input.value, settings);
        if (validDraft(normalized, settings)) {
            if (normalized !== input.value) {
                const caret = normalizeSeparator(input.value.slice(0, input.selectionStart ?? input.value.length), settings).length;
                input.value = normalized;
                input.setSelectionRange(caret, caret);
            }
            lastAccepted = input.value;
            lastCaret = input.selectionStart ?? lastAccepted.length;
            return;
        }
        input.value = lastAccepted;
        input.setSelectionRange(lastCaret, lastCaret);
    };
    const compositionStart = () => { composing = true; };
    const compositionEnd = () => {
        composing = false;
        const normalized = normalizeSeparator(input.value, settings);
        if (validDraft(normalized, settings)) {
            input.value = normalized;
            lastAccepted = input.value;
            lastCaret = input.selectionStart ?? lastAccepted.length;
            input.dispatchEvent(new Event("input", { bubbles: true }));
        } else {
            input.value = lastAccepted;
            input.setSelectionRange(lastCaret, lastCaret);
            input.dispatchEvent(new Event("input", { bubbles: true }));
        }
    };

    input.addEventListener("beforeinput", beforeInput);
    input.addEventListener("paste", paste);
    input.addEventListener("input", inputChanged);
    input.addEventListener("compositionstart", compositionStart);
    input.addEventListener("compositionend", compositionEnd);
    filters.set(input, () => {
        input.removeEventListener("beforeinput", beforeInput);
        input.removeEventListener("paste", paste);
        input.removeEventListener("input", inputChanged);
        input.removeEventListener("compositionstart", compositionStart);
        input.removeEventListener("compositionend", compositionEnd);
    });
}

export function dispose(input) {
    filters.get(input)?.();
    filters.delete(input);
}
