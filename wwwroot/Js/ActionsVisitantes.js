$(document).ready(function () {
    MoverScroll();
    MudarOrdenacao();
    mudarImagemPrincipal();
}); 
function mudarImagemPrincipal() {
    $(".img-small-wrap img").click(function () {
        var Caminho = $(this).attr("src");
        $(".img-big-wrap img").attr("src", Caminho);
        $(".img-big-wrap a").attr("href", Caminho);
    });
}
function MoverScroll() {
    if (window.location.hash.length > 0) {
        var hash = window.location.hash;
        if (hash == "#posicao-produto") {
            window.scrollBy(0, 500);
        }
    }
}
function MudarOrdenacao() {
    $("#ordenacao").change(function () {
        var Pagina=1;
        var pesquisa = "";
        var Ordenacao = $(this).val();
        var Fragmento = "#posicao-produto";

        var QueryString = new URLSearchParams(window.location.search);

        if (QueryString.has("pagina")) {
            Pagina = QueryString.get("pagina");
        }
        if (QueryString.has("pesquisa")) {
            Pagina = QueryString.get("pesquisa");
        }

        if ($("#breadcrumb").length > 0) {
            Fragmento = "";
        }

        var URL = window.location.protocol + "//" + window.location.host + window.location.pathname;

        var URLComParametros = URL + "?pagina=" + Pagina + "&pesquisa=" + pesquisa + "&ordenacao=" + Ordenacao +Fragmento;
       

        window.location.href = URLComParametros;
    });
}


