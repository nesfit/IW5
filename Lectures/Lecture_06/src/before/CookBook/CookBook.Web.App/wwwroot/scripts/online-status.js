let handler;

window.OnlineStatus = {
    Initialize: function (interop) {
        handler = function () {
            interop.invokeMethodAsync("OnlineStatus.StatusChanged", navigator.onLine);
        }

        window.addEventListener("online", handler);
        window.addEventListener("offline", handler);

        handler(navigator.onLine);
    },
    Dispose: function () {

        if (handler !== undefined && handler !== null) {

            window.removeEventListener("online", handler);
            window.removeEventListener("offline", handler);
        }
    }
};