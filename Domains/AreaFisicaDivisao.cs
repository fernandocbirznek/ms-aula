namespace ms_aula.Domains
{
    public class AreaFisicaDivisao : Entity
    {
        public string? Titulo { get; set; }
        public string Descricao { get; set; }
        public byte[]? Foto { get; set; }

        public long AreaFisicaId { get; set; }

        private AreaFisica _AreaFisica;
        public virtual AreaFisica AreaFisica { get { return _AreaFisica; } set { _AreaFisica = value; SetAreaFisica(value); } }

        private void SetAreaFisica(AreaFisica value)
        {
            AreaFisicaId = value is null ? 0 : value.Id;
        }
    }
}
