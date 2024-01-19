app.service('HttpService', function ($http, $rootScope) {
    this.GetData = function (url, successCallback) {
        $rootScope.IsLoading = true;
        $http.get(url)
            .then(function (response) {
                $rootScope.IsLoading = false;
                if (successCallback) {
                    successCallback(response.data);
                }
            })
            .catch(function (error) {
                $rootScope.IsLoading = false;
                console.error(error);
                toastMixin.fire({
                    position: 'top-right',
                    icon: 'error',
                    title: 'Đã xảy ra lỗi, vui lòng thử lại',
                    showConfirmButton: false,
                    timer: 1500,
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    },
                    buttonsStyling: false,
                });
            });
    };

    // Hàm callback gọi API sử dụng phương thức POST
    this.PostData = function (url, data, successCallback) {
        $rootScope.IsLoading = true;
        $http.post(url, data)
            .then(function (response) {
                $rootScope.IsLoading = false;
                if (successCallback) {
                    successCallback(response.data);
                }
            })
            .catch(function (error) {
                $rootScope.IsLoading = false;
                console.error(error);
                toastMixin.fire({
                    position: 'top-right',
                    icon: 'error',
                    title: 'Đã xảy ra lỗi, vui lòng thử lại',
                    showConfirmButton: false,
                    timer: 1500,
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    },
                    buttonsStyling: false,
                });
            });
    };
});