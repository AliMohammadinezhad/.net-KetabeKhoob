﻿namespace Shop.Domain.UserAgg.Services;

public interface IDomainUserService
{
    bool IsEmailExist(string email);
    bool IsPhoneNumberExist(string phoneNumber);
}