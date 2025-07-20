using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace StampinUpPlaywrightDemo.Pages
{
    public class HomePage
    {
        private readonly IPage _page;
        HeaderPage header;
        SignInModalPage signInModalPage;

        public HomePage(IPage page)
        {
            _page = page;
            header = new(_page);
            signInModalPage = new(_page);
        }


        // === NAVIGATION & ACCOUNT ===
        public ILocator AccountLink => _page.Locator("[data-testid='account-link']");
        public ILocator LogoutButton => _page.Locator("[data-testid='auth-logout']");
        public ILocator NameLabel => _page.Locator("[data-testid='name']");

        // === PRODUCTS & INTERACTIONS ===
        public ILocator ProductCardLink => _page.Locator("[data-testid='product-card-link']");
        public ILocator ProductTitle => _page.Locator("[data-testid='product-title']");
        public ILocator ProductPrice => _page.Locator("[data-testid='product-formatted-price']");
        public ILocator AddToCartButton => _page.Locator("[data-testid='add-product-to-cart']");
        public ILocator AvailableProduct => _page.Locator("[data-testid='availableProduct']");

        // === WISHLIST & FAVORITES ===
        public ILocator WishlistButton => _page.Locator("[data-testid='wishlistButton']");
        public ILocator WishlistDialog => _page.Locator("[data-testid='dialog-wishlist']");
        public ILocator NewWishlist => _page.Locator("[data-testid='newWishlist']");
        public ILocator HeartIcon => _page.Locator("[data-testid='heart-icon']");
        public ILocator ListMenu => _page.Locator("[data-testid='listMenu']");

        // === SLIDER & CONTROLS ===
        public ILocator PlayPauseButton => _page.Locator("[data-testid='play-pause-btn']");
        public ILocator SlideSelector => _page.Locator("[data-testid='slide-selector-0']");
        public ILocator NextButton => _page.Locator("[data-testid='nextBtn']");
        public ILocator PrevButton => _page.Locator("[data-testid='prevBtn']");

        // === MISC ===
        public ILocator Chat => _page.Locator("[data-testid='chatDiv']");
        public ILocator DemoHighlight => _page.Locator("[data-testid='demo-highlight']");

        public async Task VerifyOnHomePageAsync()
        {
            Console.WriteLine("Start VerifyOnHomePageAsync");

            await BaseTest.ElementToBeVisibleAsync(ProductCardLink.First);
            await BaseTest.ElementToBeVisibleAsync(SlideSelector);

            Console.WriteLine("End VerifyOnHomePageAsync");
        }

        public async Task OpenSignInModalAsync()
        {
            await BaseTest.ElementToBeVisibleAsync(header.SignInButton);
            await BaseTest.ElementClickAsync(header.SignInButton);
            await BaseTest.ElementToBeVisibleAsync(signInModalPage.CreateAccountButton);
        }
    }
}