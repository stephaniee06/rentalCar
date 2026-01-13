 
document.addEventListener("DOMContentLoaded", function () {
    
    checkLoginStatus();

 
    const btnRegister = document.getElementById("btnRegister");
    if (btnRegister) {
        btnRegister.addEventListener("click", function () {
            const fullname = document.getElementById("fullname")?.value.trim();
            const email = document.getElementById("email")?.value.trim();
            const password = document.getElementById("password")?.value;
            const repassword = document.getElementById("repassword")?.value;
            const phone = document.getElementById("phone")?.value.trim();
            const address = document.getElementById("address")?.value.trim();

            if (!fullname || !email || !password || !repassword || !phone || !address) {
                alert("Mohon lengkapi semua data registrasi.");
                return;
            }

            if (password !== repassword) {
                alert("Konfirmasi password tidak cocok.");
                return;
            }

         
            localStorage.setItem("registeredName", fullname);
            localStorage.setItem("registeredEmail", email);
            localStorage.setItem("registeredPassword", password);

            alert("Registrasi berhasil. Silakan login.");
            window.location.href = "/Account/Login";
        });
    }

 
    const btnLogin = document.getElementById("btnLogin");
    if (btnLogin) {
        btnLogin.addEventListener("click", function () {
            const email = document.getElementById("username")?.value.trim();
            const password = document.getElementById("password")?.value;

            if (!email || !password) {
                alert("Email dan password wajib diisi.");
                return;
            }

         
            const savedEmail = localStorage.getItem("registeredEmail");
            const savedPassword = localStorage.getItem("registeredPassword");
            const savedName = localStorage.getItem("registeredName");

            if (email !== savedEmail || password !== savedPassword) {
                alert("Email atau password salah.");
                return;
            }

        
            localStorage.setItem("isLoggedIn", "true");
            localStorage.setItem("userName", savedName);

            window.location.href = "/Home/Index";
        });
    }
});

 
function checkLoginStatus() {
    const isLoggedIn = localStorage.getItem("isLoggedIn");
    const userName = localStorage.getItem("userName");

 
    const authSection = document.getElementById("authSection");
    if (!authSection) return;

    if (isLoggedIn === "true" && userName) {
        authSection.innerHTML = `
            <div class="user-info">
                <span class="user-greeting">Hai, ${userName}</span>
                <button id="btnLogout" class="btn-logout-small">Logout</button>
            </div>
        `;

        document.getElementById("btnLogout").addEventListener("click", function () {
            localStorage.clear();
            window.location.href = "/Account/Login";
        });
    }
}
