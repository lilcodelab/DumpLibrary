namespace ConsoleApp.DTO
{
    public class Book
    {
        public Book(Models.Book x)
        {
            Id = x.Id;
            Title = x.Title;
            AuthorFullName = x.Author?.FullName ?? "";
            AuthorId = x.AuthorId;
        }
        public Book()
        {

        }

        public int Id { get; internal set; }
        public string Title { get; internal set; }
        public string AuthorFullName { get; internal set; }
        public int AuthorId { get; internal set; }
    }
}
