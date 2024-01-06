using Application.Slices.SoccerFields.Commands;
using Application.UnitTests.Slices.SoccerFields.TestCommon;

namespace Application.UnitTests.Slices.SoccerFields.Commands;

public class SoccerFieldUpsertCommandTests
{
    private readonly SoccerFieldRepositoryMock _repository;

    public SoccerFieldUpsertCommandTests()
    {
        _repository = new();
    }

    private SoccerFieldUpsertCommand.Handler CreateSut()
    {
        return new(_repository.Object);
    }

    private SoccerFieldUpsertCommand CreateCommand()
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Name = "La Bombonera",
            Location_StateName = "Tennessee",
            Location_City = "Chattanooga",
            Location_Street = "Main St.",
            Location_Number = "2221",
            Location_ZipCode = "30720"
        };
    }


    [Fact]
    public async Task WhenSoccerFieldDoesNotExist_CreatesOne()
    {
        // ************ ARRANGE ************

        var command = CreateCommand();

        var sut = CreateSut();
        
        _repository.SetupLoadReturnsNull();
        
        // ************ ACT ****************

        await sut.Handle(command, CancellationToken.None);

        // ************ ASSERT *************
        
        _repository.VerifyLoad(command.Id);
        
        _repository.VerifyAdd(aggregate=>
            aggregate.Id == command.Id &&
            aggregate.Name == command.Name &&
            aggregate.Location.StateName == command.Location_StateName &&
            aggregate.Location.City == command.Location_City &&
            aggregate.Location.Number == command.Location_Number &&
            aggregate.Location.ZipCode == command.Location_ZipCode);
        
        _repository.VerifyCommitChanges();
        
    }
    
    [Fact]
    public async Task WhenSoccerFieldExists_UpdatesItsName()
    {
        // ************ ARRANGE ************

        var command = CreateCommand();

        var sut = CreateSut();
        
        _repository.SetupLoad(out var aggregate);
        
        // ************ ACT ****************

        await sut.Handle(command, CancellationToken.None);

        // ************ ASSERT *************
        
        _repository.VerifyLoad(command.Id);
        
        aggregate.Verify(x=>
            x.UpdateName(command.Name));
        
        _repository.VerifyCommitChanges();
        
    }


}