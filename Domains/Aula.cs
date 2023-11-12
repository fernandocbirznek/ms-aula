namespace ms_aula.Domains
{
    public class Aula : Entity
    {
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public long Favoritado { get; set; }
        public long Curtido { get; set; }
        public long ProfessorId { get; set; }

        public virtual ICollection<AulaComentario>? AulaComentarioMany { get; set; }
        public virtual ICollection<AulaSessao>? AulaSessaoMany { get; set; }
        public long AreaFisicaId { get; set; }

        private AreaFisica _AreaFisica;
        public virtual AreaFisica AreaFisica { get { return _AreaFisica; } set { _AreaFisica = value; SetAreaFisica(value); } }

        private void SetAreaFisica(AreaFisica value)
        {
            AreaFisicaId = value is null ? 0 : value.Id;
        }
    }
}
