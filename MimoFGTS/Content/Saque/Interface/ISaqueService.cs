using Microsoft.AspNetCore.Http.HttpResults;
using MimoFGTS.Content.ResultEntity;
using MimoFGTS.Content.Saque.DTO;

namespace MimoFGTS.Content.Saque.Interface
{
    public interface ISaqueService
    {
         Task<Result<SaqueResponseDTO>> CalcularValorDisponivelParaSaqueAniversario(SaqueDTO saqueDTO);
         Task<Result<SaqueResponseDTO>> CalcularValorDisponivelParaSaqueRescisao(SaqueDTO saqueDTO);
         Task<Result<SaqueResponseDTO>> CalcularSaque(SaqueDTO saqueDTO);
    }
}
