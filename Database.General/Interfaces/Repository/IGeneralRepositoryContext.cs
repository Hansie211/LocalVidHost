using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Database.General.Interfaces.Repository.Generic;

namespace Database.General.Interfaces.Repository
{
    public interface IGeneralRepositoryContext
    {
        public Task SaveAsync();
    }
}
