using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TestAPI.Utils;

namespace TestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Base")]
    public class BaseController : Controller
    {

        public readonly IConfiguration configuration;

        public BaseController(IConfiguration configuration)
        {
            this.configuration = configuration;
            LoadLibrary();
        }
        public ICommonTest icommonTest;
        public void LoadLibrary()
        {
            var path = @"G:\Lib\";         
            try
            {
                var listAssemblies = AppDomain.CurrentDomain.GetAssemblies();



                string AssemblyPath = configuration["AssemblyPath"];


                var assemblyName = AssemblyName.GetAssemblyName(AssemblyPath);
                 var assembly = listAssemblies.FirstOrDefault(e => e.FullName == assemblyName.FullName);
                if(assembly == null)
                {
                    Console.WriteLine("loading assemply" + AssemblyPath);
                     assembly = Assembly.Load(System.IO.File.ReadAllBytes(AssemblyPath));

                }

                var types = assembly.GetTypes();// ("SampleLibrary.TestClass");
                
                foreach (var type in types)
                {
                    if (type != null && type.IsClass && typeof(ICommonTest).IsAssignableFrom(type))
                    {
                        icommonTest = Activator.CreateInstance(type, null) as ICommonTest;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //TODO dispose if any
            }
        }
    }
}