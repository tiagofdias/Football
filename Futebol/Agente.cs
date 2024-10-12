
namespace Futebol
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Agente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agente()
        {
            this.Treinador = new HashSet<Treinador>();
            this.Jogadores = new HashSet<Jogadores>();
        }
    
        public int IDAgente { get; set; }

        [Required(ErrorMessage = "O campo nome é de preenchimento obrigatório")]
        [RegularExpression(@"^[a-z^à-ú A-ZÀ-ú]+$", ErrorMessage = "Introduza o nome corretamente")]
        [MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength(100)]
        public string Morada { get; set; }

        [DataType(DataType.PostalCode)]
        [RegularExpression(@"\d{4}([\-]\d{3})?", ErrorMessage = "Introduza o código postal corretamente")]
        public string CodigoPostal { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Introduza o Telefone corretamente")]
        public Nullable<int> Telefone { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Treinador> Treinador { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jogadores> Jogadores { get; set; }
    }
}
