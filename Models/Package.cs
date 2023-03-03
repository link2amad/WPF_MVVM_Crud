using SQLite;

namespace TaskPart1WPF.Models
{
    /// <summary>
    /// This is the Package Model having necessary fields with annotaion
    /// </summary>
    public class Package
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public double Weight { get; set; }
        public string Description { get; set; }
    }
}