﻿using Authentication.Domain.Repositories.Query.Base;
using Authentication.Infra.Data;
using Microsoft.Extensions.Configuration;

namespace Authentication.Infra.Repository.Query.Base
{
    // Generic Query repository class
    public class QueryRepository<T> : DbConnector, IQueryRepository<T> where T : class
    {
        public QueryRepository(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}
