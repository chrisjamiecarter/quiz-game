namespace QuizGame.Infrastructure.Services;

/// <summary>
/// Defines the contract for seeding initial data into the database.
/// Implementations of this service will be responsible for populating the database with default or required data
/// when the application is first run or during specific seeding operations.
/// </summary>
internal interface ISeederService
{
    void SeedDatabase();
}
