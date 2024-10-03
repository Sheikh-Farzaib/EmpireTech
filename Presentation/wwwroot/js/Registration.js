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
            // Prevent default form submission
            event.preventDefault();

            // Check if the form is valid
            if (!form.checkValidity()) {
                event.stopPropagation();
            } else {
                // Custom validation for Password
                const passwordInput = form.querySelector('input[name="Password"]');
                const password = passwordInput.value;

                // Validate password
                if (!isValidPassword(password)) {
                    // If invalid, show error message and prevent AJAX call
                    $('#passwordInvalid').text("Password must be at least 6 characters long with 1 special character").show();
                    return; // Stop execution here
                } else {
                    // Clear previous error messages
                    $('#passwordInvalid').hide();
                }

                // Gather form data
                var formData = {
                    UserName: form.querySelector('input[name="UserName"]').value,
                    Email: form.querySelector('input[name="Email"]').value,
                    Password: password
                };

                // Store email in session storage
                sessionStorage.setItem("email", formData.Email);

                // AJAX call
                $.ajax({
                    url: '/Auth/UserRegistration',
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
                        // Clear previous error messages
                        $('#passwordInvalid').empty();

                        // Show validation errors if any
                        if (xhr.responseJSON && xhr.responseJSON.Errors) {
                            $('#passwordInvalid').show();
                            xhr.responseJSON.Errors.forEach(function (error) {
                                $('#passwordInvalid').append('<div>' + error + '</div>');
                            });
                        } else {
                            // General error handling
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
