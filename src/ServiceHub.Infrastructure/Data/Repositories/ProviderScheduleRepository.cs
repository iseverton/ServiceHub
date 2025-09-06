using ServiceHub.Domain.Entities;
using ServiceHub.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Repositories;

public class ProviderScheduleRepository : BaseRepository<ProviderSchedule>, IProviderScheduleRepository
{
    public ProviderScheduleRepository(ServiceHubDbContext context) : base(context)
    {
    }
}
