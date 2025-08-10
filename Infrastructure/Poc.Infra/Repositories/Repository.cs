using Poc.Domain.Core;
using Poc.Domain.Entities;
using Poc.Domain.Repositories;
using Poc.Infra.Context;

namespace Poc.Infra.Repositories;

public class Repository<T>(PocContext context) : GenericRepository<T>(context), IRepository<T> where T : BaseEntity
{
}