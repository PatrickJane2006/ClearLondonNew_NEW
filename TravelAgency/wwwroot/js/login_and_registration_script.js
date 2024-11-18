document.addEventListener('DOMContentLoaded', function () {
    // Функция для открытия/закрытия формы
    function hiddenOpen_Closeclick() {
        let x = document.querySelector(".container-login-registration");
        if (x.style.display == "none") {
            x.style.display = "grid";
        } else {
            x.style.display = "none";
        }
    }

    // Обработчики для скрытия/открытия формы
    document.getElementById("click-to-hide")?.addEventListener("click", hiddenOpen_Closeclick);
    document.querySelector(".overlay")?.addEventListener("click", hiddenOpen_Closeclick);
    document.getElementById('side-menu-button-click-to-hide').addEventListener('click', hiddenOpen_Closeclick);

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

    // Обработка формы входа
    const errorContainerSignin = document.getElementById('error-messages-signin');
    const formBtnSignin = document.querySelector('.form_btn_signin');

    if (formBtnSignin) {
        formBtnSignin.addEventListener('click', function () {
            const requestURL = '/Home/Login';
            const form = {
                email: document.querySelector("#signin_email input"),
                password: document.querySelector("#signin_password input")
            };

            // Валидация данных
            const email = form.email.value;
            const password = form.password.value;
            if (!email || !password) {
                displayErrors(['Пожалуйста, заполните все поля.'], errorContainerSignin);
                return;
            }

            const body = {
                email: email,
                password: password
            };

            sendRequest('POST', requestURL, body)
                .then(data => {
                    cleaningAndClosingForm(form, errorContainerSignin);
                    console.log('Успешный ответ', data);
                    location.reload(); // Перезагружаем страницу
                })
                .catch(err => {
                    displayErrors(err, errorContainerSignin);
                    console.log(err);
                });
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
                passwordConfirm: passwordConfirm
            };

            sendRequest('POST', requestURL, body)
                .then(data => {
                    cleaningAndClosingForm(form, errorContainerSignup);
                    console.log('Успешный ответ', data);
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
        hiddenOpen_Closeclick(); // Закрытие формы
    }

  
});
