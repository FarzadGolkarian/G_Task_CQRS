using G_Task.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_Task.Domain
{
    public class Address : BaseEntity , IAuditEntity
    {
        public string PersonAddress { get; set; }
        public AddressTypeEnum AddressType { get; set; }

        public virtual Person  Person { get; set; }
        public long PersonId { get; set; }

        public DateTimeOffset CreateDate { get; set; }
        public string CreateBy { get; set; }
    }


    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
           builder.ToTable(nameof (Address));


            builder.Property(x => x.AddressType)
                .IsRequired();

            builder.Property(x => x.PersonAddress)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.PersonId)
                .IsRequired();

        }
    }
}
