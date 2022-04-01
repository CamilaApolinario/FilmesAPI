using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsuariosApi.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinatario { get; set; } //tipo especial que identifica um endereço de email/ composição de um email/pacote MailKit e MimeKit
        public string Assunto { get; set; }
        public string Conteudo { get; set; }
        public IEnumerable<string> Destinatario1 { get; }

        public Mensagem(IEnumerable<string> destinatario, string assunto, int usuarioId, string codigo)
        {
            Destinatario = new List<MailboxAddress>(); // lista de destinatarios MailboxAddress, adiciona a ela um novo elemento um destinatarios que recebemos por parametros
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress(d)));// transforma de uma string esse detinatario a um MailboxAddress
            Assunto = assunto;
            Conteudo = $"http://localhost:6000/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";
        }
    }
}
