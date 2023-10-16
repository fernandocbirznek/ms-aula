namespace ms_aula.Domains
{
    public class AulaSessao : Entity
    {
        public long Ordem { get; set; }
        public string Conteudo { get; set; }
        public long Favoritado { get; set; }
        public AulaSessaoTipo AulaSessaoTipo { get; set; }
        public long AulaId { get; set; }

        private Aula _AulaId;
        public virtual Aula Aula { get { return _AulaId; } set { _AulaId = value; SetAula(value); } }

        private void SetAula(Aula value)
        {
            AulaId = value is null ? 0 : value.Id;
        }
    }
}
