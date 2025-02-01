using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByFilter;

public class GetUserByFilterQuery(UserFilterParams filterParams) : 
    QueryFilter<UserFilterResult, UserFilterParams>(filterParams);