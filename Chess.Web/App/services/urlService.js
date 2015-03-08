app.service('urlService', [function () {
    var baseUrl = $('base').first().attr('href');

    var normalize = function (path) {
        if (path[0] !== '/') return '/' + path;
        return path;
    };

    var relative = function (path) {
        return baseUrl + normalize(path);
    };

    return {
        relative: relative
    };
}]);