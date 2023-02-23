using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shoping.Application.Common.Interfaces.Repositories;
using Shoping.Domain.Entities;

namespace Shoping.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}