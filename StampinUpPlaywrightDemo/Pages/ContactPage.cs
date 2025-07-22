
using Microsoft.Playwright;
using StampinUpPlaywrightDemo.Enums;

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
        public ILocator EmailCard => _page.Locator("[data-testid='account-label-content'] span");
        public ILocator PhoneCard => _page.Locator("[data-testid='account-card-phone']");
        public ILocator CountryCard => _page.Locator("[data-testid='account-card-country']");
        public ILocator PasswordCard => _page.Locator("[data-testid='account-card-password']");
        public ILocator AccountContactCard => _page.Locator("[data-testid='account-card-contact']");

        // == Error Message ===
        public ILocator FirstNameRequiredError => _page.Locator("div.v-messages__message", new() { HasTextString = "The First Name field is required." });
        public ILocator EmailRequiredError => _page.Locator("div.v-messages__message", new() { HasTextString = "The Email Address field is required." });
        public ILocator ConfirmPasswordMismatchError => _page.Locator("div.v-messages__message", new() { HasTextString = "The Password field confirmation does not match." });
        public ILocator PhoneNumberLengthError => _page.Locator("div.v-messages__message", new() { HasTextString = "The Phone Number field must be at least 10 characters long." });
        public ILocator InvalidEmailError => _page.Locator("div.v-messages__message", new() { HasTextString = "The Email Address field must be a valid email" });
        public ILocator AllPasswordRequiredErrors => _page.Locator("div.v-messages__message", new() { HasTextString = "The Password field is required." });
        public ILocator NewPasswordRequiredError => AllPasswordRequiredErrors.Nth(0);
        public ILocator ConfirmPasswordRequiredError => AllPasswordRequiredErrors.Nth(1);

        // === EDITING FORM ===
        public ILocator ContactEditButton => _page.Locator("[data-testid='account-card-contact'] [data-testid='edit-contact-setting']");
        public ILocator PasswordEditButton => _page.Locator("[data-testid='account-card-password'] [data-testid='edit-contact-setting']");
        public ILocator CountryEditButton => _page.Locator("[data-testid='account-card-country'] [data-testid='edit-contact-setting']");
        public ILocator EditingSlot => _page.Locator("[data-testid='editingSlot']");
        public ILocator SaveChangesButton => _page.Locator("[data-testid='save-changes']");
        public ILocator CancelChangesButton => _page.Locator("[data-testid='cancel-changes']");
        public ILocator BirthdayDatePicker => _page.Locator("[data-testid='birthday-date-picker']");
        public ILocator SecondaryLanguageSelect => _page.Locator("[data-testid='secondaryLanguage']");
        public ILocator ObserverForm => _page.Locator("[data-testid='observer-form']");
        public ILocator AccountLabelContent => _page.Locator("[data-testid='account-label-content']");
        public ILocator CurrentPasswordInput => _page.Locator("input[data-testid='current-password']");
        public ILocator NewPasswordInput => _page.Locator("label:text-is('New Password')").Locator("xpath=following-sibling::input");

        public ILocator ConfirmNewPasswordInput => _page.Locator("label:text-is('Confirm New Password')").Locator("xpath=following-sibling::input");

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

        public ILocator FirstNameInput => _page.Locator("[data-testid='account-card-firstName']");
        public ILocator LastNameInput => _page.Locator("[data-testid='account-card-lastName']");
        public ILocator EmailInput => _page.Locator("[data-testid='account-card-email']");
        public ILocator PhoneInput => _page.Locator("[data-testid='account-card-phone']");
        public ILocator PreferredContactDropdownDisplay => _page.Locator("div.v-select__selection");
        public ILocator PreferredContactOptions => _page.Locator("div.v-list-item__title");

        public ILocator BirthdayInput => _page.Locator("[data-testid='birthday-date-picker']");

        public ILocator GetContactFieldValueSpan(string label)
        {
            return _page.Locator($"//div[normalize-space()='{label}:']/following-sibling::div[@data-testid='account-label-content']/span");
        }

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
            await BaseTest.ElementToBeVisibleAsync(ContactEditButton);
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
            await ContactEditButton.ClickAsync();
            await BirthdayDatePicker.FillAsync(newDate);
            await SaveChangesButton.ClickAsync();
        }

        internal async Task VerifyContactFieldsAreInEditMode()
        {
            Console.WriteLine("Start VerifyContactFieldsAreInEditMode");

            await BaseTest.ElementToBeVisibleAsync(FirstNameInput);
            await BaseTest.ElementToBeVisibleAsync(LastNameInput);
            await BaseTest.ElementToBeVisibleAsync(EmailInput);
            await BaseTest.ElementToBeVisibleAsync(PhoneInput);
            await BaseTest.ElementToBeVisibleAsync(PreferredContactDropdownDisplay);
            await BaseTest.ElementToBeVisibleAsync(BirthdayInput);

            Console.WriteLine("End VerifyContactFieldsAreInEditMode");
        }

        public async Task EditContactFieldAsync(ContactField field, string newValue)
        {
            Console.WriteLine($"Start EditContactFieldAsync: {field}");

            switch (field)
            {
                case ContactField.FIRST_NAME:
                    await BaseTest.ElementClearAndSendTextToAsync(FirstNameInput, newValue, "First Name");
                    break;

                case ContactField.LAST_NAME:
                    await BaseTest.ElementClearAndSendTextToAsync(LastNameInput, newValue, "Last Name");
                    break;

                case ContactField.EMAIL:
                    await BaseTest.ElementClearAndSendTextToAsync(EmailInput, newValue, "Email");
                    break;

                case ContactField.PHONE:
                    await BaseTest.ElementClearAndSendTextToAsync(PhoneInput, newValue, "Phone Number");
                    break;

                case ContactField.PREFERRED_CONTACT_METHOD:
                    var values = Enum.GetValues(typeof(PreferredContactMethod)).Cast<PreferredContactMethod>().ToList();

                    //filter out None
                    values.Remove(PreferredContactMethod.NONE_SELECTED);

                    var random = new Random();
                    var randomMethod = values[random.Next(values.Count)];

                    await SelectPreferredContactAsync(randomMethod);
                    break;

                case ContactField.BIRTHDAY:
                    // TO Save time we will skip this section
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(field), $"Unhandled contact field: {field}");
            }

            Console.WriteLine($"End EditContactFieldAsync: {field}");
        }

        public async Task SelectPreferredContactAsync(PreferredContactMethod method)
        {
            Console.WriteLine("Start SelectPreferredContactAsync");

            // Click to open the dropdown
            await BaseTest.ElementClickAsync(PreferredContactDropdownDisplay, "Preferred Contact Dropdown");

            // Match the correct text
            string optionText = method switch
            {
                PreferredContactMethod.NONE_SELECTED => "None Selected",
                PreferredContactMethod.EMAIL => "Email",
                PreferredContactMethod.PHONE_CALL => "Phone Call",
                PreferredContactMethod.TEXT_MESSAGE => "Text Message",
                _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
            };

            var option = PreferredContactOptions.Filter(new() { HasTextString = optionText });
            await BaseTest.ElementClickAsync(option, $"Preferred Contact Option: {optionText}");

            Console.WriteLine("End SelectPreferredContactAsync");
        }

    }
}