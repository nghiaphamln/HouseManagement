app.controller('RegisterController', function ($scope, $http, $window, ValidationService, $rootScope) {
    $scope.Init = function () {
        $scope.IsAcceptTerms = false;
        $scope.FullName = "";
        $scope.Email = "";
        $scope.Password = "";
        $scope.FullNameWarning = "";
        $scope.EmailWarning = "";
        $scope.PasswordWarning = "";
        $scope.IsDisable = true;
    }

    $scope.DisableSubmit = function () {
        if ($scope.IsAcceptTerms === false) {
            $scope.IsDisable = true;
            return;
        }
        
        if (!$scope.FullName) {
            $scope.IsDisable = true;
            return;
        }
        
        if (!$scope.Email) {
            $scope.IsDisable = true;
            return;
        }
        
        if (!$scope.Password) {
            $scope.IsDisable = true;
            return;
        }
        
        if ($scope.FullNameWarning) {
            $scope.IsDisable = true;
            return;
        }
        
        if ($scope.EmailWarning) {
            $scope.IsDisable = true;
            return;
        }
        
        if ($scope.PasswordWarning) {
            $scope.IsDisable = true;
            return;
        }

        $scope.IsDisable = false;
    }

    $scope.ValidateEmail = function () {
        $scope.EmailWarning = ValidationService.ValidateEmail($scope.Email);
        $scope.DisableSubmit();
    }

    $scope.ValidatePassword = function () {
        $scope.PasswordWarning = ValidationService.ValidatePassword($scope.Password);
        $scope.DisableSubmit();
    }

    $scope.ValidateFullname = function () {
        $scope.FullNameWarning = ValidationService.ValidateFullname($scope.FullName);
        $scope.DisableSubmit();
    }

    $scope.Register = function () {
        $rootScope.IsLoading = true;
        $http({
            method: "POST",
            url: "/Account/Register",
            data: {
                FullName: $scope.FullName,
                Email: $scope.Email,
                Password: $scope.Password
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

                toastMixin.fire({
                    position: "top-right",
                    icon: "success",
                    title: "Đăng ký thành công",
                    showConfirmButton: false,
                    timer: 1500,
                    customClass: {
                        confirmButton: "btn btn-primary"
                    },
                    buttonsStyling: false,
                }).then(function () {
                    $window.location.href = "/Account/Login";
                });
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