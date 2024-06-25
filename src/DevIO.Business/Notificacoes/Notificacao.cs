namespace DevIO.Business.Notificacoes
{
    public class Notificacao
    {
        public string Mensagem { get; private set; }

        public Notificacao(string mensagem)
        {
            Mensagem = mensagem;
        }
    }
}
