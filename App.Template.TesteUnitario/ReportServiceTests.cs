using App.Template.Application.Services;
using App.Template.Infrastructure.Application.Services;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Embedded;

namespace App.Template.UnitTests
{
    /// <summary>
    ///     Tests the ReportService.
    /// </summary>
    [TestFixture]
    public class ReportServiceTests
    {
        private IDocumentStore documentStore;
        private IDocumentSession documentSession;

        [TestFixtureSetUp]
        public void SetUp()
        {
            documentStore = new EmbeddableDocumentStore {RunInMemory = true, Configuration = {Storage = {Voron = {AllowOn32Bits = true}}}};
            documentStore.Initialize();
            documentSession = documentStore.OpenSession();
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            documentSession.Dispose();
            documentStore.Dispose();
        }

        private IReportService CreateReportService()
        {
            return new ReportService(NServiceBus.Testing.Test.Bus, documentSession);            
        }

        /// <summary>
        ///     Tests that there are no reports returned when there are no reports.
        /// </summary>
        [Test]
        public void GetReports_ReturnsEmpty_WhenThereAreNoReports()
        {
            Assert.IsEmpty(CreateReportService().GetReports());
        }
    }
}