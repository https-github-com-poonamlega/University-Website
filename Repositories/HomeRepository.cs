using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using websitee.Models;

namespace websitee.Repositories
{
    public class HomeRepository : IHomeRepository, IDisposable
    {
        private ApplicationDbContext context;

        public HomeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<HelpModels> GetHelpRecords()
        {
            return context.HelpModelss.ToList();
        }

        public HelpModels GetHelpRecordById(Guid helpId)
        {
            return context.HelpModelss.Find(helpId);
        }

        public IEnumerable<HelpModels> GetHelpRecordByEmail(string email)
        {
            return context.HelpModelss.Where(x => x.Email == email).ToList();
        }

        public void InsertHelpRecord(HelpModels helpData)
        {
            context.HelpModelss.Add(helpData);
        }

        public void UpdateHelpAdminResolution(HelpModels helpData)
        {
            context.Entry(helpData).State = System.Data.Entity.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}