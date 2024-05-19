namespace ms_aula.Domains
{
    public class AulaTag : Entity
    {
        public long TagId { get; set; }
        private Tag _Tag;
        public virtual Tag Tag { get { return _Tag; } set { _Tag = value; SetTag(value); } }

        private void SetTag(Tag value)
        {
            TagId = value is null ? 0 : value.Id;
        }

        public long AulaId { get; set; }
        private Aula _Aula;
        public virtual Aula Aula { get { return _Aula; } set { _Aula = value; SetAula(value); } }

        private void SetAula(Aula value)
        {
            AulaId = value is null ? 0 : value.Id;
        }
    }
}
