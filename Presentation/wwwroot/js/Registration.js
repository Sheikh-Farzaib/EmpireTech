//(function () {
//    'use strict'
//    const forms = document.querySelectorAll('.requires-validation')

//    Array.from(forms).forEach(function (form) {
//        form.addEventListener('submit', function (event) {
//            debugger
//            if (!form.checkValidity()) {
//                event.preventDefault()
//                event.stopPropagation()
//            } else {
//                event.preventDefault();

//                var formData = {
//                    UserName: form.querySelector('input[name="UserName"]').value,
//                    Email: form.querySelector('input[name="Email"]').value,
//                    Password: form.querySelector('input[name="Password"]').value
//                };

//                sessionStorage.setItem("email", formData.Email);
//                $.ajax({
//                    url: '/Auth/UserRegistration',
//                    type: 'POST',
//                    data: JSON.stringify(formData), 
//                    contentType: 'application/json', 
//                    success: function (response) {
//                        alert('Registration successful!');
//                        if (response.returnUrl) {
//                            window.location.href = response.returnUrl; 
//                        } 
//                    },
//                    error: function (xhr, status, error) {
//                        $('#passwordInvalid').empty();

//                        if (xhr.responseJSON && xhr.responseJSON.Errors) {
//                            xhr.responseJSON.Errors.forEach(function (error) {
//                                $('#passwordInvalid').append('<div>' + error + '</div>');
//                            });
//                        } else {
                            
//                            alert('Error: ' + xhr.responseText);
//                        }
//                    }
//                });
//            }

//            form.classList.add('was-validated');
//        }, false);
//    });
 
//})();
(function () {
    'use strict';
    const forms = document.querySelectorAll('.requires-validation');

    Array.from(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();

            if (!form.checkValidity()) {
                event.stopPropagation();
            } else {
                const passwordInput = form.querySelector('input[name="Password"]');
                const password = passwordInput.value;

                if (!isValidPassword(password)) {

                    $('#passwordInvalid').text("Password must be at least 6 characters long with 1 special character").show();
                    return; 
                } else {
                    $('#passwordInvalid').hide();
                }

                var formData = {
                    UserName: form.querySelector('input[name="UserName"]').value,
                    Email: form.querySelector('input[name="Email"]').value,
                    Password: password
                };

                sessionStorage.setItem("email", formData.Email);

                $.ajax({
                    url: '/Auth/UserRegistration',
                    type: 'POST',
                    data: JSON.stringify(formData),
                    contentType: 'application/json',
                    success: function (response) {
                        alert('Registration successful!');
                        console.log(response);
                        if (response.returnUrl) {
                            window.location.href = response.returnUrl;
                        }
                    },
                    error: function (xhr) {
                        console.log(xhr);
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

    // Function to validate the password
    function isValidPassword(password) {
        // Check if the password is at least 6 characters long and contains at least one special character
        const specialCharacterRegex = /[!@#$%^&*(),.?":{}|<>]/; // Add more special characters as needed
        return password.length >= 6 && specialCharacterRegex.test(password);
    }
})();
