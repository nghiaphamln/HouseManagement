angular.module('HouseManagement').service('LoaderService', function () {
    this.IsLoading = true;

    this.ShowLoader = function () {
        this.isLoading = true;
    };

    this.HideLoader = function () {
        this.isLoading = false;
    };
});