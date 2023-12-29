const app = angular
    .module('HouseManagement', [])
    .run(function ($http) {
        $http.defaults.headers.common['RequestVerificationToken'] =
            angular.element('input[name="__RequestVerificationToken"]').val();
    });