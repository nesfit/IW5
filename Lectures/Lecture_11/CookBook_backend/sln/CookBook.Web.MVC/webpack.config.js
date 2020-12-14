const path = require("path");

module.exports = {
    entry: {
        site: "./wwwroot/js/site.js"
    },
    output: {
        filename: "[name].entry.js",
        path: path.resolve(__dirname, "wwwroot", "dist")
    },
    devtool: "source-map",
    mode: "development",
    module: {
        rules: [
            { test: /\.css$/, loader: "style-loader!css-loader" },
            { test: /\.eot(\?v=\d+\.\d+\.\d+)?$/, loader: "file-loader" },
            { test: /\.(woff|woff2)$/, loader: "url-loader?prefix=font/&limit=5000" },
            { test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/, loader: "url-loader?limit=10000&mimetype=application/octet-stream" },
            { test: /\.svg(\?v=\d+\.\d+\.\d+)?$/, loader: "url-loader?limit=10000&mimetype=image/svg+xml" }
        ]
    }
};