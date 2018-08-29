using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TestAPI.Utils;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/TestDLL")]
     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TestDLLController : BaseController
    {
        public TestDLLController(IConfiguration configuration) : base(configuration)
        {
            lstMessages.Add("\n Constructor TestDLLController \n");
        }

        [HttpGet]
        public List<string> Get()
        {
            List<string> messages = new List<string>();
            try
            {
                string methodOutput = icommonTest.CommonTestMethod();
                lstMessages.Add("\n\n method output :" + methodOutput);

            }
            catch (Exception ex)
            {

                lstMessages.Add(ex.Message);

            }

            foreach (var item in lstMessages)
            {
                messages.Add(item);
            }
            lstMessages.Clear();
            return messages;

        }

    }
}
