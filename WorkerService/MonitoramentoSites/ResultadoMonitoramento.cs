namespace MonitoramentoSites
{
    public class ResultadoMonitoramento
    {
        public string Framework { get; set; }
        public string Horario { get; set; }
        public string Host { get; set; }
        public string Status { get; set; }
        public object Exception { get; set; }
    }
}