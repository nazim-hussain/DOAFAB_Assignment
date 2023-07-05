using System.Net;

namespace DOAFAB_Assignment.ViewModels
{
    public class Response
    {
        public Status Status { get; set; }
        public string Message { get; set; }
        public object ResultData { get; set; }
        public object ParentData { get; set; }
        public int? TotalRows { get; set; }
    }
    public enum Status
    {
        Success = HttpStatusCode.OK,
        Failure = HttpStatusCode.InternalServerError,
        Restricted = HttpStatusCode.Forbidden,
        PartialContent = HttpStatusCode.PartialContent,
        Conflict = HttpStatusCode.Conflict,
        BadRequest = HttpStatusCode.BadRequest,
        NotModified = HttpStatusCode.NotModified,
        Duplicate = HttpStatusCode.Conflict
    }
}
