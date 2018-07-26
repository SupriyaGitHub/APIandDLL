using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TestAPI.Utils;

namespace TestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/TestDLL")]
    public class TestDLLController : BaseController
    {
        public TestDLLController(IConfiguration configuration) : base(configuration)
        {
            lstMessages.Add("Constructor TestDLLController");
        }

        [HttpGet]
        public List<string> Get()
        {
            try
            {
                string methodOutput = icommonTest.CommonTestMethod();
                lstMessages.Add("method output :" + methodOutput);

            }
            catch (Exception ex)
            {

                lstMessages.Add(ex.Message);

            }
            return lstMessages;

        }

    }
}