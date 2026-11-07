# BlazorKinetix

Themeable controls for Blazor Server and WebAssembly. This preview includes `InputBox`, `Button`, `Tooltip`, and `Modal`.

## Run the examples

```shell
dotnet run --project BlazorKinetix.Demo
```

The demo contains live examples, a code view for every card, and light/dark and English/Spanish switches. Open `/docs` for setup and parameter examples. The demo reads the browser's preferred language on first visit: Spanish for `es` locales, English otherwise. A language choice is kept in a cookie.

## Install in an app

After publication, install the `BlazorKinetix` NuGet package. During development, add a project reference to `BlazorKinetix.Components`.

Register the text catalog in `Program.cs`:

```csharp
builder.Services.AddBlazorKinetix();
```

Add `@using BlazorKinetix.Components` and `@using BlazorKinetix.Core` to `_Imports.razor`. Keep the host application's generated CSS isolation stylesheet linked in its root page. For the demo, this is:

```html
<link rel="stylesheet" href="@Assets["BlazorKinetix.Demo.styles.css"]" />
```

The host stylesheet imports the library's isolated styles automatically during the build.

## InputBox

```razor
<InputBox TValue="string" Title="Email" Email="true" Required="true"
          LabelMode="InputBoxLabelMode.Floating"
          Appearance="InputBoxAppearance.Gradient"
          LiveValidation="true" @bind-Value="email" />
```

`InputBoxKind` supports `Text`, `Integer`, `Decimal`, and `Password`. Numeric values use the current culture. `Value`, `ValueChanged`, and `ValueExpression` support `@bind-Value` and `EditForm`. `Action` accepts a `RenderFragment`, including a `Button`. `ValidateField()` checks the current value when called through `@ref`.

Email fields use `inputmode="email"` and the component's validation message. Set `NativeValidation="true"` to use the browser's email and required-field messages instead.

## Button

```razor
<Button Text="Save" OnAction="SaveAsync" ProcessingText="Saving…" />
<Button Mode="ButtonMode.ActionFeedback" Text="Apply"
               FeedbackText="Applied!" OnAction="Apply" />
```

The modes are `Default`, `HoldToConfirm`, `ActionFeedback`, and `SlideToConfirm`. `OnAction` accepts synchronous or asynchronous handlers. A running handler disables repeated activation; `ShowProcessing` controls the spinner and working text. An action that throws does not show success feedback.

Use `Icon` for custom Razor markup, `ImageSrc` for an image, or `IconClass` for an icon library already used by the host app. The password reveal icon is an inline SVG and needs no icon package.

## Tooltip

```razor
<Tooltip Text="Available to your team">
    <button type="button">Details</button>
</Tooltip>
```

The tooltip appears on hover or focus. Set `ShowOnHover="false"` and call `ShowAsync()` through `@ref` to show it after an action.

## Modal

```razor
<Button Text="Open details" OnAction="() => detailsOpen = true" />

<Modal Title="Details" Size="ModalSize.Compact" @bind-IsOpen="detailsOpen">
    <p>Place any Razor content here.</p>
</Modal>
```

The modal closes with Escape, the close button, or a backdrop click. `Footer` accepts a `RenderFragment` for actions. `OpenAsync()` and `CloseAsync()` are available through `@ref`.

## Theme

Override variables in the host stylesheet:

```css
:root {
  --bk-primary: #075b62;
  --bk-on-primary: #fff;
  --bk-secondary: #566b96;
  --bk-surface: #fff;
  --bk-text: #243447;
  --bk-border: #bdcbd2;
  --bk-gradient-start: #7159ed;
  --bk-gradient-end: #20c4e2;
  --bk-input-radius: .7rem;
}

:root[data-theme="dark"] {
  --bk-primary: #66d3d5;
  --bk-on-primary: #102631;
  --bk-surface: #1b2636;
  --bk-soft-surface: #26364a;
  --bk-text: #e9eff8;
  --bk-muted: #aebbd0;
  --bk-border: #485a70;
}
```

The controls use host CSS variables when present and include dark fallbacks for `prefers-color-scheme: dark`. The demo uses `data-theme` to switch palettes.

## Text and culture

Visible component text can be set on the component. Shared defaults can be replaced with a JSON catalog:

```json
{
  "es": {
    "Button.Working": "Procesando…",
    "Input.Required": "{field} es obligatorio."
  }
}
```

```csharp
builder.Services.AddBlazorKinetix(texts =>
    texts.LoadJson(File.ReadAllText("kinetix-texts.json")));
```

In WebAssembly, load the JSON from `wwwroot` before registering the service. The catalog uses `CurrentUICulture`; numeric parsing uses `CurrentCulture`. Component parameters take priority over catalog text. The fallback catalog is in English. The demo keeps its page copy and control messages in the same `kinetix-texts.json` file.

## Package

```shell
dotnet pack BlazorKinetix.Core -c Release
dotnet pack BlazorKinetix.Components -c Release
```

The `BlazorKinetix` package depends on `BlazorKinetix.Core`. Both are preview packages and have not been published.

## Contributing

Open an issue before large changes. Keep public API names in English, include a small demo for new visual states, and run `dotnet build BlazorKinetix.slnx` before sending a pull request.

Licensed under MIT.

