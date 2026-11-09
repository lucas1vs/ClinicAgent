
using ClinicAgent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicAgent.Infrastructure.Data.Configurations;

public class PatientContactAuthorizationConfiguration
    : IEntityTypeConfiguration<PatientContactAuthorization>
{
    public void Configure(EntityTypeBuilder<PatientContactAuthorization> builder)
    {
        builder.HasKey(a => a.PatientContactAuthorizationId);

        builder.Property(a => a.Status)
            .IsRequired();

        builder.Property(a => a.RequestedAt)
            .IsRequired();

        builder.Property(a => a.RespondedAt);

        builder.Property(a => a.RevokedAt);

        builder.HasIndex(a => new { a.PatientId, a.ContactId })
            .IsUnique()
            .HasFilter("[Status] IN (0, 1)");

        builder.HasIndex(a => new { a.ContactId, a.Status });
    }
}
