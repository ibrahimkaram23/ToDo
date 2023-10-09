//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using ToDo.Domain.Models;

//namespace ToDo.Infrastructure.Context.Config
//{
//    internal class WorkspaceConfig : IEntityTypeConfiguration<Workspace>
//    {
//        public void Configure(EntityTypeBuilder<Workspace> builder)
//        {
//            builder.HasMany(w => w.Tasks)
//                   .WithOne()
//                   .IsRequired()
//                   .HasForeignKey(x => x.WorkspaceId)
//                   .OnDelete(DeleteBehavior.Cascade);
//        }
//    }
//}
