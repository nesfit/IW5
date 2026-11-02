const cacheName = 'cookbook-cache';
const offlineCallbackDocument = "/offline/offline.html";
const offlineCallbackImage = "/offline/offline-image.png";

importScripts('./service-worker-assets.js');
const offlineAssetsInclude = [/\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.css$/, /\.woff$/, /\.png$/, /\.jpe?g$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/];
const offlineAssetsExclude = [/^service-worker\.js$/];

function install() {
    caches.open(cacheName).then(cache => {
        const precacheAssets = self.assetsManifest.assets
            .filter(asset => offlineAssetsInclude.some(pattern => pattern.test(asset.url)))
            .filter(asset => !offlineAssetsExclude.some(pattern => pattern.test(asset.url)))
            .map(asset => new Request(asset.url, { integrity: asset.hash }));
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