using System;
using System.Reflection;

namespace ReflectionOpdracht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFile(@"C:\Users\Bram Jager\Desktop\ReflectThis.dll");
            var list = assembly.GetTypes();
            foreach (var item in list)
            {
                Console.WriteLine(item);
                Console.WriteLine("\tMethods");
                foreach (var method in item.GetMethods())
                {
                    Console.WriteLine($"\t\t{method}");
                }
                Console.WriteLine("\tFields");
                foreach (var field in item.GetFields())
                {
                    Console.WriteLine($"\t\t{field}");
                }
                Console.WriteLine("\tProperty");
                foreach (var property in item.GetProperties())
                {
                    Console.WriteLine($"\t\t{property}");
                }
                Console.WriteLine("\tInterfaces");
                foreach (var interfaceInfo in item.GetInterfaces())
                {
                    Console.WriteLine($"\t\t{interfaceInfo}");
                }
                Console.WriteLine();
            }

            var userType = assembly.GetType("ReflectThis.User");
            var userInstance = Activator.CreateInstance(userType);

            var setUsername = userType.GetMethod("set_Username");
            var getUsername = userType.GetMethod("get_Username");
            var setPassword = userType.GetMethod("set_Password");
            var getPassword = userType.GetMethod("get_Password");

            string username = "Henk";
            string password = "password";

            Console.WriteLine($"Set Username to: {username}");
            setUsername.Invoke(userInstance, new object[] { username });
            Console.WriteLine($"Set Password to: {password}");
            setPassword.Invoke(userInstance, new object[] { password });

            Console.WriteLine($"Get Username: {getUsername.Invoke(userInstance, null)}");
            Console.WriteLine($"Get Password: {getPassword.Invoke(userInstance, null)}");

            var userManagerType = assembly.GetType("ReflectThis.UserManager");
            var userManagerInstance = Activator.CreateInstance(userManagerType);

            var getUsercount = userManagerType.GetMethod("get_UserCount");
            var LogOn = userManagerType.GetMethod("Logon");
            var logOf = userManagerType.GetMethod("LogOff");

            Console.WriteLine($"Logged in with {username} & {password}: {LogOn.Invoke(userManagerInstance, new object[] {username, password})}");
            Console.WriteLine($"Number of Users: {getUsercount.Invoke(userManagerInstance, null)}");
            Console.WriteLine($"Logged off: {username}");
            logOf.Invoke(userManagerInstance, new object[] { username });
            Console.WriteLine($"Number of Users: {getUsercount.Invoke(userManagerInstance, null)}");

        }
    }
}
