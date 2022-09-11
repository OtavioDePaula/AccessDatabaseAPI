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
            var elementDAO = new ElementDAO();
            for (int i = 0; i < jsonArray.Count; i++)
            {
                Element element = new Element();
                element = JsonArrayToElement(jsonArray[i]);
                elementDAO.SaveElement(element);
            }           
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

        private Element JsonArrayToElement(JToken jsonArray)
        {
            if (jsonArray == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            var groupblockDAO = new GroupBlockDAO();
            var standardstateDAO = new StandardStateDAO();

            DateTime vYearDiscovered;
            if (jsonArray["yearDiscovered"].ToString().Equals("Ancient"))
                vYearDiscovered = new DateTime(00001, 01, 01);
            else
                vYearDiscovered = new DateTime(int.Parse(jsonArray["yearDiscovered"].ToString()), 01, 01);

            Element element = new Element()
            {
                atomicNumber = int.Parse(jsonArray["atomicNumber"].ToString()),
                name = jsonArray["name"].ToString(),
                symbol = jsonArray["symbol"].ToString(),
                atomicMass = jsonArray["atomicMass"].ToString(),
                yearDiscovered = vYearDiscovered,
                cpkHexColor = jsonArray["cpkHexColor"].ToString(),
                period = int.Parse(jsonArray["period"].ToString()),
                group = int.Parse(jsonArray["group"].ToString()),
                favorited = false,
                groupBlock = groupblockDAO.SelectGroupBlockByName(jsonArray["groupBlock"].ToString()),
                standardState = standardstateDAO.SelectStandardStateByName(jsonArray["standardState"].ToString())
            };

            return element;
        }
    }
}
