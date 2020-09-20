using System;
using MigrationTools.Core.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VstsSyncMigrator.Engine;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MigrationTools.Core.Engine.Containers;
using MigrationTools.Services;

namespace _VstsSyncMigrator.Engine.Tests
{
    [TestClass]
    public class MigrationEngineTests
    {
        IEngineConfigurationBuilder ecb;
        IServiceProvider services;

        [TestInitialize]
        public void Setup()
        {
            ecb = new EngineConfigurationBuilder();
            var sc = new ServiceCollection();
            sc.AddSingleton<IDetectOnlineService, DetectOnlineService>();
            sc.AddSingleton<IDetectVersionService, DetectVersionService>();
            sc.AddSingleton<IEngineConfigurationBuilder, EngineConfigurationBuilder>();
            sc.AddSingleton<EngineConfiguration>(ecb.BuildDefault());
            sc.AddSingleton<ProcessorContainer>();
            sc.AddSingleton<TypeDefinitionMapContainer>();
            sc.AddSingleton<GitRepoMapContainer>();
            sc.AddSingleton<ChangeSetMappingContainer>();
            sc.AddSingleton<MigrationEngine>();
            services = sc.BuildServiceProvider();
        }

        [TestMethod]
        public void TestEngineCreation()
        {

            MigrationEngine me = services.GetRequiredService<MigrationEngine>();
        }

        [TestMethod]
        public void TestEngineExecuteEmptyProcessors()
        {
            EngineConfiguration ec = ecb.BuildDefault();
            ec.Processors.Clear();
            MigrationEngine me = new MigrationEngine(services, ec);
            me.Run();

        }

        [TestMethod]
        public void TestEngineExecuteEmptyFieldMaps()
        {
            EngineConfiguration ec = ecb.BuildDefault();
            ec.Processors.Clear();
            ec.FieldMaps.Clear();
            MigrationEngine me = new MigrationEngine(services, ec);
            me.Run();
        }

        [TestMethod]
        public void TestEngineExecuteProcessors()
        {
            EngineConfiguration ec = ecb.BuildDefault();
            ec.FieldMaps.Clear();
            MigrationEngine me = new MigrationEngine(services, ec);
            me.Run();
        }

    }
}
