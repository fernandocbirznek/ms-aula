namespace ms_aula.Domains
{
    public class AulaSessaoFavoritada : Entity
    {
        public long UsuarioId { get; set; }
        public long AulaSessaoId { get; set; }
        public long MuralPosicaoX { get; set; }
        public long MuralPosicaoY { get; set; }

        private AulaSessao _AulaSessao;
        public virtual AulaSessao AulaSessao { get { return _AulaSessao; } set { AulaSessao = value; SetAulaSessao(value); } }

        private void SetAulaSessao(AulaSessao value)
        {
            AulaSessaoId = value is null ? 0 : value.Id;
        }
    }
}
