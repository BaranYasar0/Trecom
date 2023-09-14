﻿using Trecom.Api.Identity.Application.Models.Entities;

namespace Trecom.Api.Identity.Application.Observers.User;

public class UserLogToElasticAndConsole : IUserObserver
{
    private readonly IServiceProvider sp;
    private readonly Models.Entities.User user;

    public UserLogToElasticAndConsole(IServiceProvider sp)
    {
        this.sp = sp;
    }

    public void Execute()
    {
        UserCreated(user);
    }

    public void UserCreated(Models.Entities.User user)
    {
        var logger = sp.GetRequiredService<ILogger<UserLogToElasticAndConsole>>();
        
        logger.LogInformation($"{user.FirstName} için {this.GetType().FullName} observer pattern deneniyor");
    }
}
