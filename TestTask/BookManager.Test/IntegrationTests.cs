using BookManager.Models;
using BookManager.Repositories;
using BookManager.Services;

namespace BookManager.Test
{
    public class IntegrationTests
    {
        private readonly string _inputFile;
        private readonly string _outputFile;

        public IntegrationTests()
        {
            _inputFile = Path.GetTempFileName();
            _outputFile = Path.GetTempFileName();

            const string xml = """
            <?xml version="1.0" encoding="utf-8"?>
            <Books>
              <Book>
                <Title>The Ugly Duckling</Title>
                <Author>Andersen</Author>
                <Pages>32</Pages>
              </Book>
              <Book>
                <Title>It</Title>
                <Author>King</Author>
                <Pages>1138</Pages>
              </Book>
              <Book>
                <Title>The Little Mermaid</Title>
                <Author>Andersen</Author>
                <Pages>40</Pages>
              </Book>
            </Books>
            """;

            File.WriteAllText(_inputFile, xml);
        }

        [Fact]
        public void FullWorkflow_LoadAddSortSearchSave()
        {
            var repository = new BookRepository();
            var service = new BookService(repository);

            service.Load(_inputFile);
            Assert.Equal(3, service.Books.Count);

            service.Add(new Book("The Shining", "King", 447));
            Assert.Equal(4, service.Books.Count);

            service.Sort();

            Assert.Equal("Andersen", service.Books[0].Author);
            Assert.Equal("The Little Mermaid", service.Books[0].Title);

            Assert.Equal("Andersen", service.Books[1].Author);
            Assert.Equal("The Ugly Duckling", service.Books[1].Title);

            Assert.Equal("King", service.Books[2].Author);
            Assert.Equal("It", service.Books[2].Title);

            Assert.Equal("King", service.Books[3].Author);
            Assert.Equal("The Shining", service.Books[3].Title);

            var results = service.Search("little");
            Assert.Single(results);
            Assert.Equal("The Little Mermaid", results[0].Title);

            service.Save(_outputFile);

            var service2 = new BookService(repository);
            service2.Load(_outputFile);
            Assert.Equal(4, service2.Books.Count);
            Assert.Equal("The Little Mermaid", service2.Books[0].Title);
        }
    }
}
