using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Find_AthuMethod.service
{
    public static class FindAthuService
    {
        // BindingFlags.Instance | BindingFlags.DeclaredOnly || BindingFlags.Public
        public static string[] Find()
        {
            var Asm=Assembly.GetAssembly(typeof(Program));
            var Controllsers = Asm.GetTypes()
                .Where(type => typeof(Microsoft.AspNetCore.Mvc.Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.Public) )
                .Where(typ => !typ.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Where(ty => ty.GetCustomAttributes(typeof(Microsoft.AspNetCore.Authorization.AuthorizeAttribute),true).Any())
                .Select(a=> new { Controller = a.DeclaringType.Name, Action = a.Name}).ToList();
            string[] Api= new string[Controllsers.Count];
            for (int i=0; i< Controllsers.Count;i++)
            {
                Api[i] = "api:" + Controllsers[i].Controller + ":" + Controllsers[i].Action;
            }
            return Api;
        }
    }
}
