const cacheName = 'cookbook-cache';
const offlineCallbackDocument = "/offline/offline.html";
const offlineCallbackImage = "/offline/offline-image.png";

const precacheAssets = [
    '/'
];

function install() {
    caches.open(cacheName).then(cache => {
        precacheAssets.push(offlineCallbackDocument);
        precacheAssets.push(offlineCallbackImage);
        cache.addAll(precacheAssets);
    });
}

self.addEventListener('install', event => {
    event.waitUntil(install());
});

const fromNetwork = (request, timeout) =>
    new Promise((fulfill, reject) => {
        const timeoutId = setTimeout(reject, timeout);
        fetch(request).then(response => {
            clearTimeout(timeoutId);
            fulfill(response);
            if (request.method === 'GET'
                && !request.url.startsWith('chrome-extension://')
                && response.type !== 'opaque') {
                updateCache(request, response.clone());
            }
        }, reject);
    });

const fromOfflineFallback = (cache, request) => {
    if (request.destination === 'document') {
        return cache.match(offlineCallbackDocument);
    }
    else if (request.destination === 'image') {
        return cache.match(offlineCallbackImage);
    }
}

const fromCache = request =>
    caches.open(cacheName)
        .then(cache => cache
            .match(request)
            .then(matching => matching || fromOfflineFallback(cache, request)));

const updateCache = (request, response) =>
    caches.open(cacheName)
        .then(cache => cache.put(request, response));

self.addEventListener('fetch', event => {
    event.respondWith(fromNetwork(event.request, 1000)
        .catch(() => fromCache(event.request)));
});