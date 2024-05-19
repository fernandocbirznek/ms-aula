namespace ms_aula.Domains
{
    public class Tag : Entity
    {
        public string Nome { get; set; }
        public virtual ICollection<AulaTag>? AulaTagMany { get; set; }
    }
}
