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

            UserAssemblyService service = new UserAssemblyService();

            service.WriteAssemblyInfoToConsole(assemblyPath);

            var assembly = service.GetAssembly(assemblyPath);

            var managerInstance = service.CreateUserManagerInstance(assembly);

            service.LogInUser("Henk", "testing", managerInstance, assembly);
            service.WriteNumberOfLoggedInUsers(assembly, managerInstance);
            Console.WriteLine();

            service.LogInUser("username", "password", managerInstance, assembly);
            service.WriteNumberOfLoggedInUsers(assembly, managerInstance);
            Console.WriteLine();

            service.LogOfUser("Henk", assembly, managerInstance);
            service.WriteNumberOfLoggedInUsers(assembly, managerInstance);
            Console.WriteLine();

            service.WriteLogInTestSequenceToConsole("Henk", "testing", assembly);

        }
    }
}
