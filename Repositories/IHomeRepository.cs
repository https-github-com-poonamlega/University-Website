using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using websitee.Models;

namespace websitee.Repositories
{
    public interface IHomeRepository: IDisposable
    {
        IEnumerable<HelpModels> GetHelpRecords();
        void InsertHelpRecord(HelpModels helpData);

        void UpdateHelpAdminResolution(HelpModels helpData);
        HelpModels GetHelpRecordById(Guid helpId);

        IEnumerable<HelpModels> GetHelpRecordByEmail(string email);
        void Save();
    }
}