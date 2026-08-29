using Movie_Management_System.ViewModels;

namespace Movie_Management_System.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardAsync();
    }
}