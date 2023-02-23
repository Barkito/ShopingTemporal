using System.ComponentModel.DataAnnotations;

namespace Shoping.Domain;
public class BaseEntity
{
      [Key]
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedByAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsDeteleted { get; set; }
}
