namespace Application.Features.BusinessServices.Command.Update;

public class UpdateServiceResponse
{
    public int Id { get; set; }
    public int BusinessId { get; set; }
    public string Name { get; set; } 
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}