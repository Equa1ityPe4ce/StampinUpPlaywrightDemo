
using Microsoft.Playwright;

namespace StampinUpPlaywrightDemo.Pages
{
    public class FooterPage
    {
        private readonly IPage _page;

        public FooterPage(IPage page)
        {
            _page = page;
        }

        // === LEGAL LINKS ===
        public ILocator TermsLink => _page.Locator("[data-testid='terms']");
        public ILocator PrivacyPolicyLink => _page.Locator("[data-testid='privacy-policy']");
        public ILocator SupplyChainLink => _page.Locator("[data-testid='ca-supply-chain']");
        public ILocator LegalFooterNa => _page.Locator("[data-testid='LegalFooterNa']");

        // === SOCIAL ICONS ===
        public ILocator FacebookIcon => _page.Locator("[data-testid='icon-facebook']");
        public ILocator InstagramIcon => _page.Locator("[data-testid='icon-instagram']");
        public ILocator PinterestIcon => _page.Locator("[data-testid='icon-pinterest']");
        public ILocator YouTubeIcon => _page.Locator("[data-testid='icon-youtube']");

        // === GENERAL CONTENT ===
        public ILocator Label => _page.Locator("[data-testid='label']");
        public ILocator Content => _page.Locator("[data-testid='content']");

        public async Task VerifyFooterAsync()
        {
            Console.WriteLine("Start VerifyFooterAsync");

            // Legal links
            await BaseTest.ElementToBeVisibleAsync(TermsLink);
            await BaseTest.ElementToBeVisibleAsync(PrivacyPolicyLink);
            await BaseTest.ElementToBeVisibleAsync(SupplyChainLink);
            await BaseTest.ElementToBeVisibleAsync(LegalFooterNa);

            // Social icons
            await BaseTest.ElementToBeVisibleAsync(FacebookIcon);
            await BaseTest.ElementToBeVisibleAsync(InstagramIcon);
            await BaseTest.ElementToBeVisibleAsync(PinterestIcon);
            await BaseTest.ElementToBeVisibleAsync(YouTubeIcon);

            // General content
            await BaseTest.ElementToBeVisibleAsync(Label);
            await BaseTest.ElementToBeVisibleAsync(Content);

            Console.WriteLine("End VerifyFooterAsync");
        }
    }
}