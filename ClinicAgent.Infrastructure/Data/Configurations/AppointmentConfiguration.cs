
using ClinicAgent.Domain.Entities;
using ClinicAgent.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicAgent.Infrastructure.Data.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.AppointmentId);

        builder.Property(a => a.StartAt)
            .IsRequired();

        builder.Property(a => a.EndAt)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.GoogleCalendarId)
            .HasMaxLength(256);

        builder.Property(a => a.GoogleEventId)
            .HasMaxLength(512);

        // Relacionamento com Patient
        builder.HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Contact
        builder.HasOne(a => a.Contact)
            .WithMany(c => c.Appointments)
            .HasForeignKey(a => a.ContactId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com Professional
        builder.HasOne(a => a.Professional)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.ProfessionalId)
            .OnDelete(DeleteBehavior.Restrict);

        // Auto relacionamento de reagendamento
        builder.HasOne(a => a.RescheduledFromAppointment)
            .WithOne(a => a.RescheduledToAppointment)
            .HasForeignKey<Appointment>(a => a.RescheduledFromAppointmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índice para evitar consultas com o mesmo horário inicial
        // para o mesmo profissional, considerando Pending e Confirmed.
        builder.HasIndex(a => new { a.ProfessionalId, a.StartAt })
            .IsUnique()
            .HasFilter($"[Status] IN ({(int)AppointmentStatus.Pending}, {(int)AppointmentStatus.Confirmed})");
    }
}
