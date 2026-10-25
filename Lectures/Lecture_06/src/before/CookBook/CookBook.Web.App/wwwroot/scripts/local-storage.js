window.language = {
    get: () => window.localStorage['Language'],
    set: (value) => window.localStorage['Language'] = value
};