using G_Task.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace G_Task.Domain
{
    public class Person : BaseEntity, IAuditEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }

        public virtual List<Address> Addresses { get; set; }

        public DateTimeOffset CreateDate { get; set; }
        public string CreateBy { get; set; }

    }
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {

        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable(nameof(Person));

            builder.HasMany(p => p.Addresses)
                .WithOne(c => c.Person)
                .HasForeignKey(v => v.PersonId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();


            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.NationalCode)
                .HasMaxLength(10)
                .IsRequired();


        }
    }

}
