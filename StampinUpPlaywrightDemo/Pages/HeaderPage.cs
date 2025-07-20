
using Microsoft.Playwright;
using StampinUpPlaywrightDemo.Enums;
using System.Reflection.PortableExecutable;
using static Microsoft.Playwright.Assertions;

namespace StampinUpPlaywrightDemo.Pages
{
    public class HeaderPage : BaseTest
    {
        private readonly IPage _page;

        public HeaderPage(IPage page)
        {
            _page = page;
        }

        // === USER & AUTHENTICATION ===
        public ILocator SignInButton => _page.Locator("[data-testid='menu-user-btn-signin']");
        public ILocator FirstNameButton => _page.Locator("[data-testid='menu-user-firstname']");
        public ILocator AuthDialog => _page.Locator("[data-testid='auth-dialog']");
        public ILocator MobileSignInButton => _page.Locator("[data-testid='mobile-nav-btn-signin-account']");
        public ILocator MobileGreeting => _page.Locator("[data-testid='mobile-nav-greet']");
        public ILocator MobileSignInNav => _page.Locator("[data-testid='mobile-nav-signin']");
        public ILocator VAvatar => _page.Locator("[data-testid='VAvatar']");

        // === Signed in User menu ===
        public ILocator AccountSettings => _page.Locator("a[href='/account/settings']");
        public ILocator Addresses => _page.Locator("a[href='/account/address']");
        public ILocator Payments => _page.Locator("a[href='/account/payment']");
        public ILocator Orders => _page.Locator("a[href='/account/orders']");
        public ILocator Lists => _page.Locator("a[href='/account/lists']");
        public ILocator Subscriptions => _page.Locator("a[href='/account/subscriptions']");
        public ILocator Demonstrator => _page.Locator("a[href='/account/demonstrator']");
        public ILocator Rewards => _page.Locator("a[href='/account/rewards']");
        public ILocator SignOut => _page.Locator("[data-testid='auth-logout']");

        // === CART ===
        public ILocator CartQtyContainer => _page.Locator("[data-testid='cart-qty-container']");
        public ILocator CartQty => _page.Locator("[data-testid='cart-qty']");

        // === NAVIGATION BAR ===
        public ILocator NavBar => _page.Locator("[data-testid='navbar']");
        public ILocator NavButton => _page.Locator("[data-testid='nav-btn']");
        public ILocator HamburgerMenu => _page.Locator("[data-testid='nav-hamburger']");
        public ILocator NavIcons => _page.Locator("[data-testid='navicons']");
        public ILocator NavDrawerClose => _page.Locator("[data-testid='nav-drawer-close']");

        // === SEARCH ===
        public ILocator SearchContainer => _page.Locator("[data-testid='search']");
        public ILocator SearchForm => _page.Locator("[data-testid='search-form']");
        public ILocator SearchIcon => _page.Locator("[data-testid='search-icon']");
        public ILocator SearchPlaceholder => _page.Locator("[data-testid='search-placeholder']");

        // === CATEGORIES ===
        public ILocator CategoryRoot => _page.Locator("[data-testid='category']");
        public ILocator CategoryBundles => _page.Locator("[data-testid='category-Bundles']");
        public ILocator CategoryInk => _page.Locator("[data-testid='category-Ink']");
        public ILocator CategoryKits => _page.Locator("[data-testid='category-Kits']");
        public ILocator CategoryOnlineExclusives => _page.Locator("[data-testid='category-Online Exclusives']");
        public ILocator CategoryPaper => _page.Locator("[data-testid='category-Paper']");
        public ILocator CategoryStamps => _page.Locator("[data-testid='category-Stamps']");
        public ILocator CategorySuites => _page.Locator("[data-testid='category-Suites']");
        public ILocator CategoryViewAll => _page.Locator("[data-testid='category-View All']");

