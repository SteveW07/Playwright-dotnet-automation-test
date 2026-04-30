using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using PlaywrightTests.Helpers;
using PlaywrightTests.Models;

namespace PlaywrightTests.Pages;

public class AssetsPage
{
    private readonly IPage _page;

    public AssetsPage(IPage page)
    {
        _page = page;
    }

    private ILocator CreateNewMenu => _page.GetByText("Create New");
    private ILocator CreateNewAssetOption => _page.GetByRole(AriaRole.Navigation).GetByText("Asset", new() { Exact = true });
    private ILocator SelectACompany => _page.GetByRole(AriaRole.Combobox, new() { Name = "Select Company" });
    private ILocator SelectAUser => _page.GetByRole(AriaRole.Combobox, new() { Name = "Select a User" });
    private ILocator SelectAModel => _page.GetByRole(AriaRole.Combobox, new() { Name = "Select a Model" });
    private ILocator SelectAStatus => _page.GetByLabel("Select Status").GetByText("Select Status");
    private ILocator AssetTag => _page.GetByRole(AriaRole.Textbox, new() { Name = "Asset Tag", Exact = true });
    private ILocator SaveButton => _page.GetByRole(AriaRole.Button, new() { Name = "Save" }).Nth(0);
    private ILocator SearchBox => _page.GetByRole(AriaRole.Searchbox, new() { Name = "Search" });
    private ILocator SecondColumnInTable => _page.GetByRole(AriaRole.Table).Locator("tbody tr td:nth-child(2)");
    

    public async Task NavigateToCreateNewAssetAsync()
    {
        await CreateNewMenu.ClickAsync();
        await CreateNewAssetOption.ClickAsync();
    }

    public async Task NavigateToSearchDetailAsync(string assetTag)
    {
        await SecondColumnInTable
            .Filter(new() { HasText = assetTag })
            .First
            .ClickAsync();
    }

    public async Task<CreatedAssetResults> AddOneNewAssetWithRandomCompanyAndUserAsync(string model, string status)
    {
        var select2Helper = new Select2Helper(_page);

        //select a random company, and record the company value
        var selectedCompany = await select2Helper.SelectRandomOptionAsync(
            SelectACompany);

        //select the model asset
        await SelectAModel.ClickAsync();
        await _page.GetByText(model).ClickAsync();
        await SelectAStatus.ClickAsync();
        await _page.GetByRole(AriaRole.Option, new() { Name = status }).ClickAsync();

        //select a random user to check out to
        var selectedText2 = await select2Helper.SelectRandomOptionAsync(
            SelectAUser);
        //record the user value selected
        var selectedUser = StringHelper.KeepFirstTwoWords(selectedText2);

        //record the asset tag value generated
        var assetTag = await AssetTag.InputValueAsync();

        await SaveButton.ClickAsync();

        //return all the recorded values
        return new CreatedAssetResults(
            SelectedCompany: selectedCompany,
            SelectedUser: selectedUser,
            AssetTag: assetTag
        );        
    }

    public async Task SearchForAssetAsync(string assetTag)
    {
        //fill in asset tag value and search
        await SearchBox.FillAsync(assetTag);
        await SearchBox.PressAsync("Enter");
    }

    public async Task ExpectAssetSearchResultAsync(string model)
    {
        //only showing one search result
        await Assertions.Expect(_page.GetByText("Showing 1 to 1 of 1 rows").First).ToBeVisibleAsync();
        //specified model is in search result
        await Assertions.Expect(_page.GetByText(model).First).ToBeVisibleAsync();
    }

    public async Task ExpectAssetDetailAsync(string status, string user, string company)
    {
        await Assertions.Expect(_page.Locator(".main-panel").GetByText(status)).ToBeVisibleAsync();
        await Assertions.Expect(_page.Locator(".main-panel").GetByRole(AriaRole.Link, new() { Name = user })).ToBeVisibleAsync();
        await Assertions.Expect(_page.Locator(".side-box").GetByRole(AriaRole.Link, new() { Name = company })).ToBeVisibleAsync();
    }
    
}