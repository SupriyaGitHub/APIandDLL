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

        public static List<string> lstMessages = new List<string>();


        public readonly IConfiguration configuration;

        public BaseController(IConfiguration configuration)
        {
            lstMessages.Add("Constructor BaseController");
            this.configuration = configuration;
            LoadLibrary();
        }
        public ICommonTest icommonTest;
        public void LoadLibrary()
        {         
            try
            {
                var listAssemblies = AppDomain.CurrentDomain.GetAssemblies();

                lstMessages.Add("Loaded assemblies from current domain");
                string AssemblyPath = "/root/efs/CommonLibrary.dll";// configuration["AssemblyPath"];
                lstMessages.Add("configuration[\"AssemblyPath\"] : " + AssemblyPath);
                var assemblyName = AssemblyName.GetAssemblyName(AssemblyPath);

                lstMessages.Add("assemblyName: " + assemblyName);
                var assembly = listAssemblies.FirstOrDefault(e => e.FullName == assemblyName.FullName);
                if(assembly == null)
                {
                    lstMessages.Add("loading assemply" + AssemblyPath);
                    assembly = Assembly.Load(System.IO.File.ReadAllBytes(AssemblyPath));
                    lstMessages.Add("loaded assemply" + AssemblyPath);
                }

                var types = assembly.GetTypes();// ("SampleLibrary.TestClass");
                
                foreach (var type in types)
                {
                    lstMessages.Add(" type :" + type.FullName);
                    if (type != null && type.IsClass && typeof(ICommonTest).IsAssignableFrom(type))
                    {
                        icommonTest = Activator.CreateInstance(type, null) as ICommonTest;
                        lstMessages.Add("created instance");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                lstMessages.Add(ex.Message);
               
            }
            finally
            {
                //TODO dispose if any
            }
        }
    }
}