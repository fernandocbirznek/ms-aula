namespace ms_aula.Domains
{
    public class ArquivoPdf : Entity
    {
        public string Nome { get; set; }
        public byte[] Conteudo { get; set; }
        public string ContentType { get; set; }
        public long AulaId { get; set; }
    }
}
