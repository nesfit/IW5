# API, UI testing and mocking

---

## General

---

### Testing pyramid

- Manual/Automated UI
  - **All layers**
- API
  - **Multiple classes and layers**
- Integration
  - Several classes working together
- Unit
  - Individual units of source code

[Testing Pyramid : How to jumpstart Test Automation](https://www.browserstack.com/guide/testing-pyramid-for-test-automation)

---

## API testing (integration)

---

### API testing

- What is the purpose?
- We will be testing REST API.
- Mainly 4 methods involved in API Testing:
  - GET
  - POST
  - PUT
  - DELETE

---

### Tools for testing Rest API

- Postman.
- [cURL](https://curl.haxx.se) - command line tool and library
for transferring data with URLs.
- Unit testing framework + HTTP Client (e.g. [RestSharp](https://restsharp.dev)).

---

### Postman Testing

- [Writing tests](https://learning.postman.com/docs/writing-scripts/test-scripts/)
- [Automated Testing with Postman](https://www.postman.com/automated-testing/)
- [Chai Assertion Library](https://www.chaijs.com)

---

### Debugging with the Postman console

- To log a message in the console use:

```js
console.log("Test message");
```

- You can inspect sent requests directly in Postman's console. They are logged in both its raw and pretty form.

[Source](https://blog.postman.com/powerful-debugging-with-the-postman-console)

---

### API testing code examples

- Labs/Exercise-03-after/**sln** and Labs/Exercise-03-after/assets/Postman folders.

---

## Mocking

---

### What is a mock objects

"In a unit test, mock objects can simulate the behavior of complex, real objects and are therefore useful when a real object is impractical or impossible to incorporate into a unit test."

[Source: Wikipedia - Mock object](https://en.wikipedia.org/wiki/Mock_object)

---

### Mocking - Test Doubles (1)

- Dummy - objects are passed around but never actually used.
- Fake - objects actually have working implementations, but usually take some shortcut which makes them not suitable for production.
- Stubs - provide canned answers to calls made during the test.

---

### Mocking - Test Doubles (2)

- Spies - are stubs that also record some information based on how they were called.
- Mocks - objects pre-programmed with expectations which form a specification of the calls they are expected to receive.

[Source: Mocks Aren't Stubs](https://martinfowler.com/articles/mocksArentStubs.html)

---

### Mocking libraries

- [Top 20 NuGet mocking Packages](https://nugetmusthaves.com/tag/Mocking)

---

### Mocking code examples

- Labs/Exercise-03-after/**sln_mocking** and Labs/Exercise-03-after/**sln** folders.
- [Mocking ILogger with Moq](https://adamstorr.azurewebsites.net/blog/mocking-ilogger-with-moq)

---

## UI testing

---

### Manual vs automated tests

<table style="border: 1px solid white; border-collapse: collapse;">
    <thead>
        <tr>
            <th style="border: 1px solid white;"></th>
            <th style="border: 1px solid white;">Manual testing</th>
            <th style="border: 1px solid white;">Automation testing</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td rowspan=4 style="border: 1px solid white;">Best used for:</td>
            <td style="border: 1px solid white;">Ad-hoc testing</td>
            <td style="border: 1px solid white;">Regression testing</td>
        </tr>
        <tr>
            <td style="border: 1px solid white;">Exploratory testing</td>
            <td style="border: 1px solid white;">End-to-end-testing</td>
        </tr>
        <tr>
            <td style="border: 1px solid white;">Usability testing</td>
            <td style="border: 1px solid white;">Screenshot comparison</td>
        </tr>
        <tr>
            <td style="border: 1px solid white;">Early-stage UI testing</td>
            <td style="border: 1px solid white;">Testing of stable UI versions</td>
        </tr>
    </tbody>
</table>

[Source: Manual vs automation testing of the UI](https://screenster.io/manual-vs-automation-testing/)

---

### Selenium IDE vs Selenium WebDriver

- Selenium IDE
  - Record/Playback/Edit automation test scripts
- **Selenium WebDriver**
  - Code-based

---

### Selenium WebDriver testing architecture

- Test framework (e.g. xUnit)
- Test code (e.g. C#)
- IWebDriver API - WebDriver library (from NuGet)
- Browser driver (e.g. ChromeDriver)
- Browser (e.g. Chrome)
- Web Server hosting the app (e.g. IIS, Kestrel)

---

### UI testing code examples

- Labs/Exercise-03-after/**sln_ui_testing** folder.
