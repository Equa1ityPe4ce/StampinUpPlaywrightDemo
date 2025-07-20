using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using static System.Net.Mime.MediaTypeNames;
namespace StampinUpPlaywrightDemo.Pages
{
    public class SignInModalPage : BaseTest
    {
        private readonly IPage _page;

        public SignInModalPage(IPage page)
        {
            _page = page;
        }

        // === MODAL CONTAINERS ===
        public ILocator AuthenticationModal => _page.Locator("[data-testid='authentication']");
        public ILocator SignInForm => _page.Locator("[data-testid='form-sign-in']");
        public ILocator AuthForm => _page.Locator("[data-testid='form-auth']");
        public ILocator RegistrationForm => _page.Locator("[data-testid='form-reg']");
        public ILocator RegistrationSection => _page.Locator("[data-testid='registration']");
        public ILocator ConfirmDialog => _page.Locator("[data-testid='confirm-dialog']");
        public ILocator CloseDialog => _page.Locator("[data-testid='close-dialog']");

        // === SIGN-IN FIELDS ===
        public ILocator EmailInput => _page.Locator("[data-testid='auth-email']");
        public ILocator PasswordInput => _page.Locator("input[data-testid='auth-password']");
        public ILocator SubmitSignIn => _page.Locator("[data-testid='auth-submit']");

        // === REGISTRATION FIELDS ===
        public ILocator FirstNameInput => _page.Locator("[data-testid='reg-first-name']");
        public ILocator LastNameInput => _page.Locator("[data-testid='reg-last-name']");
        public ILocator RegEmailInput => _page.Locator("[data-testid='reg-email']");
        public ILocator RegPasswordInput => _page.Locator("[data-testid='reg-password']");
        public ILocator ConfirmPasswordInput => _page.Locator("[data-testid='reg-password-confirmation']");
        public ILocator SubmitRegistration => _page.Locator("[data-testid='reg-submit']");
        public ILocator CreateAccountButton => _page.Locator("[data-testid='btn-create-account']");
        public ILocator SwitchToSignInButton => _page.Locator("[data-testid='reg-btn-sign-in']");

        public async Task VerifySignInModalAsync()
        {
            Console.WriteLine("Start VerifySignInModalAsync");

            await BaseTest.ElementToBeVisibleAsync(AuthenticationModal);
            await BaseTest.ElementToBeVisibleAsync(SignInForm);
            await BaseTest.ElementToBeVisibleAsync(EmailInput);
            await BaseTest.ElementToBeVisibleAsync(PasswordInput);
            await BaseTest.ElementToBeVisibleAsync(SubmitSignIn);
            await BaseTest.ElementToBeVisibleAsync(CreateAccountButton);

            Console.WriteLine("End VerifySignInModalAsync");
        }

        public async Task SignInAsync(string email, string password)
        {
            // Wait for modal to appear
            await Expect(SignInForm).ToBeVisibleAsync();

            // Fill in the credentials
            await EmailInput.FillAsync(email);
            await PasswordInput.FillAsync(password);

            // Ensure password input is type="password"
            var inputType = await PasswordInput.GetAttributeAsync("type");
            if (inputType != "password")
            {
                throw new Exception("Password field is not masked properly.");
            }

            // Wait for the /account request to complete after sign-in
            var accountResponseTask = _page.WaitForResponseAsync(response => response.Url.Contains("/en-us/account") && response.Status == 200);

            // Click submit and wait for modal to close
            await SubmitSignIn.ClickAsync();

            var accountResponse = await accountResponseTask;
            // Get cookies from browser context
            BaseTest.SessionCookies = await _page.Context.CookiesAsync();

            await Expect(SignInForm).ToBeHiddenAsync();
        }


        public async Task StartCreateAccount()
        {
            await CreateAccountButton.ClickAsync();
        }
    }
}