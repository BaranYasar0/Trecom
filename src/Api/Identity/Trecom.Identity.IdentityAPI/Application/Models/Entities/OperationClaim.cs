using Trecom.Shared.Models;

namespace Trecom.Api.Identity.Application.Models.Entities
{
    public class OperationClaim 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public OperationClaim()
        {

        }

        public OperationClaim(int id,string name) :this()
        {
            Id = id;
            Name = name;
        }
    }
}