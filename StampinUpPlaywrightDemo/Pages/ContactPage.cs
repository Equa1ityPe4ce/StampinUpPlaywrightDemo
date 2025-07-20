
using Microsoft.Playwright;

namespace StampinUpPlaywrightDemo.Pages
{
    public class ContactPage
    {
        private readonly IPage _page;

        public ContactPage(IPage page)
        {
            _page = page;
        }

        // === CONTACT CARD DETAILS ===
        public ILocator FirstNameCard => _page.Locator("[data-testid='account-card-firstName']");
        public ILocator LastNameCard => _page.Locator("[data-testid='account-card-lastName']");
        public ILocator EmailCard => _page.Locator("[data-testid='account-card-email']");
        public ILocator PhoneCard => _page.Locator("[data-testid='account-card-phone']");
        public ILocator CountryCard => _page.Locator("[data-testid='account-card-country']");
        public ILocator PasswordCard => _page.Locator("[data-testid='account-card-password']");
        public ILocator AccountContactCard => _page.Locator("[data-testid='account-card-contact']");

        // === EDITING FORM ===
        public ILocator EditContactButton => _page.Locator("[data-testid='edit-contact-setting']");
        public ILocator EditingSlot => _page.Locator("[data-testid='editingSlot']");
        public ILocator SaveChangesButton => _page.Locator("[data-testid='save-changes']");
        public ILocator CancelChangesButton => _page.Locator("[data-testid='cancel-changes']");
        public ILocator BirthdayDatePicker => _page.Locator("[data-testid='birthday-date-picker']");
        public ILocator SecondaryLanguageSelect => _page.Locator("[data-testid='secondaryLanguage']");
        public ILocator ObserverForm => _page.Locator("[data-testid='observer-form']");
        public ILocator AccountLabelContent => _page.Locator("[data-testid='account-label-content']");

        // === NAVIGATION ===
        public ILocator AccountLink => _page.Locator("[data-testid='account-link']");
        public ILocator AccountNav => _page.Locator("[data-testid='account-nav']");
        public ILocator Nav => _page.Locator("[data-testid='nav']");
        public ILocator UserHeader => _page.Locator("[data-testid='user-header']");
        public ILocator LogoutButton => _page.Locator("[data-testid='auth-logout']");

        // === MISC ===
        public ILocator Chat => _page.Locator("[data-testid='chatDiv']");
        public ILocator DemoHighlight => _page.Locator("[data-testid='demo-highlight']");
        public ILocator NameLabel => _page.Locator("[data-testid='name']");

        public async Task VerifyContactPageAsync()
        {
            Console.WriteLine("Start VerifyContactPageAsync");

            // Contact card details
            await BaseTest.ElementToBeVisibleAsync(FirstNameCard);
            await BaseTest.ElementToBeVisibleAsync(LastNameCard);
            await BaseTest.ElementToBeVisibleAsync(EmailCard);
            await BaseTest.ElementToBeVisibleAsync(PhoneCard);
            await BaseTest.ElementToBeVisibleAsync(CountryCard);
            await BaseTest.ElementToBeVisibleAsync(PasswordCard);
            await BaseTest.ElementToBeVisibleAsync(AccountContactCard);

            // Editing form
            await BaseTest.ElementToBeVisibleAsync(EditContactButton);
            await BaseTest.ElementToBeVisibleAsync(AccountLabelContent);

            // Navigation
            await BaseTest.ElementToBeVisibleAsync(AccountLink);
            await BaseTest.ElementToBeVisibleAsync(AccountNav);
            await BaseTest.ElementToBeVisibleAsync(UserHeader);
            await BaseTest.ElementToBeVisibleAsync(LogoutButton);

            Console.WriteLine("End VerifyContactPageAsync");
        }

        // === Example Action ===
        public async Task ClickEditAndChangeBirthdayAsync(string newDate)
        {
            await EditContactButton.ClickAsync();
            await BirthdayDatePicker.FillAsync(newDate);
            await SaveChangesButton.ClickAsync();
        }
    }
}