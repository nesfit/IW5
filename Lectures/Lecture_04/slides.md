# Mocking, API and UI testing

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

- Test framework (e.g. XUnit)
- Test code (e.g. C#)
- IWebDriver API - WebDriver library (e.g. NuGet)
- Browser driver (e.g. ChromeDriver)
- Browser (e.g. Chrome)
- Web Server hosting the app (e.g. IIS, Kestrel)