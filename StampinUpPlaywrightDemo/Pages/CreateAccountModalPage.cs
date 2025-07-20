
using Microsoft.Playwright;

namespace StampinUpPlaywrightDemo.Pages
{
    public class CreateAccountModalPage
    {
        private readonly IPage _page;

        public CreateAccountModalPage(IPage page)
        {
            _page = page;
        }

        // === FORM CONTAINER ELEMENTS ===
        public ILocator AuthenticationModal => _page.Locator("[data-testid='authentication']");
        public ILocator RegistrationModal => _page.Locator("[data-testid='registration']");
        public ILocator Form => _page.Locator("[data-testid='form-reg']");
        public ILocator CloseDialogButton => _page.Locator("[data-testid='close-dialog']");
        public ILocator ConfirmDialog => _page.Locator("[data-testid='confirm-dialog']");

        // === INPUT FIELDS ===
        public ILocator FirstNameInput => _page.Locator("[data-testid='reg-first-name']");
        public ILocator LastNameInput => _page.Locator("[data-testid='reg-last-name']");
        public ILocator EmailInput => _page.Locator("[data-testid='reg-email']");
        public ILocator PasswordInput => _page.Locator("[data-testid='reg-password']");
        public ILocator ConfirmPasswordInput => _page.Locator("[data-testid='reg-password-confirmation']");

        // === BUTTONS ===
        public ILocator CreateAccountButton => _page.Locator("[data-testid='btn-create-account']");
        public ILocator SubmitButton => _page.Locator("[data-testid='reg-submit']");
        public ILocator SignInInsteadButton => _page.Locator("[data-testid='reg-btn-sign-in']");

        // === MISC ===
        public ILocator Chat => _page.Locator("[data-testid='chatDiv']");
        public ILocator DemoHighlight => _page.Locator("[data-testid='demo-highlight']");
        public ILocator Nav => _page.Locator("[data-testid='nav']");
        public ILocator NameLabel => _page.Locator("[data-testid='name']");

        public async Task VerifyCreateAccountModalAsync()
        {
            Console.WriteLine("Start VerifyCreateAccountModalAsync");

            // Modal container
            await BaseTest.ElementToBeVisibleAsync(AuthenticationModal);
            await BaseTest.ElementToBeVisibleAsync(RegistrationModal);
            await BaseTest.ElementToBeVisibleAsync(Form);
            await BaseTest.ElementToBeVisibleAsync(CloseDialogButton);

            // Input fields
            await BaseTest.ElementToBeVisibleAsync(FirstNameInput);
            await BaseTest.ElementToBeVisibleAsync(LastNameInput);
            await BaseTest.ElementToBeVisibleAsync(EmailInput);
            await BaseTest.ElementToBeVisibleAsync(PasswordInput);
            await BaseTest.ElementToBeVisibleAsync(ConfirmPasswordInput);

            // Buttons
            await BaseTest.ElementToBeVisibleAsync(CreateAccountButton);
            await BaseTest.ElementToBeVisibleAsync(SubmitButton);
            await BaseTest.ElementToBeVisibleAsync(SignInInsteadButton);

            Console.WriteLine("End VerifyCreateAccountModalAsync");
        }

        // === Example Action ===
        public async Task FillOutAndSubmitRegistrationAsync(string firstName, string lastName, string email, string password)
        {
            await FirstNameInput.FillAsync(firstName);
            await LastNameInput.FillAsync(lastName);
            await EmailInput.FillAsync(email);
            await PasswordInput.FillAsync(password);
            await ConfirmPasswordInput.FillAsync(password);
            await SubmitButton.ClickAsync();
        }
    }
}