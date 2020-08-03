<Query Kind="Program">
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
</Query>

void Main()
{
	
}


    private X509Certificate2 GetAuthCertificate()
    {
		X509Certificate2 certificate = null;
        X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
        store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

        X509Certificate2Collection col = store.Certificates;

        X509Certificate2Collection found = col.Find(X509FindType.FindByThumbprint, "5f3f7ac2569f50a4667647c6a18ca007aaedbb8e", true);

        if (found != null || found.Count > 0)
        {
            if (found.Count == 1)
            {
                certificate = found[0];
            }
        }

        store.Close();

        if (certificate == null)
            throw new InvalidOperationException("No security certificate found.");

        return certificate;
    }