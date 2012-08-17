using System.Web.Http;
using AutoMapper;
using HoldTheAllergen.API.Core;

namespace HoldTheAllergen.API.Controllers
{
    public class DefaultController : ApiController
    {
        private IMappingEngine _mappingEngine;

        public IMappingEngine MappingEngine
        {
            get { return _mappingEngine ?? (_mappingEngine = IoCHelper.GetInstance<IMappingEngine>()); }
        }
    }
}