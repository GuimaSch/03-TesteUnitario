namespace ContaBancaria;

/// <summary>
/// Classe Conta Bancária — laboratório de Testes Unitários e TDD.
/// 
/// INSTRUÇÕES:
///   1. Leia os requisitos de cada método (summary + regras).
///   2. Escreva os testes PRIMEIRO no arquivo ContaTests.cs.
///   3. Execute os testes e veja-os FALHAR (Red).
///   4. Implemente o código mínimo para os testes PASSAREM (Green).
///   5. Refatore se necessário (Refactor).
/// </summary>
public class Conta
{
    public string Titular { get; private set; }
    public decimal Saldo { get; private set; }
    public bool Ativa { get; private set; }

    /// <summary>
    /// Cria uma conta bancária.
    /// Regras:
    ///   - O titular não pode ser nulo ou vazio (lançar ArgumentException).
    ///   - O saldo inicial não pode ser negativo (lançar ArgumentException).
    ///   - A conta deve ser criada como ativa.
    /// </summary>
    public Conta(string titular, decimal saldoInicial = 0)
    {
        if (string.IsNullOrWhiteSpace(titular))
            throw new ArgumentException("O titular não pode ser nulo ou vazio.", nameof(titular));
        if (saldoInicial < 0)
            throw new ArgumentException("O saldo inicial não pode ser negativo.", nameof(saldoInicial));

        Titular = titular;
        Saldo = saldoInicial;
        Ativa = true;
    }

    /// <summary>
    /// Deposita um valor na conta.
    /// Regras:
    ///   - Valor deve ser maior que zero (lançar ArgumentException).
    ///   - Conta deve estar ativa (lançar InvalidOperationException).
    ///   - O saldo deve ser atualizado corretamente.
    /// </summary>
    public void Depositar(decimal valor)
    {
        if (valor <= 0) throw new ArgumentException("O valor do depósito deve ser maior que zero.", nameof(valor));
        if (!Ativa) throw new InvalidOperationException("Não é possível depositar em uma conta inativa.");

        Saldo += valor;
    }


    /// <summary>
    /// Saca um valor da conta.
    /// Regras:
    ///   - Valor deve ser maior que zero (lançar ArgumentException).
    ///   - Conta deve estar ativa (lançar InvalidOperationException).
    ///   - Não pode sacar mais do que o saldo (lançar InvalidOperationException).
    ///   - O saldo deve ser atualizado corretamente.
    /// </summary>
    public void Sacar(decimal valor)
    {
        if (valor <= 0) throw new ArgumentException("O valor do saque deve ser maior que zero.", nameof(valor));
        if (!Ativa) throw new InvalidOperationException("Não é possível sacar de uma conta inativa.");
        if (valor > Saldo) throw new InvalidOperationException("Saldo insuficiente para o saque.");

        Saldo -= valor;
    }

    /// <summary>
    /// Transfere valor desta conta para outra.
    /// Regras:
    ///   - As duas contas devem estar ativas (lançar InvalidOperationException).
    ///   - Valor deve ser maior que zero (lançar ArgumentException).
    ///   - Saldo deve ser suficiente (lançar InvalidOperationException).
    ///   - O saldo de ambas as contas deve ser atualizado corretamente.
    /// </summary>
    public void Transferir(Conta destino, decimal valor)
    {
        // TODO: Implemente usando TDD
        throw new NotImplementedException();
    }

    /// <summary>
    /// Encerra a conta.
    /// Regras:
    ///   - A conta já deve estar ativa (lançar InvalidOperationException se já inativa).
    ///   - O saldo deve ser zero para encerrar (lançar InvalidOperationException se houver saldo).
    ///   - A propriedade Ativa deve ser alterada para false.
    /// </summary>
    public void Encerrar()
    {
        // TODO: Implemente usando TDD
        throw new NotImplementedException();
    }
}
