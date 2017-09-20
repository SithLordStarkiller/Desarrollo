namespace GOB.SPF.ConecII.Entities.Request
{
    using System.Collections.Generic;

    public class RequestReceptoresAlerta : RequestBase
    {
        public Notificaciones Notificacion { get; set; }
        public List<ReceptorAlerta> Receptores { get; set; }
    }
}
