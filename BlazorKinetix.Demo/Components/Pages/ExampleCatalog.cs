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
            ["pagination-local"] = ("Local pagination", """
                @foreach (var item in items.Skip((page - 1) * pageSize).Take(pageSize))
                {
                    <p>@item.Name</p>
                }
                <Pagination TotalItems="@items.Count" PageSizes="@(new[] { 5, 10, 20 })"
                            MaxVisiblePages="3"
                            @bind-Page="page" @bind-PageSize="pageSize" />
                """),
            ["pagination-remote"] = ("API pagination", """
                <Pagination TotalItems="@totalCount" PageSizes="@(new[] { 5, 10, 20 })"
                            ShowPageNumbers="false" Loading="@loading"
                            @bind-Page="page" @bind-PageSize="pageSize"
                            OnChange="LoadAsync" />

                @code {
                    private Task LoadAsync(PaginationState state) =>
                        service.LoadAsync(state.Page, state.PageSize);
                }
                """),
            ["form-field"] = ("Field and validation summary", """
                <EditForm EditContext="@editContext" OnValidSubmit="Save">
                    <FormValidationSummary />
                    <FormField TValue="string" Label="Name" Required="true"
                               HelpText="Shown on your profile"
                               FieldExpression="@(() => model.Name)">
                        <FieldTemplate Context="ids">
                            <InputText id="@ids.InputId" aria-describedby="@($"{ids.HelpId} {ids.ErrorId}")"
                                       @bind-Value="model.Name" />
                        </FieldTemplate>
                    </FormField>
                    <button type="submit">Save</button>
                </EditForm>
                """),
            ["form-independent"] = ("Independent InputBox validation", """
                <InputBox TValue="string" Title="Access code" Required="true"
                          MinLength="4" LiveValidation="true" @bind-Value="code" />
                """),
            ["file-upload-preview"] = ("Small image preview", """
                <FileUpload Label="Profile image" Accept="image/png,image/jpeg"
                            MaxFiles="1" MaxFileSize="1048576"
                            MaxPreviewBytes="262144" ShowPreviews="true" />
                """),
            ["file-upload-progress"] = ("Bounded upload progress", """
                <FileUpload Label="Text documents" Accept=".txt" Multiple="true"
                            MaxFiles="2" MaxFileSize="262144" AutoUpload="false"
                            UploadAsync="UploadAsync" />

                @code {
                    private async Task UploadAsync(IBrowserFile file, IProgress<long> progress,
                        CancellationToken token)
                    {
                        await using var source = file.OpenReadStream(262144, token);
                        var buffer = new byte[16384];
                        long total = 0;
                        int read;
                        while ((read = await source.ReadAsync(buffer, token)) > 0)
                        {
                            // Process the bounded chunk in your application.
                            total += read;
                            progress.Report(total);
                        }
                    }
                }
                """),
            ["autocomplete-local"] = ("Local autocomplete", """
                <Autocomplete TValue="string" Items="@cities" Label="City"
                              @bind-Value="city" />

                @code {
                    private string? city;
                    private IReadOnlyList<ListBoxItem<string>> cities = [
                        new("Caracas", "Caracas", "Venezuela"),
                        new("Madrid", "Madrid", "Spain")
                    ];
                }
                """),
            ["autocomplete-remote"] = ("Async autocomplete", """
                <Autocomplete TValue="string" SearchAsync="SearchPeopleAsync"
                              MinCharacters="2" DebounceMilliseconds="300"
                              Label="Person" @bind-Value="person">
                    <ItemTemplate Context="entry">
                        <img src="@entry.ImageSrc" alt="" /> @entry.Text
                    </ItemTemplate>
                </Autocomplete>

                @code {
                    private string? person;
                    private Task<IReadOnlyList<ListBoxItem<string>>> SearchPeopleAsync(
                        string query, CancellationToken token) => service.SearchAsync(query, token);
                }
                """),
            ["listbox-first"] = ("First option selected", """
                <ListBox TValue="string" Items="@categories" Label="Category"
                         InitialSelection="ListBoxInitialSelection.First"
                         @bind-Value="category" />

                @code {
                    private string? category;
                    private IReadOnlyList<ListBoxItem<string>> categories = [
                        new("design", "Design"),
                        new("development", "Development")
                    ];
                }
                """),
            ["listbox-search"] = ("Searchable list box", """
                <ListBox TValue="string" Items="@projects" Label="Project"
                         Search="true" Clearable="true"
                         CssClass="my-listbox" @bind-Value="project" />

                @code {
                    private string? project;
                    private IReadOnlyList<ListBoxItem<string>> projects = [
                        new("aurora", "Aurora", IconClass: "fa-solid fa-star"),
                        new("orbit", "Orbit"),
                        new("archive", "Archive", Enabled: false)
                    ];
                }

                /* Host CSS */
                .my-listbox { --bk-listbox-accent: #7556c9; }
                """),
            ["listbox-template"] = ("Custom list box results", """
                <ListBox TValue="string" Items="@people" Search="true"
                         Clearable="true" @bind-Value="person">
                    <ItemTemplate Context="entry">
                        <img src="@entry.ImageSrc" alt="" />
                        <span>@entry.Text</span>
                    </ItemTemplate>
                    <EmptyTemplate><p>No people match your search.</p></EmptyTemplate>
                </ListBox>

                @code {
                    private string? person;
                    private IReadOnlyList<ListBoxItem<string>> people = [
                        new("alex", "Alex Rivera", ImageSrc: "/images/alex.png"),
                        new("sam", "Sam Lee", ImageSrc: "/images/sam.png")
                    ];
                }
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
            ["table-detail"] = ("Expandable table with cell template", """
                <DataTable TItem="Session" Items="@sessions" Columns="@sessionColumns"
                    RowKey="session => session.Id" DetailTemplate="@SessionDetail"
                    Height="24rem" MobileBreakpoint="640" />

                @code {
                    private RenderFragment<Session> SessionDetail => session =>
                        @<DataTable TItem="Attendee" Items="@session.Attendees"
                            Columns="@attendeeColumns" RowKey="person => person.Id" />;

                    private RenderFragment<DataTableCellContext<Attendee>> PersonCell
                        => context => @<div class="person-cell">
                            <img src="/images/avatar-member.svg" alt="" />
                            <span>@context.Item.Name</span>
                        </div>;

                    private IReadOnlyList<DataTableColumn<Attendee>> attendeeColumns => [
                        new() { Key = "person", Header = "Member",
                            Value = person => person.Name, CellTemplate = PersonCell }
                    ];
                }
                """),
            ["table-paged"] = ("Paged and sortable table", """
                <DataTable TItem="Order" Items="@orders" Columns="@columns"
                    PageSizes="@(new[] { 5, 10, 20 })" RowKey="order => order.Id" />

                @code {
                    private IReadOnlyList<DataTableColumn<Order>> columns = [
                        new() { Key = "customer", Header = "Customer",
                            Value = order => order.Customer, Sortable = true },
                        new() { Key = "total", Header = "Total",
                            Value = order => order.Total,
                            Format = "C", Culture = System.Globalization.CultureInfo.GetCultureInfo("es-VE"),
                            Sortable = true,
                            MobilePrimary = false, Width = "8rem" }
                    ];
                }
                """),
            ["table-infinite"] = ("Incremental data table", """
                <DataTable TItem="Activity" ItemsProvider="LoadActivity"
                    Columns="@columns" Mode="DataTableLoadMode.Infinite"
                    PageSizes="@(new[] { 8 })" Height="20rem"
                    ShowHeader="false" RowKey="row => row.Id" />

                @code {
                    private async Task<DataTableResult<Activity>> LoadActivity(
                        DataTableRequest request, CancellationToken token)
                    {
                        var page = await repository.GetPageAsync(
                            request.Offset, request.Count, request.SortKey,
                            request.Descending, token);
                        return new(page.Items, page.TotalCount);
                    }
                }
                """),
            ["table-states"] = ("Loading and empty table states", """
                <DataTable TItem="Order" Items="@orders" Columns="@columns"
                           Loading="@isLoading" SkeletonRows="3">
                    <EmptyTemplate>
                        <div class="empty-orders">
                            <strong>No orders yet</strong>
                            <p>New orders will appear here.</p>
                        </div>
                    </EmptyTemplate>
                </DataTable>
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
            ["menu-fan"] = ("Fan action menu", """
                <ActionMenu Mode="ActionMenuMode.Fan" Items="@actions"
                            AriaLabel="Quick actions" OnSelect="HandleAction" />

                @code {
                    private IReadOnlyList<ActionMenuItem> actions = [
                        new("edit", "Edit", IconClass: "fa-solid fa-pen"),
                        new("share", "Share", IconClass: "fa-solid fa-share"),
                        new("save", "Save", IconClass: "fa-solid fa-floppy-disk")
                    ];
                    private void HandleAction(string key) { }
                }
                """),
            ["menu-dropdown"] = ("Dropdown action menu", """
                <ActionMenu Mode="ActionMenuMode.Dropdown" Items="@actions"
                            TriggerText="Menu" TriggerIconClass="fa-solid fa-bars"
                            OnSelect="HandleAction" />
                """),
            ["progress-linear"] = ("Linear progress", """
                <ProgressIndicator Value="@progress" Label="Uploading"
                                   CompletedText="Complete"
                                   CssClass="my-progress" />

                /* Host CSS */
                .my-progress {
                    --bk-progress-start: #7159ed;
                    --bk-progress-end: #20c4e2;
                    --bk-progress-glow: #20c4e2a8;
                }

                @code { private double progress = 45; }
                """),
            ["progress-ring"] = ("Download progress", """
                <ProgressIndicator Mode="ProgressMode.Ring" Value="@downloadPercent"
                                   IsRunning="@downloading" Label="photo.zip"
                                   CompletedText="Downloaded" />

                @code {
                    private double downloadPercent;
                    private bool downloading;
                    // Update downloadPercent from your download operation.
                }
                """),
            ["progress-checklist"] = ("Checklist progress ring", """
                <ProgressIndicator Mode="ProgressMode.Ring" Items="@tasks"
                                   OnTaskChanged="ChangeTask" ShowDone="true" Label="Tasks"
                                   CompletedText="All done" />

                @code {
                    private IReadOnlyList<ProgressTaskItem> tasks = [
                        new("design", "Design"), new("build", "Build"),
                        new("ship", "Ship")
                    ];
                    private void ChangeTask(ProgressTaskChange change) =>
                        tasks = tasks.Select(item => item.Key == change.Key
                            ? item with { IsComplete = change.IsComplete }
                            : item).ToArray();
                }
                """),
            ["toast"] = ("Toast feedback", """
                @inject ToastService Toasts

                <button type="button" @onclick="Show">Save</button>
                <ToastHost />

                @code {
                    private void Show() => Toasts.Show(new ToastOptions {
                        State = ToastState.Success,
                        Message = "Changes saved",
                        ActionText = "Undo",
                        OnAction = () => Task.CompletedTask
                    });
                }
                """),
            ["tabs-basic"] = ("Basic tabs", """
                <TabControl Items="@tabs" @bind-ActiveKey="activeTab"
                            KeepPanelsMounted="true">
                    <Content Context="key">
                        @if (key == "overview") { <p>Overview content</p> }
                        else if (key == "activity") { <p>Activity content</p> }
                    </Content>
                </TabControl>

                @code {
                    private string activeTab = "overview";
                    private IReadOnlyList<TabOption> tabs = [
                        new("overview", "Overview"),
                        new("activity", "Activity"),
                        new("billing", "Billing", Enabled: false)
                    ];
                }
                """),
            ["switch-liquid"] = ("Liquid switch", """
                <Switch Mode="SwitchMode.Liquid" Label="Notifications"
                        @bind-Checked="notifications" />

                @code { private bool notifications; }
                """),
            ["switch-morph"] = ("Icon morph switch", """
                <Switch Mode="SwitchMode.IconMorph" Label="Automatic sync"
                        @bind-Checked="autoSync" />

                @code { private bool autoSync; }
                """),
            ["switch-checkbox"] = ("Animated checkbox", """
                <Switch Mode="SwitchMode.Checkbox" Label="Remember me"
                        @bind-Checked="remember" />

                @code { private bool remember; }
                """),
            ["switch-segmented"] = ("Segmented switch", """
                <Switch Mode="SwitchMode.Segmented" Items="@plans"
                        AriaLabel="Billing period" @bind-SelectedKey="period"
                        CssClass="billing-switch" />

                @code {
                    private string period = "monthly";
                    private IReadOnlyList<SwitchOption> plans = [
                        new("weekly", "Weekly"),
                        new("monthly", "Monthly"),
                        new("yearly", "Yearly")
                    ];
                }

                /* Host CSS */
                .billing-switch { --bk-switch-on: #6c4bc5; }
                """),
            ["tabs-custom"] = ("Custom tabs", """
                <TabControl Items="@tabs" CssClass="my-tabs"
                            @bind-ActiveKey="activeTab">
                    <HeaderActions><button type="button">Add</button></HeaderActions>
                    <Content Context="key"><p>@key content</p></Content>
                </TabControl>

                /* Host CSS */
                .my-tabs {
                    --bk-tabs-accent: #8061db;
                    --bk-tabs-active-background: #6c4bc5;
                    --bk-tabs-active-text: #fff;
                }

                @code {
                    private string activeTab = "files";
                    private IReadOnlyList<TabOption> tabs = [
                        new("files", "Files", IconClass: "fa-solid fa-folder"),
                        new("settings", "Settings", IconClass: "fa-solid fa-gear")
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
            ["dialog-info"] = ("Information dialog", """
                <Dialog Title="Update available" Message="A new version is ready."
                        Mode="DialogMode.Information" Animation="DialogAnimation.TopDrop"
                        @bind-IsOpen="infoOpen" />
                """),
            ["dialog-danger"] = ("Async confirmation", """
                <Dialog Title="Delete item?" Message="This action cannot be undone."
                        Mode="DialogMode.Danger" Animation="DialogAnimation.Swing"
                        ConfirmText="Delete" ProcessingText="Deleting…"
                        OnConfirm="DeleteAsync" @bind-IsOpen="deleteOpen" />
                """),
            ["dialog-custom"] = ("Custom dialog", """
                <Dialog @ref="dialog" Title="Details" Mode="DialogMode.Custom"
                        Animation="DialogAnimation.Blur" @bind-IsOpen="isOpen">
                    <ContentTemplate><p>Your custom content.</p></ContentTemplate>
                    <FooterTemplate>
                        <button type="button" @onclick="() => dialog.CloseAsync()">Close</button>
                    </FooterTemplate>
                </Dialog>
                """),
            ["dialog-states"] = ("Dialog states", """
                <Dialog Title="Check your details" Mode="DialogMode.Question"
                        Animation="DialogAnimation.Grow" @bind-IsOpen="questionOpen" />
                <Dialog Title="Connection lost" Mode="DialogMode.Warning"
                        Animation="DialogAnimation.Unfold" @bind-IsOpen="warningOpen" />
                <Dialog Title="Save failed" Mode="DialogMode.Error"
                        Animation="DialogAnimation.Grow" @bind-IsOpen="errorOpen" />
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
