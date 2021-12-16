using ReflectionOpdracth.Business;
using System;
using System.Reflection;

namespace ReflectionOpdracht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string assemblyPath = @"C:\Users\Bram Jager\Desktop\ReflectThis.dll";

            string username = "Henk";
            string password = "password";

            UserAssemblyService service = new UserAssemblyService();

            service.WriteAssemblyInfoToConsole(assemblyPath);

            var assembly = service.GetAssembly(assemblyPath);

            var managerInstance = service.CreateUserManagerInstance(assembly);

            service.LogInUser(username, password, managerInstance, assembly);
            service.WriteNumberOfLoggedInUsers(assembly, managerInstance);
            Console.WriteLine();

            service.LogInUser("username", "password", managerInstance, assembly);
            service.WriteNumberOfLoggedInUsers(assembly, managerInstance);
            Console.WriteLine();

            service.LogOfUser(username, assembly, managerInstance);
            service.WriteNumberOfLoggedInUsers(assembly, managerInstance);
            Console.WriteLine();

            service.WriteLogInTestSequenceToConsole(username, password, assembly);

        }
    }
}
