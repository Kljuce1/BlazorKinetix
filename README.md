# BlazorKinetix

Themeable controls for Blazor Server and WebAssembly. This preview includes `InputBox`, `Button`, `Tooltip`, `Modal`, `NavMenu`, `HorizontalDatePicker`, `DatePicker`, `DateRangePicker`, `TimePicker`, and `DateTimePicker`.

## Run the examples

```shell
dotnet run --project BlazorKinetix.Demo
```

The demo contains live examples, a searchable component index, a code view for every card, and light/dark and English/Spanish switches. Open `/docs` for setup and parameter examples. The demo reads the browser's preferred language on first visit: Spanish for `es` locales, English otherwise. A language choice is kept in a cookie.

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

Use `Icon` for custom Razor markup, `ImageSrc` for an image, or `IconClass` for an icon library already used by the host app. `SuccessIcon` and `SuccessIconClass` customize the completed hold or slide state. `SlideIcon` and `SlideIconClass` customize the slide handle. `InputBox` also accepts `ShowPasswordIconClass` and `HidePasswordIconClass`. Inline SVG defaults need no icon package.

To use Font Awesome, load its stylesheet in the host application and pass a class such as `IconClass="fa-solid fa-check"`. `Modal` accepts `CloseIcon` or `CloseIconClass` as well. The library does not load an icon package itself.

## NavMenu

```razor
<NavMenu Items="@menuItems" Title="Workspace"
         @bind-SelectedKey="selectedKey" @bind-IsCollapsed="collapsed" />
```

`NavMenuItem` accepts `Key`, `Text`, `Href`, `Children`, `Enabled`, and `Visible`. Icons can come from `Icon` (Razor markup), `ImageSrc`, or `IconClass`. Load Font Awesome or another icon stylesheet in the host app before using its classes. The menu supports nested groups, a desktop collapsed state, and a mobile drawer with keyboard handling. `OnSelect` reports leaf selections; `SelectedKeyChanged` supports binding. When `SelectedKey` is unset, URL items follow the current route.

Set colors and dimensions on a parent with `--bk-nav-accent`, `--bk-nav-surface`, `--bk-nav-text`, `--bk-nav-border`, `--bk-nav-active-background`, `--bk-nav-active-text`, `--bk-nav-hover`, `--bk-nav-width`, `--bk-nav-collapsed-width`, `--bk-nav-mobile-width`, and `--bk-nav-radius`. The defaults follow the host's `--bk-*` theme variables. `CssClass` adds a class to the root for further styling.

## Dates and times


```razor
<HorizontalDatePicker StartDate="@today" EndDate="@today.AddDays(13)"
    MinDate="@today" DisabledDates="@unavailable"
    Availability="@fares" @bind-Value="departure" />

<DatePicker MinDate="@today" DisabledDates="@unavailable"
    @bind-Value="selectedDate" />

<DateRangePicker MinDate="@today" DisabledDates="@unavailable"
    @bind-Value="selectedRange" RangeChanged="Search" />

<TimePicker StepMinutes="30" @bind-Value="selectedTime" />

<DateTimePicker MinDate="@today" StepMinutes="30"
    DisabledTimesForDate="TimesForDate" @bind-Value="appointment" />
```

`HorizontalDatePicker` and `DatePicker` bind to `DateOnly?`. `DateRangePicker` binds to `DateRange?`; its `RangeChanged` event runs only after both endpoints are selected. `TimePicker` binds to `TimeOnly?`. `DateTimePicker` binds to `DateTime?` and emits a value only after a day and a time are chosen. Its resulting `DateTime.Kind` is `Unspecified`; the host application decides how to store time zones.

`TimePicker` accepts `Items` for exact times supplied by the application. Each `TimeSlotItem` has a `Time`, `IsAvailable`, and optional `Message`. Unavailable items remain visible with a disabled style. Without `Items`, `StepMinutes` still generates slots between `MinTime` and `MaxTime`. `DateTimePicker` accepts `Items` for every day or `ItemsForDate` for a different list per day. `DisabledTimes` and `DisabledTimesForDate` remain available in either mode.

```razor
<TimePicker Items="@departureTimes" @bind-Value="departureTime" />
```

```csharp
IReadOnlyCollection<TimeSlotItem> departureTimes =
[
    new(new TimeOnly(9, 10)),
    new(new TimeOnly(9, 45), false, "Already booked"),
    new(new TimeOnly(11, 20))
];
```

Use `DisabledDate(DateOnly date, string? message)` and `DisabledTime(TimeOnly time, string? message)` for blocked choices. The message appears in the accessible label and when the blocked choice is activated. `DateAvailability` adds a short price or capacity label. `DayContent` replaces that label with custom markup. `DateRangePicker` rejects ranges crossing a blocked date unless `AllowDisabledDatesInsideRange="true"`.

The host application can set `--bk-date-selected`, `--bk-date-on-selected`, `--bk-date-disabled`, `--bk-date-disabled-background`, `--bk-date-outside`, `--bk-date-range`, `--bk-date-cell-width`, and `--bk-time-selected`. Navigation icons accept SVG fragments or classes such as `PreviousIconClass="fa-solid fa-chevron-left"`. Dates from the adjacent months appear in a muted tone and remain subject to the same availability rules. Host CSS variables take priority in light and dark themes; when the host provides no palette, the controls include fallbacks for `prefers-color-scheme`.

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

