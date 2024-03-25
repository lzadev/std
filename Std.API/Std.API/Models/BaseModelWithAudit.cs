namespace Std.API.Models;

public abstract class BaseModelWithAudit : BaseModel
{
    public bool Active { get; set; }
    public bool Deleted { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public DateTimeOffset? UpdateTime { get; set; }
    public DateTimeOffset? DeletionTime { get; set; }
}
