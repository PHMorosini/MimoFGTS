function handleSubmit() {
    const saqueDTO = {
        saldoFGTS: parseFloat($('#SaldoAtual').val()),
        salarioAtual: parseFloat($('#SalarioAtual').val()),
        mes: parseInt($('#mes').val()),
        tipoSaque: 1 
    };
    $.ajax({
        url: 'https://localhost:7189/api/Saque/calcular-saque',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(saqueDTO),
        success: function (response) {
            console.log(response);
            const saqueResponseHTML = `
        <div class="card shadow-lg border-0 rounded-3">
            <div class="card-body">
                <h3 class="mb-4 text-center text-primary">
                    <i class="fas fa-calculator"></i> Valores do Saque
                </h3>
                <div class="list-group">
                    <p class="list-group-item d-flex justify-content-between align-items-center">
                        <strong>Saldo FGTS:</strong>
                        <span class="badge bg-success rounded-pill">R$ ${response.saldoFgts.toFixed(2)}</span>
                    </p>
                    <p class="list-group-item d-flex justify-content-between align-items-center">
                        <strong>Saldo Disponível com Lançamentos:</strong>
                        <span class="badge bg-warning rounded-pill">R$ ${response.saldoDisponivelComLancamentos ? response.saldoDisponivelComLancamentos.toFixed(2) : 'Não disponível'}</span>
                    </p>
                    <p class="list-group-item d-flex justify-content-between align-items-center">
                        <strong>Valor do saque:</strong>
                        <span class="badge bg-info rounded-pill">R$ ${response.saldoDisponivel.toFixed(2)}</span>
                    </p>
                </div>
            </div>
        </div>

            `;
            $('#resultadoSaque').html(saqueResponseHTML);
        },
        error: function (xhr) {
            alert('Erro ao calcular saque: ' + xhr.responseText);
        }
    });
}

$(document).ready(function () {
    $('.btn-primary').on('click', function (e) {
        e.preventDefault(); 
        handleSubmit();
    });
});
