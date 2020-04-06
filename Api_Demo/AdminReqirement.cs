using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class AdminReqirement : IAuthorizationRequirement
    {
        public int Age { get; }

        public AdminReqirement(int age)
        {
            Age = age;
        }

    }


    public class AdminHandler : AuthorizationHandler<AdminReqirement>
    {


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminReqirement requirement)
        {

            var ctx = context.Resource as AuthorizationFilterContext;

            var age = ctx.HttpContext.Request.Query.FirstOrDefault(f => f.Key == "Age");
            if (age.Value.Count <= 0)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Succeed(requirement);
            }



            return Task.CompletedTask;


        }

    }
}
