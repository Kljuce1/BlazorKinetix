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
