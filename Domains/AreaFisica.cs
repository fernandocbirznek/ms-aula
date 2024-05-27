namespace ms_aula.Domains
{
    public class AreaFisica : Entity
    {
        public string? Titulo {  get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Aula>? AulaMany { get; set; }
        public string? Aplicacao { get; set; }
        public virtual ICollection<AreaFisicaDivisao>? DivisaoMany { get; set; }
    }
}
