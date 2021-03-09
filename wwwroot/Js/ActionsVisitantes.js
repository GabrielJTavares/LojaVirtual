$(document).ready(function () {
    MoverScroll();
    MudarOrdenacao();
    mudarImagemPrincipal();
    mudarQuantidadeProdutoCarrinho();
});
function numberToReal(numero) {
    var numero = numero.toFixed(2).split('.');
    numero[0] = "R$ " + numero[0].split(/(?=(?:...)*$)/).join('.');
    return numero.join(',');
}
function mudarQuantidadeProdutoCarrinho() {
    $("#order .btn-secondary").click(function () {


        if ($(this).hasClass("diminuir")) {
            LogicaMudarQuantidadeProdutoUnitarioCarrinho("diminuir", $(this));
           


        }
        if ($(this).hasClass("aumentar")) {
            LogicaMudarQuantidadeProdutoUnitarioCarrinho("aumentar", $(this)); 
           
        }
    });
}
function AtualizarSubTotal() {
    var Subtotal = 0;
    var TagsComPrice = $(".price");
    TagsComPrice.each(function () {
        var ValorReais = parseFloat($(this).text().replace("R$", "").replace(".", "").replace(" ", "").replace(",", "."));

        Subtotal += ValorReais;
    })

    $(".subtotal").text(numberToReal(Subtotal));
   }

function LogicaMudarQuantidadeProdutoUnitarioCarrinho(operacao, botao) {
    OcultarMensagemErro();
    var pai = botao.parent().parent();

    var produtoId = pai.find(".inputProdutoId").val();
    var quantidadeEstoque = parseInt(pai.find(".inputQuantidadeEstoque").val());
    var valorUnitario = parseFloat(pai.find(".inputValorUnid").val().replace(",", "."));

    var campoQuantidadeProdutoCarrinho = pai.find(".inputQuantidadeProdutoCarrinho");
    var quantidadeProdutoCarrinho = parseInt(campoQuantidadeProdutoCarrinho.val());
    var quantidadeProdutoCarrinhoAntiga = quantidadeProdutoCarrinho;
    var campoValor = botao.parent().parent().parent().parent().parent().find(".price");

    if (operacao == "aumentar") {
        if (quantidadeProdutoCarrinho == quantidadeEstoque) {
        } else {
            quantidadeProdutoCarrinho = quantidadeProdutoCarrinhoAntiga + 1;
            campoQuantidadeProdutoCarrinho.val(quantidadeProdutoCarrinho);


            AtualizarQuantidadeEvalor(valorUnitario, quantidadeProdutoCarrinho, campoValor);
            AjaxComunicarAlteracaoQuantidadeProduto(produtoId, quantidadeProdutoCarrinho, quantidadeProdutoCarrinhoAntiga, valorUnitario, campoValor);
         
        }

    } else if (operacao == "diminuir") {
        if (quantidadeProdutoCarrinho == 1) {
            //alert("Opps! Caso não deseje este produto clique no botão Remover")
        } else {
            quantidadeProdutoCarrinho = quantidadeProdutoCarrinhoAntiga - 1;

            campoQuantidadeProdutoCarrinho.val(quantidadeProdutoCarrinho);


            AtualizarQuantidadeEvalor(valorUnitario, quantidadeProdutoCarrinho, campoValor);
            AjaxComunicarAlteracaoQuantidadeProduto(produtoId, quantidadeProdutoCarrinho, quantidadeProdutoCarrinhoAntiga, valorUnitario, campoValor);
            
        }

    }
    AtualizarSubTotal();
    
}

function AtualizarQuantidadeEvalor(valorUnitario, quantidadeProdutoCarrinho, campoValor) {
    var resultado = valorUnitario * quantidadeProdutoCarrinho;
    campoValor.text(numberToReal(resultado));
}

function AjaxComunicarAlteracaoQuantidadeProduto(id, quantidade, quantidadeAntiga, valorUnitario, campoValor) {
    $.ajax({
        type: "GET",
        url: "/CarrinhoCompra/AlterarQtdItem?id=" + id + "&quantidade=" + quantidade,
        error: function (data) {
            MostrarMensagemErro(data.responseJSON.mensagem);
            quantidade = quantidadeAntiga;
            AtualizarQuantidadeEvalor(valorUnitario, quantidade, campoValor);

        },
        success: function () {

        }
    });
}
function MostrarMensagemErro(mensagem) {
    $(".alert-danger").css("display", "block");
    $(".alert-danger").text(mensagem);
}
function OcultarMensagemErro(mensagem) {
    $(".alert-danger").css("display", "none");

}
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
        var Pagina = 1;
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

        var URLComParametros = URL + "?pagina=" + Pagina + "&pesquisa=" + pesquisa + "&ordenacao=" + Ordenacao + Fragmento;


        window.location.href = URLComParametros;
    });


}





