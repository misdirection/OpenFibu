namespace OpenFibu.Data.RavenDb.Configuration;

public class DocumentStoreSettings
{
    public string DatabaseName { get; set; }
    public string[] Urls { get; set; }
    public string CertPath { get; set; }
    public string CertPassword { get; set; }
}