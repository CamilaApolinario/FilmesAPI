namespace UsuariosApi.Models
{
    public class Token
    {
        public string Value { get;}//vai carregar um valor para nosso usuario
        public Token(string value)
        {
            Value = value;
        }
    }
}
