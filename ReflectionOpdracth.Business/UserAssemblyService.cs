using System;
using System.Reflection;

namespace ReflectionOpdracth.Business
{
    public class UserAssemblyService
    {
        public void WriteAssemblyInfoToConsole(string assemblyPath)
        {
            Assembly assembly = Assembly.LoadFile(assemblyPath);
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
        }
        public Assembly GetAssembly(string assemblyPath)
        {
            return Assembly.LoadFile(assemblyPath);
        }

        public object CreateUserInstance(Assembly assembly)
        {
            var userType = assembly.GetType("ReflectThis.User");
            return Activator.CreateInstance(userType);
        }

        public object CreateUserManagerInstance(Assembly assembly)
        {
            var userManagerType = assembly.GetType("ReflectThis.UserManager");
            return Activator.CreateInstance(userManagerType);
        }

        public void LogInUser(string username, string password, object managerInstance, Assembly assembly)
        {
            var userType = assembly.GetType("ReflectThis.User");
            var userInstance = Activator.CreateInstance(userType);

            var setUsername = userType.GetMethod("set_Username");
            var setPassword = userType.GetMethod("set_Password");

            Console.WriteLine($"Set Username to: {username}");
            setUsername.Invoke(userInstance, new object[] {username});
            Console.WriteLine($"Set Password to: {password}");
            setPassword.Invoke(userInstance, new object[] {password});

            var userManagerType = assembly.GetType("ReflectThis.UserManager");

            var LogOn = userManagerType.GetMethod("Logon");
            Console.WriteLine($"Logged in with {username} & {password}: {LogOn.Invoke(managerInstance, new object[] { username, password })}");
        }

        public void LogOfUser(string username, Assembly assembly, object managerInstance)
        {
            var userManagerType = assembly.GetType("ReflectThis.UserManager");

            var logOf = userManagerType.GetMethod("LogOff");
            Console.WriteLine($"Logged off {username}: {logOf.Invoke(managerInstance, new object[] { username })}");
        }

        public int GetNumberOfLoggedInUsers(Assembly assembly, object managerInstance)
        {
            var userManagerType = assembly.GetType("ReflectThis.UserManager");

            var getUsercount = userManagerType.GetMethod("get_UserCount");

            return (int)getUsercount.Invoke(managerInstance, null);
        }

        public void WriteNumberOfLoggedInUsers(Assembly assembly, object managerInstance)
        {
            Console.WriteLine($"Number of logged in users: {GetNumberOfLoggedInUsers(assembly, managerInstance)}");
        }

        public void WriteLogInTestSequenceToConsole(string username, string password, Assembly assembly)
        {
            var userType = assembly.GetType("ReflectThis.User");
            var userInstance = Activator.CreateInstance(userType);

            var setUsername = userType.GetMethod("set_Username");
            var getUsername = userType.GetMethod("get_Username");
            var setPassword = userType.GetMethod("set_Password");
            var getPassword = userType.GetMethod("get_Password");


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

            Console.WriteLine($"Logged in with {username} & {password}: {LogOn.Invoke(userManagerInstance, new object[] { username, password })}");
            Console.WriteLine($"Number of Users: {getUsercount.Invoke(userManagerInstance, null)}");
            Console.WriteLine($"Logged off {username}: {logOf.Invoke(userManagerInstance, new object[] { username })}");
            Console.WriteLine($"Number of Users: {getUsercount.Invoke(userManagerInstance, null)}");
        }
    }
}
