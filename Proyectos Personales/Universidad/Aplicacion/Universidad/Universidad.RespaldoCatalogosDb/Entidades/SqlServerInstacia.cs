namespace Universidad.RespaldoCatalogosDb.Entidades
{
    public class SqlServerInstacia
    {
        public int IdServidor { get; set; }
        public string ServerName { get; set; }
        public string InstaceName { get; set; }
        public bool IsEncloustered { get; set; }
        public string Version { get; set; }
        public string CompleteName { get; set; }

    }
}
