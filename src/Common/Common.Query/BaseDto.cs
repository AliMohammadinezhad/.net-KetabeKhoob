namespace Common.Query;

public class BaseDto
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
    public BaseDto()
    {
    }
    public BaseDto(long id, DateTime creationDate)
    {
        Id = id;
        CreationDate = creationDate;
    }
}