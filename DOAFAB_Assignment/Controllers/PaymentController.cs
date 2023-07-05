using DOAFAB_Assignment.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DOAFAB_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        IConfiguration parentConfig;
        IConfiguration childConfig;
        List<ParentDataDTO> parentDataList;
        List<ChildDataDTO> childDataList;
        public PaymentController()
        {
            parentConfig = new ConfigurationBuilder()
                        .AddJsonFile("./Files/Parent.json")
                        .Build();
            parentDataList = parentConfig.GetSection("data").Get<List<ParentDataDTO>>();

            childConfig = new ConfigurationBuilder()
                        .AddJsonFile("./Files/Child.json")
                        .Build();
            childDataList = childConfig.GetSection("data").Get<List<ChildDataDTO>>();
        }
        [HttpGet("GetTransations")]
        public async Task<Response> GetTransations([FromQuery] ParentListingParams parentListingParams)
        {
            var response = new Response();
            

            var paginatedData = parentDataList.OrderBy(a => a.Id).Skip((parentListingParams.PageNumber - 1) * parentListingParams.PageSize)
                                                    .Take(parentListingParams.PageSize);
            foreach(var item in paginatedData)
            {
                item.TotalPaidAmount = childDataList.Where(a => a.ParentId == item.Id).OrderBy(a => a.Id).Sum(a => a.PaidAmount);
            }

            response.ResultData = paginatedData;
            response.TotalRows = parentDataList.Count;
            return response;
        }
        [HttpGet("GetTransactionDetails")]
        public async Task<Response> GetTransactionDetails([FromQuery] ChildListingParams childListingParams)
        {
            var response = new Response();
            
            var childList = childDataList.Where(a => a.ParentId == childListingParams.ParentId).OrderBy(a => a.Id).ToList();
            if (childList != null)
            {
                var paginatedData = childList.Skip((childListingParams.PageNumber - 1) * childListingParams.PageSize)
                                                    .Take(childListingParams.PageSize);
                var parentData = parentDataList.FirstOrDefault(a => a.Id == childListingParams.ParentId);
                response.ResultData = paginatedData;
                response.ParentData = parentData;
            }

            response.TotalRows = childList.Count;
            return response;
        }
    }
}
