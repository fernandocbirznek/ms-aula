namespace ms_aula.Domains
{
    public class AreaFisica : Entity
    {
        public string Descricao { get; set; }
        public virtual ICollection<Aula>? AulaMany { get; set; }
    }
}
