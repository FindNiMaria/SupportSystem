namespace HelpdeskSystem.Models
{
    public class SystemCodeDetail
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int? PedidoNo { get; set; }
        public int SystemCodeId { get; set; }
        public SystemCode SystemCode { get; set; }
        
    }
}
