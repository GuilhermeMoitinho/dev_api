using DevIO.Business.Interfaces;

namespace DevIO.Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        void INotificador.Add(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        List<Notificacao> INotificador.ObterNotificacoes()
        {
            return _notificacoes;
        }

        bool INotificador.TemNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}
