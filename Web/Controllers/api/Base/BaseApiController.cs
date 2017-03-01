using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Web.Filters;
using Web.Managers.Base.Interfaces;

namespace Web.Controllers.api.Base
{
    [InvalidModelStateFilter]
    public abstract class BaseApiController<TModel> : ApiController where TModel : class, IModel
    {
        private readonly ICrudManager<TModel> _crudManager;

        protected BaseApiController(ICrudManager<TModel> crudManager)
        {
            _crudManager = crudManager;
        }

        [HttpGet]
        [Route("")]
        public virtual async Task<HttpResponseMessage> GetAsync()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await _crudManager.GetAsync());
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public virtual async Task<HttpResponseMessage> GetAsync(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await _crudManager.GetAsync(id));
        }

        [HttpPost]
        [Route("")]
        public virtual async Task<HttpResponseMessage> InsertAsync(TModel model)
        {
            await _crudManager.InsertAsync(model);
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpPut]
        [Route("")]
        public virtual async Task<HttpResponseMessage> UpdateAsync(TModel model)
        {
            await _crudManager.UpdateAsync(model);
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public virtual async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            await _crudManager.DeleteAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _crudManager?.Dispose();
            base.Dispose(disposing);
        }
    }
}