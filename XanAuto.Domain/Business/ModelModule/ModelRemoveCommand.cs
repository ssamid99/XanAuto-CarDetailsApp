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
    public class ModelRemoveCommand : IRequest<Model>
    {
        public int Id { get; set; }
        public class ModelRemoveCommandHandler : IRequestHandler<ModelRemoveCommand, Model>
        {
            private readonly XanAutoDbContext db;

            public ModelRemoveCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Model> Handle(ModelRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Models.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                if (data == null)
                {
                    return null;
                }
                data.DeletedDate = DateTime.UtcNow.AddHours(4);
                return data;
            }
        }
    }
}
