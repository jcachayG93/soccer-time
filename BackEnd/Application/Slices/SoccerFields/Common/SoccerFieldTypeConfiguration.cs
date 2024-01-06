using Domain.Aggregates;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Slices.SoccerFields.Common;

public class SoccerFieldTypeConfiguration
: IEntityTypeConfiguration<SoccerField>
{
    public void Configure(EntityTypeBuilder<SoccerField> builder)
    {
        builder.HasKey(e => e.Id);
        builder.OwnsOne<FieldLocation>(e => e.Location);
    }
}