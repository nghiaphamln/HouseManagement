app.controller('GroupIndexController', function ($scope, $http, $window, $rootScope) {
    $scope.Init = function () {
        $scope.CreateGroupModel = {
            GroupName: null,
            LimitMember: null,
            Note: null
        };
        $scope.IsDisableCreateButton = true;
        $scope.ValidateGroupModel = {
            GroupName: null,
            Note: null
        };

        $scope.DisableCreateButton();
    }

    $scope.DisableCreateButton = function () {
        if (!$scope.CreateGroupModel.GroupName || 
            $scope.CreateGroupModel.GroupName.length < 3 ||
            $scope.CreateGroupModel.GroupName.length > 50) {
            $scope.IsDisableCreateButton = true;
            return;
        }
        
        $scope.IsDisableCreateButton = false;
    }
    
    $scope.ValidateGroupName = function () {
        if (!$scope.CreateGroupModel.GroupName) {
            $scope.ValidateGroupModel.GroupName = "Không được bỏ trống";
            $scope.DisableCreateButton();
            return;
        }

        if ($scope.CreateGroupModel.GroupName.length < 3) {
            $scope.ValidateGroupModel.GroupName = "Phải hơn 3 ký tự";
            $scope.DisableCreateButton();
            return;
        }

        if ($scope.CreateGroupModel.GroupName.length > 50) {
            $scope.ValidateGroupModel.GroupName = "Không được quá 50 ký tự";
            $scope.DisableCreateButton();
            return;
        }

        $scope.DisableCreateButton();
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
    
    $scope.GetGroup = function (pageNumber) {
        $rootScope.IsLoading = true;
        $http({
            method: "POST",
            url: "/Group/GetForPaging",
            data: JSON.stringify({
                PageSize: 20,
                PageNumber: pageNumber
            })
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
                
                $scope.Data = response.data.data;
                $scope.TotalRecord = response.data.totalRecord;
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