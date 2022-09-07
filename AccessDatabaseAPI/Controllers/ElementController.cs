using AccessDatabaseAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

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
        public void Post([FromBody] JArray jsonArray)
        {

            var element = JsonArrayToElement(jsonArray);

            if (element == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            var elementDAO = new ElementDAO();
            elementDAO.SaveElement(element);
        }

        [HttpPut]
        // PUT: api/Element/5
        public void Put(int id, [FromBody] JArray jsonArray)
        {
            var element = JsonArrayToElement(jsonArray);
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

        private Element JsonArrayToElement(JArray jsonArray)
        {
            if (jsonArray == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            var groupblockDAO = new GroupBlockDAO();
            var standardstateDAO = new StandardStateDAO();
            Element element = new Element()
            {
                atomicNumber = int.Parse(jsonArray[0]["atomicNumber"].ToString()),
                name = jsonArray[0]["name"].ToString(),
                symbol = jsonArray[0]["symbol"].ToString(),
                atomicMass = jsonArray[0]["atomicMass"].ToString(),
                yearDiscovered = new DateTime(int.Parse(jsonArray[0]["yearDiscovered"].ToString()), 01, 01),
                cpkHexColor = jsonArray[0]["cpkHexColor"].ToString(),
                period = int.Parse(jsonArray[0]["period"].ToString()),
                group = int.Parse(jsonArray[0]["group"].ToString()),
                favorited = true,
                groupBlock = groupblockDAO.SelectGroupBlockByName(jsonArray[0]["groupBlock"].ToString()),
                standardState = standardstateDAO.SelectStandardStateByName(jsonArray[0]["standardState"].ToString())
            };

            return element;
        }
    }
}
