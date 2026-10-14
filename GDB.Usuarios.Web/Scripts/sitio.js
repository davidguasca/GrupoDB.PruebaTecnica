// Comportamientos de interfaz compartidos por todas las páginas.
(function () {
    // Marca en el menú la página actual (también lo anuncia a lectores de pantalla).
    var pagina = (location.pathname.split('/').pop() || 'UsuarioConsulta.aspx').toLowerCase();
    document.querySelectorAll('.menu a').forEach(function (a) {
        if (a.getAttribute('href').toLowerCase() === pagina) a.setAttribute('aria-current', 'page');
    });

    // Evita el doble envío: bloquea el botón principal mientras el servidor responde.
    document.querySelectorAll('form').forEach(function (form) {
        form.addEventListener('submit', function () {
            if (typeof Page_IsValid === 'undefined' || Page_IsValid) form.classList.add('enviando');
        });
    });

    // Confirma con un diálogo propio los enlaces marcados con data-confirmar="nombre".
    var dialogo = document.getElementById('dialogo-confirmar');
    if (!dialogo) return;
    var pendiente = null;

    document.addEventListener('click', function (e) {
        var enlace = e.target.closest('[data-confirmar]');
        if (!enlace) return;
        e.preventDefault();
        pendiente = enlace;
        dialogo.querySelector('[data-nombre]').textContent = enlace.getAttribute('data-confirmar');
        dialogo.showModal();
    });

    // Al confirmar, hace el mismo PostBack que el LinkButton: su href es javascript:__doPostBack('control','').
    dialogo.addEventListener('click', function (e) {
        var boton = e.target.closest('[data-respuesta]');
        if (!boton) return;
        dialogo.close();
        var control = /__doPostBack\('([^']+)'/.exec(pendiente.getAttribute('href'));
        if (boton.getAttribute('data-respuesta') === 'si' && control) __doPostBack(control[1], '');
    });
})();
