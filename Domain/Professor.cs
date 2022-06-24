using ServiceStack.DataAnnotations;

namespace Domain
{
    public class Professor
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}