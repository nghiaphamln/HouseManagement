app.controller('GroupIndexController', function ($scope, $http, $window, $rootScope, HttpService) {
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
        $scope.Pager = {};
        $scope.GetGroup(1);
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
        $scope.ValidateGroupModel.GroupName = '';
        if (!$scope.CreateGroupModel.GroupName) {
            $scope.ValidateGroupModel.GroupName = 'Không được bỏ trống';
            $scope.DisableCreateButton();
            return;
        }

        if ($scope.CreateGroupModel.GroupName.length < 3) {
            $scope.ValidateGroupModel.GroupName = 'Phải hơn 3 ký tự';
            $scope.DisableCreateButton();
            return;
        }

        if ($scope.CreateGroupModel.GroupName.length > 50) {
            $scope.ValidateGroupModel.GroupName = 'Không được quá 50 ký tự';
            $scope.DisableCreateButton();
            return;
        }

        $scope.DisableCreateButton();
    }

    $scope.CreateGroup = function () {
        HttpService.PostData(
            '/Group/Create',
            JSON.stringify($scope.CreateGroupModel),
            function (response) {
                if (response.status !== 200) {
                    toastMixin.fire({
                        position: 'top-right',
                        icon: 'error',
                        title: response.message,
                        showConfirmButton: false,
                        timer: 1500,
                        customClass: {
                            confirmButton: 'btn btn-primary'
                        },
                        buttonsStyling: false,
                    });
                    return;
                }

                toastMixin.fire({
                    position: 'top-right',
                    icon: 'success',
                    title: 'Tạo mới thành công',
                    showConfirmButton: false,
                    timer: 1500,
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    },
                    buttonsStyling: false,
                }).then(function () {
                    document.getElementById('button-dismiss-create-modal').click();
                    $scope.GetGroup(1);
                });
            }
        );
    }

    $scope.GetGroup = function (pageNumber) {
        HttpService.PostData(
            '/Group/GetForPaging',
            JSON.stringify({pageNumber}),
            function (response) {
                if (response.status !== 200) {
                    toastMixin.fire({
                        position: 'top-right',
                        icon: 'error',
                        title: response.message,
                        showConfirmButton: false,
                        timer: 1500,
                        customClass: {
                            confirmButton: 'btn btn-primary'
                        },
                        buttonsStyling: false,
                    });
                    return;
                }

                $scope.Pager = response.data;
            });
    }
});