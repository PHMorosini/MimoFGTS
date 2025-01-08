using Microsoft.AspNetCore.Http.HttpResults;
using MimoFGTS.Content.ResultEntity;
using MimoFGTS.Content.Saque.DTO;
using MimoFGTS.Content.Saque.Interface;
using MimoFGTS.Content.Saque.ValueObject;

namespace MimoFGTS.Content.Saque.Service
{
    public class SaqueService : ISaqueService
    {
        // Lista de faixas para o cálculo do saque aniversário
        private static readonly List<(decimal Limite, decimal Aliquota, decimal ParcelaAdicional)> TabelaAniversario = new()
    {
        (500, 0.50m, 0),
        (1000, 0.40m, 50),
        (5000, 0.30m, 150),
        (10000, 0.20m, 650),
        (15000, 0.15m, 1150),
        (20000, 0.10m, 1900),
        (decimal.MaxValue, 0.10m, 2900)
    };

        public async Task<Result<SaqueResponseDTO>> CalcularSaque(SaqueDTO saqueDTO)
        {
            if (saqueDTO != null)
            {
                if (saqueDTO.TipoSaque == TipoSaqueEnum.Aniversario)
                {
                    return await CalcularValorDisponivelParaSaqueAniversario(saqueDTO);
                }

                if (saqueDTO.TipoSaque == TipoSaqueEnum.Rescisao)
                {
                    return await CalcularValorDisponivelParaSaqueRescisao(saqueDTO);
                }
            }
            return Result<SaqueResponseDTO>.Fail("Não foi possivel fazer o calculo.");
        }

        public async Task<Result<SaqueResponseDTO>> CalcularValorDisponivelParaSaqueAniversario(SaqueDTO saqueDTO)
        {
            return await Task.Run(() =>
            {
                decimal saldoFGTS = saqueDTO.SaldoFGTS;
                decimal saldoAtualizado = saldoFGTS;
                decimal somalancamentos = 0;
                decimal valorDisponivel = 0;

                int mesAtual = DateTime.UtcNow.Month;
                int diferencaMeses = saqueDTO.Mes - mesAtual;
                if (diferencaMeses <= 0) diferencaMeses += 12;

                if (diferencaMeses > 0)
                {
                    somalancamentos = saqueDTO.SalarioAtual * (0.08m * (diferencaMeses - 1));
                    saldoAtualizado += somalancamentos;
                }

                // Tabela de faixas com limites, alíquotas e parcelas adicionais
                var faixas = new List<(decimal Limite, decimal Aliquota, decimal ParcelaAdicional)>
        {
            (500, 0.50m, 0),
            (1000, 0.40m, 50),
            (5000, 0.30m, 150),
            (10000, 0.20m, 650),
            (15000, 0.15m, 1150),
            (20000, 0.10m, 1900),
            (decimal.MaxValue, 0.05m, 2900)
        };

                var faixaAplicavel = faixas.FirstOrDefault(f => saldoFGTS <= f.Limite);
                if (faixaAplicavel == default)
                {
                    return Result<SaqueResponseDTO>.Fail("Nenhuma faixa aplicável encontrada.");
                }

                valorDisponivel = (saldoFGTS * faixaAplicavel.Aliquota) + faixaAplicavel.ParcelaAdicional;

                if (diferencaMeses > 0)
                {
                    valorDisponivel = (saldoAtualizado * faixaAplicavel.Aliquota) + faixaAplicavel.ParcelaAdicional;
                }

                var newSaque = new SaqueResponseDTO
                {
                    SaldoFgts = saqueDTO.SaldoFGTS,
                    SaldoDisponivel = valorDisponivel,
                    TipoSaque = TipoSaqueEnum.Aniversario,
                    SaldoDisponivelComLancamentos = saldoAtualizado
                };

                return Result<SaqueResponseDTO>.Ok(newSaque);
            });
        }

        public async Task<Result<SaqueResponseDTO>> CalcularValorDisponivelParaSaqueRescisao(SaqueDTO saqueDTO)
        {
            return await Task.Run(() =>
            {
                if (saqueDTO != null)
                {
                    decimal saldoFGTS = saqueDTO.SaldoFGTS;
                    var saldoDisponivel = (saldoFGTS * 0.40m) + saldoFGTS; // 40% referente à multa rescisória

                    var newSaque = new SaqueResponseDTO
                    {
                        SaldoDisponivel = saldoDisponivel,
                        TipoSaque = TipoSaqueEnum.Rescisao
                    };
                    return Result<SaqueResponseDTO>.Ok(newSaque);
                }
                return Result<SaqueResponseDTO>.Fail("Não foi possivel fazer o calculo.");
            });
        }
    }
}
