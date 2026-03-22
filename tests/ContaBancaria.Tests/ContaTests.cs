using Xunit;
using ContaBancaria;

namespace ContaBancaria.Tests;

/// <summary>
/// Testes unitários para a classe Conta.
/// 
/// PARTE 1 — Testes de exemplo (Construtor) já estão prontos.
///           Observe o padrão AAA e o uso de [Fact] e [Theory].
///
/// PARTE 2 — Você deve escrever os testes para os demais métodos
///           seguindo rigorosamente o ciclo TDD: Red → Green → Refactor.
///
/// Para cada método da classe Conta, crie testes que cubram:
///   ✅ O cenário de sucesso (caminho feliz)
///   ❌ Cada regra de validação (cenários de exceção)
///   🔄 Casos de borda (valores limites)
/// </summary>
public class ContaTests
{
    // =======================================================
    //  PARTE 1 — EXEMPLO GUIADO: Testes do Construtor
    //  Observe o padrão Arrange-Act-Assert (AAA)
    // =======================================================

    [Fact]
    public void Construtor_DadosValidos_CriaContaCorretamente()
    {
        // Arrange & Act
        var conta = new Conta("Maria", 100);

        // Assert
        Assert.Equal("Maria", conta.Titular);
        Assert.Equal(100, conta.Saldo);
        Assert.True(conta.Ativa);
    }

    [Fact]
    public void Construtor_SemSaldoInicial_CriaContaComSaldoZero()
    {
        // Arrange & Act
        var conta = new Conta("João");

        // Assert
        Assert.Equal("João", conta.Titular);
        Assert.Equal(0, conta.Saldo);
        Assert.True(conta.Ativa);
    }

    [Fact]
    public void Construtor_TitularNulo_LancaArgumentException()
    {
        // Assert — verifica que a exceção é lançada
        Assert.Throws<ArgumentException>(() => new Conta(null!));
    }

    [Fact]
    public void Construtor_TitularVazio_LancaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Conta(""));
    }

    [Fact]
    public void Construtor_SaldoNegativo_LancaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Conta("Maria", -50));
    }

    [Theory]
    [InlineData("Ana", 0)]
    [InlineData("Carlos", 1000)]
    [InlineData("Beatriz", 0.01)]
    public void Construtor_VariosValoresValidos_CriaContaCorretamente(string titular, decimal saldo)
    {
        // Act
        var conta = new Conta(titular, saldo);

        // Assert
        Assert.Equal(titular, conta.Titular);
        Assert.Equal(saldo, conta.Saldo);
        Assert.True(conta.Ativa);
    }

    // =======================================================
    //  PARTE 2 — ESCREVA OS TESTES ABAIXO (TDD)
    //  Lembre-se: escreva o teste PRIMEIRO, veja FALHAR (Red),
    //  depois implemente o código para PASSAR (Green),
    //  e por fim faça Refactor se necessário.
    // =======================================================

    // =======================================================
    //  Testes para Depositar
    [Fact]
    public void Depositar_ValorValido_AtualizaSaldo()
    {
        var conta = new Conta("Maria", 100);
        conta.Depositar(50);
        Assert.Equal(150, conta.Saldo);
    }

    [Fact]
    public void Depositar_ValorZero_LancaArgumentException()
    {
        var conta = new Conta("Maria", 100);
        Assert.Throws<ArgumentException>(() => conta.Depositar(0));
    }

    [Fact]
    public void Depositar_ValorNegativo_LancaArgumentException()
    {
        var conta = new Conta("Maria", 100);
        Assert.Throws<ArgumentException>(() => conta.Depositar(-10));
    }

    [Fact]
    public void Depositar_ContaInativa_LancaInvalidOperationException()
    {
        var conta = new Conta("Maria", 0);
        conta.Encerrar(); // Deixa a conta inativa
        Assert.Throws<InvalidOperationException>(() => conta.Depositar(50));
    }
    // =======================================================


    // =======================================================
    //  Testes para Sacar
    [Fact]
    public void Sacar_ValorValido_AtualizaSaldo()
    {
        var conta = new Conta("Maria", 100);
        conta.Sacar(40);
        Assert.Equal(60, conta.Saldo);
    }

    [Fact]
    public void Sacar_ValorMaiorQueSaldo_LancaInvalidOperationException()
    {
        var conta = new Conta("Maria", 100);
        Assert.Throws<InvalidOperationException>(() => conta.Sacar(150));
    }

    [Fact]
    public void Sacar_ValorZero_LancaArgumentException()
    {
        var conta = new Conta("Maria", 100);
        Assert.Throws<ArgumentException>(() => conta.Sacar(0));
    }

    [Fact]
    public void Sacar_ValorNegativo_LancaArgumentException()
    {
        var conta = new Conta("Maria", 100);
        Assert.Throws<ArgumentException>(() => conta.Sacar(-20));
    }

    [Fact]
    public void Sacar_ContaInativa_LancaInvalidOperationException()
    {
        var conta = new Conta("Maria", 0);
        conta.Encerrar();
        Assert.Throws<InvalidOperationException>(() => conta.Sacar(50));
    }

    // =======================================================


    // =======================================================
    //  Testes para Transferir
    [Fact]
    public void Transferir_ValorValido_AtualizaSaldoDeAmbasContas()
    {
        var origem = new Conta("Maria", 200);
        var destino = new Conta("João", 100);

        origem.Transferir(destino, 50);

        Assert.Equal(150, origem.Saldo);
        Assert.Equal(150, destino.Saldo);
    }

    [Fact]
    public void Transferir_SaldoInsuficiente_LancaInvalidOperationException()
    {
        var origem = new Conta("Maria", 100);
        var destino = new Conta("João", 100);

        Assert.Throws<InvalidOperationException>(() => origem.Transferir(destino, 150));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Transferir_ValorZeroOuNegativo_LancaArgumentException(decimal valor)
    {
        var origem = new Conta("Maria", 200);
        var destino = new Conta("João", 100);

        Assert.Throws<ArgumentException>(() => origem.Transferir(destino, valor));
    }

    [Fact]
    public void Transferir_ContaOrigemInativa_LancaInvalidOperationException()
    {
        var origem = new Conta("Maria", 0);
        origem.Encerrar();
        var destino = new Conta("João", 100);

        Assert.Throws<InvalidOperationException>(() => origem.Transferir(destino, 50));
    }

    [Fact]
    public void Transferir_ContaDestinoInativa_LancaInvalidOperationException()
    {
        var origem = new Conta("Maria", 200);
        var destino = new Conta("João", 0);
        destino.Encerrar();

        Assert.Throws<InvalidOperationException>(() => origem.Transferir(destino, 50));
    }
    // =======================================================


    // =======================================================
    //  Testes para Encerrar
    //  Sugestão de testes:
    //    - Encerrar conta com saldo zero funciona
    //    - Encerrar conta com saldo lança InvalidOperationException
    //    - Encerrar conta já inativa lança InvalidOperationException
    //    - Conta encerrada tem Ativa == false
    // =======================================================

}
