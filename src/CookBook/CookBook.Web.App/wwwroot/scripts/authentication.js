window.cookBookAuthentication = (() => {
    let selectedScript;
    let loading;

    return {
        load(scriptPath) {
            // Both adapters define window.AuthenticationService; load only the selected one.
            if (selectedScript && selectedScript !== scriptPath) {
                return Promise.reject(new Error("An authentication adapter is already selected. Reload to switch providers."));
            }

            if (!loading) {
                selectedScript = scriptPath;
                loading = new Promise((resolve, reject) => {
                    const script = document.createElement("script");
                    script.src = scriptPath;
                    script.onload = () => resolve();
                    script.onerror = () => reject(new Error(`Failed to load authentication adapter: ${scriptPath}`));
                    document.head.appendChild(script);
                });
            }

            return loading;
        }
    };
})();
