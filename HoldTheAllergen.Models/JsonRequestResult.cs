namespace HoldTheAllergen.Models
{
    public class JsonRequestResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }

        public JsonRequestResult(bool success, T data)
        {
            Success = success;
            Data = data;
        } 
    }
}