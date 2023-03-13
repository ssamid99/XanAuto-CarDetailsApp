using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XanAuto.Domain.Models.DbContexts;
using XanAuto.Domain.Models.Entities;

namespace XanAuto.Domain.Business.ModelModule
{
    public class ModelGetAllQuery : IRequest<List<Model>>
    {
        public class ModelGetAllQueryHandler : IRequestHandler<ModelGetAllQuery, List<Model>>
        {
            private readonly XanAutoDbContext db;

            public ModelGetAllQueryHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Model>> Handle(ModelGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Models.Where(m => m.DeletedDate == null).ToListAsync(cancellationToken);
                if(data == null)
                {
                    return null;
                }
                return data;
            }
        }
    }
}
