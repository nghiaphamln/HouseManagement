app.controller('LoginController', function ($scope, $http, $window, $rootScope) {
    $scope.Init = function () {
        $scope.Email = "";
        $scope.Password = "";
        $scope.IsDisable = true;
    }

    $scope.DisableSubmit = function () {
        if (!$scope.Email) {
            $scope.IsDisable = true;
            return;
        }

        if (!$scope.Password) {
            $scope.IsDisable = true;
            return;
        }
        
        $scope.IsDisable = false;
    }

    $scope.Login = function () {
        $rootScope.IsLoading = true;
        $http({
            method: "POST",
            url: "/Account/Login",
            data: {
                Email: $scope.Email,
                Password: $scope.Password,
                RequestPath: new URL($window.location.href).searchParams.get("RequestPath")
            }
        }).then(
            function successCallback(response) {
                $rootScope.IsLoading = false;
                if (response.data.status !== 200) {
                    toastMixin.fire({
                        position: "top-right",
                        icon: "error",
                        title: response.data.message,
                        showConfirmButton: false,
                        timer: 1500,
                        customClass: {
                            confirmButton: "btn btn-primary"
                        },
                        buttonsStyling: false,
                    });
                    return;
                }

                $window.location.href = response.data.data;
            },
            function errorCallback() {
                $rootScope.IsLoading = false;
                toastMixin.fire({
                    position: "top-right",
                    icon: "error",
                    title: "Đã xảy ra lỗi, vui lòng thử lại",
                    showConfirmButton: false,
                    timer: 1500,
                    customClass: {
                        confirmButton: "btn btn-primary"
                    },
                    buttonsStyling: false,
                });
            }
        )
    }
});