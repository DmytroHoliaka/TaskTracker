namespace TaskTracker.BLL.Models.DbSet;

public abstract class BaseModel
{
    public Guid Id { get; set; }

    protected BaseModel()
    {
        Id = Guid.NewGuid();
    }

    protected BaseModel(Guid id)
    {
        Id = id;
    }
}
