const assert = require("node:assert/strict");
const { readFileSync } = require("node:fs");
const path = require("node:path");
const { test } = require("node:test");
const vm = require("node:vm");

function createLoader() {
    const scripts = [];
    const window = {};
    const document = {
        createElement: () => ({}),
        head: { appendChild: script => scripts.push(script) }
    };
    const source = readFileSync(path.join(__dirname,
        "..", "CookBook.Web.App", "wwwroot", "scripts", "authentication.js"), "utf8");
    vm.runInNewContext(source, { window, document });
    return { load: window.cookBookAuthentication.load, scripts };
}

test("waits for the selected authentication script before completing", async () => {
    const { load, scripts } = createLoader();
    let loaded = false;
    const promise = load("msal.js").then(() => { loaded = true; });
    assert.equal(scripts.length, 1);
    assert.equal(scripts[0].src, "msal.js");
    await Promise.resolve();
    assert.equal(loaded, false);
    scripts[0].onload();
    await promise;
    assert.equal(loaded, true);
});

test("loads the same adapter only once and rejects mixing adapters", async () => {
    const { load, scripts } = createLoader();
    const promise = load("msal.js");
    assert.equal(load("msal.js"), promise);
    await assert.rejects(load("oidc.js"), /already selected/);
    assert.equal(scripts.length, 1);
    scripts[0].onload();
    await promise;
});

test("reports script loading failures instead of starting without authentication", async () => {
    const { load, scripts } = createLoader();
    const promise = load("msal.js");
    scripts[0].onerror();
    await assert.rejects(promise, /Failed to load authentication adapter/);
});
