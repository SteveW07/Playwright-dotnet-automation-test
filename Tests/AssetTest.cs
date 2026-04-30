using Microsoft.Playwright.Xunit;
using PlaywrightTests.Pages;

namespace PlaywrightTests;

public class AssetTest : PageTest
{
    private const string URL = "https://demo.snipeitapp.com/login";
    private const string Username = "admin";
    private const string Password = "password";    

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await Page.GotoAsync(URL);
    }

    private const string Model = "Macbook Pro 13";
    private const string Status = "Ready to Deploy";

    [Fact]
    public async Task AddOneAssetTest()
    {
        
        //Login to the snipeit demo at https://demo.snipeitapp.com/login        
        var LoginPage = new LoginPage(Page);
        await LoginPage.LoginAsync(Username, Password);

        //Create a new Macbook Pro 13" asset with the ready to deploy status and checked out to a random user
        var AssetsPage = new AssetsPage(Page);
        await AssetsPage.NavigateToCreateNewAssetAsync();
        var createdAssets = await AssetsPage.AddOneNewAssetWithRandomCompanyAndUserAsync(Model, Status);

        //Find the asset you just created in the assets list to verify it was created successfully
        await AssetsPage.SearchForAssetAsync(createdAssets.AssetTag);
        await AssetsPage.ExpectAssetSearchResultAsync(Model);

        //Navigate to the asset page from the list and validate relevant details from the asset creation
        await AssetsPage.NavigateToSearchDetailAsync(createdAssets.AssetTag);

        //Validate the details in the "History" tab on the asset page
        await AssetsPage.ExpectAssetDetailAsync(Status, createdAssets.SelectedUser, createdAssets.SelectedCompany);
    }
}