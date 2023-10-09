//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using ToDo.Domain.Models;

//namespace ToDo.Infrastructure.Context.Config
//{
//    internal class UserConfig : IEntityTypeConfiguration<ApplicationUser>
//    {
//        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
//        {

//            builder.HasMany(u => u.Workspaces)
//                   .WithOne()
//                   .IsRequired()
//                   .OnDelete(DeleteBehavior.Cascade);
//        }
//    }
//}
