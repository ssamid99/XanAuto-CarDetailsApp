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
    public class ModelPutCommand : IRequest<Model>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class ModelPutCommandHandler : IRequestHandler<ModelPutCommand, Model>
        {
            private readonly XanAutoDbContext db;

            public ModelPutCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Model> Handle(ModelPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Models.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                if (data == null)
                {
                    return null;
                }
                data.Name = request.Name;
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
