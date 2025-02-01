using Common.Application;
using Shop.Application.Products.RemoveImage.AddImage;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Query.Roles.DTOs;
using Shop.Query.Roles.GetById;

namespace Shop.Presentation.Facade.Roles;

public interface IRoleFacade
{
    Task<OperationResult> CreateRole(CreateRoleCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> EditRole(EditRoleCommand command, CancellationToken cancellationToken = default);
    
    
    Task<RoleDto?> GetRoleById(long id, CancellationToken cancellationToken = default);
    Task<List<RoleDto>> GetRoleList(CancellationToken cancellationToken = default);

}