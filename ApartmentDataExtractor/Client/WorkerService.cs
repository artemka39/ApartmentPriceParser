using ApartmentPriceParser.Common.Models;
using HtmlAgilityPack;
using System;
using System.Timers;
using Client.DataProviders;

namespace Client
{
    public class WorkerService : IHostedService
    {
        private static System.Timers.Timer timer;
        private readonly IBackendDataManager backendDataManager;
        private readonly Dictionary<ProviderType, IDataProvider> providers;
        public WorkerService(IBackendDataManager backendDataManager, Dictionary<ProviderType, IDataProvider> providers)
        {
            this.backendDataManager = backendDataManager;
            this.providers = providers;
        }
        private async Task HandleDefinitions(ProviderType providerType, List<object> definitions, List<ApartmentData> result)
        {
            var provider = providers[providerType];
            foreach (var definition in definitions)
            {
                var apartmentData = await provider.GetApartmentData(definition);
                result.Add(apartmentData);
            }
        }
        private async Task HandleTimed()
        {
            var backendRequest = backendDataManager.GetJobDefinitions();
            var response = new Dictionary<ProviderType, List<ApartmentData>>();
            var providerTasks = new List<Task>();
            foreach (var providerData in backendRequest)
            {
                var resultList = new List<ApartmentData>();
                response.Add(providerData.Key, resultList);
                var providerTask = HandleDefinitions(providerData.Key, providerData.Value, resultList);
                providerTasks.Add(providerTask);
            }
            await Task.WhenAll(providerTasks);
            backendDataManager.UpdateData(response);
        }
        private async void OnTimed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                await HandleTimed();
            }
            catch(Exception ex)
            {
                
            }
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += OnTimed;
            timer.AutoReset = false;
            timer.Enabled = true;
            timer.Start();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Stop();
            timer.Elapsed -= OnTimed;
            return Task.CompletedTask;
        }

    }
}
