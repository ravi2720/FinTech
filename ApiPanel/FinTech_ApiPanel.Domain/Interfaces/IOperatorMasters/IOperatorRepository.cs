using FinTech_ApiPanel.Domain.Entities.OperatorMasters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech_ApiPanel.Domain.Interfaces.IOperatorMasters
{
    public interface IOperatorRepository
    {
        IEnumerable<OperatorMaster> GetAll();
        Task<OperatorMaster?> GetByIdAsync(long id);
        Task<OperatorMaster?> GetByCodeAsync(string code);
    }
}
