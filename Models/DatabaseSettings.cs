namespace LocationApi.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
         public string AdminLocationConnectionString { get; set; }         
       
       
    }

    public interface IDatabaseSettings
    {
        public string AdminLocationConnectionString { get; set; }         
       
       
    }
}