(function () {
    'use strict'
    const forms = document.querySelectorAll('.requires-validation')
    Array.from(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();

            if (!form.checkValidity()) {
                event.stopPropagation();
            } else {
                var formData = {
                    Password: form.querySelector('input[name="password"]').value,
                    Email: form.querySelector('input[name="email"]').value,
                };
                $.ajax({
                    url: '/Auth/UserLogin',
                    type: 'POST',
                    data: JSON.stringify(formData),
                    contentType: 'application/json',
                    success: function (response) {
                        alert('Registration successful!');
                        if (response.returnUrl) {
                            window.location.href = response.returnUrl;
                        }
                    },
                    error: function (xhr) {
                        $('#passwordInvalid').empty();

                        if (xhr.responseJSON && xhr.responseJSON.Errors) {
                            $('#passwordInvalid').show();
                            xhr.responseJSON.Errors.forEach(function (error) {
                                $('#passwordInvalid').append('<div>' + error + '</div>');
                            });
                        } else {
                            alert('Error: ' + xhr.responseText);
                        }
                    }
                });
            }

            form.classList.add('was-validated');
        }, false);
    });
    Array.from(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                debugger
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }
                else {
                    var formData = {
                        Password: form.querySelector('input[name="Password"]').value,
                        Email: form.querySelector('input[name="Email"]').value,
                    };

                    $.ajax({
                        url: '/Auth/UserLogin',
                        type: 'POST',
                        data: JSON.stringify(formData),
                        contentType: 'application/json',
                        success: function (response) {
                            alert('Registration successful!');
                            if (response.returnUrl) {
                                window.location.href = response.returnUrl;
                            }
                        },
                        error: function (xhr) {
                            $('#passwordInvalid').empty();

                            if (xhr.responseJSON && xhr.responseJSON.Errors) {
                                $('#passwordInvalid').show();
                                xhr.responseJSON.Errors.forEach(function (error) {
                                    $('#passwordInvalid').append('<div>' + error + '</div>');
                                });
                            } else {
                                alert('Error: ' + xhr.responseText);
                            }
                        }
                    });
                }

                form.classList.add('was-validated')
            }, false)
        })
})();
