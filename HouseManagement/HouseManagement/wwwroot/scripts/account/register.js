app.controller('RegisterController', function ($scope, ValidationService) {
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

    }
});