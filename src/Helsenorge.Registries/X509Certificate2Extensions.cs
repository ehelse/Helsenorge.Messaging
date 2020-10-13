﻿using System.Security.Cryptography.X509Certificates;

namespace Helsenorge.Registries
{
    internal static class X509Certificate2Extensions
    {
        internal static bool HasKeyUsage(this X509Certificate2 certificate, X509KeyUsageFlags keyUsage)
        {
            foreach (var extension in certificate.Extensions)
            {
                switch (extension.Oid.Value)
                {
                    case "2.5.29.15": // Key usage
                        var usageExtension = (X509KeyUsageExtension)extension;
                        if ((usageExtension.KeyUsages & keyUsage) == keyUsage)
                        {
                            return true;
                        }
                        break;
                }
            }

            return false;
        }
    }
}
