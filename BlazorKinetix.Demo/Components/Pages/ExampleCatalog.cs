namespace BlazorKinetix.Demo.Components.Pages;

internal static class ExampleCatalog
{
    internal static readonly IReadOnlyDictionary<string, (string Title, string Code)> Items =
        new Dictionary<string, (string, string)>
        {
            ["normal"] = ("Normal input", """
                <InputBox TValue="string" Title="Full name"
                          Placeholder="Enter your name" Required="true"
                          HelpText="Shown on your profile"
                          @bind-Value="name" />
                """),
            ["floating"] = ("Floating label", """
                <InputBox TValue="string" Title="Email address"
                          Email="true" LiveValidation="true"
                          LabelMode="InputBoxLabelMode.Floating"
                          @bind-Value="email" />
                """),
            ["gradient"] = ("Gradient input with action", """
                <InputBox TValue="string" Title="Promo code"
                          Appearance="InputBoxAppearance.Gradient"
                          @bind-Value="promo">
                    <Action>
                        <Button Mode="ButtonMode.ActionFeedback"
                                Text="Apply" FeedbackText="Code applied!"
                                OnAction="ApplyPromo" />
                    </Action>
                </InputBox>

                @code {
                    private string? promo;
                    private string status = "";
                    private void ApplyPromo() => status = $"Applied: {promo}";
                }
                """),
            ["underline"] = ("Underline input", """
                <InputBox TValue="string" Title="Project name"
                          Appearance="InputBoxAppearance.Underline"
                          @bind-Value="project" />
                """),
            ["password"] = ("Password reveal", """
                <InputBox TValue="string" Title="Password"
                          Kind="InputBoxKind.Password"
                          ShowPasswordStrength="true"
                          MinimumPasswordLength="8"
                          @bind-Value="password" />
                """),
            ["counter"] = ("Character counter", """
                <InputBox TValue="string" Title="Short note"
                          MaxLength="36" ShowCharacterCount="true"
                          @bind-Value="note" />
                """),
            ["default-button"] = ("Default button", """
                <Button Text="Save changes" OnAction="SaveAsync"
                        ProcessingText="Saving…" />
                <Button Text="Learn more"
                        Variant="ButtonVariant.Outline"
                        OnAction="Confirm" />
                """),
            ["hold"] = ("Hold to confirm", """
                <Button Mode="ButtonMode.HoldToConfirm"
                        Color="ColorRole.Danger"
                        Text="Hold to delete"
                        SuccessText="Deleted"
                        OnAction="Confirm">
                    <Icon><svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M3 6h18M8 6V4h8v2m3 0-1 14H6L5 6m5 4v6m4-6v6" /></svg></Icon>
                    <SuccessIcon><svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><path d="m5 12 4 4L19 6" /></svg></SuccessIcon>
                </Button>
                """),
            ["feedback"] = ("Action feedback", """
                <Button Mode="ButtonMode.ActionFeedback"
                        Text="Send invitation"
                        FeedbackText="Invitation sent!"
                        OnAction="SaveAsync" />
                """),
            ["slide"] = ("Slide to confirm", """
                <Button Mode="ButtonMode.SlideToConfirm"
                        Text="Slide to approve"
                        SuccessText="Confirmed"
                        SlideFillColor="#98dcd9"
                        OnAction="Confirm" />
                """),
            ["tooltip"] = ("Tooltip", """
                <Tooltip Text="You can replace this text"
                         Position="TooltipPosition.Top">
                    <button type="button">Hover or focus here</button>
                </Tooltip>
                """),
            ["horizontal-flight"] = ("Horizontal dates · Flights", """
                <HorizontalDatePicker StartDate="@today" EndDate="@today.AddDays(13)"
                    MinDate="@today" DisabledDates="@unavailable"
                    Availability="@fares" @bind-Value="flightDate" />

                @code {
                    private DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                    private DateOnly? flightDate;
                    private IReadOnlyCollection<DisabledDate> unavailable =>
                        [new(today.AddDays(2), "No flights on this date")];
                    private IReadOnlyCollection<DateAvailability> fares =>
                        [new(today, "$120"), new(today.AddDays(1), "$145")];
                }
                """),
            ["horizontal-class"] = ("Horizontal dates · Classes", """
                <HorizontalDatePicker StartDate="@today" EndDate="@today.AddDays(10)"
                    MinDate="@today" DisabledDates="@fullClasses"
                    Availability="@places" @bind-Value="classDate"
                    CssClass="class-date-picker" />

                /* Host CSS */
                .class-date-picker { --bk-date-selected: #7454c6; }
                """),
            ["date-travel"] = ("Date picker · Travel", """
                <DatePicker Label="Travel date" MinDate="@today"
                    MaxDate="@today.AddMonths(12)" DisabledDates="@unavailable"
                    Availability="@fares" @bind-Value="travelDate" />
                """),
            ["date-appointment"] = ("Date picker · Appointment", """
                <DatePicker Label="Appointment date" MinDate="@today"
                    MaxDate="@today.AddMonths(3)" DisabledDates="@fullClasses"
                    @bind-Value="appointmentDate" />
                """),
            ["date-range"] = ("Date range", """
                <DateRangePicker Label="Check-in and check-out"
                    MinDate="@today" DisabledDates="@unavailable"
                    @bind-Value="stayRange" RangeChanged="SearchStays" />

                @code {
                    private DateRange? stayRange;
                    private Task SearchStays(DateRange range) => Task.CompletedTask;
                }
                """),
            ["time"] = ("Time picker", """
                <TimePicker Label="Start time" MinTime="@(new TimeOnly(9, 0))"
                    MaxTime="@(new TimeOnly(17, 0))" StepMinutes="30"
                    DisabledTimes="@busyTimes" @bind-Value="meetingTime" />
                """),
            ["time-list"] = ("Time picker · Explicit items", """
                <TimePicker Label="Departure time" Items="@timesFromDatabase"
                    @bind-Value="departureTime" />

                @code {
                    private TimeOnly? departureTime;
                    private IReadOnlyCollection<TimeSlotItem> timesFromDatabase =
                    [
                        new(new TimeOnly(9, 10)),
                        new(new TimeOnly(9, 45), false, "Already booked"),
                        new(new TimeOnly(11, 20)),
                        new(new TimeOnly(14, 10))
                    ];
                }
                """),
            ["date-time"] = ("Date and time picker", """
                <DateTimePicker Label="Session date and time" MinDate="@today"
                    ItemsForDate="SlotsForDate"
                    @bind-Value="sessionDateTime" />

                @code {
                    private DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                    private DateTime? sessionDateTime;
                    private IReadOnlyCollection<TimeSlotItem>? SlotsForDate(DateOnly date)
                        => [new(new TimeOnly(9, 15)),
                            new(new TimeOnly(10, 40), false, "Already booked"),
                            new(new TimeOnly(13, 20))];
                }
                """),
            ["nav-basic"] = ("Navigation menu", """
                <NavMenu Items="@menuItems" Title="Workspace"
                         FooterText="Signed in" @bind-SelectedKey="selected"
                         @bind-IsCollapsed="collapsed" />

                @code {
                    private string? selected = "overview";
                    private bool collapsed;
                    private IReadOnlyList<NavMenuItem> menuItems = [
                        new() { Key = "overview", Text = "Overview" },
                        new() { Key = "bookings", Text = "Bookings", Children = [
                            new() { Key = "upcoming", Text = "Upcoming" },
                            new() { Key = "history", Text = "History" }] },
                        new() { Key = "billing", Text = "Billing", Enabled = false }
                    ];
                }
                """),
            ["nav-custom"] = ("Custom navigation menu", """
                <div class="project-menu">
                    <NavMenu Items="@projectItems" Title="Project"
                             @bind-SelectedKey="selected" />
                </div>

                <style>
                  .project-menu { --bk-nav-accent: #6c4bc5;
                    --bk-nav-active-background: #6c4bc5;
                    --bk-nav-hover: #6c4bc51c;
                    --bk-nav-radius: .5rem; }
                </style>

                @code {
                    private string? selected = "board";
                    private IReadOnlyList<NavMenuItem> projectItems = [
                        new() { Key = "board", Text = "Board",
                                IconClass = "fa-solid fa-table-cells" },
                        new() { Key = "activity", Text = "Activity",
                                IconClass = "fa-solid fa-chart-line" }
                    ];
                }
                """),
            ["modal"] = ("Modal", """
                <Button Text="Open details" OnAction="() => detailsOpen = true" />

                <Modal Title="Details" Size="ModalSize.Compact"
                       @bind-IsOpen="detailsOpen">
                    <p>Place any Razor content here.</p>
                </Modal>

                @code { private bool detailsOpen; }
                """)
        };
}
