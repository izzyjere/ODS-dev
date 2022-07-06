﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODS.Core.Services.Domain
{
    public class OrphanageNeedService : ServiceBase<OrphanageNeed, int>
    {
        public OrphanageNeedService(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<List<OrphanageNeed>> GetAllByEmail(string email)
        {
            return await Repository.Entities().Where(e=>e.Orphanage.Email == email).ToListAsync();
        }
    }
}
