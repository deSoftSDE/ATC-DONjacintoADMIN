appadmin = angular.module('app', ['dx', 'ng-file-model']);
appadmin.factory('Llamada', function ($http, $q) {
    var api_url = "http://" + location.host + "/Data/";
    var api_stream = "http://" + location.host + "/StreamFiles/";
    var http = {
        get: function (url) {
            var deferred = $q.defer();
            $http.get(api_url + url)
                .then(function (respuesta) {
                    console.log(respuesta);
                    deferred.resolve(respuesta);
                });
            return deferred.promise;
        },
        post: function (url, body) {
            console.log(body);
            var deferred = $q.defer();
            $.ajax({
                data: JSON.stringify(body),
                url: api_url + url,
                type: 'post',
                contentType: 'application/json',
                success: function (response) {
                    var res = { data: response };
                    console.log(res);
                    deferred.resolve(res);
                }
            });
            return deferred.promise;
        },
        getRuta: function (imagen) {
            return api_stream + "GetImagen?filename=" + imagen;
        },
        postFile: function (fd) {
            var deferred = $q.defer();
            $.ajax({
                type: 'POST',
                url: api_stream + "Post",
                data: fd,
                async: true,
                cache: false,
                contentType: false,
                processData: false
            }).done(function (d) {
                deferred.resolve(d);
                // callback function in the controller
                //$scope.myCallback(d);
            }).fail(function (x) {
                deferred.resolve(x);
            });
            return deferred.promise;
        }
    };
    return http;
});
function NotNullNotUndefinedNotEmpty(val) {
    if (val !== null && val !== undefined && val !== "") {
        return true;
    } else {
        return false;
    }
}
function VacioSiUndefined(val) {
    if (NotNullNotUndefinedNotEmpty(val)) {
        return val;
    } else {
        return "";
    }
}