        // === SIDE NAV ===
        public ILocator SideNavBlog => _page.Locator("[data-testid='side-nav-Blog']");
        public ILocator SideNavGathering => _page.Locator("[data-testid='side-nav-Gathering']");
        public ILocator SideNavJoin => _page.Locator("[data-testid='side-nav-Join']");
        public ILocator SideNavPaperPumpkin => _page.Locator("[data-testid='side-nav-Paper Pumpkin Subscription']");
        public ILocator SideNavShopBy => _page.Locator("[data-testid='side-nav-Shop By']");
        public ILocator SideNavShopProducts => _page.Locator("[data-testid='side-nav-Shop Products']");
        public ILocator SideNavSpecials => _page.Locator("[data-testid='side-nav-Specials']");
        public ILocator SideNavWhatsNew => _page.Locator("[data-testid='side-nav-What\\'s New']");

        // === DEMO + MISC ===
        public ILocator DesktopHeader => _page.Locator("[data-testid='desktop-header']");
        public ILocator HeaderDemonstrator => _page.Locator("[data-testid='header-demonstrator']");
        public ILocator FindDemonstrator => _page.Locator("[data-testid='find-demonstrator']");
        public ILocator DemoHighlight => _page.Locator("[data-testid='highlight-demo']");
        public ILocator CommunityJoinButton => _page.Locator("[data-testid='community-join-button']");
        public ILocator HeartIcon => _page.Locator("[data-testid='heart-icon']");

        public async Task NavigateToPage(NavigationEnum pageToNavigateTo)
        {
            //Verif user is logged in
            ElementToBeVisibleAsync(FirstNameButton);
            //open navigation menu
            ElementClickAsync(FirstNameButton);
            //verif menu options exist
            ElementToBeVisibleAsync(AccountSettings);
            ElementToBeVisibleAsync(Addresses);
            ElementToBeVisibleAsync(Payments);
            ElementToBeVisibleAsync(Orders);
            ElementToBeVisibleAsync(Lists);
            ElementToBeVisibleAsync(Subscriptions);
            ElementToBeVisibleAsync(Demonstrator);
            ElementToBeVisibleAsync(Rewards);
            ElementToBeVisibleAsync(SignOut);

            switch (pageToNavigateTo)
            {
                case NavigationEnum.ACCOUNT_SETTINGS:
                    await BaseTest.ElementClickAsync(AccountSettings);
                    break;
                case NavigationEnum.ADDRESSES:
                    await BaseTest.ElementClickAsync(Addresses);
                    break;
                case NavigationEnum.PAYMENT:
                    await BaseTest.ElementClickAsync(Payments);
                    break;
                case NavigationEnum.MY_ORDERS:
                    await BaseTest.ElementClickAsync(Orders);
                    break;
                case NavigationEnum.MY_LISTS:
                    await BaseTest.ElementClickAsync(Lists);
                    break;
                case NavigationEnum.SUBSCRIPTIONS:
                    await BaseTest.ElementClickAsync(Subscriptions);
                    break;
                case NavigationEnum.DEMONSTRATOR:
                    await BaseTest.ElementClickAsync(Demonstrator);
                    break;
                case NavigationEnum.REWARDS:
                    await BaseTest.ElementClickAsync(Rewards);
                    break;
                case NavigationEnum.SIGN_OUT:
                    await BaseTest.ElementClickAsync(SignOut);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pageToNavigateTo), pageToNavigateTo, "incorrect nav option provided");
            }
        }
        public async Task VerifyHeaderLoadedAsync()
        {
            Console.WriteLine("Start VerifyHeaderLoadedAsync");

            await BaseTest.ElementToBeVisibleAsync(DesktopHeader);
            await BaseTest.ElementToBeVisibleAsync(SignInButton);
            await BaseTest.ElementToBeVisibleAsync(SearchContainer);
            await BaseTest.ElementToBeVisibleAsync(NavBar);
            await BaseTest.ElementToBeVisibleAsync(CartQtyContainer);

            Console.WriteLine("End VerifyHeaderLoadedAsync");
        }

        public async Task navigateToAddressesPage()
        {
            Console.WriteLine("Start navigateToAddressesPage");

            await BaseTest.ElementClickAsync(FirstNameButton);

            await BaseTest.ElementToBeVisibleAsync(Addresses);
            await BaseTest.ElementClickAsync(Addresses);

            Console.WriteLine("End navigateToAddressesPage");
        }
    }
}