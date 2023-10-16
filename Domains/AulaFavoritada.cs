namespace ms_aula.Domains
{
    public class AulaFavoritada : Entity
    {
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }

        private Aula _Aula;
        public virtual Aula Aula { get { return _Aula; } set { Aula = value; SetAula(value); } }

        private void SetAula(Aula value)
        {
            AulaId = value is null ? 0 : value.Id;
        }
    }
}
