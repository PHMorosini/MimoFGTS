using MimoFGTS.Content.Saque.ValueObject;

namespace MimoFGTS.Content.Saque.Entity
{
    public class SaqueEntity
    {
        public decimal SaldoFGTS { get; set; }
        public decimal SalarioAtual { get; set; }
        public int Mes { get; set; }
        public TipoSaqueEnum TipoSaque { get; set; }
    }
}
