using FixerApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FixerApp.Data
{
    public interface IDatabase
    {
        // FixerJob Methods
        Task<int> AddFixerJob(FixerJob fixerJob);
        Task<int> DeleteFixerJob(FixerJob fixerJob);
        Task<FixerJob> GetFixerJob(int id);
        Task<List<FixerJob>> GetFixerJobs();
        Task<int> UpdateFixerJob(FixerJob fixerJob);

        // Kalender Methods
        Task<int> AddKalender(Kalender kalender);
        Task<int> DeleteKalender(Kalender kalender);
        Task<Kalender> GetKalender(int id);
        Task<List<Kalender>> GetKalenders();
        Task<int> UpdateKalender(Kalender kalender);


        // Faktura Methods
        Task<int> AddFaktura(Faktura faktura);
        Task<int> DeleteFaktura(Faktura faktura);
        Task<Faktura> GetFaktura(int id);
        Task<int> UpdateFaktura(Faktura faktura);
    }
}