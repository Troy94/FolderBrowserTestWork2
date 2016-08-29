(function () {
    var app = angular.module('FilesSystem', []);

    app.controller('FilesSystemController', ['$http', function ($http) {
        this.Model = { CurrentPath: "" };
        var Ctrl = this;

        this.GetDirectoryData = function (directory) {
            var path = "";
            if (directory == null) {
                path = "";
            } else if (directory === "") {
                var cutIndex = this.Model.CurrentPath.lastIndexOf("\\", this.Model.CurrentPath.length - 2);
                path = '?&dir=' + this.Model.CurrentPath.substring(0, cutIndex + 1) + '&';
            } else {
                if (this.Model.CurrentPath.length > 2) {
                    path = '?&dir=' + this.Model.CurrentPath + directory + '\\&';
                } else {
                    path = '?&dir=' + directory + '&';
                }
            }
            var requestPath = path;
            $http.get('/api/directory/' + path).success(function (data) {
                if (data != null) {
                    Ctrl.Model = data;
                    Ctrl.Model.QuantitySize10 = "Count ...";
                    Ctrl.Model.QuantitySize50 = "Count ...";
                    Ctrl.Model.QuantitySize100 = "Count ...";
                }
            });

            $http.get('/api/count/' + requestPath).success(function (dataCount) {
                if (dataCount != null && Ctrl.Model.CurrentPath == dataCount.Directory) {
                    Ctrl.Model.QuantitySize10 = dataCount.QuantitySize10;
                    Ctrl.Model.QuantitySize50 = dataCount.QuantitySize50;
                    Ctrl.Model.QuantitySize100 = dataCount.QuantitySize100;
                }
            });
        }
        this.HasParent = function () {
            return Ctrl.Model.CurrentPath.length > 2;
        }
    }]);
})();