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
            lstMessages.Add("\nConstructor BaseController");
            this.configuration = configuration;
            LoadLibrary();
        }
        public ICommonTest icommonTest;
        public void LoadLibrary()
        {         
            try
            {

                lstMessages.Add("\nBase Directory" + AppDomain.CurrentDomain.BaseDirectory);

                var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

                lstMessages.Add("\n rootDir " + rootDir);

                var listAssemblies = AppDomain.CurrentDomain.GetAssemblies();

                lstMessages.Add("\n listAssemblies count" + listAssemblies.Count());



                lstMessages.Add("\nLoaded assemblies from current domain");
                string AssemblyPath = "/efs/CommonLibrary.dll";// configuration["AssemblyPath"];



                lstMessages.Add("\nconfiguration[\"AssemblyPath\"] : " + AssemblyPath);
                var assemblyName = AssemblyName.GetAssemblyName(AssemblyPath);

                lstMessages.Add("\nassemblyName: " + assemblyName);
                //var assembly = listAssemblies.FirstOrDefault(e => e.FullName == assemblyName.FullName);
                //if(assembly == null)
               
                    lstMessages.Add("\nloading assemply" + AssemblyPath);
                   var assembly = Assembly.Load(System.IO.File.ReadAllBytes("/efs/CommonModels.dll"));
                    assembly = Assembly.Load(System.IO.File.ReadAllBytes(AssemblyPath));
                    lstMessages.Add("\nloaded assemply" + AssemblyPath);


                var ins = assembly.CreateInstance("CommonLibrary.CommonTestClass");
                icommonTest = ins as ICommonTest;

                if(ins == null)
                {
                    lstMessages.Add("null instance");
                }

                //var types = assembly.GetType("CommonLibrary.CommonTestClass");// ("SampleLibrary.TestClass");
               // icommonTest = Activator.CreateInstance(types, null) as ICommonTest;
                //foreach (var type in types)
                //{
                //    lstMessages.Add("\n type :" + type.FullName);
                //    if (type != null && type.IsClass && typeof(ICommonTest).IsAssignableFrom(type))
                //    {
                //        icommonTest = Activator.CreateInstance(type, null) as ICommonTest;
                //        lstMessages.Add("\ncreated instance");
                //        break;
                //    }
                //}
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