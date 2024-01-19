app.service('ValidationService', function () {
    this.ValidateEmail = function (email) {
        if (!email) {
            return 'Email không được trống';
        }

        if (!(/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(email))) {
            return 'Email không hợp lệ';
        }

        return '';
    }

    this.ValidatePassword = function (password) {
        if (!password) {
            return 'Mật khẩu không được trống';
        }

        if (password.length < 8) {
            return 'Mật khẩu phải có ít nhất 8 ký tự';
        }

        if (!(/[A-Z]/.test(password))) {
            return 'Mật khẩu phải bao gồm chữ hoa';
        }

        if (!(/[a-z]/.test(password))) {
            return 'Mật khẩu phải bao gồm chữ thường';
        }

        if (!(/[0-9]/.test(password))) {
            return 'Mật khẩu phải bao gồm số';
        }

        return '';
    }

    this.ValidateFullname = function (fullname) {
        if (!fullname) {
            return 'Tên không được bỏ trống';
        }

        if (!(/^[\p{L}\s']+$/u.test(fullname))) {
            return 'Tên không hợp lệ';
        }

        return '';
    }
});