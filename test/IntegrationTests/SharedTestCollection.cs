using Xunit;

namespace IntegrationTests;

[CollectionDefinition("TestCollection")]
public class SharedTestCollection: ICollectionFixture<TestDatabaseFixture>
{
    
}