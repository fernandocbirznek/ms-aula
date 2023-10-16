namespace ms_aula.Domains
{
    public class AulaComentario : Entity
    {
        public string Descricao { get; set; }
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }

        private Aula _AulaId;
        public virtual Aula Aula { get { return _AulaId; } set { _AulaId = value; SetAula(value); } }

        private void SetAula(Aula value)
        {
            AulaId = value is null ? 0 : value.Id;
        }
    }
}
