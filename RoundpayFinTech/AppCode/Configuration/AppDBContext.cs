using Microsoft.EntityFrameworkCore;
using RoundpayFinTech.AppCode.Model;
using RoundpayFinTech.Models.EFTModels;

namespace RoundpayFinTech.AppCode.Configuration
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<WebsiteMaster> MASTER_WEBSITE { get; set; }
        public DbSet<MasterRechPlanType> MASTER_RECH_PLAN_TYPE { get; set; }
        public DbSet<RechargePlans> tbl_RechargePlans { get; set; }
        public DbSet<ApplicationSetting_EFT> ApplicationSetting { get; set; }
    }
}
