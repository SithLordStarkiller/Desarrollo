namespace PubliPayments.Entidades
{
    public class EntReporteGestionMovilSp
    {
        public void ActualizarTabla()
        {
            var contexto = new SistemasCobranzaEntities();
            contexto.Database.CommandTimeout = 300;
            var result=contexto.Database.ExecuteSqlCommand("ObtenerTablaReporteGestionMovil");
        }
    }
}
