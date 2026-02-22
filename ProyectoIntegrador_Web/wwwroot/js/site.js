// 1) Navegación por click en la card
document.addEventListener("click", function (e) {
    const card = e.target.closest(".js-card");
    if (!card) return;

    // si clickeo favorito, NO navegues
    if (e.target.closest(".fav-btn-form")) return;

    const url = card.getAttribute("data-url");
    if (url) window.location.href = url;
});

// 2) Click en favorito (AJAX)
document.addEventListener("click", async function (e) {
    const btn = e.target.closest(".js-fav-btn");
    if (!btn) return;

    // que no se dispare la navegación de la card
    e.preventDefault();
    e.stopPropagation();

    const form = btn.closest("form");
    if (!form) return;

    const url = form.getAttribute("action");
    const token = form.querySelector('input[name="__RequestVerificationToken"]')?.value;

    const data = new FormData(form);

    const res = await fetch(url, {
        method: "POST",
        headers: {
            "RequestVerificationToken": token
        },
        body: data
    });

    if (!res.ok) return;

    const json = await res.json();

    if (json?.ok) {
        // pintar corazón
        btn.classList.toggle("is-fav", json.esFavorito);

        const icon = btn.querySelector("i");
        if (icon) {
            icon.classList.toggle("bi-heart-fill", json.esFavorito);
            icon.classList.toggle("bi-heart", !json.esFavorito);
        }
    }
}, true); // <-- CAPTURE (clave para ganarle a todo)


/*document.addEventListener("click", (e) => {
    const favForm = e.target.closest(".fav-btn-form");
    if (!favForm) return;

    // evita que el click “suba” y dispare el stretched-link
    e.preventDefault();
    e.stopPropagation();
}, true); // <-- CAPTURE (clave)*/