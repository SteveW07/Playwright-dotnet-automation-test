using Microsoft.Playwright;

namespace PlaywrightTests.Helpers;

public class Select2Helper 
{
    private readonly IPage _page;
    private string optionTextSelector = "div.clearfix > div:last-child";

    public Select2Helper(IPage page)
    {
        _page = page;
    }

    //function for randomly selection with option text returned
    public async Task<string> SelectRandomOptionAsync(ILocator dropdown) 
    {
        await dropdown.ClickAsync();
        var options = _page.GetByRole(AriaRole.Option);

        var count = await options.CountAsync();
        var selectedOption = options.Nth(Random.Shared.Next(count));
        var selectedText = (await selectedOption.Locator(optionTextSelector).InnerTextAsync()).Trim();

        await selectedOption.ClickAsync();

        return selectedText;
    }
}