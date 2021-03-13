namespace ConsoleApp.DTO
{
    public class Author
    {
        public Author(Models.Author x)
        {
            Id = x.Id;
            FullName = x.FullName;
        }

        public int Id { get; internal set; }
        public string FullName { get; internal set; }
    }
}
