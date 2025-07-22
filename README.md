# StampinUpTests
This project contains Playwright + NUnit UI and API automation for testing key account creation and Onboarding flows on www.stampinup.com.

## Requirements

- .NET 8 SDK  
- Node.js (only if you want to modify or update Playwright)  
- Chrome/Edge installed (or install browser binaries via Playwright CLI)

## Running Tests

To run the tests locally:

```bash  
dotnet test
```

To run tests with Playwright browsers installed:

```bash  
playwright install  
dotnet test
```
To run a specific test class:

```bash  
dotnet test --filter "FullyQualifiedName~StampinUpTests.Tests.AccountTests"
```
To run with a results log:

```bash  
dotnet test --logger:"trx;LogFileName=TestResults.trx"
```
## Project Structure

- `Tests/`: Contains all automated test classes.  
- `Models/`: Shared data models (Account, Address, Contact).  
- `Pages/`: Page Object Model classes for the site (HomePage, HeaderPage, etc.).  
- `Helpers/`: Reusable logic, HTTP clients, data loading, etc.  
- `ScreenShots/`: Folder used for saving screenshots on test failures.  
- `Interfaces/`: Interface contracts (e.g., for page abstraction or mocks).  
- `Documentation/`: Any manual test plans, notes, or diagrams.  
- `userData.json`: Stores runtime data (e.g., created accounts, emails, addresses).  
- `playwright.config.ts`: Playwright config file (optional).

## Test Framework Details

- **Test Framework**: NUnit  
- **Runner**: Microsoft.NET.Test.Sdk  
- **Automation Library**: Microsoft.Playwright  
- **Test Adapter**: Microsoft.Playwright.NUnit, NUnit3TestAdapter

## NuGet Packages Used

- Microsoft.Playwright  
- Microsoft.Playwright.NUnit  
- Microsoft.Playwright.TestAdapter  
- Microsoft.NET.Test.Sdk  
- coverlet.collector  
- NUnit  
- NUnit.Analyzers  
- NUnit3TestAdapter

##  Notes

- Playwright browser binaries are not committed to source. Make sure to run `playwright install` before running tests the first time.  
- Due to time constraints we did not create a console app and convert tests to a dll for pre packaged running as i do not assume anyone will want to run some obscure exe / dll combo on their machines as a hey you should hire me package.
