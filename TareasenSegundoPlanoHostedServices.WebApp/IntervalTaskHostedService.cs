﻿using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System;

namespace TareasenSegundoPlanoHostedServices.WebApp
{
    public class IntervalTaskHostedService : IHostedService, IDisposable
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SaveFile, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        public void SaveFile(object state)
        {
            string nameFile = "a" + new Random().Next(1000) + ".txt";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp" , nameFile);

            File.Create(path);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
