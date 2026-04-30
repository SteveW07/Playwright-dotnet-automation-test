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
```

## Prerequisites

- Latest .NET SDK
- PowerShell
- Git

## Setup

Clone the repository:

```bash
git clone https://github.com/SteveW07/Playwright-dotnet-automation-test.git
cd Playwright-dotnet-automation-test/PlaywrightTests
```

Restore dependencies:

```bash
dotnet restore
```

Build the project:

```bash
dotnet build
```

Install Playwright browsers:

```bash
pwsh bin/Debug/net10.0/playwright.ps1 install
```

## Run Tests

Run the test:

```bash
dotnet test
```

Run with detailed console output:

```bash
dotnet test --logger "console;verbosity=detailed"
```

Generate a TRX test result file:

```bash
dotnet test --logger "trx;LogFileName=test-results.trx"
```

## Design Notes

- Page Object Model is used to keep the test flow readable and separate page interactions from the test logic.
- Helper classes are used for reusable actions such as Select2 dropdown selection and string formatting.
- The test records the created asset tag, selected company, and selected user, then uses those values for validation.
