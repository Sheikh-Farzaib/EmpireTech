(function () {
    'use strict';

    const forms = document.querySelectorAll('.requires-validation');

    Array.from(forms).forEach(function (form) {
        debugger
        form.addEventListener('submit', function (event) {
            const tokenInput = form.querySelector('input[name="Token"]');
            const tokenValue = tokenInput.value;

           
            if (!isValidToken(tokenValue)) {
              
                event.preventDefault();
                tokenInput.classList.add('is-invalid');
                tokenInput.classList.remove('is-valid');
                alert('Token must have 36 characters and contain 4 hyphens!');
                return; 
            } else {
                tokenInput.classList.add('is-valid');
                tokenInput.classList.remove('is-invalid');
            }

          
            event.preventDefault();


            $.ajax({
                url: `/Auth/UserVerification?Token=${tokenValue}`, 
                type: 'POST',
                contentType: 'application/json',
                success: function (response) {
                    alert('Token verification successful!');
                    if (response.returnUrl) {
                        window.location.href = response.returnUrl;
                    }
                },
                error: function (xhr) {
                    // Handle error response
                    alert('Error: ' + xhr.responseText);
                }
            });

            form.classList.add('was-validated'); 
        }, false);
    });

    function isValidToken(token) {
        const hyphenCount = (token.match(/-/g) || []).length;
        return token.length === 36 && hyphenCount === 4;
    }
})();