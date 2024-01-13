app.controller('GroupIndexController', function ($scope, $http, $window, $rootScope) {
    $scope.Init = function () {
        $scope.CreateGroupModel = {
            GroupName: "",
            LimitMember: null,
            Note: null
        };
    }
    
    $scope.CreateGroup = function () {
        $rootScope.IsLoading = true;
        $http({
            method: "POST",
            url: "/Group/Create",
            data: JSON.stringify($scope.CreateGroupModel)
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
                    title: "Tạo mới thành công",
                    showConfirmButton: false,
                    timer: 1500,
                    customClass: {
                        confirmButton: "btn btn-primary"
                    },
                    buttonsStyling: false,
                }).then(function () {
                    $window.location.reload();
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
        );
    }
});