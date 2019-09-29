using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageMe.Interfaces;
using MessageMe.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MessageMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private IClientManager clientManager;
        public ClientController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }

        [HttpPost]
        [Route(MessageMeConstants.ROUTE_REGISTER)]
        public string Register([FromBody]Client client)
        {
            return clientManager.Register(client);
        }

        [HttpPost]
        [Route(MessageMeConstants.ROUTE_CHECK_IN)]
        public bool CheckIn([FromBody]Client client)
        {
            return clientManager.CheckIn(client);
        }
    }
}
