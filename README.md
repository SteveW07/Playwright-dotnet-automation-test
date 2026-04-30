# Playwright .NET Automation Test

This project contains an automated UI test for the Snipe-IT demo site using .NET, Playwright, and xUnit.

## Test Scenario

The test automates the following workflow:

1. Login to the snipeit demo at https://demo.snipeitapp.com/login
2. Create a new Macbook Pro 13" asset with the ready to deploy status and checked out to a random user
3. Find the asset you just created in the assets list to verify it was created successfully
4. Navigate to the asset page from the list and validate relevant details from the asset creation
5. Validate the details in the "History" tab on the asset page

## Tech Stack

- .NET 10
- Playwright for .NET
- xUnit

## Project Structure

```text
PlaywrightTests/
  Pages/      Page Object classes
  Helpers/    Shared helper methods
  Models/     Result models used by tests
