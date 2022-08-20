using AccessDatabaseAPI.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccessDatabaseAPI.Controllers
{
    public class ElementController : ApiController
    {
        // GET: api/Element
        [HttpGet]
        public IEnumerable Get()
        {
            var elementDAO = new ElementDAO();
            var elementList = elementDAO.SelectAllElements();
            return elementList;
        }

        // GET: api/Element/5
        [HttpGet]
        public Element Get(int id)
        {
            var elementDAO = new ElementDAO();
            var element = elementDAO.SelectElementById(id);

            if (element == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return element;
        }

        // POST: api/Element
        [HttpPost]
        public void Post([FromBody] List<Element> element)
        {
            if (element == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            var elementDAO = new ElementDAO();
            elementDAO.SaveElement(element.FirstOrDefault());
        }

        [HttpPut]
        // PUT: api/Element/5
        public void Put(int id, [FromBody] Element element)
        {
            if (element == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            var elementDAO = new ElementDAO();
            elementDAO.UpdateElement(element);
        }

        // DELETE: api/Element/5
        [HttpDelete]
        public Element Delete(int id)
        {
            var elementDAO = new ElementDAO();
            var element = elementDAO.SelectElementById(id);
            if (element == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            elementDAO.DeleteElement(id);
            return element;
        }
    }
}
