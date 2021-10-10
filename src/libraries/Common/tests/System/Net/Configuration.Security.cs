// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Net.Test.Common
{
    public static partial class Configuration
    {
        public static partial class Security
        {
            // Domain server environment.
            public static string ActiveDirectoryName => GetValue("DOTNET_TEST_NET_AD_DOMAINNAME");
            public static string ActiveDirectoryUserName => GetValue("DOTNET_TEST_NET_AD_USERNAME");
            public static string ActiveDirectoryUserPassword => GetValue("DOTNET_TEST_NET_AD_PASSWORD");

            // Standalone server environment.
            public static string WindowsServerRealmName => GetValue("DOTNET_TEST_NET_SERVER_REALMNAME");
            public static string WindowsServerUserName => GetValue("DOTNET_TEST_NET_SERVER_USERNAME");
            public static string WindowsServerUserPassword => GetValue("DOTNET_TEST_NET_SERVER_PASSWORD");

            public static Uri TlsServer => GetUriValue("DOTNET_TEST_NET_SECURITY_TLSSERVERURI", new Uri("https://" + DefaultAzureServer));

            public static Uri NegotiateServer => GetUriValue("DOTNET_TEST_NET_SECURITY_NEGOSERVERURI");

            public static Uri NegotiateClient => GetUriValue("DOTNET_TEST_NET_SECURITY_NEGOCLIENTURI");

            public static string NegotiateClientUser => GetValue("DOTNET_TEST_NET_SECURITY_NEGOCLIENTUSER");

            public static string TlsRenegotiationServer => GetValue("DOTNET_TEST_NET_SECURITY_TLSREGOTIATIONSERVER", "corefx-net-tls.azurewebsites.net");

            // This should be set if hostnames for all certificates within runtime-assets have been set to point to 127.0.0.1.
            // Example:
            //      127.0.0.1 testservereku.contoso.com
            //      127.0.0.1 testnoeku.contoso.com
            //      127.0.0.1 testclienteku.contoso.com

            public static string HostsFileNamesInstalled => GetValue("DOTNET_TEST_NET_SECURITY_HOSTS_FILE_INSTALLED");
            // Allows packet captures.
            public static string SecurityForceSocketStreams => GetValue("DOTNET_TEST_NET_SECURITY_FORCE_SOCKET_STREAMS");
        }
    }
}
