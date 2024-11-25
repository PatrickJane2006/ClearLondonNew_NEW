document.addEventListener('DOMContentLoaded', function () {
    // Функция для открытия/закрытия формы
    function hiddenOpen_Closeclick(container) {
        let x = document.querySelector(container);
        if (x.classList.contains('hidden')) {
            x.classList.remove('hidden');
            x.classList.add('visible');
        } else {
            x.classList.remove('visible');
            x.classList.add('hidden');
        }
    }

    // Обработчики для скрытия/открытия формы
    document.getElementById("click-to-hide").addEventListener("click", function () {
        hiddenOpen_Closeclick(".container-login-registration");
    });

    document.getElementById('side-menu-button-click-to-hide').addEventListener('click', function () {
        hiddenOpen_Closeclick(".container-login-registration");
    });

    document.querySelector(".overlay").addEventListener("click", function () {
        hiddenOpen_Closeclick(".container-login-registration");
    });

    const button_confirm = document.querySelector(".button_confirm_close");

    if (button_confirm) {
        button_confirm.addEventListener("click", function () {
            hiddenOpen_Closeclick(".container-login-registration");
        });
    }

    // Переключение между формами входа и регистрации
    const signInBtn = document.querySelector('.signin-btn');
    const signUpBtn = document.querySelector('.signup-btn');
    const formBox = document.querySelector('.form-box');
    const block = document.querySelector('.block');

    if (signInBtn && signUpBtn) {
        signUpBtn.addEventListener('click', function () {
            formBox.classList.add('active');
            block.classList.add('active');
        });

        signInBtn.addEventListener('click', function () {
            formBox.classList.remove('active');
            block.classList.remove('active');
        });
    }

    // Обработка формы регистрации
    const errorContainerSignup = document.getElementById('error-messages-signup');
    const formBtnSignup = document.querySelector('.form_btn_signup');

    if (formBtnSignup) {
        formBtnSignup.addEventListener('click', function () {
            const requestURL = '/Home/Register';
            const form = {
                login: document.querySelector("#signup_login input"),
                email: document.querySelector("#signup_email input"),
                password: document.querySelector("#signup_password input"),
                passwordConfirm: document.querySelector("#signup_confirm_password input")
            };

            // Валидация данных
            const login = form.login.value;
            const email = form.email.value;
            const password = form.password.value;
            const passwordConfirm = form.passwordConfirm.value;

            if (!login || !email || !password || !passwordConfirm) {
                displayErrors(['Пожалуйста, заполните все поля.'], errorContainerSignup);
                return;
            }

            if (password !== passwordConfirm) {
                displayErrors(['Пароли не совпадают.'], errorContainerSignup);
                return;
            }

            const body = {
                login: login,
                email: email,
                password: password,
                PasswordConfirm: passwordConfirm
            };

            sendRequest('POST', requestURL, body)
                .then(data => {
                    cleaningAndClosingForm(form, errorContainerSignup);
                    console.log('Успешный ответ', data);

                    // Закрытие формы регистрации и открытие формы подтверждения
                    hiddenOpen_Closeclick(".comfirm-email-container");
                    confirmEmail(data);
                })
                .catch(err => {
                    displayErrors(err, errorContainerSignup);
                    console.log(err);
                });
        });
    }

    // Функция отправки запроса
    function sendRequest(method, url, body = null) {
        const headers = {
            'Content-Type': 'application/json'
        };
        return fetch(url, {
            method: method,
            body: JSON.stringify(body),
            headers: headers
        }).then(response => {
            if (!response.ok) {
                return response.json().then(errorData => {
                    throw errorData; // ошибки для обработки в catch
                });
            }
            return response.json();
        });
    }

    // Функция отображения ошибок
    function displayErrors(errors, errorContainer) {
        errorContainer.innerHTML = ''; // Очистка предыдущих ошибок
        errors.forEach(error => {
            const errorMessage = document.createElement('div');
            errorMessage.classList.add('error');
            errorMessage.textContent = error;
            errorContainer.appendChild(errorMessage);
        });
    }

    // Функция очистки формы и скрытия
    function cleaningAndClosingForm(form, errorContainer) {
        errorContainer.innerHTML = '';
        for (const key in form) {
            if (form.hasOwnProperty(key)) {
                form[key].value = ''; // Очистка значений
            }
        }
        hiddenOpen_Closeclick(".container-login-registration"); // Закрытие формы
    }

    function confirmEmail(body) {
        document.querySelector(".send_confirm").addEventListener('click', function () {
            body.confirmEmail = document.getElementById('code_confirm').value;
            const requestURL = '/Home/ConfirmEmail';

            sendRequest('POST', requestURL, body)
                .then(data => {
                    console.log("Код подтверждения:", data);
                    toggleVisibility(".comfirm-email-container");
                    location.reload();
                })
                .catch(err => {
                    displayErrors(err, errorContainer);
                    console.log(err);
                });
        });
    }
});
