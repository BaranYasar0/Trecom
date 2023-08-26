namespace Trecom.Shared.Pipelines.Authorization;

public interface ISecuredRequest
{
    public string[] Roles { get; }
}