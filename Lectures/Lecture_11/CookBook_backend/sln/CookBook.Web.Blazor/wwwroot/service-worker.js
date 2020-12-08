const CACHE = "precache";

importScripts('https://storage.googleapis.com/workbox-cdn/releases/5.0.0/workbox-sw.js');

self.addEventListener("message", (event) => {
    if (event.data && event.data.type === "SKIP_WAITING") {
        self.skipWaiting();
    }
});

workbox.routing.registerRoute(
    new RegExp('https://localhost:44380/*'),
    new workbox.strategies.StaleWhileRevalidate({
        cacheName: CACHE
    })
);

workbox.routing.registerRoute(
    new RegExp('/*'),
    new workbox.strategies.NetworkFirst({
        cacheName: CACHE
    })
);