using SistemaCaixa.Models;


public interface IAuthService
{
    (bool sucesso, Usuario user, string mensagem, string token) Login(string email, string senha);
}