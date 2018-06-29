﻿using System.IO;
using Microsoft.Extensions.Configuration;

namespace NethereumChain
{
    public static class ConfigurationProvider
    {
        private static IConfiguration Configuration
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                return builder.Build();
            }
        }

        public static string BlockchainAddress =>
            $"http://{Configuration["Blockchain:Network:Host"]}:{Configuration["Blockchain:Network:Port"]}";
    }
}
