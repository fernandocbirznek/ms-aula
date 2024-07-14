using static ms_aula.services.UsuarioService;

namespace ms_aula.Interface
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> GetUsuarioByIdAsync(long id);
    }
}
