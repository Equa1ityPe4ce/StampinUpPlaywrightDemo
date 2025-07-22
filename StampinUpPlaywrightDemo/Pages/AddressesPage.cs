using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace StampinUpPlaywrightDemo.Pages
{
    public class AddressesPage
    {
        private readonly IPage _page;

        public AddressesPage(IPage page)
        {
            _page = page;
        }

        // === ADDRESS FORM FIELDS ===
        public ILocator AddressLine1Input => _page.Locator("[data-testid='address.addressLine1']");
        public ILocator AddressLine2Input => _page.Locator("[data-testid='address-field-addressLine2']");
        public ILocator CityInput => _page.Locator("[data-testid='address-field-city']");
        public ILocator PostalCodeInput => _page.Locator("[data-testid='address-field-postalCode']");
        public ILocator RegionContainer => _page.Locator("[data-testid='address-field-region-container']");
        public ILocator FirstNameInput => _page.Locator("[data-testid='address-field-first-name']");
        public ILocator LastNameInput => _page.Locator("[data-testid='address-field-last-name']");
        public ILocator PhoneInput => _page.Locator("[data-testid='address-telephone']");
        public ILocator MailingAddressCheckbox => _page.Locator("[data-testid='mailing-address']");
        public ILocator MailingAddressDefaultCheckbox => _page.Locator("label[for='input-1336']");
        public ILocator ShippingAddressDefaultCheckbox => _page.GetByText("Make this my default shipping address", new() { Exact = true });

        public ILocator HiddenAutofill => _page.Locator("[data-testid='hidden-state-autofill']");
        public ILocator AutocompleteField => _page.Locator("[data-testid='autocomplete-field-div']");
        public ILocator StateField => _page.Locator("data-testid='autocomplete-field-div'");


        // === ADDRESS ACTIONS ===
        public ILocator SaveButton => _page.Locator("[data-testid='address-save']");
        public ILocator CancelButton => _page.Locator("[data-testid='cancelButton']");
        public ILocator CreateButton => _page.Locator("[data-testid='btn-create']");
        public ILocator BackButton => _page.Locator("[data-testid='back-btn']");

        // === ADDRESS LIST ITEMS ===
        public ILocator AddressListForm => _page.Locator("[data-testid='address-form']");
        public ILocator AddressListItem => _page.Locator("[data-testid='address-list-item']");
        public ILocator AddressListDefault => _page.Locator("[data-testid='address-list-default']");
        public ILocator AddressListNonDefault => _page.Locator("[data-testid='address-list-nondefault']");
        public ILocator ShippingTitle => _page.Locator("[data-testid='address-list-default'] [data-testid='addresslist-item-title']");
        public ILocator MailingTitle => _page.Locator("[data-testid='mailing-address'] [data-testid='addresslist-item-title']");

        public ILocator ShippingPhone => _page.Locator("[data-testid='address-list-default'] [data-testid='addresslist-item-phone']");
        public ILocator MailingPhone => _page.Locator("[data-testid='mailing-address'] [data-testid='addresslist-item-phone']");

        public ILocator ShippingCountry => _page.Locator("[data-testid='address-list-default'] [data-testid='addresslist-item-country']");
        public ILocator MailingCountry => _page.Locator("[data-testid='mailing-address'] [data-testid='addresslist-item-country']");
        public ILocator ShippingEditButton => _page.Locator("[data-testid='address-list-default'] [data-testid='addresslist-item-btn-edit']");
        public ILocator MailingEditButton => _page.Locator("[data-testid='mailing-address'] [data-testid='addresslist-item-btn-edit']");

        // Mailing Address Block
        public ILocator MailingAddressBlock => _page.Locator("[data-testid='mailing-address']");
        public ILocator MailingNameRow => MailingAddressBlock.Locator("[data-testid='addresslist-row-0']").Nth(1);
        public ILocator MailingStreetRow => MailingAddressBlock.Locator("[data-testid='addresslist-row-1']").Nth(1);
        public ILocator MailingCityZipRow => MailingAddressBlock.Locator("[data-testid='addresslist-row-2']").Nth(1);

        // Shipping Address Block
        public ILocator ShippingAddressBlock => _page.Locator("[data-testid='shipping-address']");
        public ILocator ShippingNameRow => ShippingAddressBlock.Locator("[data-testid='addresslist-row-0']").First;
        public ILocator ShippingStreetRow => ShippingAddressBlock.Locator("[data-testid='addresslist-row-1']").First;
        public ILocator ShippingCityZipRow => ShippingAddressBlock.Locator("[data-testid='addresslist-row-2']").First;

        // Default Address Block (if needed)
        public ILocator DefaultAddressBlock => _page.Locator("[data-testid='address-list-default']");
        public ILocator DefaultNameRow => DefaultAddressBlock.Locator("[data-testid='addresslist-row-0']");
        public ILocator DefaultStreetRow => DefaultAddressBlock.Locator("[data-testid='addresslist-row-1']");
        public ILocator DefaultCityZipRow => DefaultAddressBlock.Locator("[data-testid='addresslist-row-2']");

        // === NAVIGATION & MISC ===
        public ILocator Nav => _page.Locator("[data-testid='nav']");
        public ILocator AccountLink => _page.Locator("[data-testid='account-link']");
        public ILocator AccountNav => _page.Locator("[data-testid='account-nav']");
        public ILocator UserHeader => _page.Locator("[data-testid='user-header']");
        public ILocator LogoutButton => _page.Locator("[data-testid='auth-logout']");

        public ILocator Chat => _page.Locator("[data-testid='chatDiv']");
        public ILocator DemoHighlight => _page.Locator("[data-testid='demo-highlight']");
        public ILocator NameLabel => _page.Locator("[data-testid='name']");

        public async Task VerifyAddressPageAsync()
        {
            Console.WriteLine("Start VerifyAddressPageAsync");

            // Address list items
            await BaseTest.ElementToBeVisibleAsync(ShippingTitle);
            await BaseTest.ElementToBeVisibleAsync(MailingTitle);
            await BaseTest.ElementToBeVisibleAsync(MailingPhone);
            await BaseTest.ElementToBeVisibleAsync(ShippingPhone);

            Console.WriteLine("End VerifyAddressPageAsync");
        }

        public async Task VerifyAddressIsNowInEditMode()
        {
            Console.WriteLine("Start VerifyAddressPageAsync");

            // Address form fields
            await BaseTest.ElementToBeVisibleAsync(AddressLine1Input);
            await BaseTest.ElementToBeVisibleAsync(CityInput);
            await BaseTest.ElementToBeVisibleAsync(PostalCodeInput);
            await BaseTest.ElementToBeVisibleAsync(RegionContainer);
            await BaseTest.ElementToBeVisibleAsync(FirstNameInput);
            await BaseTest.ElementToBeVisibleAsync(LastNameInput);
            await BaseTest.ElementToBeVisibleAsync(PhoneInput);

            Console.WriteLine("End VerifyAddressPageAsync");
        }

        public async Task VerifyAddressPhoneNumber(string phoneNumber)
        {
            Console.WriteLine("Start VerifyAddressPhoneNumber");

            await BaseTest.ElementToBeVisibleAsync(ShippingPhone);
            await BaseTest.ElementToHaveTextAsync(ShippingPhone, phoneNumber);

            Console.WriteLine("End VerifyAddressPhoneNumber");
        }
    }
}