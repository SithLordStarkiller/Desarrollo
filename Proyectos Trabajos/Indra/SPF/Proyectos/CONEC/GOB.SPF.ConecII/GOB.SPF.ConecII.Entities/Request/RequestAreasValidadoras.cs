namespace GOB.SPF.ConecII.Entities.Request
{
    using System.Collections.Generic;

    public class RequestAreasValidadoras : IRequestBase<AreasValidadoras>
    {
        public string Usuario { get; set; }
        public Paging Paging { get; set; }
        public AreasValidadoras Item { get; set; }
        public List<AreasValidadoras> Lista { get; set; }
    }
}
