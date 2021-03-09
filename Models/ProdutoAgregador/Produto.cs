using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LojaVirtual.Models.ProdutoAgregador
{
    public class Produto
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [JsonIgnore]
        public string Nome { get; set; }
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name ="Descrição")]
        [JsonIgnore]
        public string Descricao { get; set; }
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Preço")]
        [JsonIgnore]
        public decimal Valor { get; set; }
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Range(0,100000, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        [JsonIgnore]
        public int? Quantidade { get; set; }

        // Frete Correios
        [JsonIgnore]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Range(0.001, 30, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public double? Peso { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Range(11, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        [JsonIgnore]
        public double? Largura { get; set; }

        
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Range(2, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        [JsonIgnore]
        public double? Altura { get; set; }

        
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Range(16, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        [JsonIgnore]
        public double? Comprimento { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [JsonIgnore]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }


        //Imagens
        [JsonIgnore]
        public ICollection<Imagem> Imagens { get; set; }


    }
}
