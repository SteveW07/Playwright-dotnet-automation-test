using Microsoft.Playwright;
// using Microsoft.Playwright.Xunit;
// using Xunit;
// using PlaywrightTests.Helpers;

namespace PlaywrightTests.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    private ILocator EmailInput => _page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
    private ILocator PasswordInput => _page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
    private ILocator LoginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Login" });

    public async Task LoginAsync(string email, string password)
    {
        await EmailInput.FillAsync(email);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }   
}