using NUnit.Framework;

namespace App.Template.TestesUnitarios
{
    [TestFixture]
    public class UT_InicializarAplicacao
    {
        [Test]
        public void QuandoInicializarAplicacao_Entao_MensagemSucesso()
        {
            //configuracao
            var mensagem_esperada = "Aplicacao executandao com sucesso.";

            //acoes
            var aplicacao = new { Mensagem = "Aplicacao executando com sucesso." };

            //validacoes
            Assert.AreEqual(mensagem_esperada, aplicacao.Mensagem);
        }
    }
}
