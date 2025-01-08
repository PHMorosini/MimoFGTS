using MimoFGTS.Content.Saque.ValueObject;
using System.ComponentModel.DataAnnotations;

namespace MimoFGTS.Content.Saque.DTO
{
    public class SaqueDTO
    {

        [Required(ErrorMessage = "O saldo do FGTS é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O saldo do FGTS deve ser maior ou igual a zero.")]
        public decimal SaldoFGTS { get; set; }

        [Required(ErrorMessage = "O saldo atual é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O salario atual deve ser maior ou igual a zero.")]
        public decimal SalarioAtual { get; set; }

        [Required(ErrorMessage = "O mês é obrigatório.")]
        [Range(1, 12, ErrorMessage = "O mês deve estar entre 1 e 12.")]
        public int Mes { get; set; }

        [Required(ErrorMessage = "O tipo de saque é obrigatório.")]
        public TipoSaqueEnum TipoSaque { get; set; }
    }

    public class SaqueResponseDTO
    {
        public decimal SaldoFgts { get; set; }
        public decimal SaldoDisponivel { get; set; }
        public decimal? SaldoDisponivelComLancamentos { get; set; }
        public TipoSaqueEnum TipoSaque { get; set; }
    }
}
